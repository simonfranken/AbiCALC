using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public class observableString : INotifyPropertyChanged
    {
        private string _s = string.Empty;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        [NonSerialized]
        public string format = string.Empty;
        public string s 
        {
            get => string.Format(format, _s);
            set 
            {
                if(value != _s) 
                {
                    _s = value;
                    OnPropertyChanged();
                }
            }
        }
        public string unformatted 
        {
            get => _s;
        }
        protected void OnPropertyChanged([CallerMemberName] string new_Value = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(new_Value));
        }
    }
}
