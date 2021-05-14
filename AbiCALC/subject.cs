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
        public subjectTypes type;


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
            throw new NotImplementedException();
        }
    }
}
