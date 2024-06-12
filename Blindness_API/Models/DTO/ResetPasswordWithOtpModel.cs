namespace Blindness_API.Models.DTO
{
    public class ResetPasswordWithOtpModel
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
