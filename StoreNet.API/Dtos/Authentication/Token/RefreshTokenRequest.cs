using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Authentication.Token;

public record RefreshTokenRequest(
    [Required] string Token
);

