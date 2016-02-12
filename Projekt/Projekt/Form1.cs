using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

            label1.Text = "50 gold";
            label2.Text = "300 gold";
            label3.Text = "500 gold";
            button1.Text = "Buy worker";
            button2.Text = "Upgrade Farm";
            button3.Text = "Expand Inn";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GameWorld.totalGold < 300)
            {
               
            }

            else
            {
                Worker.farmUpgraded = true;
                GameWorld.totalGold -= 300;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gw.CreateWorker();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (GameWorld.totalGold < 500)
            {
                
            }
            else
            {
                Worker.innUpgraded = true;
                GameWorld.totalGold -= 500;
            }
        }
    }
}
