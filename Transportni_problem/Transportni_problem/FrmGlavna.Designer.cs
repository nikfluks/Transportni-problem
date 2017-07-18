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
            this.pnlTablica = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj ishodišta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Broj odredišta:";
            // 
            // txtBrojIshodista
            // 
            this.txtBrojIshodista.Location = new System.Drawing.Point(116, 22);
            this.txtBrojIshodista.Name = "txtBrojIshodista";
            this.txtBrojIshodista.Size = new System.Drawing.Size(53, 20);
            this.txtBrojIshodista.TabIndex = 1;
            // 
            // txtBrojOdredista
            // 
            this.txtBrojOdredista.AcceptsReturn = true;
            this.txtBrojOdredista.Location = new System.Drawing.Point(116, 71);
            this.txtBrojOdredista.Name = "txtBrojOdredista";
            this.txtBrojOdredista.Size = new System.Drawing.Size(53, 20);
            this.txtBrojOdredista.TabIndex = 2;
            // 
            // btnKreirajPraznuTablicu
            // 
            this.btnKreirajPraznuTablicu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnKreirajPraznuTablicu.Location = new System.Drawing.Point(208, 20);
            this.btnKreirajPraznuTablicu.Name = "btnKreirajPraznuTablicu";
            this.btnKreirajPraznuTablicu.Size = new System.Drawing.Size(184, 69);
            this.btnKreirajPraznuTablicu.TabIndex = 3;
            this.btnKreirajPraznuTablicu.Text = "Kreiraj praznu tablicu";
            this.btnKreirajPraznuTablicu.UseVisualStyleBackColor = true;
            this.btnKreirajPraznuTablicu.Click += new System.EventHandler(this.btnKreirajPraznuTablicu_Click);
            // 
            // pnlTablica
            // 
            this.pnlTablica.Location = new System.Drawing.Point(16, 125);
            this.pnlTablica.Name = "pnlTablica";
            this.pnlTablica.Size = new System.Drawing.Size(376, 261);
            this.pnlTablica.TabIndex = 11;
            // 
            // FrmGlavna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 423);
            this.Controls.Add(this.pnlTablica);
            this.Controls.Add(this.btnKreirajPraznuTablicu);
            this.Controls.Add(this.txtBrojOdredista);
            this.Controls.Add(this.txtBrojIshodista);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmGlavna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transportni problem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBrojIshodista;
        private System.Windows.Forms.TextBox txtBrojOdredista;
        private System.Windows.Forms.Button btnKreirajPraznuTablicu;
        private System.Windows.Forms.Panel pnlTablica;
    }
}

