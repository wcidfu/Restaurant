using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using Restaurant.Entities;

namespace Restaurant
{
    public partial class MainWindow : Window
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        private readonly bool _isAdmin;
        private readonly string _userName;

        public MainWindow(bool isAdmin, string userName)
        {
            InitializeComponent();
            _isAdmin = isAdmin;
            _userName = userName;

            // Отображаем имя пользователя
            UserNameTextBox.Text = $"{_userName}";

            // Отображаем кнопки администратора
            if (_isAdmin)
            {
                AdminButtonsPanel.Visibility = Visibility.Visible;
            }

            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _context.Products.ToList();
            ProductDataGrid.ItemsSource = products;
        }

        // Добавление продукта
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var newProduct = new ProductEntity
            {
                Name = "Новый продукт",
                Category = "Категория",
                Price = 0.0M,
                ImagePath = "Путь/к/изображению"
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            LoadProducts();
        }

        // в методе добавления ошибка - не добовляется, проблема с бд

        // Редактирование продукта
        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is ProductEntity selectedProduct)
            {
                // Пример редактирования
                selectedProduct.Name = "Отредактированный продукт";
                selectedProduct.Price += 10.0M;

                _context.Products.Update(selectedProduct);
                _context.SaveChanges();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Выберите продукт для редактирования.");
            }
        }

        // Удаление продукта
        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is ProductEntity selectedProduct)
            {
                _context.Products.Remove(selectedProduct);
                _context.SaveChanges();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Выберите продукт для удаления.");
            }
        }
    }
}
