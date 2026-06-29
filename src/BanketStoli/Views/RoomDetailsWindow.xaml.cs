using System.Windows;
using System.Windows.Media;
using BanketStoli.Models;

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
            var currentPhoto = room.CurrentPhoto;
            if (string.IsNullOrWhiteSpace(currentPhoto))
            {
                RoomImage.Source = null;
                ImageTextBlock.Text = "Фото не указано или файл не найден";
            }
            else
            {
                RoomImage.Source = new ImageSourceConverter().ConvertFromString(currentPhoto) as ImageSource;
                ImageTextBlock.Text = string.Empty;
            }
            DescriptionTextBlock.Text = room.Description;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
