using System;
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
    public partial class LoadModelForm : Form
    {
        Control control;
        ModelInfo modelInfo;
        public LoadModelForm()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            control = new Control(textModelPath.Text);
            control.Show();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                textModelPath.Text = openFileDialog1.FileName;
                loadButton.Enabled = true;
                modelInfo = new ModelInfo(textModelPath.Text);
            }
        }
    }
}
