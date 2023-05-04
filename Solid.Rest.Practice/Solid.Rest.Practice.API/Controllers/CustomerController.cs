using Microsoft.AspNetCore.Mvc;
using Solid.Rest.Practice.API.BLL;

namespace Solid.Rest.Practice.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }
    
    //AllCustomersGroupedByLastName
    [HttpGet(Name="GetAllCustomers")]
    public IResult Get()
    {
        
        return Results.Ok(_customerService.GetAllCustomersGroupedByLastName());
    }
    
    //SingleCustomerFromId
    [HttpGet("{id:int}",Name="GetCustomer")]
    public IResult Get(int id)
    {
        try
        {
            return Results.Ok(_customerService.GetCustomer(id));
        }
        catch (KeyNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }
    

    
    //CreateNewCustomer
    [HttpPost(Name="AddCustomer")]
    public IResult Post(Customer newCustomer)
    {
        try
        {
            return Results.Ok<Customer>(_customerService.AddCustomer(newCustomer));
        }
        catch (InvalidOperationException e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    //UpdateLastName
    [HttpPut(Name="UpdateCustomerLastName")]
    public IResult Put(int id, string newLastname)
    {
        try
        {
            return Results.Ok(_customerService.ReplaceLastName(id, newLastname));
        }
        catch (KeyNotFoundException e)
        {
            return Results.NotFound(e.Message);
        }
    }
    
    //DeleteACustomer
    [HttpDelete(Name="RemoveCustomer")]
    public IResult Delete(int id)
    {
        if (_customerService.RemoveCustomer(id))
        {
            return Results.Ok();
        }
        else
        {
            return Results.NotFound();
        }
    }
}