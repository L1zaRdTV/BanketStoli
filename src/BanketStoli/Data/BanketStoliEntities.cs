using System.Data.Entity;
using BanketStoli.Models;

namespace BanketStoli.Data
{
    public class BanketStoliEntities : DbContext
    {
        public BanketStoliEntities() : base("name=BanketStoliDb")
        {
        }

        public DbSet<BanquetRoom> BanquetRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<DecorationStyle> DecorationStyles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<UserRoleRecord> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanquetRoom>().ToTable("BanquetRooms");
            modelBuilder.Entity<BanquetRoom>().Property(room => room.Image).HasColumnName("ImagePath");
            modelBuilder.Entity<BanquetRoom>().Ignore(room => room.StyleName);
            modelBuilder.Entity<BanquetRoom>().Ignore(room => room.CurrentPhoto);

            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<ClientType>().ToTable("ClientTypes");
            modelBuilder.Entity<DecorationStyle>().ToTable("DecorationStyles");
            modelBuilder.Entity<Manager>().ToTable("Managers");
            modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatuses");

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Ignore(user => user.FullName);
            modelBuilder.Entity<User>().Ignore(user => user.Phone);
            modelBuilder.Entity<User>().Ignore(user => user.Email);
            modelBuilder.Entity<User>().Ignore(user => user.Role);

            modelBuilder.Entity<UserRoleRecord>().ToTable("UserRoles");
        }
    }
}
