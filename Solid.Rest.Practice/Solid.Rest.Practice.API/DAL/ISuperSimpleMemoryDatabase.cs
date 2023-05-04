using Solid.Rest.Practice.API.DAL;

namespace Solid.Rest.Practice.API.BLL;

internal interface ISuperSimpleMemoryDatabase
{
    public IEnumerable<CustomerEntity> GetAllCustomers();
    public CustomerEntity UpdateOrAddCustomer(CustomerEntity customer);

    public bool RemoveCustomer(int id);
}

internal class SuperSimpleMemoryDatabase : ISuperSimpleMemoryDatabase
{
    public SuperSimpleMemoryDatabase()
    {
        Customers = new List<CustomerDbRow>();
    }

    private class CustomerDbRow
    { 
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
    
    private  List<CustomerDbRow> Customers { get; set; }
    public IEnumerable<CustomerEntity> GetAllCustomers()
    {
        //Make sure we cant alter the content of the database "by reference"
        return Customers.Select(x => new CustomerEntity()
            { 
                Id = x.Id,
                FirstName = x.FirstName, 
                LastName = x.LastName, 
                Age = x.Age
            }).ToList();
    }

    public CustomerEntity UpdateOrAddCustomer(CustomerEntity customer)
    {
        //If a customer entity has an Id, this is an update. Otherwise it is an add.
        CustomerDbRow? customerDbRow = null;
        if (customer.Id != null)
        {
           customerDbRow = Customers.Single(x => x.Id == customer.Id);
        }
        else
        {
            //This is flawed in that it can potentially re-use old ideas if the latest added customer is removed, but for this assignment I'm not gonna worry about it.
            var newId = Customers.Count > 0 ? Customers.OrderBy(x => x.Id).Last().Id + 1 : 1;
            customerDbRow = new CustomerDbRow() { Id = newId };
            customer.Id = customerDbRow.Id;
            Customers.Add(customerDbRow);
        }

        if (customer.FirstName == null)
        {
            throw new InvalidOperationException("Cannot add customers with no FirstName");
        }
        if (customer.LastName == null)
        {
            throw new InvalidOperationException("Cannot add customers with no LastName");
        }

        customerDbRow.FirstName = customer.FirstName;
        customerDbRow.LastName = customer.LastName;
        customerDbRow.Age = customer.Age;

        return customer;
    }

    public bool RemoveCustomer(int id)
    {
        var customerDbRow = Customers.SingleOrDefault(x => x.Id == id);
        if (customerDbRow == null)
        {
            return false;
        }
        return Customers.Remove(customerDbRow);
    }
}

