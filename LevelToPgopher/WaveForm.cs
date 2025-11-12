using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;

namespace LevelToPgopher
{
    class WaveForm:IEquatable<WaveForm>
    {
        public double Energia;
        public int v;
        public int J;
        public List<PointD> spisPunktow;
        public WaveForm(int iV, int iJ, double iEner)
        {
            Energia = iEner;
            v = iV;
            J = iJ;
            spisPunktow = new List<PointD>();
        }



        public bool Equals(WaveForm other)
        {
            if (this.v == other.v)
            {
                return true;
            }
            return false;
        }
    }
}
