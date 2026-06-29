namespace BanketStoli.Models
{
    public class BanquetRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public int TableCount { get; set; }
        public decimal RentPricePerHour { get; set; }
        public string Image { get; set; }
        public string CurrentPhoto
        {
            get
            {
                if (string.IsNullOrEmpty(Image) || string.IsNullOrWhiteSpace(Image))
                {
                    return @"/Images/Zagluhca.png";
                }

                return @"/Images/" + Image;
            }
        }
        public string Description { get; set; }
    }
}
