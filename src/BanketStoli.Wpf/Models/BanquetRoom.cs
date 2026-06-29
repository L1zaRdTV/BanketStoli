namespace BanketStoli.Wpf.Models
{
    public class BanquetRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public int TableCount { get; set; }
        public decimal RentPricePerHour { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }
}
