using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelToPgopher
{
    class PRZEJSCIE
    {
        public string name;
        public int vdol;
        public int vgora;
        public double energiaprzejscia;        

        public double Bvdol;
        public double Bvgora;
        public double Dvdol;
        public double Dvgora;
        public double Hvdol;
        public double Hvgora;

        public double EnergiaDol;
        public double EnergiaGora;

        public double FC;
        public double wspA;
        public double braket;
        public double Abundancja;
        public double WAGABOLTZMANOWSKA=1.0;
        public bool show = true;
        public bool powyzejProguNatezenia = true;
        public bool WarunkiSekwencji = true;
        public bool WarunkiSelekcjiVd = true;
        public int LM1;
        public int LM2;
        public double spinJadra1;
        public double spinJadra2;
        public double ro = 0;
        public double miMass = 0;
        public double deltaIzo = 0;

        public override string ToString()
        {
            return "Przejscie: " + vgora.ToString() + " <- " + vdol.ToString() + " energia: " + (EnergiaGora - EnergiaDol).ToString() + " (edol= "+EnergiaDol.ToString()+" egora= "+EnergiaGora.ToString()+" ) Bv'': " + Bvdol.ToString() + " Dv'': " + Dvdol.ToString() + " Bv': " + Bvgora.ToString() + " Dv' " + Dvgora.ToString() + "  FC: " + FC.ToString() +" braket: "+braket.ToString()+" b^2: "+(braket*braket).ToString()+" izo.:  "+LM1.ToString()+"-"+LM2.ToString()+ "  abund: " + Abundancja.ToString()+ "  wagaBolt: "+WAGABOLTZMANOWSKA.ToString()+" stos: "+((braket*braket)/FC)+ " ro: "+ro.ToString()+" deltaIzo: "+deltaIzo.ToString()+" miMass: "+miMass.ToString();
        }
    }
}
