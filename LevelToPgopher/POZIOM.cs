using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelToPgopher
{
    class POZIOM
    {
        public int v;
        public double Energia;
        public double Bv;
        public double Dv;
        public double Hv;
        public bool isGORA = false;
        public POZIOM(int iv, double iEnergia, double iBv, double iDv,double iHv)
        {
            v = iv;
            Energia = iEnergia;
            Bv = iBv;
            Dv = iDv;
            Hv = iHv;
        }

        public override string ToString()
        {
            return "v= " + v.ToString() + "  Energia= " + Energia.ToString() + "  Bv= " + Bv.ToString() + "  Dv= " + Dv.ToString();
        }
    }
}
