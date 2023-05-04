namespace Solid.Rest.Practice.API.BLL;

public interface ICustomerService
{
    public IEnumerable<IGrouping<string, Customer>> GetAllCustomersGroupedByLastName();

    public Customer GetCustomer(int id);
    
    //I'm for now assuming that the only thing we are allowed to update on a customer is LastName
    //Otherwise this one would probably be working with some kind of update object, dictionary or any other more comprehensive solution update any given number of properties
    public Customer ReplaceLastName(int id, string newLastName);

    public Customer AddCustomer(Customer newCustomer);

    public bool RemoveCustomer(int id);
}