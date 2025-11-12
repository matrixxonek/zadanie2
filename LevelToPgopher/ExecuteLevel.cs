using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;


namespace LevelToPgopher
{

    class ExecuteLevel
    {
        public string LEVELOUT;
        public string CH8;
        public string CH9;

        public string strans;


        public void odpal(string text)
        {
            DirectoryInfo DI = new DirectoryInfo(Application.StartupPath);
            DI = DI.Parent.Parent;
            string sciezkain = Path.Combine(DI.FullName, "DANE\\level\\pro.lvlin");
            string sciezkaout = Path.Combine(DI.FullName, "DANE\\level\\pro.lvlout");
            string sciezkach8 = Path.Combine(DI.FullName, "DANE\\level\\fort.8");
            string sciezkach9 = Path.Combine(DI.FullName, "DANE\\level\\fort.9");


            FileInfo FI1 = new FileInfo(sciezkain);
            FileInfo FI2 = new FileInfo(sciezkaout);
            FileInfo FI3 = new FileInfo(sciezkach8);
            FileInfo FI4 = new FileInfo(sciezkach9);
            FI1.Delete();
            FI2.Delete();
            FI3.Delete();
            FI4.Delete();

            StreamWriter SW = new StreamWriter(sciezkain);
            SW.Write(text);
            SW.Close();
            Thread.Sleep(100);

            ProcessStartInfo startInfo;
            Process batchExecute;

            string sc = Path.Combine(DI.FullName, "DANE\\level\\lp.bat");
            startInfo = new ProcessStartInfo(sc.ToString());
            sc = Path.Combine(DI.FullName, "DANE\\level");
            startInfo.WorkingDirectory = @sc;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = true;
            batchExecute = new Process();
            batchExecute.StartInfo = startInfo;
            batchExecute.Start();
            batchExecute.WaitForExit();



            wczytajPlikizWynikami();

        }

