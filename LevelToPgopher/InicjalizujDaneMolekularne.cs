using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace LevelToPgopher
{
    partial class GlowneOkno
    {
        bool WSZYSTKIEIZOTOPY = false;
        int tempI1=0;
        int tempI2=0;
        private void inicjalizujWartosciDomyslne(int zestaw,daneMolekularne DaneMol,LevelINPUT LINPUT,int itA,int itB)
        {
            cbAIDol.Items.Clear();
            cbAIGora.Items.Clear();
            //cbAIDol.Items.Add("Inne");
            //cbAIGora.Items.Add("Inne");

            DirectoryInfo DIAI = new DirectoryInfo(Application.StartupPath);
            DIAI = DIAI.Parent.Parent;

            if (zestaw == 0)  //CdAr B-E mors
            {
                tbOED.Text = "11.1";
                tbOXED.Text = "0.56";
                tbRD.Text = "5.01";
                tbOEG.Text = "4.15";
                tbOXEG.Text = "0.225";
                tbRG.Text = "7.63";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51484";
                tbVD.Text = "1";
                tbMaxVG.Text = "10";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyArgonu;
                

                
            }
            if (zestaw == 1) //CdAr X-B mors
            {
                tbOED.Text = "19.8";
                tbOXED.Text = "0.93";
                tbRD.Text = "4.31";
                tbOEG.Text = "11.1";
                tbOXEG.Text = "0.56";
                tbRG.Text = "5.01";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyArgonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;
            }
            if (zestaw == 2) //CdAr X-A mors
            {
                tbOED.Text = "19.8";
                tbOXED.Text = "0.93";
                tbRD.Text = "4.31";
                tbOEG.Text = "39.2";
                tbOXEG.Text = "1.22";
                tbRG.Text = "3.51";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyArgonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;
               
            }

            if (zestaw ==3) //CdAr X-B mors AB-INI
            {
                tbOED.Text = "19.8";
                tbOXED.Text = "0.93";
                tbRD.Text = "4.31";
                tbOEG.Text = "11.3";
                tbOXEG.Text = "0.59";
                tbRG.Text = "5.01";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyArgonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                
                

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdAr");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 5) //CdKr Ab Ini X-B
            {
                tbOED.Text = "18.1";
                tbOXED.Text = "0.5";
                tbRD.Text = "4.27";
                tbOEG.Text = "0";
                tbOXEG.Text = "0";
                tbRG.Text = "0";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKryptonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdKr");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach(FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
                
            }



            if (zestaw == 6) //CdXe Ab Ini X-B
            {
                tbOED.Text = "20.0";
                tbOXED.Text = "0.51";
                tbRD.Text = "4.21";
                tbOEG.Text = "18.3";
                tbOXEG.Text = "0.37";
                tbRG.Text = "4.26";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "30";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKsenonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdXe");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }

            }

            if (zestaw == 7) //test dla malej liczby izotopow CdAr X-B
            {
                tbOED.Text = "19.8";
                tbOXED.Text = "0.93";
                tbRD.Text = "4.31";
                tbOEG.Text = "11.3";
                tbOXEG.Text = "0.59";
                tbRG.Text = "5.01";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                DaneMol.at1 = czesteizotopyKadmu;
                DaneMol.at2 = izotopyArgonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;
            }



            if (zestaw == 8)  //cdkr czeste izotopy
            {
                
                    tbOED.Text = "18.1";
                    tbOXED.Text = "0.5";
                    tbRD.Text = "4.27";
                    tbOEG.Text = "0";
                    tbOXEG.Text = "0";
                    tbRG.Text = "0";
                    tbASYMPTD.Text = "0.0";
                    tbASYMPTG.Text = "30656.13";
                    tbVD.Text = "0";
                    tbMaxVG.Text = "10";
                    DaneMol.at1 = czesteizotopyKadmu;
                    DaneMol.at2 = czesteizotopyKryptonu;
                    cbLAMBDAD.SelectedIndex = 0;
                    cbLAMBDAG.SelectedIndex = 2;

                    DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdKr");
                    DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                    foreach (FileInfo FI in DI.GetFiles())
                    {
                        cbAIDol.Items.Add(FI.ToString());
                        cbAIGora.Items.Add(FI.ToString());
                    }

                
            }


            if (zestaw == 9) //  Cd2 X-b
            {
                tbOED.Text = "21.4";
                tbOXED.Text = "0.35";
                tbRD.Text = "3.78";
                tbOEG.Text = "18.7";
                tbOXEG.Text = "0.34";
                tbRG.Text = "4.02";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "20";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKadmu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Cd2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 10) //  Cd2 X-A^10_u
            {
                tbOED.Text = "21.4";
                tbOXED.Text = "0.35";
                tbRD.Text = "3.78";
                tbOEG.Text = "100.5";
                tbOXEG.Text = "0.325";
                tbRG.Text = "3.03";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "43692.474";
                tbVD.Text = "0";
                tbMaxVG.Text = "41";
                tbMinVg.Text = "40";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKadmu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 0;
                tbSgora.Text = "0";
                cbLAMBDAG.SelectedIndex = 0;
                cbOmegaSelectDol.SelectedIndex=1;
                cbOmegaSelectGora.SelectedIndex = 1;
                

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Cd2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 11) //  CdNe
            {
                tbOED.Text = "15";
                tbOXED.Text = "1.94";
                tbRD.Text = "4.32";
                tbOEG.Text = "6.5";
                tbOXEG.Text = "1.1";
                tbRG.Text = "5.12";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "4";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyNeonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Cd2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 12) //  CdHe
            {
                tbOED.Text = "9.6";
                tbOXED.Text = "1.63";
                tbRD.Text = "4.6";
                tbOEG.Text = "4.7";
                tbOXEG.Text = "0.71";
                tbRG.Text = "5.3";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "5";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyHelu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdHe");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }



            if (zestaw == 13) //  Hg2
            {
                tbOED.Text = "19.6";
                tbOXED.Text = "0.26";
                tbRD.Text = "3.63";
                tbOEG.Text = "40.2";
                tbOXEG.Text = "0.18";
                tbRG.Text = "3.38";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "44042.977";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyRteci;
                DaneMol.at2 = izotopyRteci;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Hg2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 14) //  Hg2 X-D
            {
                tbOED.Text = "19.75";
                tbOXED.Text = "0.257";
                tbRD.Text = "3.605";
                tbOEG.Text = "127";
                tbOXEG.Text = "0.5";
                tbRG.Text = "2.5";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "39412.2764707746";
                tbVD.Text = "0";
                tbMaxVG.Text = "80";
                tbMinVg.Text = "40";
                DaneMol.at1 = izotopyRteci;
                DaneMol.at2 = izotopyRteci;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Hg2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 15) //CdKr B - E
            {
                tbOED.Text = "9.3";
                tbOXED.Text = "0.2";
                tbRD.Text = "4.97";
                tbOEG.Text = "3.1";
                tbOXEG.Text = "0.01";
                tbRG.Text = "5.9";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51484";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKryptonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdKr");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }

            }

            if (zestaw == 16) //  Hg2 X-G
            {
                tbOED.Text = "19.75";
                tbOXED.Text = "0.257";
                tbRD.Text = "3.605";
                tbOEG.Text = "79.0";
                tbOXEG.Text = "0.22";
                tbRG.Text = "3.0";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "54068.78";
                tbVD.Text = "0";
                tbMaxVG.Text = "60";
                tbMinVg.Text = "20";
                DaneMol.at1 = izotopyRteci;
                DaneMol.at2 = izotopyRteci;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Hg2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 17) //  Zn2
            {
                tbOED.Text = "19.75";
                tbOXED.Text = "0.69";
                tbRD.Text = "4.19";
                tbOEG.Text = "79.0";
                tbOXEG.Text = "0.69";
                tbRG.Text = "4.19";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "54068.78";
                tbVD.Text = "0";
                tbMaxVG.Text = "20";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyCynku;
                DaneMol.at2 = izotopyCynku;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Zn2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 18) //  Yb2 D-X
            {
                tbOED.Text = "22";
                tbOXED.Text = "0.1974";
                tbRD.Text = "4.653";
                tbOEG.Text = "56";
                tbOXEG.Text = "0.2492";
                tbRG.Text = "3.978";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "17992";
                tbVD.Text = "0";
                tbMaxVG.Text = "40";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyIterbu;
                DaneMol.at2 = izotopyIterbu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 0;
                tbSgora.Text = "1";
                cbSymEnable.Checked = true;
                cbSymD.SelectedIndex = 0;
                cbSymG.SelectedIndex = 1;
                cbTypTrans.SelectedIndex = 2;
                cbOmegaSelectDol.SelectedIndex = 1;
                cbOmegaSelectGora.SelectedIndex = 2;

                
                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Yb2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 19) //  Yb2 F-X
            {
                tbOED.Text = "22";
                tbOXED.Text = "0.1974";
                tbRD.Text = "4.653";
                tbOEG.Text = "26";
                tbOXEG.Text = "0.3492";
                tbRG.Text = "4.611";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "17992";
                tbVD.Text = "0";
                tbMaxVG.Text = "40";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyIterbu;
                DaneMol.at2 = izotopyIterbu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;
                tbSgora.Text = "1";
                cbSymEnable.Checked = true;
                cbSymD.SelectedIndex = 0;
                cbSymG.SelectedIndex = 1;
                cbTypTrans.SelectedIndex = 2;
                cbOmegaSelectDol.SelectedIndex = 1;
                cbOmegaSelectGora.SelectedIndex = 2;


                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Yb2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 20)  //I2
            {
                tbOED.Text = "125.67";
                tbOXED.Text = "0.7504";
                tbRD.Text = "3.02";
                tbOEG.Text = "214.53";
                tbOXEG.Text = "0.613";
                tbRG.Text = "2.66";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "100000";
                tbVD.Text = "0";
                tbMaxVG.Text = "70";
                DaneMol.at1 = izotopyJodu;
                DaneMol.at2 = izotopyJodu;
            }
            if (zestaw == 21)  //I2
            {
                tbOED.Text = "214.53";
                tbOXED.Text = "0.613";
                tbRD.Text = "2.66";
                tbOEG.Text = "125.67";
                tbOXEG.Text = "0.7504";
                tbRG.Text = "3.02";
                tbASYMPTD.Text = "12654.4";
                tbASYMPTG.Text = "20257.38";
                tbVD.Text = "0";
                tbMaxVG.Text = "60";
                DaneMol.at1 = izotopyJodu;
                DaneMol.at2 = izotopyJodu;
                cbDManualD.Checked = cbDmanualG.Checked = true;
                tbDDol.Text = "12547";
                tbDGora.Text = "4381";


            }

            if (zestaw == 22)  //CdAr E-A
            {
                tbOED.Text = "39.2";
                tbOXED.Text = "1.22";
                tbRD.Text = "3.51";
                tbOEG.Text = "106.5";
                tbOXEG.Text = "2.16";
                tbRG.Text = "2.85";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51484";
                tbVD.Text = "0";
                tbMaxVG.Text = "20";
                DaneMol.at1 = izotopyArgonu;
                DaneMol.at2 = izotopyKadmu;
                cbDManualD.Checked = cbDmanualG.Checked = true;
                tbDDol.Text = "314.9";
                tbDGora.Text = "1312.8";
                tbVD.Text = "6";
                cbLAMBDAD.SelectedIndex = 2;
                cbLAMBDAG.SelectedIndex = 0;
                tbSdol.Text = "1";
                tbSgora.Text = "1";
                cbOmegaSelectDol.SelectedIndex = 1;
                cbOmegaSelectGora.SelectedIndex = 2;
                cbOnlyOneVd.Checked = true;
                cbDManualD.Checked = false;
                cbDmanualG.Checked = false;
                cbBetaManual.Checked = false;
            }

            if (zestaw == 23)  //CdKr A-X
            {
                tbOED.Text = "18.1";
                tbOXED.Text = "0.5";
                tbRD.Text = "4.27";
                tbOEG.Text = "36.95";
                tbOXEG.Text = "0.615";
                tbRG.Text = "3.34";
                tbASYMPTD.Text = "0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "20";
                DaneMol.at1 = izotopyKryptonu;
                DaneMol.at2 = izotopyKadmu;
                //cbDmanualG.Checked = true;
                //tbDDol.Text = "314.9";
                

                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;
                tbSgora.Text = "1";
                cbSymEnable.Checked = false;
                
                cbTypTrans.SelectedIndex = 2;
                cbOmegaSelectDol.SelectedIndex = 0;
                cbOmegaSelectGora.SelectedIndex = 0;


            }

            if (zestaw == 24)  //CdKr E-A
            {
                tbOED.Text = "36.95";
                tbOXED.Text = "0.615";
                tbRD.Text = "3.34";
                tbOEG.Text = "90.97";
                tbOXEG.Text = "1.374";
                tbRG.Text = "2.99";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51484";
                tbVD.Text = "0";
                tbMaxVG.Text = "20";
                DaneMol.at1 = izotopyKryptonu;
                DaneMol.at2 = izotopyKadmu;
                //cbDmanualG.Checked = true;
                //tbDDol.Text = "314.9";


                
                tbVD.Text = "9";
          

                cbLAMBDAD.SelectedIndex = 2;
                cbLAMBDAG.SelectedIndex = 0;
                tbSdol.Text = "1";
                tbSgora.Text = "1";
                cbOmegaSelectDol.SelectedIndex = 1;
                cbOmegaSelectGora.SelectedIndex = 2;
                cbOnlyOneVd.Checked = true;
                cbDManualD.Checked = false;
                cbDmanualG.Checked = false;
                cbBetaManual.Checked = false;

            }

            if (zestaw == 25) //  Hg2 X-F cool
            {
                tbOED.Text = "18.67";
                tbOXED.Text = "0.2";
                tbRD.Text = "3.645";
                tbOEG.Text = "19.75";
                tbOXEG.Text = "0.257";
                tbRG.Text = "3.605";
                tbASYMPTD.Text = "50000";
                tbASYMPTG.Text = "0";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyRteci;
                DaneMol.at2 = izotopyRteci;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Hg2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 26) //  Cd2 c-X cool
            {
                tbOED.Text = "27.2";
                tbOXED.Text = "0.256";
                tbRD.Text = "3.86";
                tbOEG.Text = "21.4";
                tbOXEG.Text = "0.35";
                tbRG.Text = "3.76";
                tbASYMPTD.Text = "31826.952";
                tbASYMPTG.Text = "0";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKadmu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\Cd2");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 27) //  ZnAr trans
            {
                tbOED.Text = "10";
                tbOXED.Text = "0.2";
                tbRD.Text = "3";
                tbOEG.Text = "20";
                tbOXEG.Text = "0.3";
                tbRG.Text = "4";
                tbASYMPTD.Text = "25000";
                tbASYMPTG.Text = "0";
                tbVD.Text = "0";
                tbMaxVG.Text = "10";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyCynku;
                DaneMol.at2 = izotopyArgonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\ZnAr");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }



            if (zestaw == 28) //  Cd2 cos-b trans
            {
                tbOED.Text = "18.4";
                tbOXED.Text = "0.327";
                tbRD.Text = "4.05";
                tbOEG.Text = "168.6";
                tbOXEG.Text = "1.19";
                tbRG.Text = "2.84";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51382";
                tbVD.Text = "2";
                cbOnlyOneVd.Checked = true;
                tbMaxVG.Text = "61";
                tbMinVg.Text = "46";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKadmu;
                cbLAMBDAD.SelectedIndex = 2;
                cbLAMBDAG.SelectedIndex = 0;
                tbSdol.Text = "1";
                tbSgora.Text = "1";
                cbSymD.SelectedIndex = 1;
                cbSymG.SelectedIndex = 0;
                cbSymEnable.Checked = true;
                cbTypTrans.SelectedIndex = 2;
                cbOmegaSelectDol.SelectedIndex = 1;
                cbOmegaSelectGora.SelectedIndex = 2;
                

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\ZnAr");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 29) //  CdNe A-X cos-b trans
            {
                tbOED.Text = "24.9";
                tbOXED.Text = "2.2";
                tbRD.Text = "3.76";
                tbOEG.Text = "56.6";
                tbOXEG.Text = "8.8";
                tbRG.Text = "3.21";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51484";
                tbVD.Text = "2";
                cbOnlyOneVd.Checked = true;
                tbMaxVG.Text = "5";
                tbMinVg.Text = "0";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyNeonu;
                cbLAMBDAD.SelectedIndex = 2;
                cbLAMBDAG.SelectedIndex = 0;
                tbSdol.Text = "1";
                tbSgora.Text = "1";
                cbSymD.SelectedIndex = 1;
                cbSymG.SelectedIndex = 0;
                cbSymEnable.Checked = true;
                cbTypTrans.SelectedIndex = 2;
                cbOmegaSelectDol.SelectedIndex = 1;
                cbOmegaSelectGora.SelectedIndex = 2;


                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\ZnAr");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 30) //CdXe X-B Morse
            {
                tbOED.Text = "33.1";
                tbOXED.Text = "0.99";
                tbRD.Text = "4.21";
                tbOEG.Text = "18.3";
                tbOXEG.Text = "0.37";
                tbRG.Text = "4.26";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "30";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKsenonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdXe");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }

            }

            if (zestaw == 31) //CdXe X-A Morse
            {
                tbOED.Text = "33.1";
                tbOXED.Text = "0.99";
                tbRD.Text = "4.21";
                tbOEG.Text = "52.3";
                tbOXEG.Text = "0.60";
                tbRG.Text = "3.02";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "30";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyKsenonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdXe");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            if (zestaw == 32) //CdNe X-A Morse
            {
                tbOED.Text = "13.2";
                tbOXED.Text = "1.15";
                tbRD.Text = "4.26";
                tbOEG.Text = "22.6";
                tbOXEG.Text = "1.60";
                tbRG.Text = "3.62";
                tbASYMPTD.Text = "0.0";
                tbASYMPTG.Text = "30656.13";
                tbVD.Text = "0";
                tbMaxVG.Text = "6";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyNeonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdXe");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }


            if (zestaw == 33) //CdNe E-A Breck
            {
                tbOED.Text = "22.6";
                tbOXED.Text = "1.60";
                tbRD.Text = "3.62";
                tbOEG.Text = "56.6";
                tbOXEG.Text = "8.8";
                tbRG.Text = "3.21";
                tbASYMPTD.Text = "30656.13";
                tbASYMPTG.Text = "51484";
                tbVD.Text = "0";
                tbMaxVG.Text = "6";
                DaneMol.at1 = izotopyKadmu;
                DaneMol.at2 = izotopyNeonu;
                cbLAMBDAD.SelectedIndex = 0;
                cbLAMBDAG.SelectedIndex = 2;

                DaneMol.sciezkaPredefAI = Path.Combine(DIAI.FullName, "DANE\\potencjalyAbIni\\CdXe");
                DirectoryInfo DI = new DirectoryInfo(DaneMol.sciezkaPredefAI);

                foreach (FileInfo FI in DI.GetFiles())
                {
                    cbAIDol.Items.Add(FI.ToString());
                    cbAIGora.Items.Add(FI.ToString());
                }
            }

            inicjalizujCbSelectIzotopolog(DaneMol);   
            inicjalizujAktualneWartosci(DaneMol,LINPUT,itA,itB);            

            
        }

        



        private void inicjalizujCbSelectIzotopolog(daneMolekularne  DaneMol)
        {
            spisIzotopomerow listaizotop = new spisIzotopomerow();
            double sumabu = 0.0;
            double currentMaxAbund = DaneMol.at1[0].abundancja * DaneMol.at2[0].abundancja;
            int iter=0;
            int maxidx=0;
            DaneMol.maxizat1 = 0;
            DaneMol.maxizat2 = 0;
            textBox1.Text = "";
            cbSelectIzotopolog.Items.Clear();
            for (int i = 0; i < DaneMol.at1.Count; i++)
            {
                if (DaneMol.at1[0].LA != DaneMol.at2[0].LA)
                {
                    for (int j = 0; j < DaneMol.at2.Count; j++)
                    {
                        int x = DaneMol.at1[i].LM;
                        int y = DANEMol.at2[j].LM;
                        
                        double abund = DaneMol.at1[i].abundancja * DaneMol.at2[j].abundancja;                       
                        if (abund > currentMaxAbund)
                        {
                            currentMaxAbund = abund;
                            maxidx = iter;
                            DaneMol.maxizat1 = i;
                            DaneMol.maxizat2 = j;
                        }
                        sumabu += abund;
                        cbSelectIzotopolog.Items.Add(x.ToString() + "-" + y.ToString() + "   (abund " + (abund * 100).ToString("0.000") + "%)");
                        iter++;
                        //textBox1.Text += x.ToString() + "-" + y.ToString() + "   (abund " + (abund * 100).ToString("0.000") + "%)\r\n";
                        listaizotop.spis.Add(new izotopomer(x,y,abund));
                    }
                  

                }
                      
                else
                {
                    for (int j = i; j < DaneMol.at2.Count; j++)
                    {
                        int x = DaneMol.at1[i].LM;
                        int y = DANEMol.at2[j].LM;

                        double abund = 2 * DaneMol.at1[i].abundancja * DaneMol.at2[j].abundancja;
                        if (DaneMol.at1[i].LM == DaneMol.at2[j].LM && DaneMol.at1[i].LA == DaneMol.at2[j].LA)
                        {
                            abund = abund/2 ;
                        }
                        if (abund > currentMaxAbund)
                        {
                            currentMaxAbund = abund;
                            maxidx = iter;
                            DaneMol.maxizat1 = i;
                            DaneMol.maxizat2 = j;
                        }
                        sumabu += abund;
                        cbSelectIzotopolog.Items.Add(x.ToString() + "-" + y.ToString() + "   (abund " + (abund * 100).ToString("0.000") + "%)");
                        //textBox1.Text += x.ToString() + "-" + y.ToString() + "   (abund " + (abund * 100).ToString("0.000") + "%)\r\n";
                        listaizotop.spis.Add(new izotopomer(x,y,abund));
                        iter++;
                        
                    }
                }
                 
            }
               
            List<string> tempspis = listaizotop.wypiszkombinacje();
            for (int i = 0; i < tempspis.Count; i++)
            {
                cbSelectIzotopolog.Items.Add(tempspis[i]);
            }

            
            textBox1.Text += listaizotop.wypisz();

            cbSelectIzotopolog.SelectedIndex = maxidx;

            cbSelectIzotopolog.Items.Add("ALL");

            cbSelectIzotopolog.Items.Add("ALL + threshold");

            cbSelectIzotopolog.Items.Add("Select Manually");

            //MessageBox.Show(sumabu.ToString());
        }

        private void analizujCbSelectIzotopolog(daneMolekularne daneMol)
        {
            if (cbSelectIzotopolog.SelectedItem.ToString() == "ALL")
            {
                WSZYSTKIEIZOTOPY = true;
                daneMol.stosujprogabundancji = false;
            }
            else if (cbSelectIzotopolog.SelectedItem.ToString() == "ALL + threshold")
            {
                WSZYSTKIEIZOTOPY = true;
                daneMol.stosujprogabundancji = true;
            }
            else if (cbSelectIzotopolog.SelectedItem.ToString() == "Select Manually")
            {
                SelectIsotopologue SelIzo = new SelectIsotopologue(daneMol);

                SelIzo.Show();
            }
            else
            {
                WSZYSTKIEIZOTOPY = false;
                string text = cbSelectIzotopolog.SelectedItem.ToString();
                if (Regex.IsMatch(text, @"komb"))
                {
                    text = Regex.Replace(text, "komb", "");
                    DANEMol.stalaSuma = int.Parse(text);
                    WSZYSTKIEIZOTOPY = true;
                    daneMol.stosujprogabundancji = false;
                }
                else
                {
                    daneMol.stalaSuma = -1;
                    string[] tab1 = Regex.Split(text, " +");
                    string[] tab2 = Regex.Split(tab1[0], "-");
                    int x1 = int.Parse(tab2[0]);
                    int x2 = int.Parse(tab2[1]);
                    for (int i = 0; i < daneMol.at1.Count; i++)
                    {
                        if (daneMol.at1[i].LM == x1)
                        {
                            tempI1 = i;
                            daneMol.aktywneiat1 = i;
                            break;
                        }
                    }
                    for (int j = 0; j < daneMol.at2.Count; j++)
                    {
                        if (daneMol.at2[j].LM == x2)
                        {
                            tempI2 = j;
                            daneMol.aktywneiat2 = j;
                            break;
                        }
                    }
                }
            }





        }

        private void inicjalizujAktualneWartosci(daneMolekularne DaneMol, LevelINPUT LINPUT, int itA, int itB)
        {
            try
            {
                DaneMol.OED = double.Parse(tbOED.Text);
                DaneMol.OXED = double.Parse(tbOXED.Text);
                DaneMol.RD = double.Parse(tbRD.Text);
                DaneMol.OEG = double.Parse(tbOEG.Text);
                DaneMol.OXEG = double.Parse(tbOXEG.Text);
                DaneMol.RG = double.Parse(tbRG.Text);
                DaneMol.ASYMPTD = double.Parse(tbASYMPTD.Text);
                DaneMol.AsymptG = double.Parse(tbASYMPTG.Text);
                DaneMol.VD = int.Parse(tbVD.Text);
                DaneMol.maxVG = int.Parse(tbMaxVG.Text);
                DaneMol.minVg = int.Parse(tbMinVg.Text);

                DaneMol.liczGlebMorsaDolnego();
                DaneMol.liczGlebMorsaGornego();

                LINPUT.J2DD = int.Parse(tbJ2DD.Text);
                LINPUT.J2DL = int.Parse(tbJ2DL.Text);
                LINPUT.J2DU = int.Parse(tbJ2DU.Text);
                LINPUT.RH = double.Parse(tbRH.Text);
                LINPUT.RMIN = double.Parse(tbRmin.Text);
                LINPUT.RMAX = double.Parse(tbRmax.Text);
                LINPUT.EPS = double.Parse(tbEPS.Text);
                

                LINPUT.BETA1 = DaneMol.betaDol;
                LINPUT.BETA2 = DaneMol.betaGora;
                LINPUT.DSCM1 = DaneMol.glebokoscDol;
                LINPUT.DSCM2 = DaneMol.glebokoscGora;
                LINPUT.NLEV1 = DaneMol.VD;
                LINPUT.NLEV2 = DaneMol.maxVG;
                LINPUT.VLIM1 = DaneMol.ASYMPTD;
                LINPUT.VLIM2 = DaneMol.AsymptG;

                LINPUT.REQ1 = DaneMol.RD;
                LINPUT.REQ2 = DaneMol.RG;
                if (DaneMol.liczFunkcjeFalowe == true)
                {
                    LINPUT.LPRWF = -1;
                }
                else
                {
                    LINPUT.LPRWF = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Wystąpił problem z inicjalizacją danych: " + ex.Message, "PROBLEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LINPUT.IAN1 = DaneMol.at1[itA].LA;
            LINPUT.IAN2 = DaneMol.at2[itB].LA;
            LINPUT.IMN1 = DaneMol.at1[itA].LM;
            LINPUT.IMN2 = DaneMol.at2[itB].LM;
            LINPUT.spinJ1 = DaneMol.at1[itA].spinJadra;
            LINPUT.spinJ2 = DaneMol.at2[itB].spinJadra;
            

            double abund=0;
            abund = DaneMol.at1[itA].abundancja * DaneMol.at2[itB].abundancja;
           // MessageBox.Show(DaneMol.at1[itA].abundancja.ToString() + " " + DaneMol.at2[itB].abundancja.ToString());
            /*if (DaneMol.at1[0].LA != DaneMol.at2[0].LA)
            {
                
            }
            else if (DaneMol.at1[0].LA == DaneMol.at2[0].LA)
            {
                if (DaneMol.at1[itA].LM == DaneMol.at2[itB].LM)
                {
                    abund = 2 * DaneMol.at1[itA].abundancja * DaneMol.at2[itB].abundancja;
                }
                else
                {
                    abund = DaneMol.at1[itA].abundancja * DaneMol.at2[itB].abundancja;
                }
            }*/
            LINPUT.ABUNDANCJA = abund;

            LINPUT.miMass = DaneMol.at1[itA].masaatomowa * DaneMol.at2[itB].masaatomowa / (DaneMol.at1[itA].masaatomowa + DaneMol.at2[itB].masaatomowa);
       
            
        }

    }

  
}