namespace BanketStoli.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int? ClientId { get; set; }
        public int? ManagerId { get; set; }
        public UserRole Role { get; set; }
    }
}
