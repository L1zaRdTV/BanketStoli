using System.Data.SqlClient;
using BanketStoli.Wpf.Data;
using BanketStoli.Wpf.Models;

namespace BanketStoli.Wpf.Services
{
    public class UserService
    {
        public User Authenticate(string login, string password)
        {
            using (var connection = Database.CreateConnection())
            using (var command = new SqlCommand(@"SELECT u.Id, u.FullName, u.Phone, u.Email, u.Login, u.Password, r.Name AS RoleName
FROM Users u INNER JOIN UserRoles r ON r.Id = u.RoleId
WHERE u.Login = @login AND u.Password = @password", connection))
            {
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new User
                    {
                        Id = (int)reader["Id"],
                        FullName = reader["FullName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString(),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["RoleName"].ToString() == "Менеджер" ? UserRole.Manager : UserRole.Client
                    };
                }
            }
        }
    }
}
