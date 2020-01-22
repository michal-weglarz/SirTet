using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;

namespace UnitTestSirTet
{
    [TestClass]
    public class OBlockUnitTest
    {

        [TestMethod]
        public void TestIfAllThreePrivateFieldsAreSetByConstructorCall()
        {
            int middleX = 23;
            int middleY = 17;
            Block oBlock = new O_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(oBlock);

            Point actualMiddlePoint = (Point)po.GetField("middlePoint");
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] actualAllPoints = (Point[])po.GetField("allPoints");

            int expectedMiddlePointLength = 2;
            int expectedRestPointsLength = 3;
            int expectedActualAllPointsLength = 4;

            Assert.AreEqual(expectedMiddlePointLength, actualMiddlePoint.Get().Length);
            Assert.AreEqual(expectedRestPointsLength, actualRestPoints.Length);
            Assert.AreEqual(expectedActualAllPointsLength, actualAllPoints.Length);
        }

        [TestMethod]
        public void TestIfMiddlePointIsSetCorrectly()
        {
            int middleX = 4;
            int middleY = 7;
            Block oBlock = new O_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(oBlock);
            Point actualMiddlePoint = (Point)po.GetField("middlePoint");
            Point expectedMiddlePoint = new Point(middleX, middleY);
            Assert.AreEqual(expectedMiddlePoint.Get()[0], actualMiddlePoint.Get()[0]);
            Assert.AreEqual(expectedMiddlePoint.Get()[1], actualMiddlePoint.Get()[1]);
        }

        [TestMethod]
        public void TestIfRestPointsAreSetCorrectly()
        {
            int middleX = 3;
            int middleY = 5;
            Block oBlock = new O_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(oBlock);
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(3, 4), new Point(4, 4), new Point(4, 5) };
            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestIfAllPointsAreSetCorrectly()
        {
            int middleX = 23;
            int middleY = 3;
            Block oBlock = new O_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(oBlock);
            Point[] actualAllPoints = (Point[])po.GetField("allPoints");
            Point expectedMiddlePoint = new Point(middleX, middleY);
            Point[] expectedAllPoints = new Point[4] { expectedMiddlePoint, new Point(23, 2), new Point(24, 2), new Point(24, 3) };
            for (int i = 0; i < actualAllPoints.Length; i++)
            {
                Assert.AreEqual(expectedAllPoints[i].Get()[0], actualAllPoints[i].Get()[0]);
                Assert.AreEqual(expectedAllPoints[i].Get()[1], actualAllPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestIfMethodIfBlockOutOfGridOnRotateReturnsFalse()
        {
            int gridX = 5;
            Block oBlock = new O_Block(5, 6);
            bool actualResult = oBlock.IfBlockOutOfGridOnRotate(gridX);
            bool expectedResult = false;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestIfMethodIfBlockOverrideOnRotateReturnsFalse()
        {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
            bool[,] HardLayer = { { false, false, true }, { false, true, true } };
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
            Block oBlock = new O_Block(85, 6576);
            bool actualResult = oBlock.IfBlockOverrideOnRotate(HardLayer);
            bool expectedResult = false;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestIfMethodGetBlockTypeReturnGivenString()
        {
            Block oBlock = new O_Block(2, 1);
            string expectedBlockType = "O_Block";
            string actualResult = oBlock.GetBlockType();
            Assert.AreEqual(expectedBlockType, actualResult);
        }
    }
}
