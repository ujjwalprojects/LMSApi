using LMT.Api.Data;
using LMT.Api.DTOs;

namespace LMT.Api.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> AuthenticateUserAsync(LoginRequest loginRequest);
        Task RegisterAsync(RegisterRequest registerRequest);
        Task<bool> EditUserAsync(ApplicationUser editUserRequest);
        Task<TokenResponse> RefreshToken(TokenModel tokenModel);
        Task RevokeAsync(string refreshToken);
        Task RevokeAllAsync();
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
        Task<bool> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
        Task<List<UserListDTO>> GetUserListAsync();
        Task<bool> DeleteUserAsync(string userId);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}
