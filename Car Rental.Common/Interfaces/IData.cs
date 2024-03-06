using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;
using System.Linq.Expressions;

namespace Car_Rental.Common.Interfaces;

public interface IData
{
    List<T> Get<T>(Func<T, bool>? expression = null);
    T? Single<T>(Func<T, bool>? expression = null);
    public void Add<T>(T item);
    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }
    IBooking RentVehicle(int vehicleId, int customerId);
    IBooking ReturnVehicle(int vehicleId);

    public string[] VehicleStatusNames => Enum.GetNames<VehicleStatuses>();
    public string[] VehicleTypeNames => Enum.GetNames<VehicleTypes>();
    public VehicleTypes GetVehicleType(string name) => (VehicleTypes)Enum.Parse(typeof(VehicleTypes), name);
}