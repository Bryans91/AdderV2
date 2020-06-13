using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FullAdder.View
{
    public class InputViewModel : INotifyPropertyChanged
    {
        private bool value;

        public bool Value {
            get
            {
                return value;
            }
            set {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string v = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
