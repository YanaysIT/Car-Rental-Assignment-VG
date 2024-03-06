using Car_Rental.Common.Enums;
using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Motorcycle : Vehicle
{
    public Motorcycle(int id, string registrationNumber, string make, double odometer, double costKm, VehicleTypes vehicleType, VehicleStatuses status) 
        : base(id, registrationNumber, make, odometer, costKm, vehicleType, status) {}
}
    