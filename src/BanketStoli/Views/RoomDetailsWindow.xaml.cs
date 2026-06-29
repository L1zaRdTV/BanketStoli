using System.Windows;
using BanketStoli.Models;
using BanketStoli.Utilities;

namespace BanketStoli.Views
{
    public partial class RoomDetailsWindow : Window
    {
        public RoomDetailsWindow(BanquetRoom room)
        {
            InitializeComponent();
            NameTextBlock.Text = room.Name;
            StyleTextBlock.Text = "Стиль оформления: " + room.StyleName;
            TableCountTextBlock.Text = "Количество столов: " + room.TableCount;
            PriceTextBlock.Text = $"Стоимость аренды: {room.RentPricePerHour:N2} руб./час";
            RoomImage.Source = ImageLoader.Load(room.ImagePath);
            ImageTextBlock.Text = RoomImage.Source == null ? "Фото не указано" : string.Empty;
            DescriptionTextBlock.Text = room.Description;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
