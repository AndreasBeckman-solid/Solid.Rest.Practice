namespace Solid.Rest.Practice.API.BLL;

public class CounterService : ICounterService
{
    private int _singleCustomerRead = 0;

    public void ResetSingleCustomerRead()
    {
        _singleCustomerRead = 0;
    }

    public int ReadSingleCustomerRead()
    {
        return _singleCustomerRead;
    }

    public void AddSingleCustomerRead()
    {
        _singleCustomerRead++;
    }
}