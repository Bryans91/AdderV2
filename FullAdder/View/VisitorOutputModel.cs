using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FullAdder.View
{
    public class VisitorOutputModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _list = new ObservableCollection<string>();
        public ObservableCollection<string> List
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged("List");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string v = null)
        {
            Console.WriteLine("updated list");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

    }
}
