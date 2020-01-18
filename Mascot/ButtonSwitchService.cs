using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mascot
{
    class ButtonSwitchService
    {
        private Button button1;
        private Button button2;

        public ButtonSwitchService(Button button1, Button button2)
        {
            this.button1 = button1;
            this.button2 = button2;
        }

        public void ButtonSwitch()
        {
            if (button1.Enabled)
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else if (button2.Enabled)
            {
                button2.Enabled = false;
                button1.Enabled = true;
            }
        }
    }
}
