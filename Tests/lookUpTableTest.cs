using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbiCALC;
using NUnit.Framework;

namespace Tests
{
    class lookUpTableTest
    {
        [Test]
        [TestCase(900, 10)]
        [TestCase(823, 10)]
        [TestCase(822, 11)]
        [TestCase(810, 11)]
        [TestCase(804, 12)]
        [TestCase(804, 12)]
        [TestCase(785, 13)]
        [TestCase(300, 40)]
        [TestCase(299, -1)]
        [TestCase(301, 39)]

        

        public void mainTests(int p, int note) 
        {
            Assert.AreEqual(note, lookUpTable.getAbi(p));
        }
    }
}
