using System;

namespace BanketStoli.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public int? ManagerId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int PaymentStatusId { get; set; }
        public decimal PrepaymentAmount { get; set; }
        public DateTime? PaymentStatusDate { get; set; }
    }
}
