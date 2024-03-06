using Car_Rental.Common.Classes;
using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IVehicle
{
    public int Id { get; }
    string RegistrationNumber { get; }
    string Make { get; }
    double Odometer { get; }
    double CostKm { get; }
    VehicleTypes VehicleType { get; }
    VehicleStatuses Status { get; }

    void UpdateVehicleStatus(VehicleStatuses status);
    void UpdateVehicleOdometer(double? distance);
}
