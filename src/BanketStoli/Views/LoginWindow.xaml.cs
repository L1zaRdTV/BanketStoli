using System;
using System.Windows;
using BanketStoli.Services;

namespace BanketStoli.Views
{
    public partial class LoginWindow : Window
    {
        private readonly UserService userService = new UserService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль для входа в систему.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var user = userService.Authenticate(LoginTextBox.Text.Trim(), PasswordBox.Password);
                if (user == null)
                {
                    MessageBox.Show("Пользователь с указанными учетными данными не найден.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                new MainWindow(user).Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось выполнить вход. Проверьте доступность базы данных. " + ex.Message, "Непредвиденная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
