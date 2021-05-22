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
    public class observableItem<T> : INotifyPropertyChanged
    {
        private T _itemValue = default;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public T itemValue
        {
            get => _itemValue;
            set 
            {
                if(_itemValue == null || !_itemValue.Equals(value)) 
                {
                    _itemValue = value;
                    OnPropertyChanged();
                }
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string new_Value = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(new_Value));
        }
    }
}
