using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    [Serializable]
    public abstract class subject
    {
        public static subject constructor(subjectTypes t) 
        {
            switch(t.t) 
            {
                case subjectTypes.type.WSem:
                    return new seminarSubject(true);
                case subjectTypes.type.PSem:
                    return new seminarSubject(false);
                default:
                    return new normalSubject(t);

            }
        }
        
        public subjectTypes type;

        public subject(subjectTypes s) 
        {
            type = s;
        }

        [NonSerialized]
        private bool isOverride = false;
        [NonSerialized]
        private int _overridePoints;

        public bool examsValid() => getAverageGradeFromExams() != null;
        public int? getAverageGrade()
        {
            return isOverride ? overridePoints : getAverageGradeFromExams();
        }

        protected abstract int? getAverageGradeFromExams();

        public int overridePoints
        {
            get => _overridePoints;
            set
            {
                _overridePoints = value;
                isOverride = true;
            }
        }


        public void noOverride()
        {
            isOverride = false;
        }



        public static int getPredictionFactor(subject type)
        {
            if (typeof(normalSubject).IsInstanceOfType(type)) return 1;
            else if (typeof(seminarSubject).IsInstanceOfType(type)) return 4;
            else if (typeof(abiexam).IsInstanceOfType(type)) return 4;
            else return 1;
        }
    }
}
