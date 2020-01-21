using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace SirTetLogic
{
    public class Score
    {
        int mainScore = 0;
        int lineCombo = 0;
        int destroyedLines = 0;

        int[] recordsPkt = new int[10];
        string[] recordsNames = new string[10];

        string playerNick;

        static string key = "012345678901234567890123";
       
        TextBlock scoreText;
        TextBlock comboText;
        TextBlock recordText;
        TextBlock destroyLinesText;

        public Score(ref TextBlock ScoreText, ref TextBlock ComboText, ref TextBlock RecordText, ref TextBlock DestroyLinesText, ref TextBox PlayerNick)
        {
            scoreText = ScoreText;
            comboText = ComboText;
            recordText= RecordText;
            destroyLinesText = DestroyLinesText;
            playerNick = PlayerNick.Text;
            SetText();
        }

        void SetText()
        {
            scoreText.Text = "Score: " + mainScore;
            comboText.Text = "Combo: " + lineCombo;
            destroyLinesText.Text = "Destroyed Lines: " + destroyedLines;
            DrawRecords();
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

        public static void DrawRecords(ref TextBlock RecordText)
        { 
            string[] records = new string[0];
            string recordString = "";
            try
            {
                records = SaveScore.ReadFile(@"C:\Users\Public\SirTet\Records.aes");

                for(int i = 0;i < 10;i++)
                {
                    records[i] = SaveScore.DecryptString(records[i], key);
                    recordString += records[i];
                }

                RecordText.Text = "Record: \n" + recordString;
            }
            catch
            {
                string[] defaultRecords = new string[10];
                for(int i = 1;i <= 10;i++)
                {
                    defaultRecords[i - 1] = (i + ". 1000 pkt Player \n");
                }

                for(int i = 0;i < 10;i++)
                {
                    defaultRecords[i] = SaveScore.EncryptString(defaultRecords[i], key);
                }

                SaveScore.SaveFile(defaultRecords, @"C:\Users\Public\SirTet", "Records.aes");
                DrawRecords(ref RecordText);
            }

        }


        void DrawRecords()
        {
            //recordText
            string[] records = new string[0];
            string recordString = "";
            try
            {
                records = SaveScore.ReadFile(@"C:\Users\Public\SirTet\Records.aes");

                for(int i = 0;i < 10;i++)
                {
                    records[i] = SaveScore.DecryptString(records[i], key);
                    recordString += records[i];
                    recordsPkt[i] = Int32.Parse(records[i].Split(' ')[1]);
                    recordsNames[i] = records[i].Split(' ')[3];
                }

                recordText.Text = "Record: \n" + recordString;
            }
            catch
            {
                string[] defaultRecords = new string[10];
                for(int i = 1; i<=10; i++)
                {
                    defaultRecords[i-1] = (i +". 1000 pkt Player \n");
                }

                for(int i = 0; i < 10; i++)
                {
                    defaultRecords[i] = SaveScore.EncryptString(defaultRecords[i], key);
                }                

                SaveScore.SaveFile(defaultRecords, @"C:\Users\Public\SirTet", "Records.aes");
                DrawRecords();
            }
            
        }

        public void SetRecords()
        {
            int tmpScore = mainScore;
            string tmpName = playerNick;

            for(int i = 0;i < 10;i++)
            {
                if(tmpScore > recordsPkt[i])
                {
                    int tmpScores = recordsPkt[i];
                    recordsPkt[i] = tmpScore;
                    tmpScore = tmpScores;

                    string tmpNames = recordsNames[i];
                    recordsNames[i] = tmpName;
                    tmpName = tmpNames;
                }
            }

            string[] stringRecords = new string[10];
            for(int i = 1;i <= 10;i++)
            {
                stringRecords[i - 1] = (i + ". " + recordsPkt[i - 1] + " pkt " + recordsNames[i - 1] + " \n");
            }

            for(int i = 0;i < 10;i++)
            {
                stringRecords[i] = SaveScore.EncryptString(stringRecords[i], key);
            }

            SaveScore.SaveFile(stringRecords, @"C:\Users\Public\SirTet", "Records.aes");
        }
    }
}
