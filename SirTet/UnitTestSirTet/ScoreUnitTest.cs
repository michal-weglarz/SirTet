using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;
using System.Windows.Controls;

namespace UnitTestSirTet
{
    [TestClass]
    public class ScoreUnitTest
    {
        TextBox PlayerNick = new TextBox();
        TextBlock DestroyLinesText = new TextBlock();
        TextBlock RecordText = new TextBlock();
        TextBlock ComboText = new TextBlock();
        TextBlock ScoreText = new TextBlock();

        [TestMethod]
        public void TestIfConstructorCreatesObjectWithInitiliaziedFieldsCorrectly()
        {

            PlayerNick.Text = "PlayerNick";
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            var actualPlayerNick = privateScore.GetField("playerNick");
            var destroyLinesText = privateScore.GetField("destroyLinesText");
            var recordText = privateScore.GetField("recordText");
            var comboText = privateScore.GetField("comboText");
            var scoreText = privateScore.GetField("scoreText");
            Assert.AreEqual("PlayerNick", actualPlayerNick);
            Assert.AreEqual(DestroyLinesText, destroyLinesText);
            Assert.AreEqual(RecordText, recordText);
            Assert.AreEqual(ComboText, comboText);
            Assert.AreEqual(ScoreText, scoreText);
        }

        [TestMethod]
        public void TestMethodSetText()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            TextBlock destroyLinesText = (TextBlock)privateScore.GetField("destroyLinesText");
            TextBlock comboText = (TextBlock)privateScore.GetField("comboText");
            TextBlock scoreText = (TextBlock)privateScore.GetField("scoreText");
            Assert.AreEqual("Destroyed Lines: 0", destroyLinesText.Text);
            Assert.AreEqual("Combo: 0", comboText.Text);
            Assert.AreEqual("Score: 0", scoreText.Text);
        }

        [TestMethod]
        public void TestMethodGetMainScore()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            int mainScore = (int)privateScore.GetField("mainScore");
            Assert.AreEqual(0, mainScore);
        }

        [TestMethod]
        public void TestMethodGetLineCombo()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);

            int lineCombo = (int)privateScore.GetField("lineCombo");
            Assert.AreEqual(0, lineCombo);
        }
        
        [TestMethod]
        public void TestMethodAddMainScore()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            score.AddMainScore(7824);
            PrivateObject privateScore = new PrivateObject(score);
            int mainScore = (int)privateScore.GetField("mainScore");
            Assert.AreEqual(7824, mainScore);
        }

        [TestMethod]
        public void TestMethodAddDestroyLineScore()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            score.AddDestroyLineScore(23123);
            PrivateObject privateScore = new PrivateObject(score);
            int destroyedLines = (int)privateScore.GetField("destroyedLines");
            Assert.AreEqual(23123, destroyedLines);
        }

        [TestMethod]
        public void TestMethodAddLineComboIfTetrisGreaterThanZero()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            int expectedTetris = (40 / 4) * 4000;
            score.AddLineCombo(40);
            int lineCombo = (int)privateScore.GetField("lineCombo");
            int mainScore = (int)privateScore.GetField("mainScore");
            Assert.AreEqual(40, lineCombo);
            Assert.AreEqual(expectedTetris, mainScore);
        }

        [TestMethod]
        public void TestMethodAddLineComboIfTetrisEqualsZero()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            int expectedTetris = 0;
            score.AddLineCombo(0);
            int lineCombo = (int)privateScore.GetField("lineCombo");
            int mainScore = (int)privateScore.GetField("mainScore");
            Assert.AreEqual(0, lineCombo);
            Assert.AreEqual(expectedTetris, mainScore);
        }

        [TestMethod]
        public void TestMethodAddUpLineComboIfLineComboGreaterThanZero()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            privateScore.SetField("lineCombo", 62);
            score.AddUpLineCombo(12);
            int mainScore = (int)privateScore.GetField("mainScore");
            int lineCombo = (int)privateScore.GetField("lineCombo");
            Assert.AreEqual(62 * 12, mainScore);
            Assert.AreEqual(0, lineCombo);
        }


        [TestMethod]
        public void TestMethodAddUpLineComboIfLineComboEqualsZero()
        {
            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);
            privateScore.SetField("lineCombo", 0);
            score.AddUpLineCombo(0);
            int mainScore = (int)privateScore.GetField("mainScore");
            int lineCombo = (int)privateScore.GetField("lineCombo");
            Assert.AreEqual(0, mainScore);
            Assert.AreEqual(0, lineCombo);
        }
    }
}
