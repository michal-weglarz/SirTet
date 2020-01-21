using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;

namespace UnitTestSirTet
{
    [TestClass]
    public class SBlockUnitTest
    {
        [TestMethod]
        public void TestIfTwoPrivateFieldsAreSetByConstructorCall()
        {
            int middleX = 2218;
            int middleY = 7777;
            Block sBlock = new S_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(sBlock);

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
            Block sBlock = new S_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(sBlock);
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(2, 5), new Point(3, 4), new Point(4, 4) };
            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsZero()
        {

            Block sBlock = new S_Block(3, 5);
            PrivateObject po = new PrivateObject(sBlock);
            po.SetField("rotationPose", 0);
            po.Invoke("Rotate");

            Point[] actualRestPoints = (Point[])po.GetField("restPoints");

            Point[] expectedRestPoints = new Point[3] { new Point(3, 4), new Point(4, 5), new Point(4, 6) };

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }


        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsOne()
        {
            Block sBlock = new S_Block(3, 5);
            PrivateObject po = new PrivateObject(sBlock);
            po.SetField("rotationPose", 1);
            po.Invoke("Rotate");

            Point[] actualRestPoints = (Point[])po.GetField("restPoints");

            Point[] expectedRestPoints = new Point[3] { new Point(3, 6), new Point(2, 5), new Point(2, 4) };

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsTwo()
        {
            Block sBlock = new S_Block(3, 5);
            PrivateObject po = new PrivateObject(sBlock);
            po.SetField("rotationPose", 2);
            po.Invoke("Rotate");
            Point[] expectedRestPoints = new Point[3] { new Point(1, 6), new Point(2, 3), new Point(4, 2) };

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
            Block sBlock = new S_Block(3, 5);
            PrivateObject po = new PrivateObject(sBlock);
            po.SetField("rotationPose", 3);
            po.Invoke("Rotate");
            Point[] expectedRestPoints = new Point[3] { new Point(1, 4), new Point(4, 3), new Point(6, 4) };
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
            Block sBlock = new S_Block(30, 50);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(10, 23);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(0, 581);
            PrivateObject po = new PrivateObject(sBlock);
            po.SetField("rotationPose", 1);
            int gridX = 121;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsOneReturnsFalse()
        {
            Block sBlock = new S_Block(10, 23);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(0, 50);
            PrivateObject po = new PrivateObject(sBlock);
            po.SetField("rotationPose", 2);
            int gridX = 746;
            object[] args = new object[] { gridX };
            bool actualResult = (bool)po.Invoke("IfBlockOutOfGridOnRotate", args);
            bool expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestWhetherMethodIfBlockOutOfGridOnRotateWhenRotationPoseEqualsTwoReturnsFalse()
        {
            Block sBlock = new S_Block(10, 23);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(30, 50);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(10, 23);
            PrivateObject po = new PrivateObject(sBlock);
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
            Block sBlock = new S_Block(2, 1);
            string expectedBlockType = "S_Block";
            string actualResult = sBlock.GetBlockType();
            Assert.AreEqual(expectedBlockType, actualResult);
        }
    }


}
