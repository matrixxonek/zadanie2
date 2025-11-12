using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using ZedGraph;


namespace LevelToPgopher
{
    partial class GlowneOkno : Form
    {
        // string opis = "LevelToPgopher 4.0    "; 
        int jedn = 0;
        bool kombToSpacies = false;
        ThreadStart TS;
        Thread wypiszPrzejsciaT;
        public daneMolekularne DANEMol;
        LevelINPUT LI;
        ExecuteLevel LEVEXE;
        PgopherGenerator PG;
        ocenaSymulacji OSYM;
        string sciezkaZapisu = null;
        public GlowneOkno()
        {
            InitializeComponent();
            inicjalizujSkladIzotopowy();
            DANEMol = new daneMolekularne();
            LI = new LevelINPUT();
            LEVEXE = new ExecuteLevel();
            PG = new PgopherGenerator();
            cbTypTrans.SelectedIndex = 0;
            cbOmegaSelectDol.SelectedIndex = 0;
            cbOmegaSelectGora.SelectedIndex = 0;
            DANEMol.tbDD = tbDDol;
            DANEMol.tbDG = tbDGora;
            DANEMol.tbBD = tbBetaD;
            DANEMol.tbBG = tbBetaG;
            cbJednWykr.SelectedIndex = 0;

            // DANEMol.TBX = tbX;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cbSelectTransition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void GlowneOkno_Load(object sender, EventArgs e)
        {

        }

        private void cbSELECTTRANSITION_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            inicjalizujWartosciDomyslne(cbSELECTTRANSITION.SelectedIndex, DANEMol, LI, 0, 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            inicjalizujAktualneWartosci(DANEMol, LI, tempI1, tempI2);
            tbLevIN.Text = tbW1.Text = LI.GENERUJ(DANEMol);
            
           // LEVEXE.odpal(tbLevIN.Text);
           // tbLevOUT.Text = LEVEXE.LEVELOUT;
          //  tbCH8.Text = LEVEXE.CH8;
           // tbCH9.Text = LEVEXE.CH9;

        }

        private void testmaxabund(daneMolekularne DaneMol)
        {
            MessageBox.Show("Maksymalna abundancja dla: " + DaneMol.at1[DaneMol.maxizat1].LM.ToString() + "-" + DaneMol.at2[DaneMol.maxizat2].LM.ToString());

        }

        private void cbSelectIzotopolog_SelectedIndexChanged(object sender, EventArgs e)
        {
            analizujCbSelectIzotopolog(DANEMol);

        }


        private void kalkulujRozkladBoltzmana()
        {
            double Imax = double.NegativeInfinity;
            for (int i = 0; i < DANEMol.tablicaprzejsc.Count; i++)
            {
                int tempVD = DANEMol.tablicaprzejsc[i].vdol;
                try
                {

                    DANEMol.tablicaprzejsc[i].WAGABOLTZMANOWSKA = double.Parse(DANEMol.TBLIST[tempVD].Text);
                    DANEMol.tablicaprzejsc[i].show = DANEMol.CBLIST[tempVD].Checked;
                    double tempI = DANEMol.tablicaprzejsc[i].WAGABOLTZMANOWSKA * DANEMol.tablicaprzejsc[i].FC * DANEMol.tablicaprzejsc[i].Abundancja;
                    if (tempI > Imax)
                    {
                        Imax = tempI;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(tempVD.ToString() + "   " + ex.Message);
                }
            }
            DANEMol.najwyzszalinia = Imax;

            double ALL = 0;
            double liniePowyzejProgu = 0;
            double liniePonizejProgu = 0;
            int ileLiniiPowyzejProgu = 0;
            int ileLiniiPonizejProgu = 0;
            int ileLiniiSpelniaWarunkiSekwencji = 0;
            for (int i = 0; i < DANEMol.tablicaprzejsc.Count; i++)
            {
                double tempI = DANEMol.tablicaprzejsc[i].WAGABOLTZMANOWSKA * DANEMol.tablicaprzejsc[i].FC * DANEMol.tablicaprzejsc[i].Abundancja;
                ALL += tempI;
                if (tempI < DANEMol.progNatezenia * DANEMol.najwyzszalinia && DANEMol.uwzglednicImax)
                {
                    DANEMol.tablicaprzejsc[i].powyzejProguNatezenia = false;
                    liniePonizejProgu += tempI;
                    ileLiniiPonizejProgu++;
                }
                else
                {
                    liniePowyzejProgu += tempI;
                    ileLiniiPowyzejProgu++;
                }
                if (DANEMol.DELTASEKWENCJA != null && Math.Abs(DANEMol.tablicaprzejsc[i].vdol - DANEMol.tablicaprzejsc[i].vgora) > DANEMol.DELTASEKWENCJA)
                {
                    DANEMol.tablicaprzejsc[i].WarunkiSekwencji = false;
                }
                else
                {
                    if (DANEMol.tablicaprzejsc[i].powyzejProguNatezenia)
                    {
                        ileLiniiSpelniaWarunkiSekwencji++;
                    }
                }
                if (DANEMol.ONLYDV != null && (DANEMol.tablicaprzejsc[i].vgora - DANEMol.tablicaprzejsc[i].vdol) != DANEMol.ONLYDV)
                {
                    DANEMol.tablicaprzejsc[i].WarunkiSekwencji = false;
                }
                if (DANEMol.tylkoJednoVd == true && DANEMol.tablicaprzejsc[i].vdol != DANEMol.VD) //gdy wybrano opcję przejść z tylko jednego Vd inne przejscia sa wygaszane
                {
                    DANEMol.tablicaprzejsc[i].WarunkiSelekcjiVd = false;
                }

            }
            MessageBox.Show("Powyzej progu jest " + ileLiniiPowyzejProgu.ToString() + " linii o łącznym natężeniu " + liniePowyzejProgu.ToString() + "  co stanowi " + (100 * liniePowyzejProgu / ALL).ToString() + " % całego natezenia. Pominieto " + ileLiniiPonizejProgu.ToString() + " linii o sumarycznym natezeniu " + liniePonizejProgu.ToString() + "\r\nZ tego " + ileLiniiSpelniaWarunkiSekwencji.ToString() + " spełnia warunki sekwencji");

        }



        private void button3_Click(object sender, EventArgs e)
        {
            inicjalizujAktualneWartosci(DANEMol, LI, tempI1, tempI2);
            tbLevIN.Text = tbW1.Text = LI.GENERUJtoBVgora(DANEMol);
            LEVEXE.odpal(tbLevIN.Text);
            //tbLevOUT.Text = LEVEXE.LEVELOUT;
            tbCH8.Text = LEVEXE.CH8;
            tbCH9.Text = LEVEXE.CH9;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tbWIBRLEVELS.Text = "";
            TS = new ThreadStart(wypiszPrzejscia);
            wypiszPrzejsciaT = new Thread(TS);

            wypiszPrzejsciaT.Start();
            wypiszPrzejsciaT.Join();

            kalkulujRozkladBoltzmana();
            WYKRESL.LEVELwykres(ZG1, DANEMol, jedn);
            WYKRESL.RYSUJPOTENCJALYABINI(cbProgNatezenia, DANEMol, tbPotencjal);
            WYKRESL.RYSUJPOZIOMYVG(ZG3, DANEMol, true);
            tbWIBRLEVELS.Text = DANEMol.wypiszPoziomyWibracyjneGorne();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            TS = new ThreadStart(wypiszPrzejscia);
            wypiszPrzejsciaT = new Thread(TS);

            wypiszPrzejsciaT.Start();
            wypiszPrzejsciaT.Join();
            kalkulujRozkladBoltzmana();
            Thread.Sleep(10);
            generujPlikWsadowyPgophera();
            Thread.Sleep(10);
            wykonajPGOPHERA();
            WYKRESL.LEVELwykres(ZG1, DANEMol, jedn);
            WYKRESL.RYSUJPOTENCJALYABINI(cbProgNatezenia, DANEMol, tbPotencjal);
            WYKRESL.RYSUJPOZIOMYVG(ZG3, DANEMol, true);
            tbWIBRLEVELS.Text = DANEMol.wypiszPoziomyWibracyjneGorne();
        }

        private void btAbInidol_Click(object sender, EventArgs e)
        {
            OFDAbIniD.ShowDialog();
        }

        private void btAbInigora_Click(object sender, EventArgs e)
        {
            OFDAbIniG.ShowDialog();
        }

        private void OFD1_FileOk(object sender, CancelEventArgs e)
        {
            loadAIdol(OFDAbIniD.FileName);
        }

        private void loadAIdol(string sciezka)
        {
            DANEMol.AbiniDol.Clear();
            StreamReader SR = new StreamReader(sciezka);
            string linia = SR.ReadLine();
            string[] tab = Regex.Split(linia, @"[\s]+");
            if (tab[0].ToUpper() == "SKALA")
            {
                LI.RFACT1 = double.Parse(tab[1]);
                DANEMol.RFACTDol = 1.0;
                linia = SR.ReadLine();
            }
            else
            {
                LI.RFACT1 = 0.529177249;
                DANEMol.RFACTDol = 0.529177249;
            }


            DANEMol.opisAbIniDol = linia;
            linia = SR.ReadLine();
            while (linia != null)
            {
                linia = linia.Trim();
                tab = Regex.Split(linia, @"[\s]+");
                double x = double.Parse(tab[0]);
                double y = double.Parse(tab[1]);
                KeyValuePair<double, double> KVP = new KeyValuePair<double, double>(x, y);
                DANEMol.AbiniDol.Add(KVP);
                linia = SR.ReadLine();
            }
            SR.Close();
        }

        private void OFD2_FileOk(object sender, CancelEventArgs e)
        {
            loadAIGora(OFDAbIniG.FileName);
            setOpisOkna();
        }

        private void loadAIGora(string sciezka)
        {
            DANEMol.AbiniGora.Clear();
            StreamReader SR = new StreamReader(sciezka);
            string linia = SR.ReadLine().Trim();
            string[] tab = Regex.Split(linia, @"[\s]+");
            if (tab[0].ToUpper() == "SKALA")
            {
                LI.RFACT2 = double.Parse(tab[1]);
                DANEMol.RFACTGora = 1.0;
                linia = SR.ReadLine().Trim();
            }
            else
            {
                LI.RFACT2 = 0.529177249;
                DANEMol.RFACTGora = 0.529177249;
                linia = SR.ReadLine();
            }

            DANEMol.opisAbIniGora = linia;            
            while (linia != null)
            {
                linia = linia.Trim();
                tab = Regex.Split(linia, @"[\s]+");
                double x = double.Parse(tab[0]);
                double y = double.Parse(tab[1]);
                KeyValuePair<double, double> KVP = new KeyValuePair<double, double>(x, y);
                DANEMol.AbiniGora.Add(KVP);
                linia = SR.ReadLine();
            }
            SR.Close();
        }

        private void chbsDolAbIni_CheckedChanged(object sender, EventArgs e)
        {
            LI.isDolAbIni = chbIsDolAbIni.Checked;
            LI.NTP1 = -2;
            DANEMol.uzyjAbIniDol = chbIsDolAbIni.Checked;
        }

        private void chbIsGoraAbIni_CheckedChanged(object sender, EventArgs e)
        {
            LI.isGoraAbIni = chbIsGoraAbIni.Checked;
            LI.NTP2 = -2;
            DANEMol.uzyjAbIniGora = chbIsGoraAbIni.Checked;
            setOpisOkna();
        }

        private void cbLAMBDAD_SelectedIndexChanged(object sender, EventArgs e)
        {
            PG.LAMBDAD = cbLAMBDAD.SelectedItem.ToString();
        }

        private void cbLAMBDAG_SelectedIndexChanged(object sender, EventArgs e)
        {
            PG.LAMBDAG = cbLAMBDAG.SelectedItem.ToString();
        }

        private void tbSdol_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(tbSdol.Text);
                temp = temp * 2;
                PG.SD = temp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z wczytaniem S dolnego: " + ex.Message, "PROBLEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSgora_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(tbSgora.Text);
                temp = temp * 2;
                PG.SG = temp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z wczytaniem S gornego: " + ex.Message, "PROBLEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbTypTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTypTrans.SelectedIndex == 0)
            {
                PG.typprzejscia = "auto";
            }
            else if (cbTypTrans.SelectedIndex == 1)
            {
                PG.typprzejscia = "0";
            }
            else if (cbTypTrans.SelectedIndex == 2)
            {
                PG.typprzejscia = "2";
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void cbOmegaSelectGora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOmegaSelectGora.SelectedIndex != 2)
            {
                PG.omegaSelectGora = cbOmegaSelectGora.SelectedItem.ToString();
            }
            else if (cbOmegaSelectGora.SelectedIndex == 2)
            {
                PG.omegaSelectGora = "2";
            }
        }

