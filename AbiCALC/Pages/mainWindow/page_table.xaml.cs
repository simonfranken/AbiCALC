using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AbiCALC.Pages.mainWindow
{
    /// <summary>
    /// Interaction logic for page_table.xaml
    /// </summary>
    public partial class page_table : Page
    {
        public page_table()
        {
            InitializeComponent();
            init();
        }
        public void init()
        {
            IEnumerable<subjectTypes> x = serialization.database.currentData.getSubjectTypes();
            List<List<string>> l = new List<List<string>>();
            List<string> ll1 = new List<string>();
            ll1.Add("Name");
            ll1.AddRange(baseSubjetTypes.D.getHeadings());
            l.Add(ll1);
            List<string> usedOverrideIds = new List<string>();
            foreach (subjectTypes item in x)
            {
                if (item.ct.t == "D")
                {
                    List<int> r;
                    string name;
                    if (item.overrideGrades())
                    {
                        if (!usedOverrideIds.Contains(item.getOverrideId()))
                        {
                            usedOverrideIds.Add(item.getOverrideId());
                            r = subjectTypes.getOverrideValues(item.getOverrideId());
                            name = item.getOverrideId();
                        }
                        else continue;
                    }
                    else
                    {
                        r = subjectTypes.getGrades(item);
                        name = item.Name.itemValue;
                    }
                    List<string> r2 = new List<string>();
                    r2.Add(name);
                    for (int y = 0; y < r.Count; y++)
                    {
                        r2.Add(r[y] + "");
                    }
                    l.Add(r2);
                }
            }
            List<string> ll2 = new List<string>();
            ll2.Add("Name");
            ll2.AddRange(baseSubjetTypes.W.getHeadings());
            l.Add(ll2);
            foreach (subjectTypes item in x)
            {
                if (item.ct.t == "W")
                {
                    List<int> r = subjectTypes.getGrades(item);
                    string name = item.Name.itemValue;
                    List<string> r2 = new List<string>();
                    r2.Add(name);
                    for (int y = 0; y < r.Count; y++)
                    {
                        r2.Add(r[y] + "");
                    }
                    l.Add(r2);
                }
            }
            List<string> ll3 = new List<string>();
            ll3.Add("Name");
            ll3.AddRange(baseSubjetTypes.P.getHeadings());
            l.Add(ll3);
            foreach (subjectTypes item in x)
            {
                if (item.ct.t == "P")
                {
                    List<int> r = subjectTypes.getGrades(item);
                    string name = item.Name.itemValue;
                    List<string> r2 = new List<string>();
                    r2.Add(name);
                    for (int y = 0; y < r.Count; y++)
                    {
                        r2.Add(r[y] + "");
                    }
                    l.Add(r2);
                }
            }
        }
    }
}
