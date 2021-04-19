using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AbiCALC;

namespace Tests
{
    public class roundingTests
    {
        [Test]
        [TestCase(new int[] { 11, 12 }, 12)]
        [TestCase(new int[] { 11, 12, 11 }, 11)]
        public void roundingTest1(int[] i, int expected) 
        {
            normalSubject s = new normalSubject();
            foreach(int x in i) 
            {
                s.add(new exam(false, x));
            }
            int? temp = s.getAverageGrade();
            int temp2 = -1;
            if (temp != null) temp2 = (int)temp;
            else Assert.Fail("is null");
            Assert.AreEqual(expected,temp2);
        }

        [Test]
        [TestCase(new int[] { 11, 12 }, 14, 13)]
        [TestCase(new int[] { 11, 12, 11 }, 10, 11)]
        public void roundingTest2(int[] i, int j, int expected)
        {
            normalSubject s = new normalSubject();
            foreach (int x in i)
            {
                s.add(new exam(false, x));
            }
            s.add(new exam(true, j));
            int? temp = s.getAverageGrade();
            int temp2 = -1;
            if (temp != null) temp2 = (int)temp;
            else Assert.Fail("is null");
            Assert.AreEqual(expected, temp2);
        }
    }
}
