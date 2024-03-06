using Car_Rental.Common.Interfaces;

namespace Car_Rental.Common.Classes;

public class Customer : IPerson
{
    public int Id { get; set; }= default;
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public override string ToString() => $"{LastName} {FirstName} ({SocialSecurityNumber})";

    public Customer() { }
    public Customer(string socialSecurityNumber, string firstName, string lastName) :
    this(default, socialSecurityNumber, firstName, lastName){ }
    public Customer(int id, string socialSecurityNumber, string firstName, string lastName) =>
       (Id, SocialSecurityNumber, FirstName, LastName) = (id, socialSecurityNumber, firstName, lastName);
}
