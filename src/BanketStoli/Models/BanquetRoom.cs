using System;
using System.IO;

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
                if (string.IsNullOrWhiteSpace(Image))
                {
                    return null;
                }

                var photoPath = Image.Trim();
                if (Path.IsPathRooted(photoPath))
                {
                    return File.Exists(photoPath) ? photoPath : null;
                }

                var applicationImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", photoPath);
                return File.Exists(applicationImagePath) ? applicationImagePath : null;
            }
        }
        public string Description { get; set; }
    }
}
