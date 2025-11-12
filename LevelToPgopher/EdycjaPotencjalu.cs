using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZedGraph;

namespace LevelToPgopher
{
    public partial class EdycjaPotencjalu : Form
    {
       
        List<KeyValuePair<double, double>> PUNKTY;
        public EdycjaPotencjalu(List<KeyValuePair<double,double>> iPUNKTY)
        {

            InitializeComponent();
            PUNKTY = iPUNKTY;
            ZG1.GraphPane.Title.IsVisible = false;
            ZG1.GraphPane.XAxis.Title.IsVisible = false;
            ZG1.GraphPane.YAxis.Title.IsVisible = false;
        }
        double aktX = -1;
        double aktY = -1;
        int aktI = -1;
        

        private void button1_Click(object sender, EventArgs e)
        {
          
                updatePunkt(tbktual.Text, LB1.SelectedIndex);          
        }

        private void EdycjaPotencjalu_Load(object sender, EventArgs e)
        {
            wczytaj();
        }

        public void wczytaj()
        {
            for (int i = 0; i < PUNKTY.Count; i++)
            {
                KeyValuePair<double,double> P=PUNKTY[i];
                LB1.Items.Add(P.Key.ToString() + " " + P.Value.ToString());
            }
            rysuj();
        }

        private void LB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i =LB1.SelectedIndex;
            KeyValuePair<double, double> P = PUNKTY[i];
            tbktual.Text = P.Key.ToString(".00000") + "   " + P.Value.ToString(".00000");
            aktI = i;
            aktX = P.Key;
            aktY = P.Value;
            rysuj();
        }

        private void updatePunkt(string tekst,int i)
        {
            try
            {
                if (cbPrzesunWszystkiePunkty.Checked == true)
                {
                    string[] tablica = Regex.Split(tbktual.Text, @"[\s]+");
                    double dx = double.Parse(tablica[0]);
                    double dy = double.Parse(tablica[1]);
                    for (int j = 0; j < PUNKTY.Count; j++)
                    {
                        KeyValuePair<double, double> temp = PUNKTY[j];
                        PUNKTY[j] = new KeyValuePair<double, double>(temp.Key + dx, temp.Value+dy);
                    }
                    LB1.Items.Clear();
                    wczytaj();
                }
                else
                {
                    string[] tablica = Regex.Split(tekst, @"[\s]+");
                    double x = double.Parse(tablica[0]);
                    double y = double.Parse(tablica[1]);
                    PUNKTY[i] = new KeyValuePair<double, double>(x, y);
                    LB1.Items.Clear();
                    wczytaj();
                }
                
            }
            catch
            {
                MessageBox.Show("Problem");
            }
        }

        private void rysuj()
        {
            PointPairList PPL = new PointPairList();
            PointPairList PPL2 = new PointPairList();
            for (int i = 0; i < PUNKTY.Count; i++)
            {
                PPL.Add(PUNKTY[i].Key, PUNKTY[i].Value);
            }
            PPL.Sort();
            ZG1.GraphPane.CurveList.Clear();
            ZG1.GraphPane.AddCurve("potencjał", PPL, Color.Red, SymbolType.Circle);
            double min = findMin(PPL);
            double max = PPL[PPL.Count - 1].Y;
            if (aktX > 0)
            {
                PPL2.Add(aktX, aktY);
                LineItem LI=ZG1.GraphPane.AddCurve("aktual", PPL2, Color.Green, SymbolType.Square);
                LI.Symbol.Fill = new Fill(Color.Green);
            }
            ZG1.GraphPane.YAxis.Scale.Min = min - 0.1*(max - min);
            ZG1.GraphPane.YAxis.Scale.Max = max + 0.1*(max - min);
            ZG1.AxisChange();
            ZG1.Update();
            ZG1.Invalidate();
        }
        private double findMin(PointPairList PPL)
        {
            double min = double.PositiveInfinity;
            for (int i = 0; i < PPL.Count; i++)
            {
                if (PPL[i].Y < min)
                {
                    min = PPL[i].Y;
                }
            }
            return min;
        }

