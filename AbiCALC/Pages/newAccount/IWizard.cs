using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AbiCALC.Pages.newAccount
{
    public interface IWizard
    {
        public string getError();
        public bool getIsValid();
        public delegate void updateDel();
        public event updateDel update;
        public IWizard getNext();
    }
}