        private void cbOmegaSelectDol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOmegaSelectDol.SelectedIndex != 2)
            {
                PG.omegaSelectDol = cbOmegaSelectDol.SelectedItem.ToString();
            }
            else if (cbOmegaSelectDol.SelectedIndex == 2)
            {
                PG.omegaSelectDol = "2";
            }
        }

        private string generujPlikWsadowyPgophera()
        {

            try
            {
                double Xmin, Xmax;
                Xmin = DANEMol.tablicaprzejsc[0].EnergiaGora - DANEMol.tablicaprzejsc[0].EnergiaDol;
                Xmax = DANEMol.tablicaprzejsc[0].EnergiaGora - DANEMol.tablicaprzejsc[0].EnergiaDol;
                string napis = "";
                napis += PG.generujPoczątek();

                double T = double.Parse(tbT.Text);
                int Jmax = int.Parse(tbJmax.Text);
                double gauss = double.Parse(tbGauss.Text);
                double lorentz = double.Parse(tbLorentz.Text);
                int aktLM1 = DANEMol.tablicaprzejsc[0].LM1;
                int aktLM2 = DANEMol.tablicaprzejsc[0].LM2;
                int kombi = DANEMol.tablicaprzejsc[0].LM1 + DANEMol.tablicaprzejsc[0].LM2;

                if (kombToSpacies == false)
                {
                    napis += PG.startSpacies(Jmax, "ALL");
                }
                else
                {
                    napis += PG.startSpacies(Jmax, kombi.ToString());
                }

                
       

                for (int i = 0; i < DANEMol.tablicaprzejsc.Count; i++)
                {
                    PRZEJSCIE trans = DANEMol.tablicaprzejsc[i];

                    trans.name = trans.LM1.ToString() + "-" + trans.LM2.ToString() + "(" + trans.vgora.ToString() + "<-" + trans.vdol.ToString() + ")";
                    double energia = trans.energiaprzejscia = trans.EnergiaGora - trans.EnergiaDol;
                    if (energia < Xmin)
                    {
                        Xmin = energia;
                    }
                    if (energia > Xmax)
                    {
                        Xmax = energia;
                    }
                    
                    if(trans.LM1+trans.LM2!=kombi && kombToSpacies)
                    {
                       
                        kombi = trans.LM1 + trans.LM2;
                        napis+= PG.endSpecies();
                        napis +=PG.startSpacies(Jmax, kombi.ToString());
                    }

                    napis += PG.generujPrzejscie(trans);                   

                    
                }               
                napis += PG.generujKoniec(T, Xmin, Xmax, gauss, lorentz, true);
                tbW1.Text = napis;
                tbPgopherIn.Text = napis;
                return napis;
            }
            catch
            {
                MessageBox.Show("Wystąpił problem");
                return "ERROR";
            }
        }


        private void wykonajPGOPHERA()
        {
            try
            {

                StreamWriter pisz = new StreamWriter("temp.pgo");
                pisz.Write(tbPgopherIn.Text);
                pisz.Close();

                DirectoryInfo DI = new DirectoryInfo(Application.StartupPath);
                DI = DI.Parent.Parent;
                string pgopherPath = Path.Combine(DI.FullName, "DANE\\PgopherU64.exe");
                if (cbExperiment.Checked == true)
                {
                  
                    ProcessStartInfo stinfo = new ProcessStartInfo(pgopherPath);
                    string opcje = "temp.pgo \"" + DANEMol.sciezkaEXPERIM + "\"";
                    stinfo.Arguments = opcje;
                    Process.Start(stinfo);
                }
                else
                {
                    Process.Start(pgopherPath, "temp.pgo");
                }



            }
            catch
            {
                MessageBox.Show("Najpewniej nie znaleziono programu PgopherU64.exe,  powinien być na dysku C lub problem ze ścieżką z danymi eksperymentalnymi");
            }
        }


        public void blokujOdblokujPrzyciski(bool blokuj)
        {
            btGenerujPrzejscia.Enabled = blokuj;
            btLevelGen.Enabled = blokuj;
            btLevelExe.Enabled = blokuj;
            btManualExe.Enabled = blokuj;
            btPgopherGen.Enabled = blokuj;

        }


        private void wypiszPrzejscia()
        {
            //blokujOdblokujPrzyciski(false);
            DANEMol.tablicaprzejsc = new List<PRZEJSCIE>();
            DANEMol.tablicaprzejsc.Clear();
            double sumarycznaabund = 0;
            double izot = 0;
            if (WSZYSTKIEIZOTOPY == true)
            {
                int ile = 0;//uwaga powtórzenie kodu w dwóch pętlach -warto pomyśleć jak uniknąć
                for (int i = 0; i < DANEMol.at1.Count; i++)
                {
                    if (DANEMol.at1[0].LA != DANEMol.at2[0].LA)
                    {
                        for (int j = 0; j < DANEMol.at2.Count; j++)
                        {
                            double abund = DANEMol.at1[i].abundancja * DANEMol.at2[j].abundancja;

                            if (abund >= DANEMol.progabundancji / 100 || DANEMol.stosujprogabundancji == false)
                            {
                                if (DANEMol.stalaSuma == -1 || DANEMol.at1[i].LM + DANEMol.at2[j].LM == DANEMol.stalaSuma)
                                {
                                    ile++;
                                    izot++;
                                    sumarycznaabund += abund;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = i; j < DANEMol.at2.Count; j++)
                        {
                            double abund = 2 * DANEMol.at1[i].abundancja * DANEMol.at2[j].abundancja;
                            if (DANEMol.at1[i] == DANEMol.at2[j])
                            {
                                abund = abund / 2;
                            }
                            if (abund >= DANEMol.progabundancji / 100 || DANEMol.stosujprogabundancji == false)
                            {
                                if (DANEMol.stalaSuma == -1 || DANEMol.at1[i].LM + DANEMol.at2[j].LM == DANEMol.stalaSuma)
                                {
                                    ile++;
                                    izot++;
                                    sumarycznaabund += abund;
                                    //MessageBox.Show("dodatno");
                                }
                            }
                        }
                    }
                }
                //ile= DANEMol.at1.Count * DANEMol.at2.Count;
                pBar1.Maximum = ile;
                int x = 0;
                string uwzglednioneizotopomery = "";
                string nieuwzglednioneizotopomery = "";
                for (int i = 0; i < DANEMol.at1.Count; i++)
                {
                    double abund; //uwaga powtórzenie kodu w dwóch pętlach -warto pomyśleć jak uniknąć
                    if (DANEMol.at1[0].LA != DANEMol.at2[0].LA)
                    {
                        for (int j = 0; j < DANEMol.at2.Count; j++)
                        {
                            abund = DANEMol.at1[i].abundancja * DANEMol.at2[j].abundancja;
                            if (abund >= DANEMol.progabundancji / 100 || DANEMol.stosujprogabundancji == false)
                            {
                                if (DANEMol.stalaSuma == -1 || DANEMol.at1[i].LM + DANEMol.at2[j].LM == DANEMol.stalaSuma)
                                {
                                    wypiszPrzejsciaInner(i, j);
                                    x++;
                                    pBar1.Value = x;
                                    pBar1.Update();
                                    pBar1.Invalidate();
                                    uwzglednioneizotopomery += DANEMol.at1[i].LM.ToString() + " " + DANEMol.at2[j].LM.ToString() + " abund: " + abund.ToString() + "\r\n";
                                    Thread.Sleep(50);
                                }


                            }
                            else
                            {
                                nieuwzglednioneizotopomery += DANEMol.at1[i].LM.ToString() + " " + DANEMol.at2[j].LM.ToString() + " abund: " + abund.ToString() + "\r\n";
                            }
                        }
                    }
                    else
                    {
                        for (int j = i; j < DANEMol.at2.Count; j++)
                        {
                            abund = 2 * DANEMol.at1[i].abundancja * DANEMol.at2[j].abundancja;
                            if (DANEMol.at1[i].LM == DANEMol.at2[j].LM)
                            {
                                abund = abund / 2;
                            }
                            if (abund >= DANEMol.progabundancji / 100 || DANEMol.stosujprogabundancji == false)
                            {
                                if (DANEMol.stalaSuma == -1 || DANEMol.at1[i].LM + DANEMol.at2[j].LM == DANEMol.stalaSuma)
                                {
                                    wypiszPrzejsciaInner(i, j);
                                    x++;
                                    pBar1.Value = x;
                                    pBar1.Update();
                                    pBar1.Invalidate();
                                    uwzglednioneizotopomery += DANEMol.at1[i].LM.ToString() + " " + DANEMol.at2[j].LM.ToString() + " abund: " + abund.ToString() + "\r\n";
                                    Thread.Sleep(50);
                                }

                            }
                            else
                            {
                                nieuwzglednioneizotopomery += DANEMol.at1[i].LM.ToString() + " " + DANEMol.at2[j].LM.ToString() + " abund: " + abund.ToString() + "\r\n";
                            }
                        }
                    }

                }
                tbW1.Text = "uwzgledniono \r\n";
                tbW1.Text += uwzglednioneizotopomery;
                tbW1.Text += "\r\n\r\npominięto \r\n";
                tbW1.Text += nieuwzglednioneizotopomery;


            }
            else
            {
                wypiszPrzejsciaInner(tempI1, tempI2);
            }
            if (DANEMol.pokazDodatkoweInfo == true)
            {
                MessageBox.Show("Wyliczono " + DANEMol.tablicaprzejsc.Count.ToString() + " przejść. \r\nPowinno być " + (DANEMol.at1.Count * DANEMol.at2.Count * DANEMol.maxVG).ToString() + " przejsc o sumarycznej abundancji " + (sumarycznaabund * 100).ToString() + "%\r\nlub " + (izot * DANEMol.maxVG).ToString() + " wybranych przejść");
            }
            //blokujOdblokujPrzyciski(true);


        }

        private void wypiszPrzejsciaInner(int i, int j)
        {
            DANEMol.aktywneiat1 = i;
            DANEMol.aktywneiat2 = j;
            DANEMol.poziomyDolne.Clear();
            DANEMol.poziomyGorne.Clear();
            DANEMol.tablicaelementow.Clear();
            inicjalizujAktualneWartosci(DANEMol, LI, i, j);
            if(cbManual.Checked)
            {
                LEVEXE.generujStałeRotacyjne(LI, DANEMol, tbLevIN, tbLevelIN2,tbLevOUT, tbLEVELout2, DGVPrzejscia, DGVPrzejsciaWgKomb,tbLevIN.Text);
            }
            else
            {
            LEVEXE.generujStałeRotacyjne(LI, DANEMol, tbLevIN, tbLevelIN2, tbLevOUT, tbLEVELout2, DGVPrzejscia, DGVPrzejsciaWgKomb);
            }
            tbW1.Text = tbTrans.Text = LEVEXE.strans;
            //tbLevOUT.Text = LEVEXE.LEVELOUT;
            tbCH8.Text = LEVEXE.CH8;
            tbCH9.Text = LEVEXE.CH9;
        }

        private void btPredefABINI_Click(object sender, EventArgs e)
        {

        }

        private void cbAIDol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sciezka = DANEMol.sciezkaPredefAI + "\\" + cbAIDol.SelectedItem.ToString();
            loadAIdol(sciezka);

        }

        private void cbAIGora_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sciezka = DANEMol.sciezkaPredefAI + "\\" + cbAIGora.SelectedItem.ToString();
            loadAIGora(sciezka);
        }



        private void wczytajExperim(string path = null)
        {
            if (path == null)
            {
                if (cbExperiment.Checked == true)
                {
                    openExperim.ShowDialog();
                    DANEMol.readExperim();
                }
                else
                {
                    DANEMol.sciezkaEXPERIM = "";
                }
            }
            else
            {
                DANEMol.sciezkaEXPERIM = path;
                DANEMol.readExperim();
            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void openExperim_FileOk(object sender, CancelEventArgs e)
        {
            DANEMol.sciezkaEXPERIM = openExperim.FileName;
        }

        private void btManualExe_Click(object sender, EventArgs e)
        {
            LEVEXE.odpal(tbLevIN.Text);
            tbLevOUT.Text = LEVEXE.LEVELOUT;
            tbCH8.Text = LEVEXE.CH8;
            tbCH9.Text = LEVEXE.CH9;
        }

        private void cbDManual_CheckedChanged(object sender, EventArgs e)
        {
            tbDDol.Enabled = cbDManualD.Checked;

            DANEMol.recznaglebokoscMD = cbDManualD.Checked;
        }

        private void cbBetaManual_CheckedChanged(object sender, EventArgs e)
        {
            tbBetaD.Enabled = tbBetaG.Enabled = cbBetaManual.Checked;
            DANEMol.recznaBetaMD = DANEMol.recznaBetaMG = cbBetaManual.Checked;
        }

        private void btEditAIGora_Click(object sender, EventArgs e)
        {
            EdycjaPotencjalu EP = new EdycjaPotencjalu(DANEMol.AbiniGora);
            EP.Show();
        }

        private void cbDvsign_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDvsign.Checked == true)
            {
                PG.znakDv = -1;
            }
            else
            {
                PG.znakDv = 1;
            }
        }

        private void cbPodpisy_CheckedChanged(object sender, EventArgs e)
        {
            DANEMol.pokazPodpisy = cbPodpisy.Checked;
            WYKRESL.LEVELwykres(ZG1, DANEMol, jedn);
        }

        private void cbprogabund_SelectedIndexChanged(object sender, EventArgs e)
        {
            double x = double.Parse(cbprogabund.SelectedItem.ToString());
            DANEMol.progabundancji = x;
        }

        private void tbRH_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbMaxVG_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbPokazInfo_CheckedChanged(object sender, EventArgs e)
        {
            DANEMol.pokazDodatkoweInfo = cbPokazInfo.Checked;
        }

        private void cbIzoToSpecies_CheckedChanged(object sender, EventArgs e)
        {
            kombToSpacies = cbIzoToSpecies.Checked;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbJednWykr_SelectedIndexChanged(object sender, EventArgs e)
        {
            jedn = cbJednWykr.SelectedIndex;
        }

        private void cbUwzglH_CheckedChanged(object sender, EventArgs e)
        {
            PG.uwzglH = cbUwzglH.Checked;
        }

        private void tbVD_TextChanged(object sender, EventArgs e)
        {
            DANEMol.VD = int.Parse(tbVD.Text);
            VDselectUpdate();
            cbSekwencjaDV.Items.Clear();
            for (int i = 0; i <= DANEMol.VD; i++)
            {
                cbSekwencjaDV.Items.Add(i);
            }
            cbOnly.Items.Clear();
            for (int i = -DANEMol.VD; i <= DANEMol.VD; i++)
            {
                cbOnly.Items.Add(i);
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show(DANEMol.TBLIST[0].Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProgNatLin.SelectedIndex == 0)
            {
                DANEMol.progNatezenia = 0.001;
            }
            else if (cbProgNatLin.SelectedIndex == 1)
            {
                DANEMol.progNatezenia = 0.0001;
            }
            else if (cbProgNatLin.SelectedIndex == 2)
            {
                DANEMol.progNatezenia = 0.00001;
            }
            else if (cbProgNatLin.SelectedIndex == 3)
            {
                DANEMol.progNatezenia = 0.000001;
            }
            else if (cbProgNatLin.SelectedIndex == 4)
            {
                DANEMol.progNatezenia = 0.01;

            }
            else if (cbProgNatLin.SelectedIndex == 5)
            {
                DANEMol.progNatezenia = 0.004;

            }
            else if (cbProgNatLin.SelectedIndex == 6)
            {
                DANEMol.progNatezenia = 0.002;
            }
            else if (cbProgNatLin.SelectedIndex == 7)
            {
                DANEMol.progNatezenia = 0.0005;
            }
            else if (cbProgNatLin.SelectedIndex == 8)
            {
                DANEMol.progNatezenia = 0.0002;
            }

            if (cbProgNatLin.SelectedIndex > 3)
            {
                MessageBox.Show("wybrany prog natezenia wynosi: " + DANEMol.progNatezenia.ToString());
            }
        }

        private void cbuwzglednijIlinii_CheckedChanged(object sender, EventArgs e)
        {
            DANEMol.uwzglednicImax = cbuwzglednijIlinii.Checked;
        }

        private void btStowrzLJ_Click(object sender, EventArgs e)
        {

            string potencjaLJdol = "";
            string potencjaLJgora = "";

            double Dd = double.Parse(tbDDol.Text);
            double Dg = double.Parse(tbDGora.Text);
            double Rd = double.Parse(tbRD.Text);
            double Rg = double.Parse(tbRG.Text);
            double asD = double.Parse(tbASYMPTD.Text);
            double asG = double.Parse(tbASYMPTG.Text);


            // double temp = Dg * (Math.Pow((Rg / 5.5), 12) - 2 * Math.Pow((Rg /5.5), 6)) + asG;
            //MessageBox.Show(temp.ToString());


            for (double x = 2.0; x < 50; x += 0.25)
            {
                if (x > 15.0)
                {
                    x += 0.75;
                }
                if (x > 30.0)
                {
                    x += 1.75;
                }
                double Eg = Dg * (Math.Pow((Rg / x), 12) - 2 * Math.Pow((Rg / x), 6)) + asG;
                double Ed = Dd * (Math.Pow((Rd / x), 12) - 2 * Math.Pow((Rd / x), 6)) + asD;

                if (Ed < asD + 10000 * Dd)
                {
                    double pozX = x / 0.529177249;
                    potencjaLJdol += pozX.ToString() + "  " + Ed.ToString() + "\r\n";
                }
                if (Eg < asG + 10000 * Dg)
                {
                    double pozX = x / 0.529177249;
                    potencjaLJgora += pozX.ToString() + "  " + Eg.ToString() + "\r\n";
                }
            }
            StreamWriter SWdol = new StreamWriter(DANEMol.sciezkaPredefAI + "\\tempLJdol.txt");
            SWdol.Write(potencjaLJdol);
            SWdol.Close();

            StreamWriter SWgora = new StreamWriter(DANEMol.sciezkaPredefAI + "\\tempLJgora.txt");
            SWgora.Write(potencjaLJgora);
            SWgora.Close();

            chbIsGoraAbIni.Checked = true;
            chbIsDolAbIni.Checked = true;

            cbAIDol.Items.Clear();
            cbAIGora.Items.Clear();
            DirectoryInfo DI = new DirectoryInfo(DANEMol.sciezkaPredefAI);

            foreach (FileInfo FI in DI.GetFiles())
            {
                cbAIDol.Items.Add(FI.ToString());
                cbAIGora.Items.Add(FI.ToString());
            }
            cbAIDol.SelectedItem = "tempLJdol.txt";
            cbAIGora.SelectedItem = "tempLJgora.txt";

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            testmaxabund(DANEMol);
        }

        private void cbSymD_SelectedIndexChanged(object sender, EventArgs e)
        {
            PG.symD = cbSymD.SelectedItem.ToString();
        }

        private void cbSymG_SelectedIndexChanged(object sender, EventArgs e)
        {
            PG.symG = cbSymG.SelectedItem.ToString();
        }

        private void cbSymEnable_CheckedChanged(object sender, EventArgs e)
        {

            PG.symEnable = cbSymD.Enabled = cbSymG.Enabled = cbSymEnable.Checked;


        }

        private void cbSequence_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSequence.Checked)
            {
                cbSekwencjaDV.Enabled = true;
                cbOnlyDV.Checked = false;
            }
            else
            {
                DANEMol.DELTASEKWENCJA = null;
                cbSekwencjaDV.Enabled = false;
            }
        }

        private void cbSekwencjaDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            DANEMol.DELTASEKWENCJA = int.Parse(cbSekwencjaDV.SelectedItem.ToString());
        }

        private void cbOnlyOneVd_CheckedChanged(object sender, EventArgs e)
        {
            DANEMol.tylkoJednoVd = cbOnlyOneVd.Checked;
        }

        private void tbT_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSpOrbD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PG.SpinOrbitaD = tbSpOrbD.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z wczytaniem stałej Spin-Orbita stanu dolnego: " + ex.Message, "PROBLEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSpOrbG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PG.SpinOrbitaG = tbSpOrbG.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z wczytaniem stałej Spin-Orbita stanu górnego: " + ex.Message, "PROBLEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbDmanualG_CheckedChanged(object sender, EventArgs e)
        {

            tbDGora.Enabled = cbDmanualG.Checked;
            DANEMol.recznaglebokoscMG = cbDmanualG.Checked;
        }





        private void button1_Click_4(object sender, EventArgs e)
        {
            TS = new ThreadStart(wypiszPrzejscia);
            wypiszPrzejsciaT = new Thread(TS);

            wypiszPrzejsciaT.Start();
            wypiszPrzejsciaT.Join();
            kalkulujRozkladBoltzmana();
            Thread.Sleep(100);
            generujPlikWsadowyPgophera();
            Thread.Sleep(100);
            WYKRESL.LEVELwykres(ZG1, DANEMol, jedn);
            WYKRESL.RYSUJPOTENCJALYABINI(cbProgNatezenia, DANEMol, tbPotencjal);
            WYKRESL.RYSUJPOZIOMYVG(ZG3, DANEMol, true);
            tbWIBRLEVELS.Text = DANEMol.wypiszPoziomyWibracyjneGorne();
            Thread.Sleep(100);
            Bitmap obrazek = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(obrazek as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, obrazek.Size);
            savePSiPGOPH.ShowDialog();
            if (sciezkaZapisu != null)
            {
                obrazek.Save(sciezkaZapisu + ".png", System.Drawing.Imaging.ImageFormat.Png);
                StreamWriter SW = new StreamWriter(sciezkaZapisu + ".pgo");
                SW.Write(tbPgopherIn.Text);
                SW.Close();
            }
        }

        private void savePSiPGOPH_FileOk(object sender, CancelEventArgs e)
        {
            sciezkaZapisu = savePSiPGOPH.FileName;
        }

        private void setOpisOkna()
        {
            string opis = "LevelToPgopher 7.5 (LEVEL 16)    ";
            if (cbDipol.Checked) opis += " MomDip:  " + OFDIPOL.FileName;
            if (chbIsGoraAbIni.Checked) opis += "    Points:  " + OFDAbIniG.FileName;
            this.Text = opis;

        }

        private void cbDipol_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDipol.Checked)
            {
                LI.Dipol = true;
                OFDIPOL.ShowDialog();
                PG.dipol = true;
                DANEMol.dipol = true;

            }
            else
            {
                LI.Dipol = false;
                PG.dipol = false;
                DANEMol.dipol = false;
            }
            setOpisOkna();
        }

        private void OFDIPOL_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                LoadDipol(OFDIPOL.FileName);
                rysujMD(LI.dipoleTemp);
            }
            catch
            {
                MessageBox.Show("Wystąpił Problem z wczytaniem momentów dipolowych");
            }
        }

        private void rysujMD(PointPairList tempPPL)
        {
            ZGMD.GraphPane.CurveList.Clear();
            ZGMD.GraphPane.AddCurve("", tempPPL, Color.Red, SymbolType.Circle);
            ZGMD.GraphPane.AxisChange();
            ZGMD.GraphPane.Title.Text = "Moment Dipolowy";
            ZGMD.Update();
            ZGMD.Invalidate();
        }

        private PointPairList LoadDipol(string sciezka)
        {

            StreamReader SR = new StreamReader(sciezka);
            PointPairList PPL = new PointPairList();
            string linia = SR.ReadLine();
            string[] tab = Regex.Split(linia, @"[\s]+");
            double FX = double.Parse(tab[0]);
            double FY = double.Parse(tab[1]);
            LI.RDIPfact = FX;
            LI.MomDipfact = FY;
            linia = SR.ReadLine();
            //string temp = "";
            int ile = 0;

            while (linia != null)
            {
                string[] tabliczka = Regex.Split(linia, @"[\s]+");
                double R = LI.dipoleOffset + double.Parse(tabliczka[0]);
                double M = double.Parse(tabliczka[1]);
                PPL.Add(R, M);
                ile++;
                linia = SR.ReadLine();
            }
            LI.dipol0 = PPL;
            LI.updateOffsetDipol();
            LI.NRFN = ile;
            SR.Close();
            return PPL;

        }

        private void cbOnlyDV_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOnlyDV.Checked)
            {
                cbSequence.Checked = false;
                cbOnly.Enabled = true;
            }
            else
            {
                cbOnly.Enabled = false;
                DANEMol.ONLYDV = null;
            }
        }

        private void cbOnly_SelectedIndexChanged(object sender, EventArgs e)
        {
            DANEMol.ONLYDV = int.Parse(cbOnly.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void btSaveCfg_Click(object sender, EventArgs e)
        {
            saveConfig.ShowDialog();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            OFD.ShowDialog();
        }

        private void OFD_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader SR = new StreamReader(OFD.FileName);
            string x = SR.ReadToEnd();
            readSettings(x);
            SR.Close();
        }

        private void cbExperiment_Click(object sender, EventArgs e)
        {
            wczytajExperim();
        }

        private void cbExperiment_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbWaveForms_CheckedChanged(object sender, EventArgs e)
        {
            DANEMol.liczFunkcjeFalowe = cbWaveForms.Checked;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DirectoryInfo DI = new DirectoryInfo(Application.StartupPath);
            DI = DI.Parent.Parent;           
            string sciezkach10 = Path.Combine(DI.FullName, "DANE\\level\\fort.10"); 
            tbCh10.Clear();
            comboWF.Items.Clear();

            if (File.Exists(sciezkach10))
            {
                StreamReader SR = new StreamReader(sciezkach10);
                string ch10 = SR.ReadToEnd();
                tbCh10.Text = ch10;
                obrobCh10(ch10);
                SR.Close();
            }

        }
        public List<WaveForm> spisWF;

        private string showWF(WaveForm WF)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < WF.spisPunktow.Count; i++)
            {
                SB.Append(WF.spisPunktow[i].X.ToString() + " " + WF.spisPunktow[i].Y.ToString() + "\r\n");
            }
            return SB.ToString();
        }

        private void obrobCh10(string tekst)
        {
            comboWF.Items.Clear();

            spisWF = new List<WaveForm>();
            tekst = Regex.Replace(tekst, "D", "E");
            string[] tab = Regex.Split(tekst, "\r\n");
            for (int i = 0; i < tab.Length; i++)
            {
                string akt = tab[i].Trim();
                if (akt.Contains("Level"))
                {
                    
                    string[] tabliczka = Regex.Split(akt, @"[\s]+");
                    WaveForm WF = new WaveForm(int.Parse(tabliczka[2]), int.Parse(tabliczka[4]), double.Parse(tabliczka[6]));
                    i = i + 2;
                    akt = tab[i].Trim();
                    
                    while (akt.Length > 0)
                    {
                        tabliczka = Regex.Split(akt, @"[\s]+");
                        for (int j = 0; j < tabliczka.Length; j = j + 2)
                        {
                            ZedGraph.PointD punkt = new ZedGraph.PointD(double.Parse(tabliczka[j]), double.Parse(tabliczka[j + 1]));
                            WF.spisPunktow.Add(punkt);
                        }
                        i++;
                        akt = tab[i].Trim();
                    }
                    if (!spisWF.Contains(WF))
                    {
                        spisWF.Add(WF);
                    }

                }

            }
            for (int i = 0; i < spisWF.Count; i++)
            {
                string descr = spisWF[i].v.ToString() + " " + spisWF[i].Energia.ToString();

                comboWF.Items.Add(descr);
            }

        }

        private void comboWF_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCh10.Text = showWF(spisWF[comboWF.SelectedIndex]);
            plotWF(spisWF[comboWF.SelectedIndex]);
        }

        private void plotWF(WaveForm WF)
        {
            ZGWF.GraphPane.CurveList.Clear();
            ZedGraph.PointPairList PPL = new ZedGraph.PointPairList();
            for (int i = 0; i < WF.spisPunktow.Count; i++)
            {
                PPL.Add(WF.spisPunktow[i].X, WF.spisPunktow[i].Y);
            }
            ZGWF.GraphPane.AddCurve("", PPL, Color.Red, ZedGraph.SymbolType.None);
            ZGWF.AxisChange();
            ZGWF.Update();
            ZGWF.Invalidate();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SWF.ShowDialog();
        }

        private void SWF_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter SW = new StreamWriter(SWF.FileName);
            SW.Write(tbCh10.Text);
            SW.Close();
        }

        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OFDOcenEksperyment.ShowDialog();
        }

        private void OFDeksperyment_FileOk(object sender, CancelEventArgs e)
        {
            OSYM = new ocenaSymulacji(cbVOD, cbVDO, cbOrderBy, liboxIso);
            OSYM.wczytaj(OFDOcenEksperyment.FileName);
            tbOcena.Text = OSYM.wypiszLinieEksp();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {

            OSYM.wybierz();
            tbOcena.Text = OSYM.wypiszLinieSpisPodreczny();

        }

        private void btSavePoziomy_Click(object sender, EventArgs e)
        {
            SFpoziomy.ShowDialog();
        }

        private void btCorelCoeff_Click(object sender, EventArgs e)
        {
            tbOcena.Text += "\r\n\r\n" + OSYM.ocen(DANEMol);
        }

        private void tbMomDipRoffset_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(tbMomDipRoffset.Text, out LI.dipoleOffset);
            LI.updateOffsetDipol();
            rysujMD(LI.dipoleTemp);
        }

        private void cbOODR_CheckedChanged(object sender, EventArgs e)
        {
            PgopherGenerator.OODR = cbOODR.Checked;
        }

    }
}
  