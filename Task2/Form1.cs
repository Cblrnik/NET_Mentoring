using System;
using System.Windows.Forms;
using HelloLibrary;
using System.Runtime;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("You should enter your name!");
            }
            else
            {
                MessageBox.Show(Helper.CreateHelloUserString(name));
            }
        }
    }
}
