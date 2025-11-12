using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Collections;
using ZedGraph;
using System.Threading;

namespace LevelToPgopher
{
    partial class GlowneOkno : Form
    {
        private void VDselectUpdate()
        {
            
                
                panelVDoptions.Controls.Clear();
                addLabels();
                addCheckBoxes();
                addTextBoxes();
                addHScrollBars();
                addPOZOSTALE();

                //lastVD = dane.vstanudolnego;
           
        }

        private void addLabels()
        {
            for (int i = 0; i <= DANEMol.VD; i++)
            {
                System.Windows.Forms.Label L = new System.Windows.Forms.Label();
                L.Text = "Vd=" + i.ToString();
                L.Size = new Size(40, 15);
                L.Location = new Point(5, 50 + i * 25);
                panelVDoptions.Controls.Add(L);

            }
        }
        private void addCheckBoxes()
        {

            DANEMol.CBLIST.Clear();
            for (int i = 0; i <= DANEMol.VD; i++)
            {

                CheckBox CB = new CheckBox();
                CB.Text = "";
                CB.Checked = true;
                CB.Location = new Point(45, 50 + i * 25);
                CB.Size = new Size(15, 15);
                panelVDoptions.Controls.Add(CB);
                DANEMol.CBLIST.Add(CB);
            }
        }
        private void addTextBoxes()
        {
            DANEMol.TBLIST.Clear();
            for (int i = 0; i <= DANEMol.VD; i++)
            {

                TextBox TB = new TextBox();
                TB.Text = "1";
                TB.Location = new Point(215, 50 + i * 25);
                TB.Size = new Size(40, 15);
                TB.Validating += TBValidate;
                panelVDoptions.Controls.Add(TB);
                DANEMol.TBLIST.Add(TB);
            }
        }
        private void addHScrollBars()
        {
            DANEMol.HSCROLLBARLIST.Clear();
            for (int i = 0; i <= DANEMol.VD; i++)
            {
                HScrollBar HSB = new HScrollBar();
                HSB.Location = new Point(60, 50 + i * 25);
                HSB.Size = new Size(145, 15);
                HSB.Minimum = 0;
                HSB.Maximum = 100;
                HSB.SmallChange = 1;
                panelVDoptions.Controls.Add(HSB);
                HSB.Scroll += HSBVDChanges;
                DANEMol.HSCROLLBARLIST.Add(HSB);
            }
        }

        private void HSBVDChanges(object sender, ScrollEventArgs e)
        {
          
            for (int i = 0; i < DANEMol.HSCROLLBARLIST.Count; i++)
            {
                if (sender == DANEMol.HSCROLLBARLIST[i])
                {
                    DANEMol.TBLIST[i].Text = (e.NewValue * Math.Pow(10, DANEMol.TB1.Value)).ToString();
                   
                }
            }
           
            if (e.Type == ScrollEventType.EndScroll)
            {
                // maleprzelicz();
            }
        }
        private void TBValidate(object sender, EventArgs e)
        {
            try
            {
                double y = double.Parse((sender as TextBox).Text) / DANEMol.TB1.Value;
                int x = (int)y;
                for (int i = 0; i < DANEMol.TBLIST.Count; i++)
                {
                    if (sender == DANEMol.TBLIST[i])
                    {
                        MessageBox.Show("zmiana");
                        if (x >= DANEMol.HSCROLLBARLIST[i].Minimum && x <= DANEMol.HSCROLLBARLIST[i].Maximum)
                        {
                            DANEMol.HSCROLLBARLIST[i].Value = x;
                        }
                        break;
                    }
                }
                //maleprzelicz();
            }
            catch
            {
            }
        }

