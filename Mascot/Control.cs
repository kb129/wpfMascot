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
    public partial class Control : Form
    {
        private Viewer viewer;
        private string modelPath;
        private float speed;
        public Control(string modelPath)
        {
            this.speed = 0.5f;
            this.modelPath = modelPath;
            InitializeComponent();
        }

        private void speedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            speed = ((TrackBar)sender).Value / 10.0f;
            if (viewer.Created)
            {
                viewer.SetSpeed(speed);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (viewer == null)
            {
                viewer = new Viewer(modelPath);
                viewer.SetSpeed(speed);
                viewer.Show();

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
        }
    }
}
