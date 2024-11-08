﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TheGame
{
    public partial class TitleForm : Form
    {
        public TitleForm()
        {
            InitializeComponent();
            Highscores.LoadScores();
        }

        private void labelTestEnter_Click(object sender, EventArgs e)
        {
            string name = String.Empty;
            TextInputForm nameInput = new TextInputForm("Put your name in the box here", "name");
            nameInput.Text = "Please input your name";
            if (nameInput.ShowDialog() == DialogResult.OK)
            {
                name = nameInput.txt();
                Form game = new GameForm(name);
                this.Hide();
                game.ShowDialog();
                this.Close();
            }
            
        }

        private void labelLeaderBoard_Click(object sender, EventArgs e)
        {
            FormLeaderBoard leaderboard = new FormLeaderBoard(this);
            this.Hide();
            leaderboard.ShowDialog();
        }
    }
}
