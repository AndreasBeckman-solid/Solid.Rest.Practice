using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Solid.Rest.Practice.API.BLL;

public interface ICustomerService
{
    public IEnumerable<IGrouping<string, Customer>> GetAllCustomersGroupedByLastName();

    public Customer GetCustomer(int id);
    
    //I'm for now assuming that the only thing we are allowed to update on a customer is LastName
    //Otherwise this one would probably be working with some kind of update object, dictionary or any other more comprehensive solution update any given number of properties
    public Customer ReplaceLastName(int id, string newLastName);

    public Customer AddCustomer(Customer newCustomer);

    public int RemoveCustomer(int id);
}

internal class CustomerService : ICustomerService
{
    private readonly ICustomerDatabase _customerDatabase;

    public CustomerService(ICustomerDatabase customerDatabase)
    {
        _customerDatabase = customerDatabase;
    }
    
    public IEnumerable<IGrouping<string, Customer>> GetAllCustomersGroupedByLastName()
    {
        throw new NotImplementedException();
    }

    public Customer GetCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public Customer ReplaceLastName(int id, string newLastName)
    {
        throw new NotImplementedException();
    }

    public Customer AddCustomer( Customer newCustomer)
    {
        if (newCustomer.Id != null)
        {
            throw new InvalidOperationException("New customer's should not have an Id");
        }
        
        throw new NotImplementedException();
    }

    public int RemoveCustomer(int id)
    {
        throw new NotImplementedException();
    }
}