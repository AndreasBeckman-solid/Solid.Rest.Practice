namespace Solid.Rest.Practice.API.BLL;

internal class DateProvider : IDateProvider
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}