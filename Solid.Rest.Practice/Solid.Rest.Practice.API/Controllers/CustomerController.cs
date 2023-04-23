using System.Collections;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Solid.Rest.Practice.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger)
    {
        _logger = logger;
    }
    
    //AllCustomersGroupedByLastName
    public IEnumerable<IGrouping<string, Customer>> Get()
    {
        return new List<Customer>().GroupBy(x => x.LastName);
    }

    //SingleCustomerFromId
    public IEnumerable<IGrouping<string, Customer>> Get(int id)
    {
        return new List<Customer>().GroupBy(x => x.LastName);
    }
    
    
    
    //CreateNewCustomer
    public IResult Post(Customer newCustomer)
    {
        //Call BLL and make actual decisions regarding result after
        
        return Results.Ok<Customer>(newCustomer);
    }
    
    //UpdateLastName
    public IEnumerable<IGrouping<string, Customer>> Put(int id, string newLastname)
    {

        return new List<Customer>().GroupBy(x => x.LastName);
    }
    
    //DeleteACustomer
    public IEnumerable<IGrouping<string, Customer>> Delete(int id)
    {

        return new List<Customer>().GroupBy(x => x.LastName);
    }
}