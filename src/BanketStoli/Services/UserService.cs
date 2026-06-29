using System.Linq;
using BanketStoli.Data;
using BanketStoli.Models;

namespace BanketStoli.Services
{
    public class UserService
    {
        public User Authenticate(string login, string password)
        {
            using (var context = new BanketStoliEntities())
            {
                var userData = (from user in context.Users
                                join role in context.UserRoles on user.RoleId equals role.Id
                                where user.Login == login && user.Password == password
                                select new { User = user, RoleName = role.Name }).FirstOrDefault();

                if (userData == null) return null;

                userData.User.Role = userData.RoleName == "Менеджер" ? UserRole.Manager : UserRole.Client;
                return userData.User;
            }
        }
    }
}
