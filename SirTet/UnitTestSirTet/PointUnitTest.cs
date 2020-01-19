using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;

namespace UnitTestSirTet
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIfFieldsAreInitializedCorrectlyWithConstructorCall()
        {
            var point = new Point(2, 3);
            var expectedX = 2;
            var expectedY = 3;
            var actualX = point.Get()[0];
            var actualY = point.Get()[1];
            Assert.AreEqual(expectedX, actualX);
            Assert.AreEqual(expectedY, actualY);
        }

        [TestMethod]
        public void TestIfFieldsAreInitializedCorrectlyWithSetMethod()
        {
            var point = new Point(8, 3);
            point.Set(1, 10);
            var expectedX = 1;
            var expectedY = 10;
            var actualX = point.Get()[0];
            var actualY = point.Get()[1];
            Assert.AreEqual(expectedX, actualX);
            Assert.AreEqual(expectedY, actualY);
        }

    }
}
