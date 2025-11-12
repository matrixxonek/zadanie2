using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelToPgopher
{
    class PgopherGenerator
    {
        static int kolorit=0;
        public static bool OODR = false;
        public bool dipol = false;
        public bool uwzglH=false;
        public string Sname="";       
        public int Jmax=0;
        //public daneMolekularne daneMol;
        
        public string LAMBDAD;
        public string LAMBDAG;
        public string symD = "none";
        public string symG = "none";
        public bool symEnable = false;
        public int SD;
        public int SG;
        public string omegaSelectDol;
        public string omegaSelectGora;
        public string typprzejscia;
        public string[] kolory = { "Red", "Green", "Blue", "Olive", "Lime","Orange","Aqua", "Fuchsia","Teal","Gray","Brown","Navy" };
        public string SpinOrbitaD = "0";
        public string SpinOrbitaG = "0";

        public string daneeksperymentalne="";
        public int znakDv = -1;

        public string generujwpis()
        {
            string napis = "<Species Name=\""+Sname +"\" Colour=\"Red\" Jmax=\""+(2*Jmax).ToString()+"\">\r\n";
            return napis;
        }

        public string generujPoczątek()
        {
            string napis = "<?xml version=\"1.0\"?>\r\n<Mixture Version=\"Pgopher 6.0.111 10 Jul 2009 abc\">\r\n";
            return napis;
        }

        public string startSpacies(int Jmax,string name)
        {
            //MessageBox.Show(kolorit.ToString() + "   " + kolory.Length.ToString());
            string napis="   <Species Name=\""+name+"\" Colour=\""+kolory[kolorit]+"\" Jmax=\"" + (2 * Jmax).ToString() + "\">\r\n";
            kolorit++;
            if (kolorit % kolory.Length == 0)
            {
                kolorit = 0;
            }
            return napis;
          
            
            
        }

        public string endSpecies()
        {
            return "</Species>\r\n";
        }

        public string generujKoniec(double Temp, double Xmin, double Xmax, double Gauss, double Lorentz,bool endspacies)
        {
            string napis = "";
            if (endspacies == true)
            {
                napis += "</Species>\r\n";
            }
            napis+="<Parameter Name=\"Gaussian\" Value=\""+Gauss.ToString()+"\"/>\r\n";
            napis+="<Parameter Name=\"Lorentzian\" Value=\""+Lorentz.ToString()+"\"/>\r\n";
            napis += "<Parameter Name=\"Temperature\" Value=\"" + Temp.ToString() + "\"/>\r\n";
            napis+="<Parameter Name=\"Fmin\" Value=\""+(Xmin-7).ToString()+"\"/>\r\n";
            napis+="<Parameter Name=\"Fmax\" Value=\""+(Xmax+7).ToString()+"\"/>\r\n";
            napis += "</Mixture>\r\n";
            return napis;
        }
 


        //public string generujPrzejscie(string molname, int vd, int vg, double abund, double ENERGIA, double Bvdolne, double Bvgorne, double Dvdolne, double Dvgorne,double FC)
        public string generujPrzejscie(PRZEJSCIE trans)           
        {
            string napis = "";
            if (trans.show && trans.powyzejProguNatezenia&&trans.WarunkiSekwencji&&trans.WarunkiSelekcjiVd)
            {
                string LNG = "vg=" + trans.vgora.ToString();
                string LND = "vd=" + trans.vdol.ToString();

                string symetria = "";
                string symetriaD = "";
                string symetriaG = "";
                string symWT = "";
                string AsymWT = "";
                if (trans.LM1 == trans.LM2&&symEnable==true)
                {
                    symetria = " Symmetric=\"True\"";
                    double absSpinJadra = Math.Abs(trans.spinJadra1);
                    if (Math.Abs(Math.Floor(absSpinJadra) - absSpinJadra) < 0.001)  //calkowity spin jadra
                    {
                        double SWT = (absSpinJadra + 1) * (2 * absSpinJadra + 1);
                        double AsWT = absSpinJadra * (2 * absSpinJadra + 1);
                        symWT = " SymWt=\"" + ((int)SWT).ToString() + "\" ";
                        AsymWT = " AsymWt=\"" + ((int)AsWT).ToString() + "\" ";
                       // MessageBox.Show("odkryto spin calkowity" +trans.spinJadra1.ToString());
                    }
                    else//polowkowy spin jadra
                    {
                        double SWT = absSpinJadra * (2 * absSpinJadra + 1);
                        double AsWT = (absSpinJadra + 1) * (2 * absSpinJadra + 1);
                        symWT = " SymWt=\"" + ((int)SWT).ToString() + "\" ";
                        AsymWT = " AsymWt=\"" + ((int)AsWT).ToString() + "\" ";
                        //MessageBox.Show("odkryto spin polowkowy" + trans.spinJadra1.ToString());
                    }
                    if (symD == "u")
                    {
                        symetriaD = " gerade= \"False\"";
                    }
                    if (symD == "g")
                    {
                        symetriaD = " gerade= \"True\"";
                    }
                    if (symG == "u")
                    {
                        symetriaG = " gerade= \"False\"";
                    }
                    if (symG == "g")
                    {
                        symetriaG = " gerade= \"True\"";
                    }
                }
      
                
                napis += "  <LinearMolecule Name=\"" + trans.name + "\" nNuclei=\"2\""+symetria + symWT + AsymWT+">\r\n";
                double nat = 1;
                if (dipol == true)
                {                   
                    nat = trans.braket * trans.braket;
                   // nat = trans.braket;
                }
                else
                {
                    nat = trans.FC;
                }
                napis += "        <Parameter Name=\"Abundance\" Value=\"" + (nat * trans.Abundancja * trans.WAGABOLTZMANOWSKA).ToString() + "\"/>\r\n";
                napis += "        <LinearManifold Name=\"Ground\" Initial=\"True\">\r\n";
                napis += "           <Linear Name=\"" + LND + "\" Lambda= \"" + LAMBDAD + "\"  S= \"" + SD.ToString() + "\" OmegaSelect=\"" + omegaSelectDol + "\""+symetriaD+">\r\n";
                napis += "                <Parameter Name=\"B\" Value=\"" + trans.Bvdol.ToString() + "\"/>\r\n";
                napis += "                <Parameter Name=\"A\" Value=\"" + SpinOrbitaD+ "\"/>\r\n";
                napis += "                <Parameter Name=\"D\" Value=\"" + (znakDv * trans.Dvdol).ToString() + "\"/>\r\n";
                if (uwzglH == true)
                {
                    napis += "                <Parameter Name=\"H\" Value=\"" + trans.Hvdol.ToString() + "\"/>\r\n";
                }
                napis += "          </Linear>\r\n";
                napis += "       </LinearManifold>\r\n";
                napis += "       <LinearManifold Name=\"Excited\">\r\n";
                napis += "           <Linear Name=\"" + LNG + "\" Lambda= \"" + LAMBDAG + "\"  S= \"" + SG.ToString() + "\" OmegaSelect=\"" + omegaSelectGora + "\""+symetriaG+">\r\n";
                if (OODR == false)
                {
                    napis += "                 <Parameter Name=\"Origin\" Value=\"" + trans.energiaprzejscia.ToString() + "\"/>\r\n";

                }
                else
                {
                    napis += "                 <Parameter Name=\"Origin\" Value=\"" + trans.EnergiaGora.ToString() + "\"/>\r\n";

                }
                napis += "                <Parameter Name=\"B\" Value=\"" + trans.Bvgora.ToString() + "\"/>\r\n";
                napis += "                <Parameter Name=\"A\" Value=\"" + SpinOrbitaG + "\"/>\r\n";
                napis += "                <Parameter Name=\"D\" Value=\"" + (znakDv * trans.Dvgora).ToString() + "\"/>\r\n";
                if (uwzglH == true)
                {
                    napis += "                <Parameter Name=\"H\" Value=\"" + trans.Hvgora.ToString() + "\"/>\r\n";
                }
                napis += "          </Linear>\r\n";
                napis += "       </LinearManifold>\r\n";
                napis += "        <TransitionMoments Bra=\"Ground\" Ket=\"Excited\">\r\n";
                napis += "          <SphericalTransitionMoment Component=\"" + typprzejscia + "\" Bra=\"" + LND + "\" Ket=\"" + LNG + "\"/>\r\n";
                napis += "        </TransitionMoments>\r\n";
                napis += "  </LinearMolecule>\r\n";
            }
            return napis;
        }

       
    }
}
