using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;

namespace LevelToPgopher
{
    class WYKRESL
    {
        public static void LEVELwykres(ZedGraphControl ZG1, daneMolekularne danmol,int jedn)
        {
            //jedn: 0 cm, 1 angstr, 2 nm

            ZG1.GraphPane.Legend.IsVisible = false;
            ZG1.IsSynchronizeXAxes = true;

            ZG1.GraphPane.CurveList.Clear();
            List<PointPairList> PPLTable = new List<PointPairList>();
            PointPairList PPL = new PointPairList();
            for (int i = 0; i <= danmol.VD; i++)
            {
                PPLTable.Add(new PointPairList());
            }
         
            ZG1.GraphPane.GraphObjList.Clear();
           // foreach (ELEMENTYMACIERZOWE ELMA in danmol.tablicaelementow)
            foreach(PRZEJSCIE Trans in danmol.tablicaprzejsc)
            {
                double nat = Trans.FC;
                if (danmol.dipol == true)
                {
                    nat = Trans.braket * Trans.braket;
                    //nat = Trans.braket;
                }
                
                int aktVD = Trans.vdol;
                if (Trans.WarunkiSelekcjiVd && Trans.WarunkiSekwencji)
                {
                    if (jedn == 0 && Trans.show)
                    {
                        //PPLTable[aktVD].Add(Trans.EnergiaGora - Trans.EnergiaDol, Trans.FC * Trans.Abundancja*Trans.WAGABOLTZMANOWSKA);
                        PPLTable[aktVD].Add(Trans.EnergiaGora - Trans.EnergiaDol, nat * Trans.Abundancja * Trans.WAGABOLTZMANOWSKA);
                        PPL.Add(Trans.EnergiaGora - Trans.EnergiaDol, nat * Trans.Abundancja * Trans.WAGABOLTZMANOWSKA);

                        if (danmol.pokazPodpisy == true)
                        {
                            TextObj te = new TextObj(Trans.vgora.ToString() + "-" + Trans.vdol.ToString() + "\r\n" + Trans.LM1.ToString() + "-" + Trans.LM2.ToString(), Trans.EnergiaGora - Trans.EnergiaDol, nat * Trans.Abundancja * Trans.WAGABOLTZMANOWSKA * 1.1);

                            ZG1.GraphPane.GraphObjList.Add(te);
                        }
                    }
                    if (jedn == 1 && Trans.show)
                    {
                        double energia = Trans.EnergiaGora - Trans.EnergiaDol;
                        PPLTable[aktVD].Add(100000000 / (1.0003 * energia), nat * Trans.Abundancja * Trans.WAGABOLTZMANOWSKA);
                        if (danmol.pokazPodpisy == true)
                        {
                            TextObj te = new TextObj(Trans.vgora.ToString() + "-" + Trans.vdol.ToString() + "\r\n" + Trans.LM1.ToString() + "-" + Trans.LM2.ToString(), 100000000 / (1.0003 * energia), nat * Trans.Abundancja * Trans.WAGABOLTZMANOWSKA * 1.1);

                            ZG1.GraphPane.GraphObjList.Add(te);
                        }
                    }
                    if (jedn == 2 && Trans.show)
                    {
                        double energia = Trans.EnergiaGora - Trans.EnergiaDol;
                        PPLTable[aktVD].Add(10000000 / (1.0003 * energia), nat* Trans.Abundancja * Trans.WAGABOLTZMANOWSKA);
                        if (danmol.pokazPodpisy == true)
                        {
                            TextObj te = new TextObj(Trans.vgora.ToString() + "-" + Trans.vdol.ToString() + "\r\n" + Trans.LM1.ToString() + "-" + Trans.LM2.ToString(), 10000000 / (1.0003 * energia), nat * Trans.Abundancja * Trans.WAGABOLTZMANOWSKA * 1.1);

                            ZG1.GraphPane.GraphObjList.Add(te);
                        }
                    }
                }
                
            }
            
            if (danmol.sciezkaEXPERIM != "")
            {
                LineItem experim = ZG1.GraphPane.AddCurve("exper", danmol.PPLEXPER, Color.Blue, SymbolType.None);
                experim.IsY2Axis = true;
                double zakres = danmol.PPLEXPERmax - danmol.PPLEXPERmin;
                ZG1.GraphPane.Y2Axis.Scale.Min = danmol.PPLEXPERmin-0.05*zakres;
                ZG1.GraphPane.Y2Axis.Scale.Max = danmol.PPLEXPERmax + 0.05 * zakres;
                ZG1.GraphPane.AxisChange();
                ZG1.Update();
                ZG1.Invalidate();


            }

           

            Color[] ColorTable = new Color[6];
            ColorTable[0] = Color.Red;
            ColorTable[1] = Color.Blue;
            ColorTable[2] = Color.Green;
            ColorTable[3] = Color.Orange;
            ColorTable[4] = Color.Lime;
            ColorTable[5] = Color.Olive;
            /*
            for (int i = 0; i < PPLTable.Count; i++)
            {
                Color kol;
                if (i < ColorTable.Length)
                {
                    kol = ColorTable[i];
                }
                else
                {
                    kol = Color.FromArgb((i * 50) % 255, (i * 50) % 255, (i * 100) % 255);
                }
                ZG1.GraphPane.AddBar("Vd="+i.ToString(), PPLTable[i], kol);
            }*/
            ZG1.GraphPane.AddBar("x", PPL, Color.Red);
            ZG1.GraphPane.BarSettings.ClusterScaleWidth = 0.1;

            ZG1.GraphPane.XAxis.Scale.IsUseTenPower = false;
            ZG1.GraphPane.XAxis.Scale.Mag = 0;
            ZG1.GraphPane.XAxis.Title.IsVisible = false;
            ZG1.GraphPane.YAxis.Title.IsVisible = false;
            ZG1.GraphPane.Title.IsVisible = false;
            ZG1.GraphPane.XAxis.Scale.IsUseTenPower = false;
            ZG1.GraphPane.XAxis.Scale.Mag = 0;

            ZG1.IsSynchronizeXAxes = true;
            ZG1.GraphPane.AxisChange();
            ZG1.Update();
            ZG1.Invalidate();
        }

