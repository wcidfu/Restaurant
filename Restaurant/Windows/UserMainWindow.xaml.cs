using Microsoft.EntityFrameworkCore;
using Restaurant.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        private readonly bool _isAdmin;
        private readonly string _userName;

        private ObservableCollection<ProductEntity> _products;
        public ObservableCollection<ProductEntity> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductEntity> SelectedDishes { get; set; }

        public UserMainWindow(bool isAdmin, string userName)
        {
            InitializeComponent();
            _isAdmin = isAdmin;
            _userName = userName;

            // Отобразим имя пользователя
            UserNameTextBox.Text = $"{_userName}";
            DataContext = this;
            SelectedDishes = new ObservableCollection<ProductEntity>();
            SelectedDishesList.ItemsSource = SelectedDishes; // Привязываем список
            LoadProducts();
        }

        //доделать..

        private async void LoadProducts()
        {
            using (var context = new RestaurantDbContext())
            {
                Products = new ObservableCollection<ProductEntity>(await context.Products.ToListAsync());
                DishItemsControl.ItemsSource = Products.Where(p => p.Category == "Закуски").ToList();
            }
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string category = button.Tag.ToString();
            DishItemsControl.ItemsSource = Products.Where(p => p.Category == category).ToList();
        }

        private void Dish_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = (Border)sender;
            var product = border.DataContext as ProductEntity;

            if (product != null)
            {
                if (SelectedDishes.Contains(product))
                {
                    SelectedDishes.Remove(product);
                }
                else
                {
                    SelectedDishes.Add(product);
                }

                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            decimal totalPrice = SelectedDishes.Sum(d => d.Price);
            TotalPriceText.Text = totalPrice.ToString("C");
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ваш заказ успешно оформлен!");
            SelectedDishes.Clear();
            UpdateTotalPrice();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


    

