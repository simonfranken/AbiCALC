using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable()]
    public class data : IName
    {
        private semester[] semesters = new semester[4];
        private List<abiexam> abiexams = new List<abiexam>();
        private observableItem<string> _name = new observableItem<string>();
        private mins min;
        public Dictionary<string, (subjectTypes, subjectTypes, int, int)> overrideDict = new Dictionary<string, (subjectTypes, subjectTypes, int, int)>();

        [OnDeserialized]
        private void deserialized(StreamingContext context)
        {
            _name.func = (string s) => { return $"Hallo, {s}!"; };
            _name.PropertyChanged += (object? sender, PropertyChangedEventArgs e) => { serialization.database.saveCurrent(); };
        }

        public IEnumerable<subjectTypes> getSubjectTypes()
        {
            List<subjectTypes> ret = new List<subjectTypes>();
            foreach (semester item in semesters)
            {
                foreach (subjectTypes item2 in item.dict.Keys)
                {
                    if (!ret.Contains(item2)) ret.Add(item2);
                }
            }
            return ret;
        }

        public observableItem<string> Name 
        {
            get => _name;
        }
        public data(selections.selection _selection)
        {
            deserialized(default);
            Name.itemValue = _selection.name;
            abiexams = _selection.abis;
            min = new mins(_selection);
            for (int i = 0; i < semesters.Length; i++)
            {
                semesters[i] = new semester();
            }
            foreach (subjectTypes item in min.belegt.Keys)
            {
                for (int i = 0; i < min.belegt[item]; i++)
                {
                    semesters[i].dict[item] = subject.constructor(item);
                }
            }
        }
    }
}
