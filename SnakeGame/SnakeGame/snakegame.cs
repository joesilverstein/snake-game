using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMPLib;
/***
 * snakegame.cs
 * The game Snake
 * User controls the snake using the arrow keys to gat to the fruit
 * Brian Belleville
 * 5/22/11
 * */
namespace WindowsFormsApplication1
{
    public partial class snakegame : Form
    {
        public snakegame()
        {
            InitializeComponent();
        }
        Bitmap myCanvas;//the game will be drawn on myCanvas
        Graphics g;
        Snake s;
        GamePoint fruit;
        int score;
        int scoreMultiplier=10;
        //URL of sound files
        string backgroundsoundlocation = "D:/new music/rainforest_ambience-GlorySunz-1938133500.mp3";
        string turnsoundlocation = "D:/new music/fireballsound.mp3";
        string fruitsoundlocation = "D:/new music/Mushroom_Sound_Effect.mp3";
        bool sounds = true;//user can enable or disable sounds
        WMPLib.WindowsMediaPlayer backgroundplayer;
        WMPLib.WindowsMediaPlayer turnplayer;
        WMPLib.WindowsMediaPlayer fruitplayer;
        //when form loads, load sounds, start new game
        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundplayer = new WindowsMediaPlayer();
            backgroundplayer.URL = @backgroundsoundlocation;
            backgroundplayer.controls.play();
            turnplayer = new WindowsMediaPlayer();
            turnplayer.URL = @turnsoundlocation;
            turnplayer.controls.stop();
            fruitplayer = new WindowsMediaPlayer();
            fruitplayer.URL = @fruitsoundlocation;
            fruitplayer.controls.stop();
            myCanvas = new Bitmap(600, 400);
            g = Graphics.FromImage(myCanvas);
            pictureBox1.Image = myCanvas;
            newGame();
            timer1.Start();
        }

        //starts a new game
        private void newGame()
        {
            score = 0;
            Random r = new Random();
            fruit = new GamePoint(r.Next(60), r.Next(40));
            g.Clear(Color.White);
            g.FillRectangle(new SolidBrush(Color.Black), fruit.toRectangle());
            s = new Snake();
            s.draw(g);
            timer1.Start();
        }

        //moves the snake, checks if it has reached a fruit, or hit the edges or itself
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            g.FillRectangle(new SolidBrush(Color.Black), fruit.toRectangle());
            g.Clear(Color.White);
            label1.Text = "Score: " + score.ToString();
            //check if snake has reached fruit, if it has, increase score, and play sound
            if (s.head().Equals(fruit))
            {
                fruit = new GamePoint(r.Next(60), r.Next(40));
                s.add();
                score += scoreMultiplier;
                if (sounds)
                    fruitplayer.controls.play();
            }
            s.move();
            g.FillRectangle(new SolidBrush(Color.Black), fruit.toRectangle());
            s.draw(g);
            //check if snake has hit edges or itself
            if (!(s.head().x < 60 && s.head().x >= 0 && s.head().y < 40 && s.head().y >= 0) || s.headhit())
            {
                timer1.Stop();
                if (MessageBox.Show("Game Over. Your Score is: " + score.ToString() + " Start a new game?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                    newGame();
                else
                    pauseToolStripMenuItem.Text = "New Game";
            }
            else
                this.Refresh();
            if (sounds)
            {
                if (backgroundplayer.playState == WMPPlayState.wmppsStopped)
                {
                    backgroundplayer.controls.play();
                }
            }
        }

        //when user presses the arrow keys, change snake direction, and play sound
        private void snakegame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                if (s.DirectionOfTravel != 'd')
                {

                    if (sounds && s.DirectionOfTravel != 'u')
                        turnplayer.controls.play();
                    s.DirectionOfTravel = 'u';
                }
            if (e.KeyCode == Keys.Down)
                if (s.DirectionOfTravel != 'u')
                {

                    if (sounds && s.DirectionOfTravel != 'd')
                        turnplayer.controls.play();
                    s.DirectionOfTravel = 'd';
                }
            if (e.KeyCode == Keys.Left)
                if (s.DirectionOfTravel != 'r')
                {

                    if (sounds && s.DirectionOfTravel != 'l')
                        turnplayer.controls.play();
                    s.DirectionOfTravel = 'l';
                }
            if (e.KeyCode == Keys.Right)
                if (s.DirectionOfTravel != 'l')
                {

                    if (sounds && s.DirectionOfTravel != 'r')
                        turnplayer.controls.play();
                    s.DirectionOfTravel = 'r';
                }
        }

        //setings for easy mode
        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 150;
            scoreMultiplier = 5;
            newGame();
        }
        //settings for mediumm mode
        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 70;
            scoreMultiplier = 10;
            newGame();
        }
        //settings for hard mode
        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 45;
            scoreMultiplier = 25;
            newGame();
        }

        //user can pause, resume, or if game has ended start a new game
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pauseToolStripMenuItem.Text == "Pause")
            {
                timer1.Stop();
                pauseToolStripMenuItem.Text = "Resume";
            }
            else if (pauseToolStripMenuItem.Text == "Resume")
            {
                timer1.Start();
                pauseToolStripMenuItem.Text = "Pause";
            }
            else if (pauseToolStripMenuItem.Text == "New Game")
            {
                newGame();
                pauseToolStripMenuItem.Text = "Pause";
            }
        }

        //displays help and pauses the game
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            pauseToolStripMenuItem.Text = "Resume";
            Form HelpScreen = new Help();
            HelpScreen.Show();
        }

        //close form
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //user can enable or disable sounds
        private void disableSoundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (disableSoundsToolStripMenuItem.Text == "Disable Sounds")
            {
                sounds = false;
                disableSoundsToolStripMenuItem.Text = "Enable Sounds";
                backgroundplayer.controls.stop();
            }
            else if (disableSoundsToolStripMenuItem.Text == "Enable Sounds")
            {
                sounds = true;
                disableSoundsToolStripMenuItem.Text = "Disable Sounds";
                backgroundplayer.controls.play();
            }
        }
    }
}
