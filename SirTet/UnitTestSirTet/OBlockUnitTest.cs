using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;

namespace UnitTestSirTet
{
    [TestClass]
    public class OBlockUnitTest
    {

        Point middlePoint;
        Point[] restPoints;
        Point[] allPoints;

        [TestMethod]
        public void TestIfConstructorIsCalledCorrectly()
        {
            int middleX = 4;
            int middleY = 7;

            Block oBlock = new O_Block(middleX, middleY);

        }
    }
}
