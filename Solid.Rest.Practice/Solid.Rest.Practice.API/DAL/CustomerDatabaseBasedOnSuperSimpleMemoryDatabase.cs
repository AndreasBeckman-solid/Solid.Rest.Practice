using Solid.Rest.Practice.API.BLL;

namespace Solid.Rest.Practice.API.DAL;

internal class CustomerDatabaseBasedOnSuperSimpleMemoryDatabase : ICustomerDatabase
{
    private readonly ISuperSimpleMemoryDatabase _superSimpleMemoryDatabase;

    public CustomerDatabaseBasedOnSuperSimpleMemoryDatabase(ISuperSimpleMemoryDatabase superSimpleMemoryDatabase)
    {
        _superSimpleMemoryDatabase = superSimpleMemoryDatabase;
    }


    public IEnumerable<CustomerEntity> GetAllCustomers()
    {
        return _superSimpleMemoryDatabase.GetAllCustomers();
    }

    public CustomerEntity AddNewCustomer(CustomerEntity newCustomer)
    {
        return _superSimpleMemoryDatabase.UpdateOrAddCustomer(newCustomer);
    }

    public CustomerEntity UpdateCustomer(CustomerEntity customerWithChanges)
    {
        return _superSimpleMemoryDatabase.UpdateOrAddCustomer(customerWithChanges);
    }

    public bool RemoveCustomer(int id)
    {
        return _superSimpleMemoryDatabase.RemoveCustomer(id);
    }
}