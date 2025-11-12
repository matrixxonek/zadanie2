using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelToPgopher
{
    class manualIzo
    {
        int m1;
        double multiplier;
        int m2;
        int kombin = 0;



        #region setget
        public int M1
        {
            get { return m1; }
            set { m1 = value; }
        }


        public int M2
        {
            get { return m2; }
            set { m2 = value; }
        }


        public double Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        public int Kombin
        {
            get { return kombin; }
        }

        #endregion

        public manualIzo(int im1, int im2)
        {
            if (im2 > im1)
            {
                m1 = im2;
                m2 = im1;
            }
            else
            {
                m1 = im1;
                m2 = im2;
            }
            kombin = m1 + m2;
        }
    }
}
