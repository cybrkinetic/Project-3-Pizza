using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StonksPizzaWPF.Models
{
    public class Ingredient : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private ulong id;

        public ulong Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string ingredientNaam;

        public string IngredientNaam
        {
            get { return ingredientNaam; }
            set { ingredientNaam = value; OnPropertyChanged(); }
        }

        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged(); }
        }



    }
}
