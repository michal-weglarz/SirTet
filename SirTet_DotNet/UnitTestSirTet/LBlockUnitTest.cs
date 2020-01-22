using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;

namespace UnitTestSirTet
{
    [TestClass]
    public class LBlockUnitTest
    {
        [TestMethod]
        public void TestIfTwoPrivateFieldsAreSetByConstructorCall()
        {
            int middleX = 2654;
            int middleY = 1;
            Block lBlock = new L_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(lBlock);

            Point actualMiddlePoint = (Point)po.GetField("middlePoint");
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");

            int expectedMiddlePointLength = 2;
            int expectedRestPointsLength = 3;

            Assert.AreEqual(expectedMiddlePointLength, actualMiddlePoint.Get().Length);
            Assert.AreEqual(expectedRestPointsLength, actualRestPoints.Length);
        }

        [TestMethod]
        public void TestIfMiddlePointIsSetCorrectly()
        {
            int middleX = 4;
            int middleY = 7;
            Block lBlock = new L_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(lBlock);
            Point actualMiddlePoint = (Point)po.GetField("middlePoint");
            Point expectedMiddlePoint = new Point(middleX, middleY);
            Assert.AreEqual(expectedMiddlePoint.Get()[0], actualMiddlePoint.Get()[0]);
            Assert.AreEqual(expectedMiddlePoint.Get()[1], actualMiddlePoint.Get()[1]);
        }

        [TestMethod]
        public void TestIfRestPointsAreSetCorrectly()
        {
            int middleX = 30;
            int middleY = 50;
            Block lBlock = new L_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(lBlock);
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(29, 50), new Point(31, 50), new Point(31, 49) };
            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsZero()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 0);
            po.Invoke("Rotate");
            
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(30, 49), new Point(30, 51), new Point(31, 51) };

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }


        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsOne()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 1);
            po.Invoke("Rotate");

            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(30, 51), new Point(30, 49), new Point(29, 49) };

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsTwo()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 2);
            po.Invoke("Rotate");

            Point[] expectedRestPoints = new Point[3] { new Point(28, 51), new Point(32, 49), new Point(31, 47) };
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsThree()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 3);
            po.Invoke("Rotate");
            Point[] expectedRestPoints = new Point[3] { new Point(28, 49), new Point(32, 51), new Point(33, 49) };
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsZeroReturnsTrue()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 0);
            int gridX = 10;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsZeroReturnsFalse()
        {
            Block lBlock = new L_Block(10, 23);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 0);
            int gridX = 56;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = false;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsOneReturnsTrue()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 1);
            int gridX = 10;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsOneReturnsFalse()
        {
            Block lBlock = new L_Block(10, 23);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 1);
            int gridX = 56;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = false;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsTwoReturnsTrue()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 2);
            int gridX = 10;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsTwoReturnsFalse()
        {
            Block lBlock = new L_Block(10, 23);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 2);
            int gridX = 56;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = false;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsThreeReturnsTrue()
        {
            Block lBlock = new L_Block(30, 50);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 3);
            int gridX = 10;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsThreeReturnsFalse()
        {
            Block lBlock = new L_Block(10, 23);
            PrivateObject po = new PrivateObject(lBlock);
            po.SetField("rotationPose", 3);
            int gridX = 56;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = false;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestIfMethodGetBlockTypeReturnGivenString()
        {
            Block lBlock = new L_Block(2, 1);
            string expectedBlockType = "L_Block";
            string actualResult = lBlock.GetBlockType();
            Assert.AreEqual(expectedBlockType, actualResult);
        }

    }
}
