namespace travelapi.Domain.Models
{
    public class AuthUser
    {
        public string accessToken { get; set; }
        public User user { get; set; } 
    }
}
