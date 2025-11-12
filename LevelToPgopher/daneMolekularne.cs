using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LevelToPgopher
{


    internal class daneMolekularne
    {
        public bool liczFunkcjeFalowe = false;
        public bool tylkoJednoVd = false;
        public int? DELTASEKWENCJA = null;
        public int? ONLYDV = null;
       // public TextBox TBX;
        public double najwyzszalinia = 0;
        public double progNatezenia = 0;
        public bool uwzglednicImax = false;
        public List<CheckBox> CBLIST;
        public List<TextBox> TBLIST;
        public List<HScrollBar> HSCROLLBARLIST;
        public TrackBar TB1;
        public TextBox TBBoltzman;
        public Dictionary<int, double> wagistanudolnego;
        public int aktywneiat1 = -1;
        public int aktywneiat2 = -1;

        public int maxizat1 = -1;
        public int maxizat2 = -1;

        public double masamiMaxIzo = -1;
        public double roo = -1;

        public int stalaSuma = -1;
        public bool pokazPodpisy = true;
        public bool pokazDodatkoweInfo = true;
        public double progabundancji = 0;
        public bool stosujprogabundancji = false;

        public string opisAbIniDol;
        public string opisAbIniGora;

        public bool uzyjAbIniDol = false;
        public bool uzyjAbIniGora = false;
        public string sciezkaEXPERIM="";
        public string sciezkaPredefAI;

        public List<KeyValuePair<double, double>> AbiniGora;
        public List<KeyValuePair<double, double>> AbiniDol;

        public PointPairList PPLEXPER;
        public double PPLEXPERmin = double.PositiveInfinity;
        public double PPLEXPERmax = double.NegativeInfinity;


        public TextBox tbDD;
        public TextBox tbDG;
        public TextBox tbBD;
        public TextBox tbBG;
    

        static double h = 6.6260693e-34;
        static double c = 299792458;
        static double u = 1.66053e-27;

        public bool recznaglebokoscMD = false;
        public bool recznaglebokoscMG = false;
        public bool recznaBetaMD = false;
        public bool recznaBetaMG = false;

       
        public List<PRZEJSCIE> tablicaprzejsc;
        public List<ELEMENTYMACIERZOWE> tablicaelementow;

        public List<POZIOM> poziomyDolne;
        public List<POZIOM> poziomyGorne;

        public double RFACTDol = 1.0;
        public double RFACTGora = 1.0;

        public bool dipol = false;


        public void readExperim()
        {
            
            PPLEXPER.Clear();
            System.IO.StreamReader SR = new System.IO.StreamReader(sciezkaEXPERIM);
            string linia = SR.ReadLine();
            while (linia != null)
            {
                string[] tab = System.Text.RegularExpressions.Regex.Split(linia, @"[\s]+");
                double x = double.Parse(tab[0]);
                double y = double.Parse(tab[1]);
                if (y > PPLEXPERmax)
                {
                    PPLEXPERmax = y;
                }
                if (y < PPLEXPERmin)
                {
                    PPLEXPERmin = y;
                }
                PPLEXPER.Add(x, y);
                linia = SR.ReadLine();
            }
            SR.Close();
        }

        public daneMolekularne()
        {
            poziomyDolne = new List<POZIOM>();
            poziomyGorne = new List<POZIOM>();
            tablicaelementow = new List<ELEMENTYMACIERZOWE>();
            AbiniDol = new List<KeyValuePair<double, double>>();
            AbiniGora = new List<KeyValuePair<double, double>>();
            PPLEXPER = new PointPairList();


            CBLIST = new List<CheckBox>();
            TBLIST = new List<TextBox>();
            HSCROLLBARLIST = new List<HScrollBar>();
            wagistanudolnego = new Dictionary<int, double>();
        }

        public List<wpis> at1;
        public List<wpis> at2;

        public double OED;
        public double OEG;
        public double OXED;
        public double OXEG;
        public double RD;
        public double RG;
        public double ASYMPTD;
        public double AsymptG;
        public int VD;
        public int maxVG;
        public int minVg;


        public double betaDol;
        public double glebokoscDol;

        public double[] betaGora;
        public double glebokoscGora;


        public void liczGlebMorsaDolnego()
        {
            
            double w1;          
            double masami = at1[aktywneiat1].masaatomowa * at2[aktywneiat2].masaatomowa / (at1[aktywneiat1].masaatomowa + at2[aktywneiat2].masaatomowa);
            double masamiMaxIz = at1[maxizat1].masaatomowa * at2[maxizat2].masaatomowa / (at1[maxizat1].masaatomowa + at2[maxizat2].masaatomowa);
            masamiMaxIzo = masamiMaxIz; //na potrzeby innych obliczeń;
            double ro = Math.Sqrt(masamiMaxIz / masami);
            roo = ro;//na potrzeby zewnetrznych obliczen;
            //MessageBox.Show(masami.ToString());
            w1 = 8 * Math.PI * Math.PI * c * u * masami * ro*ro*OXED / (100 * h);
           
            if (recznaBetaMD == false)
            {
                betaDol = Math.Sqrt(w1) / 100000000;
                tbBD.Text = betaDol.ToString();
            }
            else
            {
                betaDol = double.Parse(tbBD.Text);
                
            }
            if (recznaglebokoscMD == false)
            {
                glebokoscDol = OED * OED / (4 * OXED);
                tbDD.Text = glebokoscDol.ToString();
            }
            else
            {
                glebokoscDol = double.Parse(tbDD.Text);
            }
            //TBX.Text += betaGora.ToString() +"   "+ro.ToString()+ "\r\n"; ;
         
        }

        public double liczDeltaIzotop(int vd,int vg)
        {
            double przycz1 = (1 - roo) * (OEG* (0.5 + vg) - OED * (0.5 + vd));
            double przycz2 = (1 - roo * roo) * (OXEG * (0.5 + vg) * (0.5 + vg) - OXED * (0.5 + vd) * (0.5 + vd));
            return przycz1 - przycz2;
        }

        public void liczGlebMorsaGornego()
        {

            double w1;           
            double masami = at1[aktywneiat1].masaatomowa * at2[aktywneiat2].masaatomowa / (at1[aktywneiat1].masaatomowa + at2[aktywneiat2].masaatomowa);
            double masamiMaxIz = at1[maxizat1].masaatomowa * at2[maxizat2].masaatomowa / (at1[maxizat1].masaatomowa + at2[maxizat2].masaatomowa);
            double ro = Math.Sqrt(masamiMaxIz / masami);
            
            w1 = 8 * Math.PI * Math.PI * c * u * masami * ro*ro* OXEG / (100 * h);          

            if (recznaBetaMG == false)
            {

                betaGora = new double[1] {Math.Sqrt(w1) / 100000000};
                tbBG.Text = betaGora[0].ToString();
                try
                {
                    tbBG.Update();
                    tbBG.Invalidate();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("tb betaG update "+ex.Message);
                }
            }
            else
            {
                string[] tempS=Regex.Split(tbBG.Text,@"[\s]+");
                betaGora = new double[tempS.Length];
                for (int i = 0; i < tempS.Length;i++ )
                {
                    betaGora[i] = double.Parse(tempS[i]);
                }
                    //betaGora = double.Parse(tbBG.Text);
            
            }
            
            if (recznaglebokoscMG == false)
            {
                glebokoscGora = OEG * OEG / (4 *OXEG);
                tbDG.Text = glebokoscGora.ToString();
            }
            else
            {
                glebokoscGora = double.Parse(tbDG.Text);
            }
            
            
        }

        public double liczPunktMorsaDol(double X)
        {
            return (glebokoscDol * ((1 - Math.Exp(-betaDol * (X - RD))) * (1 - Math.Exp(-betaDol * (X - RD))) - 1) + ASYMPTD);
        }

        public double liczPunktMorsaGora(double X)
        {
            return (glebokoscGora * ((1 - Math.Exp(-betaGora[0] * (X - RG))) * (1 - Math.Exp(-betaGora[0] * (X - RG))) - 1) + AsymptG);
        }


        public string wypiszPoziomyWibracyjneGorne()
        {
            string temp = "";
            for (int i = 0; i < poziomyGorne.Count; i++)
            {
                temp += poziomyGorne[i].v.ToString() + "   " + poziomyGorne[i].Energia.ToString() + "\r\n";
            }
            return temp;
        }



    }
}
