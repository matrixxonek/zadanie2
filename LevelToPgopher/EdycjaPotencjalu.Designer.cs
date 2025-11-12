namespace LevelToPgopher
{
    partial class EdycjaPotencjalu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btupdate = new System.Windows.Forms.Button();
            this.LB1 = new System.Windows.Forms.ListBox();
            this.tbktual = new System.Windows.Forms.TextBox();
            this.ZG1 = new ZedGraph.ZedGraphControl();
            this.btL = new System.Windows.Forms.Button();
            this.btR = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btUP = new System.Windows.Forms.Button();
            this.btReload = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbPrzesunWszystkiePunkty = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btupdate
            // 
            this.btupdate.Location = new System.Drawing.Point(270, 78);
            this.btupdate.Name = "btupdate";
            this.btupdate.Size = new System.Drawing.Size(104, 31);
            this.btupdate.TabIndex = 0;
            this.btupdate.Text = "Manual Update";
            this.btupdate.UseVisualStyleBackColor = true;
            this.btupdate.Click += new System.EventHandler(this.button1_Click);
            // 
            // LB1
            // 
            this.LB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB1.FormattingEnabled = true;
            this.LB1.Location = new System.Drawing.Point(0, 0);
            this.LB1.Name = "LB1";
            this.LB1.Size = new System.Drawing.Size(140, 745);
            this.LB1.TabIndex = 1;
            this.LB1.SelectedIndexChanged += new System.EventHandler(this.LB1_SelectedIndexChanged);
            // 
            // tbktual
            // 
            this.tbktual.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbktual.Location = new System.Drawing.Point(55, 41);
            this.tbktual.Name = "tbktual";
            this.tbktual.Size = new System.Drawing.Size(319, 31);
            this.tbktual.TabIndex = 2;
            // 
            // ZG1
            // 
            this.ZG1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZG1.Location = new System.Drawing.Point(0, 0);
            this.ZG1.Name = "ZG1";
            this.ZG1.ScrollGrace = 0;
            this.ZG1.ScrollMaxX = 0;
            this.ZG1.ScrollMaxY = 0;
            this.ZG1.ScrollMaxY2 = 0;
            this.ZG1.ScrollMinX = 0;
            this.ZG1.ScrollMinY = 0;
            this.ZG1.ScrollMinY2 = 0;
            this.ZG1.Size = new System.Drawing.Size(1136, 622);
            this.ZG1.TabIndex = 3;
            // 
            // btL
            // 
            this.btL.Location = new System.Drawing.Point(6, 41);
            this.btL.Name = "btL";
            this.btL.Size = new System.Drawing.Size(43, 31);
            this.btL.TabIndex = 4;
            this.btL.Text = "<";
            this.btL.UseVisualStyleBackColor = true;
            this.btL.Click += new System.EventHandler(this.btL_Click);
            // 
            // btR
            // 
            this.btR.Location = new System.Drawing.Point(380, 41);
            this.btR.Name = "btR";
            this.btR.Size = new System.Drawing.Size(43, 31);
            this.btR.TabIndex = 5;
            this.btR.Text = ">";
            this.btR.UseVisualStyleBackColor = true;
            this.btR.Click += new System.EventHandler(this.btR_Click);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(177, 78);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(80, 31);
            this.btDown.TabIndex = 6;
            this.btDown.Text = "V";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUP
            // 
            this.btUP.Font = new System.Drawing.Font("Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btUP.Location = new System.Drawing.Point(177, 4);
            this.btUP.Name = "btUP";
            this.btUP.Size = new System.Drawing.Size(80, 31);
            this.btUP.TabIndex = 7;
            this.btUP.Text = "L";
            this.btUP.UseVisualStyleBackColor = true;
            this.btUP.Click += new System.EventHandler(this.btUP_Click);
            // 
            // btReload
            // 
            this.btReload.Location = new System.Drawing.Point(55, 78);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(104, 31);
            this.btReload.TabIndex = 8;
            this.btReload.Text = "Reload";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.btReload_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbPrzesunWszystkiePunkty);
            this.panel1.Controls.Add(this.btUP);
            this.panel1.Controls.Add(this.btReload);
            this.panel1.Controls.Add(this.btupdate);
            this.panel1.Controls.Add(this.tbktual);
            this.panel1.Controls.Add(this.btDown);
            this.panel1.Controls.Add(this.btL);
            this.panel1.Controls.Add(this.btR);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1136, 120);
            this.panel1.TabIndex = 9;
            // 
            // cbPrzesunWszystkiePunkty
            // 
            this.cbPrzesunWszystkiePunkty.AutoSize = true;
            this.cbPrzesunWszystkiePunkty.Location = new System.Drawing.Point(560, 41);
            this.cbPrzesunWszystkiePunkty.Name = "cbPrzesunWszystkiePunkty";
            this.cbPrzesunWszystkiePunkty.Size = new System.Drawing.Size(146, 17);
            this.cbPrzesunWszystkiePunkty.TabIndex = 9;
            this.cbPrzesunWszystkiePunkty.Text = "Przesuwaj cały potencjał";
            this.cbPrzesunWszystkiePunkty.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LB1);
            this.splitContainer1.Panel1MinSize = 140;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1280, 746);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.TabIndex = 10;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1MinSize = 120;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ZG1);
            this.splitContainer2.Size = new System.Drawing.Size(1136, 746);
            this.splitContainer2.SplitterDistance = 120;
            this.splitContainer2.TabIndex = 0;
            // 
            // EdycjaPotencjalu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 746);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EdycjaPotencjalu";
            this.Text = "EdycjaPotencjalu";
            this.Load += new System.EventHandler(this.EdycjaPotencjalu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btupdate;
        private System.Windows.Forms.ListBox LB1;
        private System.Windows.Forms.TextBox tbktual;
        private ZedGraph.ZedGraphControl ZG1;
        private System.Windows.Forms.Button btL;
        private System.Windows.Forms.Button btR;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUP;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox cbPrzesunWszystkiePunkty;
    }
}