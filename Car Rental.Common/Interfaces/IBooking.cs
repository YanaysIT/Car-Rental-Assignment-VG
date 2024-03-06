using Car_Rental.Common.Enums;

namespace Car_Rental.Common.Interfaces;

public interface IBooking
{
    int Id { get; }
    IPerson Customer { get; }
    IVehicle Vehicle { get; }
    DateTime DateRented { get; }
    double KmRented { get;}
    DateTime? DateReturned { get; }
    double? KmReturned { get; }
    public double? Distance { get; set; }
    double? Cost { get; }
    BookingStatuses Status { get; }

    void CloseBooking(double? distance);
    public string[] BookingStatusesNames => Enum.GetNames<BookingStatuses>();
}

