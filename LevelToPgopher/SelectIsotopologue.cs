using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelToPgopher
{
    struct cbtb
    {
        public CheckBox cb;
        public TextBox tb;
        public manualIzo mi;
        public cbtb(CheckBox iCb,TextBox iTb, manualIzo iMi)
        {
            cb = iCb;
            tb = iTb;
            mi = iMi;
        }
    }

    struct kombi
    {
        public TextBox tbComb;
        public CheckBox cbComb;
        public List<cbtb> tempList;
        public kombi(CheckBox iCbKombi, TextBox iTbKombi, List<cbtb> iTempList)
        {
            tbComb = iTbKombi;
            cbComb = iCbKombi;
            tempList = iTempList;
        }

        
    }
    internal partial class SelectIsotopologue : Form
    {
        Dictionary<int, kombi> slownik;
        public SelectIsotopologue(daneMolekularne danMolek)
        {
            InitializeComponent();
            List<manualIzo> allIzo = new List<manualIzo>();
            for(int i=0;i<danMolek.at1.Count;i++)
            {
                for(int j=0;j<danMolek.at2.Count;j++)
                {
                    int m1 = danMolek.at1[i].LM;
                    int m2=danMolek.at2[j].LM;
                    manualIzo temp = new manualIzo(m1, m2);
                    if(!allIzo.Contains(temp))
                    {
                        allIzo.Add(temp);
                    }
                }
            }
            allIzo = (from i in allIzo orderby i.Kombin select i).ToList();
           // List<CheckBox> cbList = new List<CheckBox>();
            List<cbtb> kontrolList = new List<cbtb>();
            slownik = new Dictionary<int, kombi>();
            int currentCombination = -1;
            List<cbtb> tempList = null; 
            int offset = 0;
            for(int i=0;i<allIzo.Count;i++)
            { 
                CheckBox cbTemp = new CheckBox();
                cbTemp.Text = allIzo[i].M1.ToString() + "-" + allIzo[i].M2.ToString();
                TextBox tbTemp = new TextBox();
                tbTemp.Text = "0";
                cbtb temp = new cbtb(cbTemp, tbTemp, allIzo[i]);
                if(allIzo[i].Kombin!=currentCombination)
                {
                    currentCombination = allIzo[i].Kombin;
                    tempList = new List<cbtb>();
                    
                    //add separator
                    Label lbl = new Label();
                    lbl.Location = new Point(10, i * 20 +offset+5);                    
                    lbl.BorderStyle = BorderStyle.Fixed3D;
                    lbl.Height = 3;
                    this.P1.Controls.Add(lbl);
                    //add kombin descr
                    Label descr = new Label();
                    descr.Text = "Kombinacja " + currentCombination;
                    descr.Location = new Point(200, i * 20+ offset+15);
                    this.P1.Controls.Add(descr); 
                    //add kombin CB
                    CheckBox cbCombin = new CheckBox();
                    cbCombin.Text = currentCombination.ToString();
                    cbCombin.Location = new Point(320, i * 20 + offset + 15);
                    cbCombin.CheckedChanged += cbCombin_CheckedChanged;
                    this.P1.Controls.Add(cbCombin);
                    //add kombin TB
                    TextBox tbKomb = new TextBox();
                    tbKomb.Text = "1";
                    tbKomb.Location = new Point(440, i * 20 + offset + 15);
                    tbKomb.Width = 50;
                    this.P1.Controls.Add(tbKomb);


                    offset += 15;
                    kombi komb = new kombi(cbCombin, tbKomb, tempList);
                    slownik.Add(currentCombination, komb);
                    

                }

                cbTemp.Location = new Point(10, i * 20 + offset);
                
                tbTemp.Location = new Point(120, i * 20 + offset);
                tbTemp.Width = 50;
                kontrolList.Add(temp);
                tempList.Add(temp);
                this.P1.Controls.Add(cbTemp);
                this.P1.Controls.Add(tbTemp);
            }

            Button btConfirm = new Button();
            btConfirm.Text = "Zatwierdź";
            btConfirm.Width = 100;
            btConfirm.Height = 80;
            btConfirm.Location = new Point(550, 25);
            this.P1.Controls.Add(btConfirm);
            //MessageBox.Show(allIzo.Count.ToString());
        }

        void cbCombin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox temp= sender as CheckBox;
            if(temp!=null)
            {
            int currentComb=int.Parse(temp.Text);
            setCombinParameter(currentComb);
            }
        }

        private void setCombinParameter(int combin)
        {
            kombi temp;
            slownik.TryGetValue(combin, out temp);
            foreach(var x in temp.tempList)
            {
                x.cb.Checked = temp.cbComb.Checked;
                if(temp.cbComb.Checked==false)
                {
                    x.tb.Text = "0";
                }
                else
                {
                    x.tb.Text = temp.tbComb.Text;
                }
                
            }

        }
    }
}
