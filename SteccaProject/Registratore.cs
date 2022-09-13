using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SteccaProject
{
    class Registratore
    {
        public static Bitmap DammiSchermata(Graphics g)
        {
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);
            g = Graphics.FromImage(bm);
            g.CopyFromScreen(0,0,0,0,bm.Size);
            return bm;
        }
    }
}
