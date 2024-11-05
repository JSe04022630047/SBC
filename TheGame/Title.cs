using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public partial class Title : Form
    {
        public Title()
        {
            InitializeComponent();
        }

        private void labelTestEnter_Click(object sender, EventArgs e)
        {
            string name = String.Empty;
            TextInputForm nameInput = new TextInputForm("Put your name in the box here");
            nameInput.Tag = "name";
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
    }
}
