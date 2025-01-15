using StonksPizzaWPF.Models;
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

namespace StonksPizzaWPF.Views
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
        #region fields
        private readonly StonksPizzaDB db = new StonksPizzaDB();
        private readonly string serviceDeskBericht = "\n\nNeem contact op met de service desk";
        #endregion


        #region Properties
        private ObservableCollection<Models.Order> orders = new();
        public ObservableCollection<Models.Order> Orders
        {
            get { return orders; }
            set { orders = value; OnPropertyChanged(); }
        }

        private Models.Order? selectedOrder;
        public Models.Order? SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; PopulateBesteldePizzas();  OnPropertyChanged(); }
        }
        #endregion


        private ObservableCollection<Models.BesteldePizza> pizzas = new();
        public ObservableCollection<Models.BesteldePizza> Pizzas
        {
            get { return pizzas; }
            set { pizzas = value; OnPropertyChanged(); }
        }

        private Models.BesteldePizza? selectedPizza;
        public Models.BesteldePizza? SelectedPizza
        {
            get { return selectedPizza; }
            set { selectedPizza = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Models.Status> statusPizza = new();
        public ObservableCollection<Models.Status> StatusPizza
        {
            get { return statusPizza; }
            set { statusPizza = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Models.Status> statusOrder = new();
        public ObservableCollection<Models.Status> StatusOrder
        {
            get { return statusOrder; }
            set { statusOrder = value; OnPropertyChanged(); }
        }




        public Order()
        {
            InitializeComponent();
            DataContext = this;
            PopulateAll();
        }

        private void PopulateAll()
        {
            PopulateOrders();
            PopulateStatusOrder();
            PopulateStatusPizza();
            PopulateBesteldePizzas();
        }

        private void PopulateBesteldePizzas()
        {
            Pizzas.Clear();
            if (SelectedOrder != null)
            {
                string dbResult = db.GetBesteldePizzas(SelectedOrder.Id, Pizzas);
                if (dbResult != StonksPizzaDB.OK)
                {
                    MessageBox.Show(dbResult + serviceDeskBericht);
                }
            }
        }

        private void PopulateOrders()
        {
            Orders.Clear();

            string dbResult = db.GetOrders(Orders);
            if (dbResult != StonksPizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }

        }

        private void PopulateStatusOrder()
        {
            StatusOrder.Clear();

            string dbResult = db.GetStatusOrder(StatusOrder);
            if (dbResult != StonksPizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }

        }

        private void PopulateStatusPizza()
        {
            StatusPizza.Clear();

            string dbResult = db.GetStatusPizza(StatusPizza);
            if (dbResult != StonksPizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }

        }


        private void UpdateOrderClick(object sender, RoutedEventArgs e)
        {
            if (SelectedOrder == null)
            {
                return;
            }
            
            string dbResult = db.UpdateOrderStatus(SelectedOrder.Id, SelectedOrder);
            MessageBox.Show(dbResult);

            PopulateAll();
            
            OnPropertyChanged();
        }

        private void UpdatePizzaClick(object sender, RoutedEventArgs e)
        {
            if (SelectedPizza == null)
            {
                return;
            }
            ulong test = SelectedPizza.PizzaStatusId;

            
            string dbResult = db.UpdateBesteldePizza(SelectedPizza.Id, SelectedPizza);
            MessageBox.Show(dbResult);

            PopulateStatusOrder();
            PopulateStatusPizza();
            PopulateBesteldePizzas();

            OnPropertyChanged();
        }
    }
}
