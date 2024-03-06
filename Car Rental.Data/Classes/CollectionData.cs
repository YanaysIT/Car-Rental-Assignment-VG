using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using Car_Rental.Common.Classes;
using System.Linq.Expressions;
using Car_Rental.Common.Extensions;

namespace Car_Rental.Data.Classes;

public class CollectionData : IData
{
    readonly List<IPerson> _persons = new();
    readonly List<IVehicle> _vehicles = new();
    readonly List<IBooking> _bookings = new();
    public CollectionData() => SeedData();

    #region
    public int NextPersonId => _persons.Count == 0 ? 1 : _persons.Max(b => b.Id) + 1;
    public int NextVehicleId => _vehicles.Count == 0 ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextBookingId => _bookings.Count == 0 ? 1 : _bookings.Max(b => b.Id) + 1;
    #endregion

    void SeedData()
    {
        _persons.Add(new Customer(NextPersonId, "12345", "Jhon", "Doe"));
        _persons.Add(new Customer(NextPersonId, "98765", "Jackie", "Chan"));
        _persons.Add(new Customer(NextPersonId, "67890", "Betty", "Boop"));

        _vehicles.Add(new Vehicle(NextVehicleId, "ABC123", "Volvo", 10000, 1, VehicleTypes.Combi, VehicleStatuses.Available));
        _vehicles.Add(new Vehicle(NextVehicleId, "DEF456", "Saab", 20000, 1, VehicleTypes.Sedan, VehicleStatuses.Booked));
        _vehicles.Add(new Vehicle(NextVehicleId, "GHI789", "Tesla", 1000, 3, VehicleTypes.Sedan, VehicleStatuses.Booked));
        _vehicles.Add(new Vehicle(NextVehicleId, "JKL012", "Jeep", 5000, 1.5, VehicleTypes.Van, VehicleStatuses.Available));
        _vehicles.Add(new Vehicle(NextVehicleId, "MNO234", "Yamaha", 30100, 0.5, VehicleTypes.Motorcycle, VehicleStatuses.Available));

        _bookings.Add(new Booking(NextBookingId, _persons[0], _vehicles[2], DateTime.Now, _vehicles[2].Odometer));
        _bookings.Add(new Booking(NextBookingId, _persons[1], _vehicles[1], DateTime.Now.AddDays(-2), _vehicles[1].Odometer));
        _bookings.Add(new Booking(NextBookingId, _persons[2], _vehicles[4], DateTime.Now.AddDays(-3), 30000, _vehicles[4].Odometer, DateTime.Now, 50, BookingStatuses.Closed));
    }

    public List<T> Get<T>(Func<T, bool>? expression = null)
    {
        List<T> listToFilter = new();
        listToFilter = listToFilter.GetFieldOfType<T>(this);

        return expression is null ? listToFilter : listToFilter.Where(expression).ToList();
    }
    public T? Single<T>(Func<T, bool>? expression = null) => Get<T>().SingleOrDefault(expression);
    public void Add<T>(T item) => this.AddItemToCollection<T>(item);
    public IBooking RentVehicle(int vehicleId, int customerId) 
    {
            var vehicle = Single<IVehicle>(v => v.Id == vehicleId) ?? throw new Exception("No vehicle matching the provided Id found");
            var customer = Single<IPerson>(c => c.Id == customerId) ?? throw new Exception("No customer matching the provided Id found");
      
            return new Booking(NextBookingId, customer, vehicle, DateTime.Now, vehicle.Odometer);
    }
    public IBooking ReturnVehicle(int vehicleId) => Single<IBooking>(b => b.Vehicle.Id == vehicleId && b.Status == BookingStatuses.Open) ?? 
            throw new Exception("No open bookings matching the provided vehicle data found"); 
}
