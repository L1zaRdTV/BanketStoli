using System;
using System.Globalization;
using System.Windows;
using BanketStoli.Wpf.Models;
using BanketStoli.Wpf.Services;

namespace BanketStoli.Wpf.Views
{
    public partial class RoomEditWindow : Window
    {
        private readonly RoomService roomService = new RoomService();
        private readonly BanquetRoom room;

        public RoomEditWindow(BanquetRoom sourceRoom)
        {
            InitializeComponent();
            room = sourceRoom == null ? new BanquetRoom() : new BanquetRoom { Id = sourceRoom.Id, Name = sourceRoom.Name, StyleId = sourceRoom.StyleId, TableCount = sourceRoom.TableCount, RentPricePerHour = sourceRoom.RentPricePerHour, ImagePath = sourceRoom.ImagePath, Description = sourceRoom.Description };
            StyleComboBox.ItemsSource = roomService.GetStyles();
            NameTextBox.Text = room.Name;
            StyleComboBox.SelectedValue = room.StyleId;
            TableCountTextBox.Text = room.TableCount == 0 ? string.Empty : room.TableCount.ToString();
            PriceTextBox.Text = room.RentPricePerHour == 0 ? string.Empty : room.RentPricePerHour.ToString(CultureInfo.CurrentCulture);
            ImagePathTextBox.Text = room.ImagePath;
            DescriptionTextBox.Text = room.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput(out var tableCount, out var price)) return;
            room.Name = NameTextBox.Text.Trim();
            room.StyleId = (int)StyleComboBox.SelectedValue;
            room.TableCount = tableCount;
            room.RentPricePerHour = price;
            room.ImagePath = ImagePathTextBox.Text.Trim();
            room.Description = DescriptionTextBox.Text.Trim();
            try { roomService.SaveRoom(room); DialogResult = true; }
            catch (Exception ex) { MessageBox.Show("Не удалось сохранить комнату. " + ex.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private bool ValidateInput(out int tableCount, out decimal price)
        {
            tableCount = 0; price = 0;
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || StyleComboBox.SelectedValue == null || string.IsNullOrWhiteSpace(DescriptionTextBox.Text)) { MessageBox.Show("Заполните название, стиль оформления и описание комнаты.", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            if (!int.TryParse(TableCountTextBox.Text, out tableCount) || tableCount <= 0) { MessageBox.Show("Количество столов должно быть положительным целым числом.", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            if (!decimal.TryParse(PriceTextBox.Text, out price) || price < 0) { MessageBox.Show("Стоимость аренды должна быть неотрицательным числом.", "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
