using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Api.DTOs
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int District_Id { get; set; }
    }
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
    public class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? userId { get; set; }
        public string? profileName { get; set; }
        public DateTime? expiration { get; set; }
        public IList<string>? role { get; set; }
        public string? phonenumber { get; set; }
        public string? email { get; set; }
        public string? userName { get; set; }
    }
    public class ResetPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
    public class ChangePasswordRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
    }
    public class UserListDTO
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }

    public class EditUserRequest
    {
        public string UserId { get; set; }      
        public string Username { get; set; }  
        public string PhoneNumber { get; set; }  
    }
}
