namespace Solid.Rest.Practice.API.Test;

public class CustomerServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CustomersUnder10AreExcludedIfOlderCustomersWithSameLastName()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor", LastName = "Macleod", Age = 56},
                new(){Id = 2, FirstName = "George", LastName = "Macleod", Age = 9}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(1);

        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 2, 28));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customers = customerService.GetAllCustomersGroupedByLastName();

        Assert.IsFalse(customers.First().Any(x => x.Id == 2), "Should not return a customer under the age of ten when there are other customers over 10 with the same last name");
    }
    
    [Test]
    public void OnlyCustomersUnder10AreExcludedIfManyOlderCustomersWithSameLastName()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor", LastName = "Macleod", Age = 56},
                new(){Id = 2, FirstName = "George", LastName = "Macleod", Age = 9}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(1);

        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 2, 28));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customers = customerService.GetAllCustomersGroupedByLastName();

        Assert.IsTrue(customers.First().Any(x => x.Id == 1), "Customer 10 or over should be returned even with shared name");

    }
    
    [Test]
    public void CustomersUnder10AreNotExcludedIfNoOlderCustomersWithSameLastName()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor", LastName = "Macleod", Age = 7},
                new(){Id = 2, FirstName = "George", LastName = "Macleod", Age = 9}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(1);

        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 2, 28));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customers = customerService.GetAllCustomersGroupedByLastName();

        Assert.That(customers.First().Count(), Is.EqualTo(2), "If no customer with shared name 10 or above in age, all customers with that name should be returned");
    }
    
    [Test]
    public void CustomersWithCombinedNamesOver40LongHaveTheirFirstNameShortened()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor_aaaa_aaaa_aaaa_aaaa_aaaa_a", LastName = "Macleod", Age = 7},
                new(){Id = 2, FirstName = "Susan_aaaa_aaaa_aaaa_aaaa_aaaa_aaa", LastName = "Macleod", Age = 9}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(1);

        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 2, 28));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customers = customerService.GetAllCustomersGroupedByLastName();
        
        Assert.That(customers.First().Single(x => x.Id == 2).FirstName, Is.EqualTo("S"), "If combined length of names for a customer is over 40, only return first letter of firstname for that property");
    }
    
    [Test]
    public void OnlyCustomersWithCombinedNamesOver40LongHaveTheirFirstNameShortened()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor_aaaa_aaaa_aaaa_aaaa_aaaa_a", LastName = "Macleod", Age = 7},
                new(){Id = 2, FirstName = "Susan_aaaa_aaaa_aaaa_aaaa_aaaa_aaa", LastName = "Macleod", Age = 9}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(1);

        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 2, 28));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customers = customerService.GetAllCustomersGroupedByLastName();
        
        Assert.That(customers.First().Single(x => x.Id == 1).FirstName, Is.EqualTo("Gregor_aaaa_aaaa_aaaa_aaaa_aaaa_a"), "If combined length of names for a customer is 40 or lower, return the complete first name");
    }
    
    [Test]
    public void OnAprilFirstEveryTenthLastNameIsReversed()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor", LastName = "Macleod", Age = 7}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(10);

        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 4, 1));
        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customer = customerService.GetCustomer(1);

        Assert.That(customer.FirstName, Is.EqualTo("rogerG"), "On april first, every 10 calls return first name reversed");

    }

    [Test]
    public void OnAprilFirstOnlyEveryTenthLastNameIsReversed()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor", LastName = "Macleod", Age = 7}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(4);
        
        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 4, 1));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);
        
        var customer = customerService.GetCustomer(1);

        Assert.That(customer.FirstName, Is.EqualTo("Gregor"), "Even if april first, if not the 10th call, return first name normally");
    }

    
    [Test]
    public void OnlyOnAprilFirstEveryTenthLastNameIsReversed()
    {
        var customerDatabase = new Mock<ICustomerDatabase>();
        customerDatabase.Setup(x => x.GetAllCustomers())
            .Returns( new List<CustomerEntity>()
            {
                new(){Id = 1, FirstName = "Gregor", LastName = "Macleod", Age = 7}
            });
        
        var counterService = new Mock<ICounterService>();
        counterService.Setup(x => x.ReadSingleCustomerRead()).Returns(10);
        
        var dateProvider = new Mock<IDateProvider>();
        dateProvider.Setup(x => x.Now()).Returns(new DateTime(2023, 4, 2));

        var customerService = new CustomerService(customerDatabase.Object, dateProvider.Object, counterService.Object);

        var customer = customerService.GetCustomer(1);

        Assert.That(customer.FirstName, Is.EqualTo("Gregor"), "Even if the 10th call, if not april first, return first name normally");

    }

}