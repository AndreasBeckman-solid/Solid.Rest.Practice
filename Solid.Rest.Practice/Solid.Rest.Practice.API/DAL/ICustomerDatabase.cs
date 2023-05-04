namespace Solid.Rest.Practice.API.DAL;

public interface ICustomerDatabase
{
    public IEnumerable<CustomerEntity> GetAllCustomers();

    public CustomerEntity AddNewCustomer(CustomerEntity newCustomer);

    public CustomerEntity UpdateCustomer(CustomerEntity customerWithChanges);

    public bool RemoveCustomer(int id);
}