        private void addPOZOSTALE()
        {
            Button B = new Button();
            B.Location = new Point(2, 10);
            B.Text = "RESET";
            B.Size = new Size(55, 25);
            B.Click += RESET;
            panelVDoptions.Controls.Add(B);

            TrackBar TB1 = new TrackBar();
            TB1.Location = new Point(60, 0);
            TB1.Size = new Size(80, 10);
            panelVDoptions.Controls.Add(TB1);
            TB1.Minimum = -2;
            TB1.Maximum = 2;
            DANEMol.TB1 = TB1;

            Button BBOLTZ = new Button();
            BBOLTZ.Location = new Point(145, 3);
            BBOLTZ.Text = "ROZK. BOLTZM.";
            BBOLTZ.Size = new Size(65, 40);
            panelVDoptions.Controls.Add(BBOLTZ);
            BBOLTZ.Click += ROZKLADBOLTZMANOWSKIVD;



            TextBox TBBoltzman = new TextBox();
            TBBoltzman.Text = "50";
            DANEMol.TBBoltzman = TBBoltzman;
            TBBoltzman.Location = new Point(215, 10);
            TBBoltzman.Size = new Size(40, 10);
            panelVDoptions.Controls.Add(TBBoltzman);


        }
        private void RESET(object sender, EventArgs e)
        {

            if (MessageBox.Show("Czy jesteś pewien, że chcesz przywrócić ustawienia domyślne tej części programu???", "Are You sure...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i <= DANEMol.VD; i++)
                {
                    DANEMol.CBLIST[i].Checked = true;
                    DANEMol.TBLIST[i].Text = "1";
                    DANEMol.HSCROLLBARLIST[i].Value = 1;
                }
                DANEMol.TB1.Value = 0;
                //przelicz();
            }
        }

        private void ROZKLADBOLTZMANOWSKIVD(object sender, EventArgs e)
        {

            double cmtoEv = 0.00012398;

            
           // TBW2.Text = "";
            double T = 1.0;
            
            try
            {
                T = double.Parse(DANEMol.TBBoltzman.Text);
            }
            catch
            {
                MessageBox.Show("Problem z wczytaniem temperatury boltzmanowskiej");
            }
            double EnergiaV0 = cmtoEv * DANEMol.poziomyDolne[0].Energia;
           // MessageBox.Show(EnergiaV0.ToString());
           
           // Test.asymptotadol - test.Ddol + dane.oeD * (0 + 0.5) - dane.oxeD * (0 + 0.5) * (0 + 0.5);
            for (int jvd = 1; jvd <= DANEMol.VD; jvd++)
            {
                double poziom = cmtoEv * DANEMol.poziomyDolne[jvd].Energia;
                double delta = poziom - EnergiaV0;
               
                double OBSADZENIE = Math.Exp(-delta /(8.6173324*0.00001* T));

                DANEMol.TBLIST[jvd].Text = OBSADZENIE.ToString("0.0000");
                //TBW2.Text += jvd.ToString()+"   " + poziom.ToString() +"  "+OBSADZENIE.ToString()+ "\r\n";
            }
          
    
           
        }


        private void OLDROZKLADBOLTZMANOWSKIVDold(object sender, EventArgs e)
        {


            // TBW2.Text = "";
            double T = 1.0;
            double KonweterCmtoT = 1.44;
            try
            {
                T = double.Parse(DANEMol.TBBoltzman.Text);
            }
            catch
            {
                MessageBox.Show("Problem z wczytaniem temperatury boltzmanowskiej");
            }
            double EnergiaV0 = DANEMol.poziomyDolne[0].Energia;
            // MessageBox.Show(EnergiaV0.ToString());

            // Test.asymptotadol - test.Ddol + dane.oeD * (0 + 0.5) - dane.oxeD * (0 + 0.5) * (0 + 0.5);
            for (int jvd = 1; jvd <= DANEMol.VD; jvd++)
            {
                double poziom = DANEMol.poziomyDolne[jvd].Energia;
                double delta = poziom - EnergiaV0;
                double deltaK = delta * KonweterCmtoT;
                double OBSADZENIE = Math.Exp(-deltaK / T);

                DANEMol.TBLIST[jvd].Text = OBSADZENIE.ToString("0.000");
                //TBW2.Text += jvd.ToString()+"   " + poziom.ToString() +"  "+OBSADZENIE.ToString()+ "\r\n";
            }


            // przelicz();

        }
    }


}
