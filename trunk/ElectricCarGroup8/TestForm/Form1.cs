using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectricCarLib;

namespace TestForm
{
    public partial class Form1 : Form
    {
        private ConnectionCtr cCtr = new ConnectionCtr();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cCtr.addNewRecord(
                Convert.ToInt32(textBox1.Text), 
                Convert.ToInt32(textBox2.Text), 
                Convert.ToDecimal(textBox3.Text), 
                Convert.ToDecimal(textBox4.Text));
            richTextBox1.Lines = cCtr.getAllInfo().ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MConnection c = cCtr.getRecord(
                Convert.ToInt32(textBox1.Text),
                Convert.ToInt32(textBox2.Text),
                false);
            richTextBox1.Text = c.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cCtr.updateRecord(
                Convert.ToInt32(textBox1.Text), 
                Convert.ToInt32(textBox2.Text), 
                Convert.ToDecimal(textBox3.Text), 
                Convert.ToDecimal(textBox4.Text));
            richTextBox1.Lines = cCtr.getAllInfo().ToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cCtr.deleteRecord(
                Convert.ToInt32(textBox1.Text),
                Convert.ToInt32(textBox2.Text));
            richTextBox1.Lines = cCtr.getAllInfo().ToArray();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Lines = cCtr.getAllInfo().ToArray();
        }
    }
}
