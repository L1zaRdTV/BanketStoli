using System.Windows;
using BanketStoli.Wpf.Models;

namespace BanketStoli.Wpf.Views
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
            RoomImage.Source = string.IsNullOrWhiteSpace(room.ImagePath) ? null : new System.Windows.Media.Imaging.BitmapImage(new System.Uri(room.ImagePath, System.UriKind.RelativeOrAbsolute));
            ImageTextBlock.Text = string.IsNullOrWhiteSpace(room.ImagePath) ? "Изображение не указано" : string.Empty;
            DescriptionTextBlock.Text = room.Description;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
