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
                                join client in context.Clients on user.ClientId equals client.Id into clientProfiles
                                from client in clientProfiles.DefaultIfEmpty()
                                join manager in context.Managers on user.ManagerId equals manager.Id into managerProfiles
                                from manager in managerProfiles.DefaultIfEmpty()
                                where user.Login == login && user.Password == password
                                select new
                                {
                                    User = user,
                                    RoleName = role.Name,
                                    ClientName = client.FullName,
                                    ManagerName = manager.FullName
                                }).FirstOrDefault();

                if (userData == null) return null;

                userData.User.Role = userData.RoleName == "Менеджер" ? UserRole.Manager : UserRole.Client;
                userData.User.FullName = userData.User.Role == UserRole.Manager ? userData.ManagerName : userData.ClientName;
                return userData.User;
            }
        }
    }
}
