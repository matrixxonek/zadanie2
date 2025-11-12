using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelToPgopher
{
    class izotopomer
    {
        public int LM1;
        public int LM2;
        public double ABUND;
        public int sumaLM;

        public izotopomer(int iLM1, int iLM2, double iABU)
        {
            LM1 = iLM1;
            LM2 = iLM2;
            sumaLM = iLM1 + iLM2;
            ABUND = iABU;
        }
    }
    class spisIzotopomerow
    {
        public string nazwa = "";
        public List<izotopomer> spis;

        public spisIzotopomerow()
        {
            spis = new List<izotopomer>();
        }

        public string wypisz()
        {
            string temp = nazwa ;
            double sumabu = 0;
            while (spis.Count > 0)
            {
                double currentSumMass=double.PositiveInfinity;
                foreach(izotopomer izo in spis)
                {
                    if(izo.sumaLM<currentSumMass)
                    {
                        currentSumMass=izo.sumaLM;
                    }
                }

          
                temp += "\r\nA1+A2=" + currentSumMass.ToString() + "\r\n";
                for (int i = spis.Count - 1; i >= 0; i--)
                {
                    if (spis[i].sumaLM == currentSumMass)
                    {
                        temp += spis[i].LM1.ToString() + "-" + spis[i].LM2.ToString() + "  " + (100 * spis[i].ABUND).ToString() + "%\r\n";
                        sumabu += spis[i].ABUND;
                        spis.RemoveAt(i);
                    }
                }
            }
            temp += "\r\nSumaryczna abundancja: " + sumabu.ToString()+"\r\n";
            return temp;
        }

        public List<string> wypiszkombinacje()
        {
            List<string> komb = new List<string>();
            //double sumabu = 0;
            while (spis.Count > 0)
            {
                double currentSumMass = double.PositiveInfinity;
                foreach (izotopomer izo in spis)
                {
                    if (izo.sumaLM < currentSumMass)
                    {
                        currentSumMass = izo.sumaLM;
                    }
                }

                komb.Add("komb "+currentSumMass.ToString());
               
                for (int i = spis.Count - 1; i >= 0; i--)
                {
                    if (spis[i].sumaLM == currentSumMass)
                    {                      
                        spis.RemoveAt(i);
                    }
                }
            }
            
            return komb;
        }
    }
}
