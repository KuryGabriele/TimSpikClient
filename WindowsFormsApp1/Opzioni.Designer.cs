namespace WindowsFormsApp1 {
    partial class Opzioni {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.okBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.listaEntrata = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listaUscita = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(422, 415);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "Salva";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.listaEntrata);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listaUscita);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 397);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Microfono";
            // 
            // listaEntrata
            // 
            this.listaEntrata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.listaEntrata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.listaEntrata.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaEntrata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.listaEntrata.FormattingEnabled = true;
            this.listaEntrata.Location = new System.Drawing.Point(83, 61);
            this.listaEntrata.Name = "listaEntrata";
            this.listaEntrata.Size = new System.Drawing.Size(399, 22);
            this.listaEntrata.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Casse";
            // 
            // listaUscita
            // 
            this.listaUscita.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.listaUscita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.listaUscita.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaUscita.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.listaUscita.FormattingEnabled = true;
            this.listaUscita.Location = new System.Drawing.Point(83, 25);
            this.listaUscita.Name = "listaUscita";
            this.listaUscita.Size = new System.Drawing.Size(399, 22);
            this.listaUscita.TabIndex = 0;
            // 
            // Opzioni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(509, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.okBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Opzioni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox listaUscita;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox listaEntrata;
    }
}