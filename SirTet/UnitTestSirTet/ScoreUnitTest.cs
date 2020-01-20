using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SirTetLogic;
using System.Windows.Controls;

namespace UnitTestSirTet
{
    [TestClass]
    public class ScoreUnitTest
    {
        [TestMethod]
        public void TestIfConstructorCreatesObjectWithInitiliaziedFieldsCorrectly()
        {
            TextBox PlayerNick = new TextBox();
            PlayerNick.Text = "PlayerNick";
            TextBlock DestroyLinesText = new TextBlock();
            TextBlock RecordText = new TextBlock();
            TextBlock ComboText = new TextBlock();
            TextBlock ScoreText = new TextBlock();

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
            TextBox PlayerNick = new TextBox();
            PlayerNick.Text = "PlayerNick";
            TextBlock DestroyLinesText = new TextBlock();
            TextBlock RecordText = new TextBlock();
            TextBlock ComboText = new TextBlock();
            TextBlock ScoreText = new TextBlock();

            Score score = new Score(ref ScoreText, ref ComboText, ref RecordText, ref DestroyLinesText, ref PlayerNick);
            PrivateObject privateScore = new PrivateObject(score);

            TextBlock destroyLinesText = (TextBlock)privateScore.GetField("destroyLinesText");
            TextBlock comboText = (TextBlock)privateScore.GetField("comboText");
            TextBlock scoreText = (TextBlock)privateScore.GetField("scoreText");
            Assert.AreEqual("Destroyed Lines: 0", destroyLinesText.Text);
            Assert.AreEqual("Combo: 0", comboText.Text);
            Assert.AreEqual("Score: 0", scoreText.Text);
        }
    }
}
