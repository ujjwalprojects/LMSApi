namespace LMT.Api.DTOs
{
    public class GetRegisteredFieldOfficersDTO
    {
        public string UserFullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
    }
}
