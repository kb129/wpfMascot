using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DxLibDLL;

namespace Mascot
{
    public partial class LoadModelForm : Form
    {
        private ModelInfo modelInfo;
        private Viewer viewer;
        private string modelPath;
        private float speed;
        private ButtonSwitchService bss;
        
        public LoadModelForm()
        {
            InitializeComponent();
            this.speed = 0.5f;
            bss = new ButtonSwitchService(startButton, stopButton);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                modelPath = textModelPath.Text = openFileDialog1.FileName;
                loadButton.Enabled = true;
                modelInfo = new ModelInfo(textModelPath.Text);
                
                startButton.Visible = true;
                stopButton.Visible = true;
            }
        }
        private void speedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            speed = ((TrackBar)sender).Value / 10.0f;
            if (viewer != null)
            {
                viewer.SetSpeed(speed);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            bss.ButtonSwitch();
            if (viewer == null)
            {
                viewer = new Viewer(modelPath);
                viewer.SetSpeed(speed);
                viewer.Show();

                Height = 500;

                while (viewer.Created)
                {
                    viewer.MainLoop();
                    Application.DoEvents();
                }
            }
            else
            {
                viewer.SetSpeed(speedTrackBar.Value / 10.0f);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            viewer.SetSpeed(0);
            bss.ButtonSwitch();
        }

        private void Control_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (viewer != null)
            {
                DX.DxLib_End();
                viewer.Close();
                viewer.Dispose();
            }
        }
    }
}