        private void btL_Click(object sender, EventArgs e)
        {
            if (cbPrzesunWszystkiePunkty.Checked == true)
            {
                
                double dx = 0.05;                
                for (int i = 0; i < PUNKTY.Count; i++)
                {
                    KeyValuePair<double, double> temp = PUNKTY[i];
                    PUNKTY[i] = new KeyValuePair<double, double>(temp.Key-dx, temp.Value);
                }
                LB1.Items.Clear();
                wczytaj();
            }
            else
            {
                if (aktI > -1)
                {
                    string[] tablica = Regex.Split(tbktual.Text, @"[\s]+");
                    double x = double.Parse(tablica[0]);
                    double y = double.Parse(tablica[1]);
                    PUNKTY[aktI] = new KeyValuePair<double, double>(x - 0.05, y);
                    LB1.Items.Clear();
                    tbktual.Text = PUNKTY[aktI].Key.ToString(".00000") + "   " + PUNKTY[aktI].Value.ToString(".00000");
                    wczytaj();
                }
            }
        }

        private void btR_Click(object sender, EventArgs e)
        {
            if (cbPrzesunWszystkiePunkty.Checked == true)
            {

                double dx = 0.05;
                
                for (int i = 0; i < PUNKTY.Count; i++)
                {
                    KeyValuePair<double, double> temp = PUNKTY[i];
                    PUNKTY[i] = new KeyValuePair<double, double>(temp.Key+dx,temp.Value);
                }
                LB1.Items.Clear();               
                wczytaj();
            }
            else
            {
                if (aktI > -1)
                {
                    string[] tablica = Regex.Split(tbktual.Text, @"[\s]+");
                    double x = double.Parse(tablica[0]);
                    double y = double.Parse(tablica[1]);
                    PUNKTY[aktI] = new KeyValuePair<double, double>(x + 0.05, y);
                    LB1.Items.Clear();
                    tbktual.Text = PUNKTY[aktI].Key.ToString(".00000") + "   " + PUNKTY[aktI].Value.ToString(".00000");
                    wczytaj();
                }
            }

        }

        private void btUP_Click(object sender, EventArgs e)
        {
            if (cbPrzesunWszystkiePunkty.Checked == true)
            {

                double dy = 0.1;
                for (int i = 0; i < PUNKTY.Count; i++)
                {
                    KeyValuePair<double, double> temp = PUNKTY[i];
                    PUNKTY[i] = new KeyValuePair<double, double>(temp.Key, temp.Value-dy);
                }
                LB1.Items.Clear();
                wczytaj();
            }
            else
            {
                if (aktI > -1)
                {
                    string[] tablica = Regex.Split(tbktual.Text, @"[\s]+");
                    double x = double.Parse(tablica[0]);
                    double y = double.Parse(tablica[1]);
                    PUNKTY[aktI] = new KeyValuePair<double, double>(x, y + 0.1);
                    LB1.Items.Clear();
                    tbktual.Text = PUNKTY[aktI].Key.ToString(".00000") + "   " + PUNKTY[aktI].Value.ToString(".00000");
                    wczytaj();
                }
            }
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (cbPrzesunWszystkiePunkty.Checked == true)
            {

                double dy = 0.1;
                for (int i = 0; i < PUNKTY.Count; i++)
                {
                    KeyValuePair<double, double> temp = PUNKTY[i];
                    PUNKTY[i] = new KeyValuePair<double, double>(temp.Key, temp.Value + dy);
                }
                LB1.Items.Clear();
                wczytaj();
            }
            else
            {
                if (aktI > -1)
                {
                    string[] tablica = Regex.Split(tbktual.Text, @"[\s]+");
                    double x = double.Parse(tablica[0]);
                    double y = double.Parse(tablica[1]);
                    PUNKTY[aktI] = new KeyValuePair<double, double>(x, y - 0.1);
                    LB1.Items.Clear();
                    tbktual.Text = PUNKTY[aktI].Key.ToString(".00000") + "   " + PUNKTY[aktI].Value.ToString(".00000");
                    wczytaj();
                }
            }
        }

        private void btReload_Click(object sender, EventArgs e)
        {
            wczytaj();
        }
    }
}
