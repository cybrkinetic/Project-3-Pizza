using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StonksPizzaWPF.Models
{
    public class Order : INotifyPropertyChanged
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

        private ulong statusId;

        public ulong StatusId
        {
            get { return statusId; }
            set { statusId = value; OnPropertyChanged(); }
        }

        public Status Status { get; set; }


    }
}
