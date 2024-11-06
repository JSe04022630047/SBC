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
        public TextInputForm(string txt = "DefaultText", string tag = "Default")
        {
            InitializeComponent();
            Tag = tag;
            label1.Text = txt;
            buttonOK.DialogResult = DialogResult.OK;
            if (tag == "name") textBox.MaxLength = 12;
        }

        public string txt()
        {
            return textBox.Text;
        }

    }
}
