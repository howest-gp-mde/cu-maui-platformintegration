namespace Mde.PlatformIntegration.Domain.Models
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ConsentAnalytics { get; set; }
        public bool ConsentPersonalized { get; set; }
    }
}
