using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;
using System.Collections.Immutable;

namespace Car_Rental.Business.Classes;

public class BookingProcessor
{
    private readonly IData _db;
    public BookingProcessor(IData db) => _db = db;

    public string Error { get; private set; } = string.Empty;
    public bool Busy;

    public Customer newCustomer = new();
    public Vehicle newVehicle = new();
    public Booking newBooking = new();

    public IEnumerable<IBooking> GetBookings()
    {
        var bookings = _db.Get<IBooking>();
        if ( bookings.Count == 0)
            Error = "Now bookings found";
        return bookings;
    }
    public IEnumerable<IPerson> GetCustomers()
    {
        var persons = _db.Get<IPerson>();
        if (persons.Count == 0)
            Error = "Now customers found";
        return persons;
    }
    public IPerson? GetPerson(string ssn) => _db.Single<IPerson>(p => p.SocialSecurityNumber.ToLower() == ssn.ToLower());
    public IEnumerable<IVehicle> GetVehicles(VehicleStatuses status = default)
    {
        var vehicles = _db.Get<IVehicle>();
        if (vehicles.Count == 0)
            Error = "No vehicles found.";
        return vehicles;
    }
    public IVehicle? GetVehicle(int vehicleId) => _db.Single<IVehicle>(v => v.Id == vehicleId);
    public IVehicle? GetVehicle(string regNo) => _db.Single<IVehicle>(v => v.RegistrationNumber.ToUpper() == regNo.ToUpper());
    public async Task<IBooking?> RentVehicle(int vehicleId, int customerId)
    {
        Error = string.Empty;
        if (customerId == 0)
        {
            Error = "Select a customer";
            return null;
        }
        Busy = true;
        try
        {
            var booking = await Task.Run(()=> _db.RentVehicle(vehicleId, customerId));
            await Task.Delay(5000);
            _db.Add(booking);
            GetVehicle(booking.Vehicle.RegistrationNumber)?.UpdateVehicleStatus(VehicleStatuses.Booked);
            return booking;
        }
        catch
        {
            throw new Exception( "Failed to rent vehicle.");
        }
        finally { Busy = false; }
}
    public IBooking ReturnVehicle(int vehicleId, double? distance)
    {
        Error = string.Empty;
        try
        {
            var booking = _db.ReturnVehicle(vehicleId);
            if (distance < 0 || distance == null)
            {
                Error = "Invalid distance value.";
                newBooking.Distance = null;
                return booking;
            }
            booking.CloseBooking(distance);
            booking.Vehicle.UpdateVehicleStatus(VehicleStatuses.Available);
            booking.Vehicle.UpdateVehicleOdometer(newBooking.Distance);
            newBooking.Distance = null;
            return booking;
        }
        catch
        {
            throw new Exception( "Failed to return vehicle.");
        }
    }
    public void AddVehicle(string make, string registrationNumber, double odometer, double costKm, VehicleStatuses status, VehicleTypes type)
    {
        Error = string.Empty;
        if (make != null && make.Length > 0 && registrationNumber != null && registrationNumber.Length == 6 && odometer >= 0
            && costKm >= 0 && type != 0)
        {
            var vehicleExist = GetVehicle(registrationNumber.ToUpper());
            if (vehicleExist != null)
            {
                Error = "The vehicle was in the list already.";
                return;
            }
            Vehicle vehicle = new(_db.NextVehicleId, registrationNumber.ToUpper(), make, odometer, costKm, type, status);
            _db.Add<IVehicle>(vehicle);
        }
        else { Error = "Could not add vehicle"; }
        newVehicle = new();
    }
    public void AddCustomer(string socialSecurityNumber, string firstName, string lastName)
    {
        Error = string.Empty;
        if (socialSecurityNumber != null && socialSecurityNumber.Length == 5 && firstName != null && firstName.Length > 1 
            && firstName != null && lastName.Length > 1 && firstName.All(c => Char.IsLetter(c)) && 
            lastName.All(c => Char.IsLetter(c)))
        {
            var customerExist = GetPerson(socialSecurityNumber.ToLower());
            if (customerExist != null)
            {
                Error = "The customer was in the list already.";
                return;
            }
            firstName = char.ToUpper(firstName[0]) + firstName[1..].ToLower();
            lastName = char.ToUpper(lastName[0]) + lastName[1..].ToLower();
            
            Customer newCustomer = new(_db.NextPersonId, socialSecurityNumber, firstName, lastName);
            _db.Add<IPerson>(newCustomer);
        }
        else
            Error =" Could not add customer.";
        newCustomer = new();
    }

    public string[] VehicleStatusNames => _db.VehicleStatusNames;
    public string[] VehicleTypeNames => _db.VehicleTypeNames;
    public VehicleTypes GetVehicleType(string name) => _db.GetVehicleType(name);
}