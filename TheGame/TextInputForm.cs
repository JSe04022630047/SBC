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
    public partial class TextInputForm : Form
    {
        public TextInputForm(string txt = "DefaultText")
        {
            InitializeComponent();
            label1.Text = txt;
            buttonOK.DialogResult = DialogResult.OK;
        }

        public string txt()
        {
            return textBox.Text;
        }
    }
}
