namespace Solid.Rest.Practice.API.BLL;

internal interface ICustomerDatabase
{
}

class CustomerDatabase : ICustomerDatabase
{
    public CustomerDatabase(ISuperSimpleMemoryDatabase superSimpleMemoryDatabase)
    {
        
    }
}