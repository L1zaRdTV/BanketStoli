using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
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
            RoomImage.Source = TryLoadImage(room.ImagePath);
            ImageTextBlock.Text = RoomImage.Source == null ? "Фото не указано" : string.Empty;
            DescriptionTextBlock.Text = room.Description;
        }

        private static BitmapImage TryLoadImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return null;
            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = Uri.TryCreate(imagePath, UriKind.Absolute, out var absoluteUri) ? absoluteUri : new Uri(Path.GetFullPath(imagePath), UriKind.Absolute);
                image.EndInit();
                image.Freeze();
                return image;
            }
            catch
            {
                return null;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
