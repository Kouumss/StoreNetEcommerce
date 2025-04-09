using StoreNet.Application.Dtos.Carts;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Dtos.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public CartDto? Cart { get; set; }
    public bool IsAvailable { get; set; }

    public UserDto(Guid id, string firstName, string lastName, string email, bool isAvailable)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        IsAvailable = isAvailable;
    }
}