        public void wczytajPlikizWynikami()
        {
            // Thread.Sleep(1);
            DirectoryInfo DI = new DirectoryInfo(Application.StartupPath);
            DI = DI.Parent.Parent;
            string sciezkaout = Path.Combine(DI.FullName, "DANE\\level\\pro.lvlout");
            string sciezkach8 = Path.Combine(DI.FullName, "DANE\\level\\fort.8");
            string sciezkach9 = Path.Combine(DI.FullName, "DANE\\level\\fort.9");
            try
            {
                StreamReader SR = new StreamReader(sciezkaout);
                LEVELOUT = SR.ReadToEnd();
                SR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z wczytaniem pliku pro.lvlout :" + ex, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LEVELOUT = "ERROR";
            }

            try
            {
                StreamReader SRCh8 = new StreamReader(sciezkach8);
                CH8 = SRCh8.ReadToEnd();
                SRCh8.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(sciezkach8);
                MessageBox.Show("TERAZ Problem z wczytaniem pliku LVLOUT.ch8 :" + ex, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CH8 = "ERROR";
            }
            try
            {
                StreamReader SRCh9 = new StreamReader(sciezkach9);
                CH9 = SRCh9.ReadToEnd();
                SRCh9.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z wczytaniem pliku fort.9 :" + ex, "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CH9 = "ERROR";
            }
        }

        public void generujStałeRotacyjne(LevelINPUT LEVIN, daneMolekularne danMol, TextBox tbLin, TextBox tbLin2, TextBox tbLVLout1, TextBox tbLVLout2, DataGridView DGV, DataGridView DGV1, string manualInput = null)
        {
            DGVini(DGV);
            danMol.poziomyDolne.Clear();
            danMol.poziomyGorne.Clear();
            danMol.liczGlebMorsaDolnego();
            danMol.liczGlebMorsaGornego();


            DirectoryInfo DI = new DirectoryInfo(Application.StartupPath);
            DI = DI.Parent.Parent;
            string sciezkaout = Path.Combine(DI.FullName, "DANE\\level\\pro.lvlout");
            string sciezkach8 = Path.Combine(DI.FullName, "DANE\\level\\fort.8");
            string sciezkach9 = Path.Combine(DI.FullName, "DANE\\level\\fort.9");

            string lin = "";
            if (manualInput == null) lin = LEVIN.GENERUJ(danMol);
            else lin = manualInput;

            tbLin2.Text = lin;
            //MessageBox.Show(lin);
            odpal(lin);
            //MessageBox.Show("wygenerowano nowy");
            string linia;
            try
            {
                StreamReader SR = new StreamReader(sciezkach9);

                linia = SR.ReadLine();
                linia = SR.ReadLine();
                linia = SR.ReadLine();
                linia = SR.ReadLine();
                linia = SR.ReadLine();                
                linia = SR.ReadLine();
                //linia = SR.ReadLine();
                while (linia != null)
                {
                    danMol.poziomyDolne.Add(obroblinie(linia, false));
                    //MessageBox.Show(linia);
                    linia = SR.ReadLine();
                }
                SR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił problem z obróbką pliku fort.9.  " + ex.Message);
            }

            readChOut(danMol, sciezkaout,tbLVLout1);

            // MessageBox.Show("Ilość wpisów w tempElement: "+ danMol.tablicaelementow.Count.ToString());
            // MessageBox.Show(LEVIN.GENERUJtoBVgora(danMol));
            string lin2=LEVIN.GENERUJtoBVgora(danMol);
            odpal(lin2);
            tbLin.Text = lin2;
            StreamReader SR2 = new StreamReader(sciezkaout);
            tbLVLout2.Text = SR2.ReadToEnd();
            SR2.Close();
            // MessageBox.Show("wygenerowano BV gora ?");
            StreamReader SR1 = new StreamReader(sciezkach9);
            linia = SR1.ReadLine();
            linia = SR1.ReadLine();
            linia = SR1.ReadLine();
            linia = SR1.ReadLine();
            linia = SR1.ReadLine();
            linia = SR1.ReadLine();
         // linia = SR1.ReadLine();
            while (linia != null)
            {
                POZIOM tempPoz = obroblinie(linia, true);
                if (tempPoz.v >= danMol.minVg && tempPoz.v <= danMol.maxVG)
                {
                    danMol.poziomyGorne.Add(tempPoz);
                }
                linia = SR1.ReadLine();
            }
            SR1.Close();


            tworzprzejscia(LEVIN, danMol);
            strans = "";
            /*
            var temTabPrzejsc = from przejscie in danMol.tablicaprzejsc
                                orderby przejscie.FC descending
                                select przejscie;

            foreach (PRZEJSCIE p in temTabPrzejsc)
            {
                string opis = p.ToString();
                strans += opis + "\r\n";
                DGVFillRow(DGV, opis);
            }*/

            for (int i = 0; i < danMol.tablicaprzejsc.Count; i++)
            {
                string opis = danMol.tablicaprzejsc[i].ToString();
                strans += opis + "\r\n";
                DGVFillRow(DGV, opis);
            }

            strans += "\r\n\r\n";
            for (int i = 0; i < danMol.tablicaelementow.Count; i++)
            {
                string opis = danMol.tablicaelementow[i].ToString();
                strans += opis + "\r\n";

            }

            generujTablicePrzejscZPodzialemNaKombinacje(danMol, DGV1);

        }
        private static void readChOut(daneMolekularne danMol, string sciezkaOut, TextBox LVLout)
        {
            try
            {
                StreamReader SR8 = new StreamReader(sciezkaOut);
                string napis = SR8.ReadToEnd();
                LVLout.Text = napis;
                SR8.Close();
                string[] linijki = Regex.Split(napis, @"\r\n");
                for (int i = 0; i < linijki.Length; i++)
                {
                    if (Regex.IsMatch(linijki[i], "Coupling"))
                    {
                        string opis1 = linijki[i];
                        string opis2 = "";//linijki[i + 2];
                        if (Regex.IsMatch(linijki[i + 1], "FCF"))
                        {
                            opis2 = linijki[i + 1];
                        }
                        else opis2 = linijki[i + 2];
                        dodaj(opis1, opis2, danMol);
                    }
                }
            }



            catch (Exception ex)
            {
                //   MessageBox.Show("Wystąpił problem z obróbką pliku chout.  " + ex.Message);
            }
        }

        public static void dodaj(string opis1, string opis2, daneMolekularne danMol)
        {
            try
            {
                int p1a = opis1.IndexOf("v=");
                int p2a = opis1.IndexOf(",");

                string sVini = opis1.Substring(p1a + 2, (p2a - p1a - 2));
               

                int vInitial = int.Parse(sVini);
                int p3a = opis1.IndexOf("J=");
                int p4a = opis1.IndexOf(")=");

                string sJini = opis1.Substring(p3a + 2, (p4a - p3a - 2));
                int Jinitial = int.Parse(sJini);

                int p5a = opis1.IndexOf("to");
                string sEIni = opis1.Substring(p4a + 2, (p5a - p4a - 2));
                double eIni = double.Parse(sEIni);                
                opis1 = opis1.Substring(p5a + 2);

                int p1b = opis1.IndexOf("v=");
                int p2b = opis1.IndexOf(",");

                string sVFinal = opis1.Substring(p1b + 2, (p2b - p1b - 2));
                int vFinal = int.Parse(sVFinal);


                int p3b = opis1.IndexOf("J=");
                int p4b = opis1.IndexOf(")=");

            

                string sJFinal = opis1.Substring(p3b + 2, (p4b - p3b - 2));
                int JFinal = int.Parse(sJFinal);


                string sEFinal = opis1.Substring(p4b + 2);
                double eFinal = double.Parse(sEFinal);

                

                int p6 = opis2.IndexOf("FCF=");
                int p7 = opis2.IndexOf("<M>=");
                string sFCF = opis2.Substring(p6 + 4, p7 - p6 - 4);
                sFCF = sFCF.Replace("D", "e");

                double FCF = double.Parse(sFCF);

               
                int p8 = opis2.IndexOf("d(E)");
               
                string sBraket = opis2.Substring(p7 + 4, p8 - p7 - 4);
                sBraket = sBraket.Replace("D", "e");
                
                double braket = double.Parse(sBraket);
                
                ELEMENTYMACIERZOWE tempELEMENT = new ELEMENTYMACIERZOWE(vInitial, vFinal, eFinal - eIni, FCF, 0, braket, "Z", danMol.roo, danMol.liczDeltaIzotop(vInitial, vFinal));
                
                danMol.tablicaelementow.Add(tempELEMENT);
               
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }


            

           

        }

        private static void readCh8(daneMolekularne danMol, string sciezkach8)
        {
            try
            {
                StreamReader SR8 = new StreamReader(sciezkach8);
                string napis = SR8.ReadLine();

                while (!Regex.IsMatch(napis, @"dJ\(J"))
                {
                    napis = SR8.ReadLine();
                }
                SR8.ReadLine();
                napis = SR8.ReadLine();
                string[] tab = null;
                danMol.tablicaelementow.Clear();
                while (napis != null)
                {
                    tab = Regex.Split(napis, @"[\s]+");

                    napis = SR8.ReadLine();
                    string typ = tab[1];
                    int vg = int.Parse(tab[3]);
                    int vd = int.Parse(tab[5]);
                    double Energy = double.Parse(tab[7]);
                    double AEints = double.Parse(Regex.Replace(tab[8], "D", "E"));
                    double FC = double.Parse(Regex.Replace(tab[9], "D", "E"));
                    double braket = double.Parse(Regex.Replace(tab[10], "D", "E"));
                    ELEMENTYMACIERZOWE tempELEMENT = new ELEMENTYMACIERZOWE(vd, vg, Energy, FC, AEints, braket, tab[1].Substring(0, 1), danMol.roo, danMol.liczDeltaIzotop(vd, vg));
                    danMol.tablicaelementow.Add(tempELEMENT);

                }
                // MessageBox.Show(tab[3]+"   "+tab[5]+"   "+tab[7]+"  "+tab[8]+"   "+tab[9]);
                SR8.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił problem z obróbką pliku fort.8.  " + ex.Message);
            }
        }



        private void generujTablicePrzejscZPodzialemNaKombinacje(daneMolekularne danMol, DataGridView dgv)
        {
            List<PRZEJSCIE> tempkomb = new List<PRZEJSCIE>();
            Dictionary<int, List<PRZEJSCIE>> spisKomb = new Dictionary<int, List<PRZEJSCIE>>();
            for (int i = 0; i < danMol.tablicaprzejsc.Count; i++)
            {
                PRZEJSCIE temp = danMol.tablicaprzejsc[i];
                int sumaMas = temp.LM1 + temp.LM2;
                if (spisKomb.ContainsKey(sumaMas))
                {
                    spisKomb[sumaMas].Add(temp);
                }
                else
                {
                    spisKomb.Add(sumaMas, new List<PRZEJSCIE>());
                    spisKomb[sumaMas].Add(temp);
                }
            }
            DGVini(dgv);
            foreach (KeyValuePair<int, List<PRZEJSCIE>> temp in spisKomb)
            {
                DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                row.Cells[10].Value = temp.Key.ToString();
                dgv.Rows.Add(row);

                double abundKomb = 0;
                for (int i = 0; i < temp.Value.Count; i++)
                {
                    abundKomb += temp.Value[i].Abundancja;
                    DGVFillRow(dgv, temp.Value[i].ToString());
                }
                DataGridViewRow row2 = (DataGridViewRow)dgv.Rows[0].Clone();
                row2.Cells[12].Value = abundKomb.ToString();
                dgv.Rows.Add(row2);
            }



        }

        private void DGVini(DataGridView DGV)
        {

            DGV.Columns.Clear();
            DGV.Rows.Clear();
            DGV.Columns.Add("v'", "v'");
            DGV.Columns.Add("v''", "v''");
            DGV.Columns.Add("E_trans", "E_trans");
            DGV.Columns.Add("E_dol", "E_dol");
            DGV.Columns.Add("E_gora", "E_gora");
            DGV.Columns.Add("Bv''", "Bv''");
            DGV.Columns.Add("Dv''", "Dv''");
            DGV.Columns.Add("Bv'", "Bv'");
            DGV.Columns.Add("Dv'", "Dv'");
            DGV.Columns.Add("FCF", "FCF");
            DGV.Columns.Add("BRAKET", "BRAKET");
            DGV.Columns.Add("IZO", "IZO");
            DGV.Columns.Add("abund", "abund");
            DGV.Columns.Add("", "");
            DGV.Columns.Add("ro", "ro");
            DGV.Columns.Add("deltaIzo", "deltaIzo");
            DGV.Columns.Add("mu", "mu");

        }
        private void DGVFillRow(DataGridView DGV, string opisPrzejscia)
        {
            string[] tab = Regex.Split(opisPrzejscia, @"[\s]+");
            DataGridViewRow row = (DataGridViewRow)DGV.Rows[0].Clone();



            row.Cells[0].Value = tab[1];
            row.Cells[1].Value = tab[3];
            row.Cells[2].Value = tab[5];
            row.Cells[3].Value = tab[7];
            row.Cells[4].Value = tab[9];
            row.Cells[5].Value = tab[12];
            row.Cells[6].Value = tab[14];
            row.Cells[7].Value = tab[16];
            row.Cells[8].Value = tab[18];
            row.Cells[9].Value = tab[20];           
            row.Cells[11].Value = tab[26];
            row.Cells[10].Value = tab[22];
            row.Cells[12].Value = tab[28];
            row.Cells[14].Value = tab[34];
            row.Cells[15].Value = tab[36];
            row.Cells[16].Value = tab[38];
            DGV.Rows.Add(row);
        }

        private POZIOM obroblinie(string line, bool isgora)
        {
            string[] tablica = Regex.Split(line, @"[\s]+");
            int v = int.Parse(tablica[1]);
            double Energia = double.Parse(tablica[3]);
            double Bv = double.Parse(Regex.Replace(tablica[4], "D", "e"));
            double Dv = double.Parse(Regex.Replace(tablica[5], "D", "e"));
            double Hv = double.Parse(Regex.Replace(tablica[6], "D", "e"));
            POZIOM P = new POZIOM(v, Energia, Bv, Dv, Hv);
            P.isGORA = isgora;
            return P;
        }


        private void tworzprzejscia(LevelINPUT LI, daneMolekularne DANMOL)
        {

            for (int i = 0; i < DANMOL.poziomyDolne.Count; i++)
            {
                for (int j = 0; j < DANMOL.poziomyGorne.Count; j++)
                {
                    PRZEJSCIE TRANS = new PRZEJSCIE();
                    TRANS.LM1 = LI.IMN1;
                    TRANS.LM2 = LI.IMN2;
                    TRANS.spinJadra1 = LI.spinJ1;
                    TRANS.spinJadra2 = LI.spinJ2;


                    POZIOM PD = DANMOL.poziomyDolne[i];
                    TRANS.vdol = PD.v;
                    TRANS.EnergiaDol = PD.Energia;
                    TRANS.Bvdol = PD.Bv;
                    TRANS.Dvdol = PD.Dv;
                    TRANS.Hvdol = PD.Hv;

                    POZIOM PG = DANMOL.poziomyGorne[j];
                    TRANS.vgora = PG.v;
                    TRANS.EnergiaGora = PG.Energia;
                    TRANS.Bvgora = PG.Bv;
                    TRANS.Dvgora = PG.Dv;
                    TRANS.Hvgora = PG.Hv;

                    ELEMENTYMACIERZOWE tempel = null;
                    foreach (ELEMENTYMACIERZOWE e in DANMOL.tablicaelementow)
                    {
                        if (e.vdolne == PD.v && e.vgorne == PG.v)
                        {
                            tempel = e;
                            break;
                        }
                    }
                    if (tempel != null)
                    {
                        TRANS.FC = tempel.FC;
                        TRANS.wspA = tempel.AEinsteina;
                        TRANS.braket = tempel.braket;
                        TRANS.ro = tempel.ro;
                        TRANS.deltaIzo = tempel.deltaIzo;

                        TRANS.Abundancja = LI.ABUNDANCJA;
                        TRANS.miMass = LI.miMass;

                        if (LI.IAN1 == LI.IAN2 && LI.IMN1 != LI.IMN2)
                        {
                            TRANS.Abundancja = 2 * LI.ABUNDANCJA;
                        }
                        DANMOL.tablicaprzejsc.Add(TRANS);
                    }
                }
            }
            //MessageBox.Show(DANMOL.tablicaelementow.Count.ToString() + " " + DANMOL.tablicaprzejsc.Count.ToString()+"  "+DANMOL.poziomyDolne.Count.ToString()+"  "+DANMOL.poziomyGorne.Count.ToString());

        }
    }
}
