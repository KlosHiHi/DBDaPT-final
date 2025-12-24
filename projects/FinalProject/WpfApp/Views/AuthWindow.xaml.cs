using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Models;
using ShoeShopLibrary.Services;
using System.Windows;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private AuthService _authService = new(new ShoeShopDbContext());

        public AuthWindow()
        {
            InitializeComponent();
        }

        private async void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            await StartNewSessionAsync();
        }

        private async Task StartNewSessionAsync()
        {
            var user = await AuthUser();

            if (user is null)
            {
                MessageBox.Show("При авторизации произошла ошибка");
                return;
            }

            UserSession.Instance.SetCurrentUser(user);
            MessageBox.Show("Авторизация прошла успешно");

            OpenMainWindow();
        }

        private async Task<User?> AuthUser()
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            LoginRequest loqinRequest = new(login, password);

            var user = await _authService.AuthUserAsync(loqinRequest);

            return user;
        }

        private void OpenMainWindow()
        {
            ShoeMainWindow window = new();
            window.Show();
            Close();
        }
    }
}