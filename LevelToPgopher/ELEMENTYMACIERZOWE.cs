using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelToPgopher
{
    class ELEMENTYMACIERZOWE
    {
        public int vdolne;
        public int vgorne;
        public double Energia;
        public double FC;
        public double AEinsteina;
        public double braket; //ostatnia liczba
        public string TYP;
        public double ro = -1;
        public double deltaIzo = double.PositiveInfinity;

        public ELEMENTYMACIERZOWE(int ivd, int ivg, double ienergia, double iFC, double iAEinsteina, double iBraket, string iTYP, double iro=-1,double iDeltaIzo=double.PositiveInfinity)
        {
           // MessageBox.Show(ivd.ToString() + "  " + ivg.ToString() + "  " + ienergia.ToString() + "  " + iFC.ToString() + "   " + iAEinsteina.ToString() + "   " + iBraket.ToString() + "  " + iTYP.ToString());
            vdolne = ivd;
            vgorne = ivg;
            Energia = ienergia;
            FC = iFC;
            AEinsteina = iAEinsteina;
            TYP = iTYP;
            braket = iBraket;
            ro = iro;
            deltaIzo = iDeltaIzo;
        }

        public override string ToString()
        {
            return TYP + "  " + vgorne.ToString() + " <- " + vdolne.ToString() + "  " + Energia.ToString() + " " + FC.ToString();
        }
    }
}
