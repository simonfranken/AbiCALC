using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.serialization
{
    [Serializable]
    public class databaseInfo
    {
        private string s = null;
        [NonSerialized]
        private FileInfo _lastSelected = null;
        public FileInfo lastSelected
        {
            get => _lastSelected;

            set => _lastSelected = value;
        }

        public bool isSet() => lastSelected != null;

        [OnDeserialized()]
        private void deserialized(StreamingContext context)
        {
            lastSelected = new FileInfo(s);
        }

        [OnSerializing]
        private void serializing(StreamingContext context)
        {
            s = lastSelected.FullName;
        }
    }
}
