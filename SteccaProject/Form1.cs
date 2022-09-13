using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteccaProject
{
    public partial class Form1 : Form
    {
        private Server s;
        private Client c;
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            s = new Server(g);
            //c = new Client();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {/*
            if (c.CreaClient().Result)
            {
                while (true)
                { pictureBox1.Image = c.AcquisisciSchermo().Result; }
            }
            */
        }

        private void startStream_Click(object sender, EventArgs e)
        {
            //s.CondividiSchermo();
        }
    }
}
