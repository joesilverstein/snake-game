using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/**
 * HelpBox.cs
 * displays the help box for the snakegame
 * Brian Belleville
 * 5/22/11
 * */
namespace WindowsFormsApplication1
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            label1.Text = "Control the snake by using the arrow keys on your keyboard.\n"+
                "Points are awarded for eating the fruits.\n"+
                "Game ends if the snake hits the walls or his own body.\n"+
                "The speed of the snake can be adjusted in the Difficulty menu.\n"+
                "You can pause and resume the game using the game menu.";
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
