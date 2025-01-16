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
    /// Interaction logic for Ingredient.xaml
    /// </summary>
    public partial class Ingredient : Window, INotifyPropertyChanged
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

        private Models.Ingredient? newIngredient = new();

        public Models.Ingredient? NewIngredient
        {
            get { return newIngredient; }
            set
            {
                newIngredient = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public Ingredient()
        {
            InitializeComponent();
            DataContext = this;
            PopulateIngredients();
        }

        private void PopulateIngredients()
        {
            Ingredients.Clear();
            string dbResult = db.GetIngredients(Ingredients);
            if (dbResult != StonksPizzaDB.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (NewIngredient == null || string.IsNullOrEmpty(NewIngredient.IngredientNaam)
            || string.IsNullOrEmpty(NewIngredient.Unit))
            {
                return;
            }
            string dbResult = db.CreateIngredient(NewIngredient);
            MessageBox.Show(dbResult);
            NewIngredient = new();
            PopulateIngredients();
            OnPropertyChanged();
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            if (selectedIngredient == null || string.IsNullOrEmpty(selectedIngredient.IngredientNaam)
            || string.IsNullOrEmpty(selectedIngredient.Unit))
            {
                return;
            }
            string dbResult = db.UpdateIngredient(selectedIngredient.Id, selectedIngredient);
            MessageBox.Show(dbResult);

            PopulateIngredients();
            OnPropertyChanged();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Models.Ingredient test = new();
            Button verwijder = ((Button)sender);
            test = (Models.Ingredient)verwijder.DataContext;
            string dbResult = db.DeleteIngredient(test.Id);

            PopulateIngredients();
            OnPropertyChanged();
        }
    }
}
