using Microsoft.AspNetCore.Identity;

namespace Blindness_API.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public List<Result> Results { get; set; }
        public string OtpCode { get; set; } 
        public DateTime? OtpExpiryTime { get; set; }

    }
}
