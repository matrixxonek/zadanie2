using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace LevelToPgopher
{
    class LevelINPUT
    {        
        public bool isDolAbIni = false;
        public bool isGoraAbIni = false;
        public double ABUNDANCJA = 0;
        public double miMass = 0;
        public string DIPOLPUNKTY;

        public PointPairList dipol0;
        public PointPairList dipoleTemp;
        public double dipoleOffset = 0;

        public void updateOffsetDipol()
        {
            StringBuilder SB = new StringBuilder();
            dipoleTemp = new PointPairList();
            for (int i = 0; i < dipol0.Count; i++)
            {
                SB.Append((dipol0[i].X + dipoleOffset).ToString() + "  " + dipol0[i].Y.ToString() + "\r\n");
                dipoleTemp.Add(dipol0[i].X + dipoleOffset, dipol0[i].Y);
            }
            DIPOLPUNKTY = SB.ToString();

        }

        string punktyAbIniDol;
        string punktyAbIniGora;
        //line 1
        public int IAN1, IAN2; //liczby atomowe
        public int IMN1, IMN2; //liczby masowe
        public int CHARGE = 0; //ładunek molekuły  - u nas zero
        public int NUMPOT = 2; //jeden czy dwa potencjały;

        public double spinJ1 = 0;
        public double spinJ2 = 0;//spiny jadra na potrzeby Pgophera

        protected string line1()
        {
            return IAN1.ToString() + "  " + IMN1.ToString() + "  " + IAN2.ToString() + "  " + IMN2.ToString() + "  " + CHARGE.ToString() + "  " + NUMPOT.ToString() + "          %linia 1 \r\n";
        }

        protected string line1toBVgora()
        {
            return IAN1.ToString() + "   " + IMN1.ToString() + "   " + IAN2.ToString() + "  " + IMN2.ToString() + "  " + CHARGE.ToString() + "   " + 1 + "          %linia 1 \r\n";
        }

        //line 3
        protected string line3()
        {
            return "wzbudzenie jakieśtam            %linia 3 \r\n";
        }

        //line 4
        public double RH, RMIN, RMAX, EPS;
        protected string line4()
        {
            return RH.ToString() + "   " + RMIN.ToString() + "   " + RMAX.ToString() + "   " + EPS.ToString() + "         %linia 4: RH,rmin, rmax, EPS\r\n\r\n";
        }

        //line 5
        public int NTP1 = -2;
        public int LPPOT1=0;
        public int omega1=0;
        public double VLIM1;
        protected string line51()
        {
            return NTP1.ToString() + "   " + LPPOT1.ToString() + "   " + omega1.ToString() + "   " + VLIM1.ToString() + "         %linia 5v1: NTP, LPPOT, omega, VLIM \r\n";
        }

        // kroki 6-8 tylko dla potencjałów PUNKTOWYCH (NTP>0), w przeciwnym wypadku należy wykonać kroki 9-16;
        //line 6       
        public int NUSE1 = -8;
        public int IR21 = 0;
        public int ILR1 = 0;
        public int NCN1 = 0;
        public int CNN1 = 0;
        protected string line61()
        {
            return NUSE1.ToString()+"   "+IR21.ToString()+"   "+ILR1.ToString()+"   "+NCN1.ToString()+"   "+CNN1.ToString()+"            %linia 6v1:NUSE, IR2, ILR, NCN, CNN \r\n";
        }

        //line 7
        public double RFACT1 = 0.529177249; //czynnik mnożący R dla potencjałów punktowych
        public double EFACT1=1.0; //czynnik mnożący energię dla potencjałów punktowych
        public double VSHIFT1 = 0; //przesuniecie potencjału punktowego

        protected string line71()
        {
            return RFACT1.ToString() + "   " + EFACT1.ToString() + "   " + VSHIFT1.ToString() + "            %line 7v1: RFACT, EFACT, VSHIFT\r\n";
        }
        
        //line 8
        //ODCZYT PUNKTOW AB INITIO
        protected string line81()
        {
            return   punktyAbIniDol+ "\r\n\r\n";
        }


        //kroki 9-16 jedynie dla potencjałów analitycznych 

        //line 9
        public int IPOTL1 = 3;//an integer specifying the type of analytic function used for the potential,  
        public int MPAR1 = 0;
        public int NSR1 = 0;   //MPAR and NSR Integers used to characterize particular potential forms
        public int NCMM1 = 0;   //Integer specifying how many inverse-power terms will be used to defne the long-range part of the potential for cases
        public int NVARD1 = 1; //Integer specifying the number of (real) parameters PARM(i) read in to defne the potential (dla morsa 1, De oraz Re nie liczą się jako parametry...)
        public int IBOB1 = -1; //An integer to specify whether (for IBOB>0 ) or not (for IBOB0 ) atomic-mass-dependent Born-Oppenheimer breakdown correction terms are to be included in the rotationless (electronic) and/or centrifugal ....
        public double DSCM1; // GŁĘBOKOŚć MORSA Normally (except for the IPOTL=2 case, above), the potential well depth De in cm-1.
        public double REQ1;  //promień równowagowy w Angstr. 

        protected string line91()
        {
            return "3 1 0 0 -1 0 %  IPOTL, QPAR, PPAR, Nbeta, APSE, IBOB \r\n" +              
            DSCM1.ToString() + "   " + REQ1.ToString() + "   "+ REQ1.ToString()+
            "          %linia 9v1 , GLEBOKOSC, REQ(promien rownowagowy)\r\n";
        }

        //line 10 nie wystepuje gdy GE<=4;
        //line 11 - wczytujemy parametry - dla morsa tylko parametr beta;
        public double BETA1;
        protected string line111()
        {
            return BETA1.ToString() + "            %linia 11v1 gdy mors (IPOTL=3) BETA morsa...\r\n\r\n";
        }

        //linie 12-16 OMIJAMY dla IBOB<=0 ;

        //line 52
        public int NTP2 = -2;
        public int LPPOT2=0;
        public int omega2=0;
        public double VLIM2;
        protected string line52()
        {
            return NTP2.ToString() + "   " + LPPOT2.ToString() + "   " + omega2.ToString() + "   " + VLIM2.ToString() + "         %linia 5v2: NTP, LPPOT, omega, VLIM \r\n";
        }

        // kroki 6-8 tylko dla potencjałów PUNKTOWYCH (NTP>0), w przeciwnym wypadku należy wykonać kroki 9-16;
        //line 6       
        public int NUSE2 = -8;
        public int IR22 = 0;
        public int ILR2 = 0;
        public int NCN2 = 0;
        public int CNN2 = 0;
        protected string line62()
        {
            return NUSE2.ToString() + "   " + IR22.ToString() + "   " + ILR2.ToString() + "   " + NCN2.ToString() + "   " + CNN2.ToString() + "            %linia 6v2 :NUSE, IR2, ILR, NCN, CNN \r\n";
        }

        //line 7
        public double RFACT2 = 0.529177249; //czynnik mnożący R dla potencjałów punktowych
        public double EFACT2=1.0; //czynnik mnożący energię dla potencjałów punktowych
        public double VSHIFT2 = 0; //przesuniecie potencjału punktowego

        protected string line72()
        {
            return RFACT2.ToString() + "   " + EFACT2.ToString() + "   " + VSHIFT2.ToString() + "            %line 7v2: RFACT, EFACT, VSHIFT\r\n";
        }

        //line 8
        //ODCZYT PUNKTOW AB INITIO
        protected string line82()
        {
            return punktyAbIniGora+"\r\n\r\n";
        }


        //kroki 9-16 jedynie dla potencjałów analitycznych 

        //line 9
        public int IPOTL2 = 3;//an integer specifying the type of analytic function used for the potential,  
        public int MPAR2 = 0;
        public int NSR2 = 0;   //MPAR and NSR Integers used to characterize particular potential forms
        public int NCMM2 = 0;   //Integer specifying how many inverse-power terms will be used to defne the long-range part of the potential for cases
        public int NVARD2 = 1; //Integer specifying the number of (real) parameters PARM(i) read in to defne the potential (dla morsa 1, De oraz Re nie liczą się jako parametry...)
        public int IBOB2 = -1; //An integer to specify whether (for IBOB>0 ) or not (for IBOB0 ) atomic-mass-dependent Born-Oppenheimer breakdown correction terms are to be included in the rotationless (electronic) and/or centrifugal ....
        public double DSCM2; // GŁĘBOKOŚć MORSA Normally (except for the IPOTL=2 case, above), the potential well depth De in cm-1.
        public double REQ2;  //promień równowagowy w Angstr. 

        protected string line92()
        {
            return "3  1  0  "+(BETA2.Length-1).ToString()+ "  -1  0  %  IPOTL, QPAR, PPAR, Nbeta, APSE, IBOB\r\n" + 
            DSCM2.ToString() + "   " + REQ2.ToString() + "    "+REQ2.ToString()+ "          %linia 9v2 DSCM(dla morsa glebokosc), REQ(promien rownowagowy)\r\n";
        }

        //line 10 nie wystepuje gdy GE<=4;
        //line 11 - wczytujemy parametry - dla morsa tylko parametr beta;
        public double[] BETA2;
        protected string line112()
        {
            string temp = "";
            for (int i = 0; i < BETA2.Length;i++)
            {
                temp += BETA2[i].ToString() + "  ";
            }
                return temp + "            %linia 11v2 gdy mors (IPOTL=3) BETA morsa...\r\n\r\n";
        }

        //linie 12-16 OMIJAMY dla IBOB<=0 ;







        //line 17
        public int NLEV1;  //liczba poziomow oscylacyjnych, podawać ujemną
        public int AUTO1 = 1;
        public int LCDC = 2; // czy liczyć Bv do pliku???  If LCDC > 0 , calculate the inertial rotational constant Bv and the frst 6 centrifugal distortion constants
        public int LXPCT = 4;  //An integer controlling what expectation values/matrix elements are to be calculated
        public int NJM = 0;
        public int JDJR = 0;
        public int IWR = -1; //An integer controlling the printout of diagnostics and calculation details inside SCHRQ, normally -1
        public int LPRWF = 0; //If LPRWF > 0 write to channel{6 the wavefunction at every fLPRWFgth mesh point. If LPRWF < 0 write wavefunction compactly to channel{10 at every jLPRWFjth mesh point.

        protected string line17()
        {
            return (-1*NLEV1).ToString() + "   " + AUTO1.ToString() + "   " + LCDC.ToString() + "   " + LXPCT.ToString() + "   " + NJM.ToString() + "   " + JDJR.ToString() + "   " + IWR.ToString() + "   " + LPRWF.ToString() + "            %linia17 NLEV1, AUTO1, LCDC, LXPCT, NJM, JDJR, IWR, LPRWF\r\n";
        }


        protected string line17toBvgora()
        {
            return (-1 * NLEV2).ToString() + "   " + AUTO1.ToString() + "   " + LCDC.ToString() + "   " + LXPCT.ToString() + "   " + NJM.ToString() + "   " + JDJR.ToString() + "   " + IWR.ToString() + "   " + LPRWF.ToString() + "            %linia17 NLEV1, AUTO1, LCDC, LXPCT, NJM, JDJR, IWR, LPRWF\r\n";
        }


        //line18
        protected string line18()
        {
            return "-10 0           %line18 \r\n\r\n";
        }

        //line 19
        public bool Dipol = false;
        public int MORDR = -1;
        public int IRFN = 0;
        public int RREF = 0;
        protected string line19()
        {
            if (Dipol == false)
            {
                MORDR = 1;
                IRFN = 0;
            }
            else
            {
                MORDR = 5;
                IRFN = 11;
            }
           return MORDR.ToString() + "   " + IRFN.ToString() + "   " + "1           %linia 19 MORDR, IRFN, RREF \r\n";
          
        }

        //line DIPOL (21-24)

        public double RDIPfact = 1.0;
        public double MomDipfact = 1.0;
        
        public int NRFN = 0;
        protected string line2124()
        {          
            double RFLIM = 0;

            int NUSE1 = -8;
            int IR21 = 0;
            int ILR1 = 0;
            int NCN1 = 0;
            string temp = "";
            temp = NRFN.ToString() + " " + RFLIM.ToString() + "  %ile punktow mom dipol + asymptota\r\n" ;
            temp += NUSE1.ToString() + "   " + IR21.ToString() + "   " + ILR1.ToString() + "   " + NCN1.ToString() + "              %jak interpolować??? :NUSE, IR2, ILR, NCN, \r\n";
            temp += RDIPfact.ToString() + "  " + MomDipfact.ToString() + "     %skalowanie\r\n";
            temp += DIPOLPUNKTY + "\r\n";
            return temp;
        }
      
       
       

        


        //line 20
        protected string line20()
        {
            

           // return "1.0         %potrzebne\r\n";
            if (Dipol == true)
            {
                return line2124();
            }
            else return "0 0\r\n";
        }

        //line25
        public int NLEV2;
        public int AUTO2=1;
        public int J2DL=1;
        public int J2DU=1;
        public int J2DD=1;
        protected string line25()
        {
            return (NLEV2).ToString() + "   " + AUTO2.ToString() + "   " + J2DL.ToString() + "   " + J2DU.ToString() + "   " + J2DD.ToString() + "            %line 25 NLEV2, AUTO2, J2DL, J2DU, J2DD\r\n";
        }
        protected string line26()
        {
            string koncowka = "";
            for (int i = 0; i < NLEV2; i++)
            {
                koncowka += i.ToString() + " ";
            }
            koncowka += "\r\n";
            return koncowka;
        }


        protected void AbIni(daneMolekularne danMol)
        {
            if (isDolAbIni == true)
            {
                int ile = danMol.AbiniDol.Count;
                punktyAbIniDol = "";
                NTP1 = ile;
                for (int i = 0; i < ile; i++)
                {
                    KeyValuePair<double, double> wpis = danMol.AbiniDol[i];
                    punktyAbIniDol += wpis.Key.ToString() + " " + wpis.Value.ToString() + "\r\n";
                }
            }
            else
            {
                NTP1 = -2;
            }

            if (isGoraAbIni == true)
            {
                int ile = danMol.AbiniGora.Count;
                punktyAbIniGora = "";
                NTP2 = ile;
                for (int i = 0; i < ile; i++)
                {
                    KeyValuePair<double, double> wpis = danMol.AbiniGora[i];
                    punktyAbIniGora += wpis.Key.ToString() + " " + wpis.Value.ToString() + "\r\n";
                }
            }
            else
            {
                NTP2 = -2;
            }

        }


        public string GENERUJ(daneMolekularne danMol)
        {
            AbIni(danMol);
            StringBuilder SB = new StringBuilder();
            SB.Append(line1());
            SB.Append(line3());
            SB.Append(line4());
            SB.Append(line51());
            if (NTP1 > 0)
            {
                //SB.Append(line51());
                SB.Append(line61());
                SB.Append(line71());
                SB.Append(line81());
            }
            if (NTP1 < 0)
            {
                SB.Append(line91());
                SB.Append(line111());                
            }

            if (NUMPOT == 2)//czyli gdy 2 potencjały
            {
                SB.Append(line52());
                if (NTP2 > 0)
                {
                    SB.Append(line62());
                    SB.Append(line72());
                    SB.Append(line82());
                }
                if (NTP2 < 0)
                {
                    SB.Append(line92());
                    SB.Append(line112());
                }
            }

            SB.Append(line17());
            SB.Append(line18());
            if (LXPCT != 0 && LXPCT != -1)
            {
                SB.Append(line19());
                SB.Append(line20());
                SB.Append(line25());
                SB.Append(line26());
            }

            return SB.ToString();
        }

        public string GENERUJtoBVgora(daneMolekularne danMol)
        {
            AbIni(danMol);
            StringBuilder SB = new StringBuilder();
            SB.Append(line1toBVgora());
            SB.Append(line3());
            SB.Append(line4());
            SB.Append(line52());
            if (NTP2 > 0)
            {
                //SB.Append(line52());
                SB.Append(line62());
                SB.Append(line72());
                SB.Append(line82());
            }
            if (NTP2 < 0)
            {
                SB.Append(line92());
                SB.Append(line112());
            }


            SB.Append(line17toBvgora());
            SB.Append(line18());
            if (LXPCT != 0 && LXPCT != -1)
            {
                SB.Append(line19());
                SB.Append(line20());
                SB.Append(line25());
                SB.Append(line26());
            }

            return SB.ToString();
        }


        public string GENERUJtoBVDol(daneMolekularne danMol)
        {
            AbIni(danMol);
            StringBuilder SB = new StringBuilder();
            SB.Append(line1toBVgora());
            SB.Append(line3());
            SB.Append(line4());
            SB.Append(line51());
            if (NTP1 > 0)
            {
                //SB.Append(line52());
                SB.Append(line61());
                SB.Append(line71());
                SB.Append(line81());
            }
            if (NTP1 < 0)
            {
                SB.Append(line91());
                SB.Append(line111());
            }


            SB.Append(line17());
            SB.Append(line18());
            if (LXPCT != 0 && LXPCT != -1)
            {
                SB.Append(line19());
                SB.Append(line20());
                SB.Append(line25());
                SB.Append(line26());
            }

            return SB.ToString();
        }
    }
}
