using Solid.Rest.Practice.API.DAL;

namespace Solid.Rest.Practice.API.BLL;

public class CustomerService : ICustomerService
{
    private readonly ICustomerDatabase _customerDatabase;
    private readonly IDateProvider _dateProvider;
    private readonly ICounterService _counterService;
    private const string ThereIsNoCustomerWithThatId = "There is no customer with that Id";

    public CustomerService(ICustomerDatabase customerDatabase, IDateProvider dateProvider, ICounterService counterService)
    {
        _customerDatabase = customerDatabase;
        _dateProvider = dateProvider;
        _counterService = counterService;
    }
    
    public IEnumerable<IGrouping<string, Customer>> GetAllCustomersGroupedByLastName()
    {
        var customerEntities = _customerDatabase.GetAllCustomers().ToList();

        var filteredCustomerEntities = customerEntities.ToList();
        var underTenSubset = customerEntities.Where(x => x.Age < 10).ToList();

        //Drop anyone under 10, if there is someone 10 or over with the same last name
        foreach (var underTenEntity in underTenSubset)
        {
            if (filteredCustomerEntities.Any(x => x.LastName.Equals(underTenEntity.LastName) && x.Age >= 10))
            {
                filteredCustomerEntities.Remove(underTenEntity);
            }
        }

        //Convert entities to customer objects
        var filteredCustomers = filteredCustomerEntities.Select(x => new Customer()
        {
            Id = x.Id, 
            FirstName = x.FirstName, 
            LastName = x.LastName, 
            Age = x.Age
        }).ToList();

        //If the combined length of first and last name is longer, reduce first name to first letter
        filteredCustomers.ForEach(x =>
        {
            if ((x.LastName.Length + x.FirstName.Length) > 40)
            {
                x.FirstName = x.FirstName.Substring(0, 1);
            }
        });

        //Before returning, group the result by LastName
        return filteredCustomers.GroupBy(x => x.LastName).ToList();
    }

    public Customer GetCustomer(int id)
    {
        var customerEntity = _customerDatabase.GetAllCustomers().SingleOrDefault(x => x.Id == id);
        if (customerEntity == null)
        {
            throw new KeyNotFoundException(ThereIsNoCustomerWithThatId);
        }

        _counterService.AddSingleCustomerRead();
        var customer = ToCustomer(customerEntity);
        
        if (_counterService.ReadSingleCustomerRead() % 10 == 0 && 
            _dateProvider.Now().Month == 4 && 
            _dateProvider.Now().Day == 1)
        {
            var namePreReverse = customer.FirstName;
            var nameAsArray = namePreReverse.ToCharArray();
            Array.Reverse(nameAsArray);
            customer.FirstName = new string(nameAsArray);
        }
        
        return customer;
    }

    public Customer ReplaceLastName(int id, string newLastName)
    {
        var customerEntity = _customerDatabase.GetAllCustomers().SingleOrDefault(x => x.Id == id);
        if (customerEntity == null)
        {
            throw new KeyNotFoundException(ThereIsNoCustomerWithThatId);
        }
        customerEntity.LastName = newLastName;

        return ToCustomer(_customerDatabase.UpdateCustomer(customerEntity));
    }

    public Customer AddCustomer( Customer newCustomer)
    {
        if (newCustomer.Id != null)
        {
            throw new InvalidOperationException("New customer's should not have an Id");
        }

        if (string.IsNullOrEmpty(newCustomer.FirstName))
        {
            throw new InvalidOperationException("New customer's should have a FirstName");
        }

        if (string.IsNullOrEmpty(newCustomer.LastName))
        {
            throw new InvalidOperationException("New customer's should have a LastName");
        }

        return ToCustomer(_customerDatabase.AddNewCustomer(ToCustomerEntity(newCustomer)));

    }

    public bool RemoveCustomer(int id)
    {
        return _customerDatabase.RemoveCustomer(id);
    }
    
    private static CustomerEntity ToCustomerEntity(Customer customer)
    {
        return new CustomerEntity()
        {
            Id = customer.Id, 
            FirstName = customer.FirstName, 
            LastName = customer.LastName,
            Age = customer.Age
        };
    }
    
   private static Customer ToCustomer(CustomerEntity customerEntity)
    {
        return new Customer()
        {
            Id = customerEntity.Id,
            FirstName = customerEntity.FirstName,
            LastName = customerEntity.LastName,
            Age = customerEntity.Age
        };
    }
}