using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;

namespace UnitTestSirTet
{
    [TestClass]
    public class JBlockUnitTest
    {
        [TestMethod]
        public void TestIfTwoPrivateFieldsAreSetByConstructorCall()
        {
            int middleX = 22254;
            int middleY = 11212;
            Block jBlock = new J_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(jBlock);

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
            Block jBlock = new J_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(middleX, middleY);
            PrivateObject po = new PrivateObject(jBlock);
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(2, 4), new Point(2, 5), new Point(4, 5) };
            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsZero()
        {

            Block jBlock = new J_Block(3, 5);
            PrivateObject po = new PrivateObject(jBlock);
            po.SetField("rotationPose", 0);
            po.Invoke("Rotate");
        
            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
            Point[] expectedRestPoints = new Point[3] { new Point(4, 4), new Point(3, 4), new Point(3, 6) };

            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }


        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsOne()
        {
            Block jBlock = new J_Block(3, 5);
            PrivateObject po = new PrivateObject(jBlock);
            po.SetField("rotationPose", 1);
            po.Invoke("Rotate");

            Point[] actualRestPoints = (Point[])po.GetField("restPoints");
      
            Point[] expectedRestPoints = new Point[3] { new Point(2, 6), new Point(3, 6), new Point(3, 4) };
         
            for (int i = 0; i < actualRestPoints.Length; i++)
            {
                Assert.AreEqual(expectedRestPoints[i].Get()[0], actualRestPoints[i].Get()[0]);
                Assert.AreEqual(expectedRestPoints[i].Get()[1], actualRestPoints[i].Get()[1]);
            }
        }

        [TestMethod]
        public void TestRotateMethodWhenRotationPoseEqualsTwo()
        {
            Block jBlock = new J_Block(3, 5);
            PrivateObject po = new PrivateObject(jBlock);
            po.SetField("rotationPose", 2);
            po.Invoke("Rotate");
            Point[] expectedRestPoints = new Point[3] { new Point(0, 4), new Point(1, 6), new Point(5, 4) };

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
            Block jBlock = new J_Block(3, 5);
            PrivateObject po = new PrivateObject(jBlock);
            po.SetField("rotationPose", 3);
            po.Invoke("Rotate");

            Point[] expectedRestPoints = new Point[3] { new Point(2, 2), new Point(1, 4), new Point(5, 6) };

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
            Block jBlock = new J_Block(30, 50);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(10, 23);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(0, 581);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(10, 23);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(30, 50);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(10, 23);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(30, 50);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(10, 23);
            PrivateObject po = new PrivateObject(jBlock);
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
            Block jBlock = new J_Block(2, 1);
            string expectedBlockType = "J_Block";
            string actualResult = jBlock.GetBlockType();
            Assert.AreEqual(expectedBlockType, actualResult);
        }
    }


}
