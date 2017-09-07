namespace Transportni_problem
{
    partial class FrmGlavna
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBrojIshodista = new System.Windows.Forms.TextBox();
            this.txtBrojOdredista = new System.Windows.Forms.TextBox();
            this.btnKreirajPraznuTablicu = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.radioSZKut = new System.Windows.Forms.RadioButton();
            this.radioMinTros = new System.Windows.Forms.RadioButton();
            this.radioVogel = new System.Windows.Forms.RadioButton();
            this.groupOdabirPocetnogRasporeda = new System.Windows.Forms.GroupBox();
            this.btnPrikaziPocetniRaspored = new System.Windows.Forms.Button();
            this.groupOdabirPocetnogRasporeda.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj ishodišta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(25, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Broj odredišta:";
            // 
            // txtBrojIshodista
            // 
            this.txtBrojIshodista.Location = new System.Drawing.Point(128, 25);
            this.txtBrojIshodista.Name = "txtBrojIshodista";
            this.txtBrojIshodista.Size = new System.Drawing.Size(53, 20);
            this.txtBrojIshodista.TabIndex = 1;
            // 
            // txtBrojOdredista
            // 
            this.txtBrojOdredista.AcceptsReturn = true;
            this.txtBrojOdredista.Location = new System.Drawing.Point(128, 74);
            this.txtBrojOdredista.Name = "txtBrojOdredista";
            this.txtBrojOdredista.Size = new System.Drawing.Size(53, 20);
            this.txtBrojOdredista.TabIndex = 2;
            // 
            // btnKreirajPraznuTablicu
            // 
            this.btnKreirajPraznuTablicu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnKreirajPraznuTablicu.Location = new System.Drawing.Point(220, 25);
            this.btnKreirajPraznuTablicu.Name = "btnKreirajPraznuTablicu";
            this.btnKreirajPraznuTablicu.Size = new System.Drawing.Size(184, 69);
            this.btnKreirajPraznuTablicu.TabIndex = 3;
            this.btnKreirajPraznuTablicu.Text = "Kreiraj matricu troškova";
            this.btnKreirajPraznuTablicu.UseVisualStyleBackColor = true;
            this.btnKreirajPraznuTablicu.Click += new System.EventHandler(this.btnKreirajPraznuTablicu_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(20, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(241, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Odaberite metodu za početni raspored:";
            // 
            // radioSZKut
            // 
            this.radioSZKut.AutoSize = true;
            this.radioSZKut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioSZKut.Location = new System.Drawing.Point(23, 61);
            this.radioSZKut.Name = "radioSZKut";
            this.radioSZKut.Size = new System.Drawing.Size(183, 19);
            this.radioSZKut.TabIndex = 13;
            this.radioSZKut.TabStop = true;
            this.radioSZKut.Text = "Metoda sjeverozapdnog kuta";
            this.radioSZKut.UseVisualStyleBackColor = true;
            // 
            // radioMinTros
            // 
            this.radioMinTros.AutoSize = true;
            this.radioMinTros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioMinTros.Location = new System.Drawing.Point(23, 93);
            this.radioMinTros.Name = "radioMinTros";
            this.radioMinTros.Size = new System.Drawing.Size(180, 19);
            this.radioMinTros.TabIndex = 14;
            this.radioMinTros.TabStop = true;
            this.radioMinTros.Text = "Metoda minimalnih troškova";
            this.radioMinTros.UseVisualStyleBackColor = true;
            // 
            // radioVogel
            // 
            this.radioVogel.AutoSize = true;
            this.radioVogel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioVogel.Location = new System.Drawing.Point(23, 126);
            this.radioVogel.Name = "radioVogel";
            this.radioVogel.Size = new System.Drawing.Size(206, 19);
            this.radioVogel.TabIndex = 15;
            this.radioVogel.TabStop = true;
            this.radioVogel.Text = "Vogelova aproksimativna metoda";
            this.radioVogel.UseVisualStyleBackColor = true;
            // 
            // groupOdabirPocetnogRasporeda
            // 
            this.groupOdabirPocetnogRasporeda.Controls.Add(this.label3);
            this.groupOdabirPocetnogRasporeda.Controls.Add(this.radioVogel);
            this.groupOdabirPocetnogRasporeda.Controls.Add(this.radioSZKut);
            this.groupOdabirPocetnogRasporeda.Controls.Add(this.radioMinTros);
            this.groupOdabirPocetnogRasporeda.Location = new System.Drawing.Point(25, 232);
            this.groupOdabirPocetnogRasporeda.Name = "groupOdabirPocetnogRasporeda";
            this.groupOdabirPocetnogRasporeda.Size = new System.Drawing.Size(280, 171);
            this.groupOdabirPocetnogRasporeda.TabIndex = 5;
            this.groupOdabirPocetnogRasporeda.TabStop = false;
            this.groupOdabirPocetnogRasporeda.Text = "Odabir metode početnog rasporeda";
            // 
            // btnPrikaziPocetniRaspored
            // 
            this.btnPrikaziPocetniRaspored.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPrikaziPocetniRaspored.Location = new System.Drawing.Point(25, 431);
            this.btnPrikaziPocetniRaspored.Name = "btnPrikaziPocetniRaspored";
            this.btnPrikaziPocetniRaspored.Size = new System.Drawing.Size(183, 28);
            this.btnPrikaziPocetniRaspored.TabIndex = 6;
            this.btnPrikaziPocetniRaspored.Text = "Prikaži početni raspored";
            this.btnPrikaziPocetniRaspored.UseVisualStyleBackColor = true;
            this.btnPrikaziPocetniRaspored.Click += new System.EventHandler(this.btnPrikaziPocetniRaspored_Click);
            // 
            // FrmGlavna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(463, 497);
            this.Controls.Add(this.btnPrikaziPocetniRaspored);
            this.Controls.Add(this.groupOdabirPocetnogRasporeda);
            this.Controls.Add(this.btnKreirajPraznuTablicu);
            this.Controls.Add(this.txtBrojOdredista);
            this.Controls.Add(this.txtBrojIshodista);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmGlavna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transportni problem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupOdabirPocetnogRasporeda.ResumeLayout(false);
            this.groupOdabirPocetnogRasporeda.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBrojIshodista;
        private System.Windows.Forms.TextBox txtBrojOdredista;
        private System.Windows.Forms.Button btnKreirajPraznuTablicu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioSZKut;
        private System.Windows.Forms.RadioButton radioMinTros;
        private System.Windows.Forms.RadioButton radioVogel;
        private System.Windows.Forms.GroupBox groupOdabirPocetnogRasporeda;
        private System.Windows.Forms.Button btnPrikaziPocetniRaspored;
    }
}

