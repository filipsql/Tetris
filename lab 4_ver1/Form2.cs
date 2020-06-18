using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Podaci;

namespace lab_4_ver1
{
    public partial class Form2 : Form
    {
        private Form1 q;
        public Form2(Form1 p)
        {
            InitializeComponent();
            q = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (proveri())
            {
                q.Start(true);
                Refresh();
                q.button2.Enabled = false;
                this.Close();
            }
            else
                MessageBox.Show("Morate uneti ime i prezime!");
        }
        bool proveri()
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                return false;
            q._listaosoba.DodajOsobu(new Osoba(textBox1.Text, textBox2.Text));
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
            q.button2.Enabled = true;
            //textBox2.Text = "";
            // textBox1.Text = "";
            // q.button2.Enabled = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";

        }
    }
}
