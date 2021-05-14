using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public abstract class subject
    {
        public subjectTypes type;
        public int? getAverageGrade() 
        {
            return isOverride ? overridePoints : getAverageGradeFromExams();
        }

        protected abstract int? getAverageGradeFromExams();

        public abstract bool isValid();

        public bool isOK() 
        {
            return isValid() && examsValid();
        }

        public bool examsValid() => getAverageGradeFromExams() != null;


        private bool isOverride = false;
        public int overridePoints 
        {
            get => _overridePoints;
            set 
            {
                _overridePoints = value;
                isOverride = true;
            }
        }
        private int _overridePoints;
    }
}
