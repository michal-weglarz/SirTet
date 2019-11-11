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

        TextBlock scoreText;
        TextBlock comboText;
        TextBlock recordText;

        public Score(ref TextBlock ScoreText, ref TextBlock ComboText, ref TextBlock RecordText)
        {
            scoreText = ScoreText;
            comboText = ComboText;
            recordText= RecordText;
            recordText.Text = "Record: ";//Ustawianie rekordu
            SetText();
        }

        void SetText()
        {
            scoreText.Text = "Score: " + mainScore;
            comboText.Text = "Combo: " + lineCombo;            
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
            lineCombo += value;
            SetText();
        }

        public void AddUpLineCombo(int comboWorth) 
        {
            if(lineCombo > 1)
            {
                int tetris = lineCombo / 4;
                if(tetris > 0)
                {
                    mainScore += tetris * comboWorth * 4;
                    lineCombo -= tetris * 4;
                }
                mainScore += lineCombo * comboWorth;                  
            }
            lineCombo = 0;
            SetText();
        }
    }
}
