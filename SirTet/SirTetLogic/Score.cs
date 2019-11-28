using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SirTetLogic
{
    class Score
    {
        int mainScore = 0;
        int lineCombo = 0;
        int destroyedLines = 0;

        TextBlock scoreText;
        TextBlock comboText;
        TextBlock recordText;
        TextBlock destroyLinesText;

        public Score(ref TextBlock ScoreText, ref TextBlock ComboText, ref TextBlock RecordText, ref TextBlock DestroyLinesText)
        {
            scoreText = ScoreText;
            comboText = ComboText;
            recordText= RecordText;
            destroyLinesText = DestroyLinesText;
            recordText.Text = "Record: ";//Ustawianie rekordu
            SetText();
        }

        void SetText()
        {
            scoreText.Text = "Score: " + mainScore;
            comboText.Text = "Combo: " + lineCombo;
            destroyLinesText.Text = "Destroyed Lines: " + destroyedLines;
        }

        public int GetMainScore()
        {
            return mainScore;
        }

        public void AddMainScore(int value) //Potrzebne dodatkowe zabezpieczenia
        {
            mainScore += value;
        }

        public int GetLineCombo()
        {
            return lineCombo;
        }

        public void AddLineCombo(int value) //Potrzebne dodatkowe zabezpieczenia
        {
            int tetris = value / 4;
            if(tetris > 0)
            {
                mainScore += tetris * 4000; 
            }
            lineCombo += value;
            SetText();
        }

        public void AddUpLineCombo(int comboWorth) 
        {
            if(lineCombo > 1)
            {                
                mainScore += lineCombo * comboWorth;                  
            }
            lineCombo = 0;
            SetText();
        }

        public void AddDestroyLineScore(int value)
        {
            destroyedLines += value;
            SetText();
        }
    }
}
