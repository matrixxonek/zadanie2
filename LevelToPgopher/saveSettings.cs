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

namespace LevelToPgopher
{
    partial class GlowneOkno : Form
    {
        private string saveSettings()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("SelTrans " + cbSELECTTRANSITION.SelectedIndex.ToString() + "\r\n");
            SB.Append("SelIzotop " + cbSelectIzotopolog.SelectedIndex.ToString() + "\r\n");
            SB.Append("OED " + tbOED.Text + "\r\n");
            SB.Append("OXED " + tbOXED.Text + "\r\n");
            SB.Append("OEG " + tbOEG.Text + "\r\n");
            SB.Append("OXEG " + tbOXEG.Text + "\r\n");
            SB.Append("R_D " + tbRD.Text + "\r\n");
            SB.Append("R_G " + tbRG.Text + "\r\n");
            SB.Append("Asymptota_D " + tbASYMPTD.Text + "\r\n");
            SB.Append("Asymptota_G " + tbASYMPTG.Text + "\r\n");
            SB.Append("Vd " + tbVD.Text + "\r\n");
            SB.Append("Only_one_Vd " + cbOnlyOneVd.Checked.ToString() + "\r\n");
            SB.Append("Max_VG " + tbMaxVG.Text + "\r\n");
            SB.Append("Min_VG " + tbMinVg.Text + "\r\n");
            SB.Append("manual_GD " + cbDManualD.Checked.ToString() + "\r\n");
            SB.Append("manual_GG " + cbDmanualG.Checked.ToString() + "\r\n");
            SB.Append("glebD "+tbDDol.Text+"\r\n");
            SB.Append("glebG " + tbDGora.Text + "\r\n");
            SB.Append("Gauss " + tbGauss.Text + "\r\n");
            SB.Append("Lorentz " + tbLorentz.Text + "\r\n");
            SB.Append("sciezka_experymentu " + cbExperiment.Checked.ToString() +" "+ DANEMol.sciezkaEXPERIM + "\r\n");
            SB.Append("S_dol " + tbSdol.Text + "\r\n");
            SB.Append("S_gora " + tbSgora.Text + "\r\n");
            SB.Append("LambdaD " + cbLAMBDAD.SelectedIndex.ToString() + "\r\n");
            SB.Append("LambdaG " + cbLAMBDAG.SelectedIndex.ToString() + "\r\n");
            SB.Append("OmegaD " + cbOmegaSelectDol.SelectedIndex.ToString() + "\r\n");
            SB.Append("OmegaG " + cbOmegaSelectGora.SelectedIndex.ToString() + "\r\n");
            SB.Append("TypPrzejscia " + cbTypTrans.SelectedIndex.ToString() + "\r\n");
            SB.Append("ProgAbund " + cbprogabund.SelectedIndex.ToString() + "\r\n");
            SB.Append("Temperatura " + tbT.Text + "\r\n");
            SB.Append("GeradEnable " + cbSymEnable.Checked.ToString() + "\r\n");
            if (cbSymEnable.Checked)
            {
                SB.Append("GeradD " + cbSymD.SelectedIndex.ToString() + "\r\n");
                SB.Append("GeradG " + cbSymG.SelectedIndex.ToString() + "\r\n");
            }
            SB.Append("UwzglProgNatLin " + cbuwzglednijIlinii.Checked.ToString() + "\r\n");
            if(cbuwzglednijIlinii.Checked)
            {
                SB.Append("WartoscProguNatLin " + cbProgNatLin.SelectedIndex.ToString() + "\r\n");
            }
            SB.Append("Sekwencja " + cbSequence.Checked.ToString() + "\r\n");
            if(cbSequence.Checked)
            {
                SB.Append("DeltaSekwencji " + cbSekwencjaDV.SelectedIndex.ToString() + "\r\n");
            }


            return SB.ToString();
        }

