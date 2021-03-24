using NUnit.Framework;
using AbiCALC;

namespace Tests
{
    public class CalcTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(2,3,2,3)]
        [TestCase(2,4,1,2)]
        [TestCase(1,5,1,5)]
        [TestCase(4,8,1,2)]
        public void fractionTest_shorten(int i1, int i2, int i3, int i4)
        {
            fraction f = new fraction(i1, i2);
            fraction expected = new fraction(i3, i4);
            Assert.AreEqual(expected, f);
        }

        [Test]
        [TestCase(1,2,1,2,1,1)]
        [TestCase(1,4,1,8,3,8)]
        [TestCase(1,4,1,2,3,4)]
        [TestCase(0,4,1,2,1,2)]
        public void fractionTest_add(int f1n, int f1d, int f2n, int f2d, int fexpn, int fexpd) 
        {
            Assert.AreEqual(new fraction(f1n, f1d) + new fraction(f2n, f2d), new fraction(fexpn, fexpd));
        }
    }
}