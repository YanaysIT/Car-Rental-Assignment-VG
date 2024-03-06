using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Vehicle : IVehicle
{
    public int Id { get; set; } = default;
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public double Odometer { get; set; } = 0;
    public double CostKm { get; set; } = 0;
    public VehicleTypes VehicleType { get; set; } = VehicleTypes.Sedan;
    public VehicleStatuses Status { get; set; } = VehicleStatuses.Available;

    public Vehicle() { }
    public Vehicle(int id, string registrationNumber, string make, double odometer, double costKm, VehicleTypes vehicleType, 
        VehicleStatuses status) =>

       (Id, RegistrationNumber, Make, Odometer, CostKm, VehicleType, Status) =
            (id, registrationNumber, make, odometer, costKm, vehicleType, status);

    public void UpdateVehicleStatus(VehicleStatuses status) => Status = status;
    public void UpdateVehicleOdometer(double? distance) => Odometer += (double)distance;
}
