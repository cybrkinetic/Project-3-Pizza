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
    /// Interaction logic for Pizza.xaml
    /// </summary>
    public partial class Pizza : Window, INotifyPropertyChanged
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
        private ObservableCollection<Models.Ingredient> ingredients = new();
        public ObservableCollection<Models.Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged(); }
        }

        private Models.Ingredient? selectedIngredient;
        public Models.Ingredient? SelectedIngredient
        {
            get { return selectedIngredient; }
            set { selectedIngredient = value; OnPropertyChanged(); }
        }



        private ObservableCollection<Models.Ingredient> pizzaIngredients = new();
        public ObservableCollection<Models.Ingredient> PizzaIngredients
        {
            get { return pizzaIngredients; }
            set { pizzaIngredients = value; OnPropertyChanged(); }
        }

        private Models.Ingredient? selectedPizzaIngredient;
        public Models.Ingredient? SelectedPizzaIngredient
        {
            get { return selectedPizzaIngredient; }
            set { selectedPizzaIngredient = value; OnPropertyChanged(); }
        }


        private ObservableCollection<Models.Pizza> pizzas = new();
        public ObservableCollection<Models.Pizza> Pizzas
        {
            get { return pizzas; }
            set { pizzas = value; OnPropertyChanged(); }
        }

        private Models.Pizza? selectedPizza;
        public Models.Pizza? SelectedPizza
        {
            get { return selectedPizza; }
            set { selectedPizza = value; PopulateIngredients(); PopulatePizzaIngredients(); OnPropertyChanged(); }
        }


        private Models.Pizza? newPizza = new();
        public Models.Pizza? NewPizza
        {
            get { return newPizza; }
            set
            {
                newPizza = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public Pizza()
        {
            InitializeComponent();
            DataContext = this;
            PopulateAll();
        }

        private void PopulateAll()
        {
            PopulatePizzas();
            PopulateIngredients();
            PopulatePizzaIngredients();
        }

        private void PopulatePizzas()
        {
            Pizzas.Clear();
            string dbResult = db.GetPizzas(Pizzas);
            if (dbResult != StonksPizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void PopulateIngredients()
        {
            Ingredients.Clear();
            if (SelectedPizza != null)
            {
                string dbResult = db.GetNotAddedPizzaIngredients(Ingredients, SelectedPizza.Id);
                if (dbResult != StonksPizzaDB.OK)
                {
                    MessageBox.Show(dbResult + serviceDeskBericht);
                }
            }
        }

        private void PopulatePizzaIngredients()
        {
            PizzaIngredients.Clear();
            if(SelectedPizza != null)
            {
                string dbResult = db.GetAddedPizzaIngredients(PizzaIngredients, SelectedPizza.Id);
                if (dbResult != StonksPizzaDB.OK)
                {
                    MessageBox.Show(dbResult + serviceDeskBericht);
                }
            }
        }


        private void DeletePizzaIngredientClick(object sender, RoutedEventArgs e)
        {
            Models.Ingredient test = new();
            Button verwijder = ((Button)sender);
            test = (Models.Ingredient)verwijder.DataContext;
            if (SelectedPizza == null)
            {
                return;
            }
            string dbResult = db.DeletePizzaIngredient(test.Id, SelectedPizza.Id);

            PopulateIngredients();
            PopulatePizzaIngredients();
            OnPropertyChanged();
        }

        private void AddPizzaIngredientClick(object sender, RoutedEventArgs e)
        {
            if (SelectedPizza == null)
            {
                return;
            }
            Models.Ingredient test = new();
            Button Add = ((Button)sender);
            test = (Models.Ingredient)Add.DataContext;

            string dbResult = db.CreatePizzaIngredient(test.Id, SelectedPizza.Id);
            MessageBox.Show(dbResult);
            PopulateIngredients();
            PopulatePizzaIngredients();
            OnPropertyChanged();
        }

        private void DeletePizzaClick(object sender, RoutedEventArgs e)
        {
            Models.Pizza test = new();
            Button verwijder = ((Button)sender);
            test = (Models.Pizza)verwijder.DataContext;
            string dbResult = db.DeletePizza(test.Id);

            PopulateAll();
            OnPropertyChanged();
        }

        private void ChangePizzaClick(object sender, RoutedEventArgs e)
        {
            if (SelectedPizza == null || string.IsNullOrEmpty(SelectedPizza.PizzaNaam) 
                || SelectedPizza.PizzaPrijs < 0)
            {
                return;
            }
            string dbResult = db.UpdatePizza(SelectedPizza.Id, SelectedPizza);
            MessageBox.Show(dbResult);

            PopulateAll();
            OnPropertyChanged();
        }

        private void AddPizzaClick(object sender, RoutedEventArgs e)
        {
            if (NewPizza == null || string.IsNullOrEmpty(NewPizza.PizzaNaam) 
                || NewPizza.PizzaPrijs < 0)
            {
                return;
            }
            string dbResult = db.CreatePizza(NewPizza);
            MessageBox.Show(dbResult);
            NewPizza = new();
            PopulateAll();
            OnPropertyChanged();
        }
    }
}
