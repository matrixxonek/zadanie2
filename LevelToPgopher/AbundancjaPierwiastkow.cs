using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelToPgopher
{
    class wpis
    {
        public int LA;//liczba atomowa;
        public int LM;// liczba masowa;
        public double abundancja;
        public double masaatomowa;
        public double spinJadra=0;
        public wpis(int liczbaatomowa, int liczbamasowa, double masaat, double abund)
        {
            LA = liczbaatomowa;
            LM = liczbamasowa;
            abundancja = abund;
            masaatomowa = masaat;
        }
        public wpis(int liczbaatomowa, int liczbamasowa, double masaat, double abund, double IspinJadra)
        {
            LA = liczbaatomowa;
            LM = liczbamasowa;
            abundancja = abund;
            masaatomowa = masaat;
            spinJadra = IspinJadra;
        }
    }

    partial class GlowneOkno
    {
        public List<wpis> izotopyKadmu;
        public List<wpis> izotopyHelu;
        public List<wpis> izotopyNeonu;
        public List<wpis> izotopyArgonu;
        public List<wpis> izotopyKryptonu;
        public List<wpis> izotopyKsenonu;
        public List<wpis> czesteizotopyKadmu;
        public List<wpis> czesteizotopyKryptonu;
        public List<wpis> izotopyRteci;
        public List<wpis> izotopyCynku;
        public List<wpis> izotopyIterbu;
        public List<wpis> izotopyJodu;


        public void inicjalizujSkladIzotopowy()
        {
            izotopyKadmu = new List<wpis>();
            izotopyHelu = new List<wpis>();
            izotopyNeonu = new List<wpis>();
            izotopyArgonu = new List<wpis>();
            izotopyKryptonu = new List<wpis>();
            izotopyKsenonu = new List<wpis>();
            czesteizotopyKadmu = new List<wpis>();
            czesteizotopyKryptonu = new List<wpis>();
            izotopyRteci = new List<wpis>();
            izotopyCynku = new List<wpis>();
            izotopyIterbu = new List<wpis>();
            izotopyJodu = new List<wpis>();
            

            izotopyKadmu.Add(new wpis(48,106,105.906459,0.0125));
            izotopyKadmu.Add(new wpis(48,108,107.904184,0.0089));
            izotopyKadmu.Add(new wpis(48,110,109.9030021,0.1249));
            izotopyKadmu.Add(new wpis(48,111,110.9041781,0.1280,0.5));
            izotopyKadmu.Add(new wpis(48,112,111.9027578,0.2413));
            izotopyKadmu.Add(new wpis(48,113,112.9044017,0.1222,0.5));
            izotopyKadmu.Add(new wpis(48,114,113.9033585,0.2873));
            izotopyKadmu.Add(new wpis(48,116,115.904756,0.0749));


            czesteizotopyKadmu.Add(new wpis(48,114,113.9033585,0.2873));
            czesteizotopyKadmu.Add(new wpis(48,112,111.9027578,0.2413));
            czesteizotopyKadmu.Add(new wpis(48, 110, 109.9030021, 0.1249));
            czesteizotopyKadmu.Add(new wpis(48, 111, 110.9041781, 0.1280, 0.5));
            czesteizotopyKadmu.Add(new wpis(48, 113, 112.9044017, 0.1222, 0.5));


            izotopyHelu.Add(new wpis(2, 4, 4.00260325415, 1));

            izotopyNeonu.Add(new wpis(10, 20, 19.9924401754, 0.9048));
            izotopyNeonu.Add(new wpis(10, 21, 20.99384668, 0.0027, 1.5));
            izotopyNeonu.Add(new wpis(10, 22, 21.991385114, 0.0925));
                
            izotopyArgonu.Add(new wpis(18,40,39.9623831225,0.996035));
            izotopyArgonu.Add(new wpis(18,36,35.967545106,0.003336));
            izotopyArgonu.Add(new wpis(18,38,37.9627324,0.000629));  

            izotopyKryptonu.Add(new wpis(36,78, 77.9203648, 0.00355));
            izotopyKryptonu.Add(new wpis(36, 80, 79.9163790, 0.02286));
            izotopyKryptonu.Add(new wpis(36, 82, 81.9134836, 0.11593));
            izotopyKryptonu.Add(new wpis(36, 83, 82.914136, 0.11500,4.5));
            izotopyKryptonu.Add(new wpis(36, 84, 83.911507, 0.56987));
            izotopyKryptonu.Add(new wpis(36, 86, 85.91061073, 0.17279));

            czesteizotopyKryptonu.Add(new wpis(36, 82, 81.9134836, 0.11593));
            czesteizotopyKryptonu.Add(new wpis(36, 83, 82.914136, 0.11500, 4.5));
            czesteizotopyKryptonu.Add(new wpis(36, 84, 83.911507, 0.56987));
            czesteizotopyKryptonu.Add(new wpis(36, 86, 85.91061073, 0.17279));

            

            izotopyKsenonu.Add(new wpis(54, 128, 127.9035313, 0.019102));
            izotopyKsenonu.Add(new wpis(54, 129, 128.9047794, 0.264006,0.5));
            izotopyKsenonu.Add(new wpis(54, 130, 129.9035080, 0.040710));
            izotopyKsenonu.Add(new wpis(54, 131, 130.9050824, 0.212324,1.5));
            izotopyKsenonu.Add(new wpis(54, 132, 131.9041535, 0.269086));
            izotopyKsenonu.Add(new wpis(54, 134, 133.9053945, 0.104357));
            izotopyKsenonu.Add(new wpis(54, 136, 135.907219, 0.088573));


            izotopyRteci.Add(new wpis(80, 196,195.965833,0.0015));  
            izotopyRteci.Add(new wpis(80, 198,197.9667690,0.0997)); 
            izotopyRteci.Add(new wpis(80, 199,198.9682799,0.1687,0.5));  
            izotopyRteci.Add(new wpis(80, 200,199.9683260,0.2310));  
            izotopyRteci.Add(new wpis(80, 201,200.9703023,0.1318,1.5));  
            izotopyRteci.Add(new wpis(80, 202,201.9706430,0.2986)); 
            izotopyRteci.Add(new wpis(80, 204,203.9734939,0.0687));

            izotopyCynku.Add(new wpis(30, 64, 63.9291422, 0.4917));
            izotopyCynku.Add(new wpis(30, 66, 65.9260334, 0.2773));
            izotopyCynku.Add(new wpis(30, 67, 66.9271273, 0.0404, -2.5));
            izotopyCynku.Add(new wpis(30, 68, 67.9248442, 0.1845));
            izotopyCynku.Add(new wpis(30, 70, 69.9253193, 0.0061));


            izotopyIterbu.Add(new wpis(70, 168, 167.933897, 0.0013));
            izotopyIterbu.Add(new wpis(70, 170, 169.9347618, 0.0304));
            izotopyIterbu.Add(new wpis(70, 171, 170.9363258, 0.1428,-0.5));
            izotopyIterbu.Add(new wpis(70, 172, 171.9363815, 0.2183));
            izotopyIterbu.Add(new wpis(70, 173, 172.9382108, 0.1613,-2.5));
            izotopyIterbu.Add(new wpis(70, 174, 173.9388621, 0.3183));
            izotopyIterbu.Add(new wpis(70, 176, 175.9425717, 0.1276));

            izotopyJodu.Add(new wpis(53, 127, 126.904473,1, 2.5));
     
        }

    }
}
