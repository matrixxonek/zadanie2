using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LevelToPgopher
{
    partial class GlowneOkno
    {
        private void tbJ2DL_MouseHover(object sender, EventArgs e)
        {
            string H1 = @"For matrix element calculations, couple each level of Potential-1, generated as specifed by Read s #17 &18, to all rotation levels of the NLEV2 vibrational levels v =IV2(i) allowed by the rotational selection rules DeltaJ =J2DL to J2DU in steps of J2DD (e.g., for P & R transitions: J2DL=-1 , J2DU=+1 & J2DD=+2 ).";
            Help.ShowPopup(tbJ2DL, H1, new Point(Cursor.Position.X, Cursor.Position.Y + 15));
            
        }
        private void tbJ2DU_MouseHover(object sender, EventArgs e)
        {
            string H1 = @"For matrix element calculations, couple each level of Potential-1, generated as specifed by Read s #17 &18, to all rotation levels of the NLEV2 vibrational levels v =IV2(i) allowed by the rotational selection rules DeltaJ =J2DL to J2DU in steps of J2DD (e.g., for P & R transitions: J2DL=-1 , J2DU=+1 & J2DD=+2 ).";
            Help.ShowPopup(tbJ2DU, H1, new Point(Cursor.Position.X, Cursor.Position.Y + 15));
        }

        private void tbJ2DD_MouseHover(object sender, EventArgs e)
        {
            string H1 = @"For matrix element calculations, couple each level of Potential-1, generated as specifed by Read s #17 &18, to all rotation levels of the NLEV2 vibrational levels v =IV2(i) allowed by the rotational selection rules DeltaJ =J2DL to J2DU in steps of J2DD (e.g., for P & R transitions: J2DL=-1 , J2DU=+1 & J2DD=+2 ).";
            Help.ShowPopup(tbJ2DD, H1, new Point(Cursor.Position.X, Cursor.Position.Y + 15));
    
        }

    }
}