        public static void RYSUJPOTENCJALYABINI(ZedGraphControl ZG, daneMolekularne danmol, System.Windows.Forms.TextBox TB)
        {
            ZG.GraphPane.YAxis.Scale.IsUseTenPower = false;
            ZG.GraphPane.YAxis.Scale.Mag = 0;
            ZG.GraphPane.Y2Axis.Scale.IsUseTenPower = false;
            ZG.GraphPane.Y2Axis.Scale.Mag = 0;
            ZG.GraphPane.YAxis.Title.IsVisible = false;
            ZG.GraphPane.Y2Axis.IsVisible = true;
            ZG.GraphPane.Title.IsVisible = false;
            ZG.GraphPane.XAxis.Title.Text = "R[A]";

            ZG.GraphPane.XAxis.Scale.IsUseTenPower = false;
            ZG.GraphPane.XAxis.Scale.Mag = 0;

            double minD = 0;
            double minG = 0;

            PointPairList PPLD = new PointPairList();

            if (danmol.uzyjAbIniDol == true)
            {
                minD = danmol.AbiniDol[0].Value;
                for (int i = 0; i < danmol.AbiniDol.Count; i++)
                {
                    KeyValuePair<double, double> KVP = danmol.AbiniDol[i];
                    PPLD.Add(KVP.Key * danmol.RFACTDol, KVP.Value);
                    if (KVP.Value < minD)
                    {
                        minD = KVP.Value;
                    }
                }
            }
            else
            {
               
                minD = danmol.liczPunktMorsaDol(1.5);
                for (double i = 1.5; i < 20; i+=0.25)
                {
                    
                    double tempwar = danmol.liczPunktMorsaDol(i);
                    PPLD.Add(i, tempwar);
                  
                    if(tempwar<minD)
                    {
                        minD=tempwar;
                    }
                }
               
            }


            ZG.GraphPane.CurveList.Clear();
            ZG.GraphPane.AddCurve("dół", PPLD, Color.Red);
            PointPairList PPLG = new PointPairList();

            if(danmol.uzyjAbIniGora==true)
            {
            minG=danmol.AbiniGora[0].Value;
            for (int i = 0; i < danmol.AbiniGora.Count; i++)
            {
                KeyValuePair<double, double> KVP = danmol.AbiniGora[i];
                PPLG.Add(KVP.Key* danmol.RFACTGora, KVP.Value);
                if (KVP.Value < minG)
                {
                    minG = KVP.Value;
                }
            }
            }
            else{
                minG = danmol.liczPunktMorsaGora(1.5);
                StringBuilder SB = new StringBuilder();
                for (double i = 1.5; i < 20; i += 0.25)
                {
                    double tempwar = danmol.liczPunktMorsaGora(i);
                    PPLG.Add(i, tempwar);
                    SB.Append(i.ToString() + "  " + tempwar.ToString() + "\r\n");
                    if (tempwar < minG)
                    {
                        minG = tempwar;
                    }
                }
                TB.Text = SB.ToString();
            }
            LineItem L = ZG.GraphPane.AddCurve("Góra", PPLG, Color.Blue);
            L.IsY2Axis = true;
            ZG.GraphPane.XAxis.Scale.Min = 0;
            ZG.GraphPane.XAxis.Scale.Max = 15;
            double asymptg = PPLG[PPLG.Count - 1].Y;
            double asymptd = PPLD[PPLD.Count - 1].Y;
            double fac1 = 0.1;
            double fac2 = 3;
            ZG.GraphPane.Y2Axis.Scale.Min = minG - Math.Abs(minG - asymptg) * fac1;
            ZG.GraphPane.Y2Axis.Scale.Max = asymptg + Math.Abs(minG - asymptg) * fac2;
            ZG.GraphPane.YAxis.Scale.Min = minD - Math.Abs(minD - asymptd) * fac1;
            ZG.GraphPane.YAxis.Scale.Max = asymptd + Math.Abs(minD - asymptd) * fac2;
            ZG.AxisChange();
            ZG.Update();
            ZG.Invalidate(); 
        }


