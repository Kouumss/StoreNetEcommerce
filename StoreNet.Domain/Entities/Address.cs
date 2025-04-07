namespace StoreNet.Domain.Entities;

public class Address : BaseEntity
{
    public string StreetNumber { get; set; }
    public string StreetName { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; }

    public Address(string streetNumber, string streetName, string city, string postalCode, string country)
    {
        StreetNumber = streetNumber;
        StreetName = streetName;
        City = city;
        PostalCode = postalCode;
        Country = country;
    }

    public override string ToString()
    {
        return $"{StreetNumber} {StreetName}, {City}, {PostalCode}, {Country}";
    }
}
