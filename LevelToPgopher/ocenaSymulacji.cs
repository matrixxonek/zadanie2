using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LevelToPgopher
{
    struct porownanieES
    {
        int vg;

        public int Vg
        {
            get { return vg; }
            set { vg = value; }
        }
        double esim;

        public double Esim
        {
            get { return esim; }
            set { esim = value; }
        }
        double eexp;

        public double Eexp
        {
            get { return eexp; }
            set { eexp = value; }
        }

        string izoString;

        public string IzoString
        {
          get { return izoString; }
          set { izoString = value; }
        }

        public porownanieES(int ivg, string iIzoString, double iEsim, double iEexp)
        {
            esim = iEsim;
            eexp = iEexp;
            vg = ivg;
            izoString = iIzoString;
        }
    }

    struct liniaEksperymentalna
    {
        public override string ToString()
        {
            return this.m1.ToString() + "-" + this.m2.ToString() + "    " + this.vg.ToString() + "     " + this.energiaPrzejscia.ToString() + "\r\n";
        }
        int m1, m2;
        int vg;
        double energiaPrzejscia;
        string izoString;

        public string IzoString
        {
            get { return izoString; }
            set { izoString = value; }
        }

        public void liczIzoString()
        {
            if (M1 >= M2)
            {
                izoString = M1.ToString() + "-" + M2.ToString();
            }
            else
            {
                izoString = M2.ToString() + "-" + M1.ToString();
            }
        }

        public int M2
        {
            get { return m2; }
            set { m2 = value; }
        }

        public int M1
        {
            get { return m1; }
            set { m1 = value; }
        }
        
        public double EnergiaPrzejscia
        {
            get { return energiaPrzejscia; }
            set { energiaPrzejscia = value; }
        }        

        public int Vg
        {
            get { return vg; }
            set { vg = value; }
        }
    }
    class ocenaSymulacji
    {
        public List<liniaEksperymentalna> eksperymentSpisLinii;
        public double wspZgodnosci=0.0;
        public int vgDlaNajlepszegoWspolczynnika=0;

        IEnumerable<liniaEksperymentalna> spisPodreczny;
        //IEnumerable<liniaEksperymentalna> istotneLinieExper;


        ComboBox CBvMin;
        ComboBox CBvMax;
        ComboBox CBuporzadkuj;
        CheckedListBox LiBoxIso;
        public ocenaSymulacji(ComboBox Vmin, ComboBox Vmax, ComboBox orderBy, CheckedListBox libox)
        {
            CBuporzadkuj = orderBy;
            CBuporzadkuj.SelectedIndex = 0;
            CBvMin = Vmin;
            CBvMax = Vmax;
            LiBoxIso = libox;
        }

        
        public void wczytaj(string path)
        {
            try
            {
                CBvMin.Items.Clear();
                CBvMax.Items.Clear();
                eksperymentSpisLinii = new List<liniaEksperymentalna>();
                StreamReader SR = new StreamReader(path);
                string tekst = SR.ReadToEnd().Trim();
                SR.Close();
                string[] tab = Regex.Split(tekst, "\r\n");
                for (int i = 0; i < tab.Length; i++)
                {
                    liniaEksperymentalna temp = odczytajLinie(tab[i]);
                    eksperymentSpisLinii.Add(temp);       
                    string iso=temp.M1.ToString()+"-"+temp.M2.ToString();
                    if (!CBvMin.Items.Contains(temp.Vg))
                    {
                        CBvMin.Items.Add(temp.Vg);
                        CBvMax.Items.Add(temp.Vg);
                    }
                    if (!LiBoxIso.Items.Contains(iso))
                    {
                        LiBoxIso.Items.Add(iso);
                    }
                    
                }
                CBvMin.SelectedIndex = 0;
                CBvMax.SelectedIndex = CBvMax.Items.Count - 1;
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Problem z wczytaniem pozycji linii eksperymentalnych. " + ex.Message);
            }

        }
        public liniaEksperymentalna odczytajLinie(string linia)
        {
            liniaEksperymentalna temp=new liniaEksperymentalna();
            string[] tabliczka=Regex.Split(linia,@"[\s,-]+");
            temp.Vg = int.Parse(tabliczka[0]);
            temp.M1 = int.Parse(tabliczka[1]);
            temp.M2 = int.Parse(tabliczka[2]);
            temp.EnergiaPrzejscia= double.Parse(tabliczka[3]);
            temp.liczIzoString();
            return temp;
        }

        public IEnumerable<liniaEksperymentalna> wybierz()
        {
            List<string> wybraneIzotopy = new List<string>();
            foreach( var i in LiBoxIso.CheckedItems)
            {
                string[] spis = Regex.Split(i.ToString(), "-");
                int M1 = int.Parse(spis[0]);
                int M2 = int.Parse(spis[1]);
                string tempS="";
                if (M1 >= M2) tempS = M1.ToString() + "-" + M2.ToString();
                else tempS= M2.ToString() + "-" + M1.ToString();            
                wybraneIzotopy.Add(tempS);
            }
            spisPodreczny = from linia in eksperymentSpisLinii join izoto in wybraneIzotopy on linia.IzoString equals izoto
                            where linia.Vg >= (int)CBvMin.SelectedItem & linia.Vg <= (int)CBvMax.SelectedItem
                            select linia ;
            switch(CBuporzadkuj.SelectedIndex)
            {
                case 0:  
                    spisPodreczny = spisPodreczny.OrderBy((x)=>x.Vg);
                    break;
                case 1:
                    spisPodreczny = spisPodreczny.OrderBy((x) => x.M1 + x.M2);
                    break;
                case 3:
                    spisPodreczny = spisPodreczny.OrderBy((x) => x.EnergiaPrzejscia);
                    break;
            }
            return spisPodreczny;
        }

        public string wypiszLinieEksp()
        {
            StringBuilder SB = new StringBuilder();
            foreach(liniaEksperymentalna LE in eksperymentSpisLinii)
            {
                SB.Append(LE.ToString());
            }
            return SB.ToString();
        }

        public string wypiszLinieSpisPodreczny()
        {
            StringBuilder SB = new StringBuilder();
            foreach (liniaEksperymentalna LE in spisPodreczny)
            {
                SB.Append(LE.ToString());
            }
            return SB.ToString();
        }

        public string ocen(daneMolekularne danMol)
        {
            List<PRZEJSCIE> istotnePrzejscia = new List<PRZEJSCIE>();
            List<porownanieES> spisPorownan = new List<porownanieES>();

            foreach (liniaEksperymentalna linSim in spisPodreczny)
            {
                PRZEJSCIE temp=czyDobraLinia(linSim,danMol.tablicaprzejsc);
                istotnePrzejscia.Add(temp);
                spisPorownan.Add(new porownanieES(temp.vgora,linSim.IzoString,linSim.EnergiaPrzejscia,temp.EnergiaGora-temp.EnergiaDol));
            }
            StringBuilder SB = new StringBuilder();
            List<double> oceny = new List<double>();
            for (int i = 0; i < spisPorownan.Count; i++)
            {
                double off = 0;
                double ocena= liczocene(spisPorownan, i,out off);
                oceny.Add(ocena);
                SB.Append(spisPorownan[i].Eexp.ToString() + "  " + spisPorownan[i].Esim.ToString()+"     "+ocena.ToString()+"  "+off.ToString()+"\r\n");
                
            }
            SB.Append("\r\nNajmniejsza wartosc to "+ oceny.Min());
            oceny.Min();
            
            return SB.ToString();
            
        }
        private double liczocene(List<porownanieES> spis,int indexOffsetu, out double off)
        {
           double sumka = 0;
           double offset = spis[indexOffsetu].Esim - spis[indexOffsetu].Eexp;
           for (int i = 0; i < spis.Count; i++)
           {
               porownanieES temp = spis[i];
               sumka+= Math.Pow((temp.Esim - temp.Eexp-offset), 2);
           }
           off = offset;
           return sumka / spis.Count; ;
        }


        private PRZEJSCIE czyDobraLinia(liniaEksperymentalna linExp, List<PRZEJSCIE> spis)
        {
            foreach (PRZEJSCIE trans in spis)
            {
                string transIzo = "";
                if (trans.LM1 >= trans.LM2) transIzo = trans.LM1 + "-" + trans.LM2;
                else transIzo = trans.LM2 + "-" + trans.LM1;
                if (transIzo == linExp.IzoString && trans.vgora == linExp.Vg)
                {
                    return trans;
                }
            }
            return null;
        }
    }
}
