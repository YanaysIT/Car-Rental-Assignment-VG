using Car_Rental.Common.Classes;

namespace Car_Rental.Common.Interfaces;

public interface IPerson
{
    int Id { get;set; }
    string SocialSecurityNumber { get; }
    string FirstName { get; }
    string LastName { get; }
}