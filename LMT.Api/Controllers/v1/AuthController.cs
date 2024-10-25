using Asp.Versioning;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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
        [HttpPost("forgot-password/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authService.ForgotPasswordAsync(email);
            if (!result)
            {
                return BadRequest(new { message = "User not found" });
            }

            return Ok(new { message = "Password reset link sent" });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                await _authService.ResetPasswordAsync(request);
                return Ok("Password reset successful");
            }
            catch (Exception ex)
            {
                // Return an appropriate error response
                return BadRequest(ex.Message);
            }
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
            var result = await _authService.RegisterAsync(registerRequest);
            return Ok(result);
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] int code)
        {
            try
            {
                await _authService.AccountConfirmation(email, code);
                return Ok("Your email has been confirmed successfully.");
            }
            catch (Exception ex)
            {
                // Return an appropriate error response
                return BadRequest(ex.Message);
            }
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
            if (!users.Any()) return NotFound("No users found.");
            else
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
        [HttpPut("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserRequest request)
        {
            // Validate the request model
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            // Find the user by ID
            var user = await _authService.GetUserByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update user properties
            user.UserFullName = request.Username;
            user.UserName = request.Username.Split(' ').First().ToLower();
            user.PhoneNumber = request.PhoneNumber;

            // Call the repository to edit the user
            var result = await _authService.EditUserAsync(user);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user.");
            }

            return Ok("User updated successfully.");
        }
        [HttpGet("registered-users/{userId}")]
        public async Task<IActionResult> GetRegisteredUsers(string userId)
        {
            var users = await _authService.GetRegisteredUsersListAsync(userId);
            if (!users.Any()) return NotFound("No users found.");
            else
                return Ok(users);
        }
        [HttpPost("send-account-confirmation/{email}")]
        public async Task<IActionResult> SendReAccountConfirmation(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            try
            {
                var result = await _authService.SendReAccountConfirmation(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending re-account confirmation: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
