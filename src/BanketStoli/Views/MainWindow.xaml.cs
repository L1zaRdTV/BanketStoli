using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BanketStoli.Models;
using BanketStoli.Services;

namespace BanketStoli.Views
{
    public partial class MainWindow : Window
    {
        private readonly User currentUser;
        private readonly RoomService roomService = new RoomService();

        public MainWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            UserTextBlock.Text = $"{currentUser.FullName} ({(currentUser.Role == UserRole.Manager ? "менеджер" : "клиент")})";
            AddButton.Visibility = EditButton.Visibility = DeleteButton.Visibility = currentUser.Role == UserRole.Manager ? Visibility.Visible : Visibility.Collapsed;
            LoadStyles();
            LoadRooms();
        }

        private BanquetRoom SelectedRoom => RoomsGrid.SelectedItem as BanquetRoom;

        private void LoadStyles()
        {
            var styles = roomService.GetStyles();
            styles.Insert(0, new DecorationStyle { Id = 0, Name = "Все стили" });
            StyleComboBox.ItemsSource = styles;
            StyleComboBox.SelectedIndex = 0;
        }

        private void LoadRooms()
        {
            try
            {
                var styleId = StyleComboBox.SelectedValue is int id && id > 0 ? id : (int?)null;
                var sortMode = (SortComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString();
                var rooms = roomService.GetRooms(SearchTextBox.Text.Trim(), styleId, sortMode);
                RoomsGrid.ItemsSource = rooms;
                if (rooms.Count == 0) MessageBox.Show("По заданным условиям комнаты не найдены.", "Нет результатов", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось загрузить список комнат. " + ex.Message, "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filters_Changed(object sender, EventArgs e) { if (IsLoaded) LoadRooms(); }

        private void RoomsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedRoom == null) return;
            new RoomDetailsWindow(SelectedRoom) { Owner = this }.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) { if (new RoomEditWindow(null) { Owner = this }.ShowDialog() == true) LoadRooms(); }
        private void EditButton_Click(object sender, RoutedEventArgs e) { if (SelectedRoom == null) { MessageBox.Show("Выберите комнату для редактирования.", "Действие недоступно", MessageBoxButton.OK, MessageBoxImage.Warning); return; } if (new RoomEditWindow(SelectedRoom) { Owner = this }.ShowDialog() == true) LoadRooms(); }
        private void DeleteButton_Click(object sender, RoutedEventArgs e) { if (SelectedRoom == null) { MessageBox.Show("Выберите комнату для удаления.", "Действие недоступно", MessageBoxButton.OK, MessageBoxImage.Warning); return; } if (MessageBox.Show("Удалить выбранную комнату?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) { roomService.DeleteRoom(SelectedRoom.Id); LoadRooms(); } }
        private void ChangeUserButton_Click(object sender, RoutedEventArgs e) { new LoginWindow().Show(); Close(); }
    }
}
