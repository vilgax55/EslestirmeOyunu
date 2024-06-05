using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Label pic_1 = null;
        Label pic_2 = null;

        int secondsElapsed = 0;

        Random random = new Random();
        List<string> icons;

  
        public Form1() 
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            icons = new List<string>()
            {
                "r","r","Y","Y",
                "u","u","d","d",
                "h","h","x","x",
                "j","j","m","m",
            };

            iconYerlestir();
            secondsElapsed = 0;
            LabelKronometre.Text = "Geçen Süre: 0 saniye";

            timer1.Stop();
            timer2.Start();
        }

        private void iconYerlestir()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomIndex = random.Next(icons.Count);
                    iconLabel.Text = icons[randomIndex];
                    iconLabel.ForeColor = iconLabel.BackColor; // İkonu gizle
                    icons.RemoveAt(randomIndex);
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null && clickedLabel.ForeColor == clickedLabel.BackColor)
            {
                if (pic_1 == null)
                {
                    pic_1 = clickedLabel;
                    pic_1.ForeColor = Color.Black;
                    return;
                }

                pic_2 = clickedLabel;
                pic_2.ForeColor = Color.Black;

                if (pic_1.Text == pic_2.Text)
                {
                    pic_1 = null;
                    pic_2 = null;
                    CheckForCompletion();
                    return;
                }

                timer1.Start();
            }
        }

        private void CheckForCompletion()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                {
                    return;
                }
            }

            timer2.Stop();
            MessageBox.Show("Oyun Sona Erdi. Toplam Geçen Süre: " + secondsElapsed + " saniye");
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            pic_1.ForeColor = pic_1.BackColor;
            pic_2.ForeColor = pic_2.BackColor;
            pic_1 = null;
            pic_2 = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            LabelKronometre.Text = "Süre: " + secondsElapsed + " saniye";
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
