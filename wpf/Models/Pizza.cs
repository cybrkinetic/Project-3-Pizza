using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StonksPizzaWPF.Models
{
    public class Pizza : INotifyPropertyChanged
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

        private string pizzaNaam;

        public string PizzaNaam
        {
            get { return pizzaNaam; }
            set { pizzaNaam = value; OnPropertyChanged(); }
        }

        private decimal pizzaPrijs;

        public decimal PizzaPrijs
        {
            get { return pizzaPrijs; }
            set { pizzaPrijs = value; OnPropertyChanged(); }
        }

    }
}
