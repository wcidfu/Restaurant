using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using Restaurant.Entities;

namespace Restaurant.Windows
{
    public partial class SignInWindow : Window
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        public SignInWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;
            var user = _context.Users.FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введенные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверяем, не заблокирован ли пользователь
            if (user.BlockedUntil.HasValue && user.BlockedUntil > DateTime.Now)
            {
                MessageBox.Show("Вы заблокированы. 🐷 Обратитесь к администратору", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Password == password)
            {
                user.FailedLoginAttempts = 0;
                _context.SaveChanges();

                MessageBox.Show("Вы успешно авторизовались", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                bool isAdmin = user.Role_id == 2;
                MainWindow mainWindow = new MainWindow(isAdmin, user.Name);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= 3)
                {
                    user.BlockedUntil = DateTime.Now.AddYears(100); // Блокируем навсегда до ручной разблокировки
                    MessageBox.Show("Вы заблокированы. 🐷 Обратитесь к администратору", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введенные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                _context.SaveChanges();
            }
        }
        //выявить ошибку..
        private void SignInGuestButton_Click(object sender, RoutedEventArgs e)
        {
            string Guest = "Гость";

            UserMainWindow userMainWindow = new UserMainWindow(false, Guest);
            userMainWindow.Show();
            this.Close();
        }
    }
}
