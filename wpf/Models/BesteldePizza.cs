using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StonksPizzaWPF.Models
{
    public class BesteldePizza:  INotifyPropertyChanged
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

        private ulong orderId;

        public ulong OrderId
        {
            get { return orderId; }
            set { orderId = value; OnPropertyChanged(); }
        }

        private ulong pizzaId;

        public ulong PizzaId
        {
            get { return pizzaId; }
            set { pizzaId = value; OnPropertyChanged(); }
        }

        public Pizza Pizza { get; set; }

        private ulong sizeId;

        public ulong SizeId
        {
            get { return sizeId; }
            set { sizeId = value; OnPropertyChanged(); }
        }

        public Size Size { get; set; }

        private ulong pizzaStatusId;

        public ulong PizzaStatusId
        {
            get { return pizzaStatusId; }
            set { pizzaStatusId = value; OnPropertyChanged(); }
        }

        public Status Status { get; set; }





    }
}
