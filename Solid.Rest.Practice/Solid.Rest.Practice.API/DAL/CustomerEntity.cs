namespace Solid.Rest.Practice.API.DAL;

public class CustomerEntity
{
    public int? Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}