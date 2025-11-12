using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace LevelToPgopher
{
    class Level16InputCreator: LevelINPUT
    {
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
