namespace StoreNet.Application.Dtos.Auth;

// Password Reset
public record ResetPasswordDto(
    string Email,
    string Token,
    string NewPassword,
    string ConfirmNewPassword);