using Microsoft.EntityFrameworkCore;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.Models;
using System.Windows;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ShoeMainWindow.xaml
    /// </summary>
    public partial class ShoeMainWindow : Window
    {
        private ShoeShopDbContext _context = new();
        private List<Shoe> _shoes = new();

        public ShoeMainWindow()
        {
            InitializeComponent();

            InitializeData();
        }

        private async Task InitializeData()
        {
            _shoes = await _context.Shoes
                .Include(s => s.Maker)
                .Include(s => s.Vendor)
                .Include(s => s.Category)
                .ToListAsync();

            ShoeDataGrid.ItemsSource = _shoes;
        }
    }
}
