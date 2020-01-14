﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mascot
{
    public partial class Control : Form
    {
        Form1 form1;
        public Control()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1 = new Form1();
            form1.Show();
            while (form1.Created)
            {
                form1.MainLoop();
                Application.DoEvents();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }
    }
}