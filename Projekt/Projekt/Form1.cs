using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt
{
    public partial class Form1 : Form
    {
        private GameWorld gw;
        private Graphics dc;
        public Form1()
        
        {
            InitializeComponent();
        }
        //This creates the GameWorld and secures that the background color is white.
        private void Form1_Load(object sender, EventArgs e)
        {
            if (dc == null)
            {
                dc = CreateGraphics();
            }
            gw = new GameWorld(dc, DisplayRectangle);
            BackColor = Color.White;
        }
    }
}
