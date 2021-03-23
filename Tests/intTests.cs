using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Tests
{
    class intTests
    {
        [Test]
        [TestCase(1,2,0)]
        [TestCase(2,3,0)]
        [TestCase(3,3,1)]
        [TestCase(4,3,1)]
        public void div(int n, int d, int expected) 
        {
            Assert.AreEqual(expected, n / d);
        }
    }
}