        public static void RYSUJPOZIOMYVG(ZedGraphControl ZG, daneMolekularne danmol,bool podpisy)
        {
            //double minD = 0;
            double minG = 0;
            ZG.GraphPane.Title.IsVisible = false;
            ZG.GraphPane.Legend.IsVisible = false;
            ZG.GraphPane.XAxis.Title.IsVisible = false;
            ZG.GraphPane.YAxis.Scale.Mag = 0;
            ZG.GraphPane.YAxis.Scale.IsUseTenPower = false;
            ZG.GraphPane.YAxis.Title.IsVisible = false;

            ZG.GraphPane.CurveList.Clear();

            ZG.GraphPane.GraphObjList.Clear();
            int it = 1;
            foreach (POZIOM P in danmol.poziomyGorne)
            {
                it = it * -1;
                PointPairList PPL = new PointPairList();
                PPL.Add(2, P.Energia);
                PPL.Add(5, P.Energia);
                ZG.GraphPane.AddCurve("tekst", PPL, Color.Green, SymbolType.None);
                if (podpisy == true)
                {
                    TextObj te;
                    if (it == -1)
                    {
                        te = new TextObj(P.v.ToString(), 2, P.Energia);
                    }
                    else
                    {
                        te = new TextObj(P.v.ToString(), 5, P.Energia);
                    }
                    ZG.GraphPane.GraphObjList.Add(te);
                }

            }


         
            
            



            PointPairList PPLG = new PointPairList();

            if (danmol.uzyjAbIniGora == true)
            {
                minG = danmol.AbiniGora[0].Value;
                for (int i = 0; i < danmol.AbiniGora.Count; i++)
                {
                    KeyValuePair<double, double> KVP = danmol.AbiniGora[i];
                    PPLG.Add(KVP.Key * 0.529177249, KVP.Value);
                    if (KVP.Value < minG)
                    {
                        minG = KVP.Value;
                    }
                }
            }
            else
            {
                minG = danmol.liczPunktMorsaGora(1.5);
                for (double i = 1.5; i < 20; i += 0.1)
                {
                    double tempwar = danmol.liczPunktMorsaGora(i);
                    PPLG.Add(i, tempwar);
                    if (tempwar < minG)
                    {
                        minG = tempwar;
                    }
                }
            }
            LineItem L = ZG.GraphPane.AddCurve("Góra", PPLG, Color.Red, SymbolType.None);



            ZG.GraphPane.XAxis.Scale.Min = 0;
            ZG.GraphPane.XAxis.Scale.Max = 15;
            double asymptg = PPLG[PPLG.Count - 1].Y;
            PointPairList ASYM = new PointPairList();
            ASYM.Add(0, asymptg);
            ASYM.Add(15, asymptg);
            ZG.GraphPane.AddCurve("AsymptG", ASYM, Color.Blue, SymbolType.None);



            double fac1 = 0.1;
            double fac2 = 0.4;

            ZG.GraphPane.YAxis.Scale.Min = minG - Math.Abs(minG - asymptg) * fac1;
            ZG.GraphPane.YAxis.Scale.Max = asymptg + Math.Abs(minG - asymptg) * fac2;
            ZG.AxisChange();
            ZG.Update();
            ZG.Invalidate();
        }
    }
}
