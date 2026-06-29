namespace BanketStoli.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int ClientTypeId { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
    }
}
