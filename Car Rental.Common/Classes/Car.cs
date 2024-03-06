using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Classes;

public class Car : Vehicle
{
    public Car(int id, string registrationNumber, string make, double odometer, double costKm, VehicleTypes vehicleType, VehicleStatuses status) : 
        base(id, registrationNumber, make, odometer, costKm, vehicleType, status) {}
}   