        private void saveConfig_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter SW = new StreamWriter(saveConfig.FileName);
            SW.Write(saveSettings());
            SW.Close();
        }

        private void readSettings(string napis)
        {
            //int iter=0;
            string[] tab = Regex.Split(napis, @"\r\n");
            for (int i = 0; i < tab.Length; i++)
            {
                string[] tabliczka = Regex.Split(tab[i], @"[\s]+");
                if (tabliczka[0] == "SelTrans")
                {
                    cbSELECTTRANSITION.SelectedIndex = int.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "SelIzotop")
                {
                    cbSelectIzotopolog.SelectedIndex = int.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "OED")
                {
                    tbOED.Text = tabliczka[1];
                }
                if (tabliczka[0] == "OXED")
                {
                    tbOXED.Text = tabliczka[1]; 
                }
                if (tabliczka[0] == "OEG")
                {
                    tbOEG.Text = tabliczka[1];
                }
                if (tabliczka[0] == "OXEG")
                {
                    tbOXEG.Text = tabliczka[1];
                }
                if (tabliczka[0] == "R_D")
                {
                    tbRD.Text = tabliczka[1];
                }
                if (tabliczka[0] == "R_G")
                {
                    tbRG.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Asymptota_D")
                {
                    tbASYMPTD.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Asymptota_G")
                {
                    tbASYMPTG.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Vd")
                {
                    tbVD.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Only_one_Vd")
                {
                    cbOnlyOneVd.Checked = bool.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "Max_VG")
                {
                    tbMaxVG.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Min_VG")
                {
                    tbMinVg.Text = tabliczka[1]; 
                }
                if (tabliczka[0] == "manual_GD")
                {
                    cbDManualD.Checked = bool.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "manual_GG")
                {
                    cbDmanualG.Checked = bool.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "glebD")
                {
                    tbDDol.Text = tabliczka[1];
                }
                if (tabliczka[0] == "glebG")
                {
                    tbDGora.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Gauss")
                {
                    tbGauss.Text = tabliczka[1];
                }
                if (tabliczka[0] == "Lorentz")
                {
                    tbLorentz.Text = tabliczka[1];
                }
                if (tabliczka[0] == "sciezka_experymentu" && bool.Parse(tabliczka[1]))
                {
                    string path="";
                    for (int j = 2; j < tabliczka.Length; j++)
                    {
                        path += tabliczka[j] + " ";
                    }
                   
                    cbExperiment.Checked = true;
                    wczytajExperim(path);
                }
                if (tabliczka[0] == "S_dol")
                {
                    tbSdol.Text=tabliczka[1];
                }
                if(tabliczka[0]=="S_gora")
                {
                    tbSgora.Text = tabliczka[1];
                }
                if (tabliczka[0] == "LambdaD")
                {
                    cbLAMBDAD.SelectedIndex= int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="LambdaG")
                {
                    cbLAMBDAG.SelectedIndex=int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="OmegaD")
                {
                    cbOmegaSelectDol.SelectedIndex= int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="OmegaG")
                {
                    cbOmegaSelectGora.SelectedIndex= int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="TypPrzejscia")
                {
                    cbTypTrans.SelectedIndex=int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="ProgAbund")
                {
                    cbprogabund.SelectedIndex = int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="Temperatura")
                {
                    tbT.Text = tabliczka[1];
                }
                if (tabliczka[0] == "GeradEnable")
                {
                    cbSymEnable.Checked = bool.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "GeradD")
                {
                    cbSymD.SelectedIndex = int.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "GeradG")
                {
                    cbSymG.SelectedIndex = int.Parse(tabliczka[1]);
                }
                if (tabliczka[0] == "UwzglProgNatLin")
                {
                    cbuwzglednijIlinii.Checked = bool.Parse(tabliczka[1]);
                    
                }
                if (tabliczka[0] == "WartoscProguNatLin")
                {
                    cbProgNatLin.SelectedIndex = int.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="Sekwencja")
                {
                    cbSequence.Checked = bool.Parse(tabliczka[1]);
                }
                if(tabliczka[0]=="DeltaSekwencji")
                {
                    cbSekwencjaDV.SelectedIndex = int.Parse(tabliczka[1]);
                }
            }
           
          
        }
      
    }
}