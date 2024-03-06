using Car_Rental.Common.Enums;
using Car_Rental.Common.Extensions;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Booking : IBooking
{
    public int Id { get; init; }
    public IPerson Customer { get; init; }
    public IVehicle Vehicle { get; init; }
    public DateTime DateRented { get; init; } = DateTime.Now;
    public double KmRented { get; init; } 
    public DateTime? DateReturned { get; private set; } = null;
    public double? KmReturned { get; private set; } = null;
    public double? Distance { get; set; } = null;
    public double? Cost { get; private set; } = null;
    public BookingStatuses Status { get; private set; } = BookingStatuses.Open;

    public Booking() { }
    public Booking(int id, IPerson customer, IVehicle vehicle, DateTime dateRented, double kmRented) : this
        (id, customer, vehicle, dateRented, kmRented, null, null, null, BookingStatuses.Open){ }
    public Booking(int id, IPerson customer, IVehicle vehicle, DateTime dateRented, double kmRented, double? kmReturned, 
        DateTime? dateReturned, double? cost, BookingStatuses status) =>
        (Id, Customer, Vehicle, DateRented, KmRented, KmReturned, DateReturned, Cost, Status) =
        (id, customer, vehicle, dateRented, kmRented, kmReturned, dateReturned, cost, status);

    public void CloseBooking(double? distance)
    {
            var cost = distance * Vehicle.CostKm + DateRented.Duration(DateTime.Now) * (int)Vehicle.VehicleType;
            if (cost is null || cost < 0) return;
            (DateReturned, KmReturned, Cost, Status) = (DateTime.Now, Vehicle.Odometer + distance, cost, BookingStatuses.Closed);
    }
}

