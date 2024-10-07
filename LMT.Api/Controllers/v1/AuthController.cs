using Asp.Versioning;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMT.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _authService.AuthenticateUserAsync(loginRequest);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authService.ForgotPasswordAsync(request.Email);
            if (!result)
            {
                return BadRequest(new { message = "User not found" });
            }

            return Ok(new { message = "Password reset link sent" });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);
            if (!result)
            {
                return BadRequest(new { message = "Failed to reset password" });
            }

            return Ok(new { message = "Password reset successful" });
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _authService.ChangePasswordAsync(request);
            if (!result)
            {
                return BadRequest(new { message = "Failed to change password" });
            }

            return Ok(new { message = "Password change successful" });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            await _authService.RegisterAsync(registerRequest);
            return Ok();
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            await _authService.RefreshToken(tokenModel);
            return Ok();
        }
        [Authorize]
        [HttpPost]
        [Route("revoke/{refreshToken}")]

        public async Task<IActionResult> RevokeAsync(string refreshToken)
        {
            await _authService.RevokeAsync(refreshToken);
            return NoContent();
        }
        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _authService.RevokeAllAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation($"Method GetAllUsers invoked.");
            var users = await _authService.GetUserListAsync();
            return Ok(users);
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _authService.DeleteUserAsync(userId);
            if (result)
            {
                return Ok(new { message = "User deleted successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to delete user" });
            }
        }
    }
}
