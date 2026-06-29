using System;

namespace BanketStoli.Wpf.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int StatusId { get; set; }
        public decimal? PrepaymentAmount { get; set; }
        public DateTime? PrepaymentDate { get; set; }
    }
}
