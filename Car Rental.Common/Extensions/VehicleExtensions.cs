namespace Car_Rental.Common.Extensions;

public static class VehicleExtensions
{
    public static int Duration(this DateTime startDate, DateTime endDate) 
    {
            var duration = (endDate - startDate).Days;
            return duration >= 0 ? duration : throw new ArgumentException("The start date has to be earlier than the end date.");
    }
}
