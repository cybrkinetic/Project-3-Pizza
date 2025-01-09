using System;
using System.Collections.Generic;
using System.Linq;
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

namespace StonksPizzaWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza Pizza = new Pizza();
            Pizza.Show();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Order Order = new Order();
            Order.Show();
        }

        private void btnIngredient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient Ingredient = new Ingredient();
            Ingredient.Show();
        }
    }
}
