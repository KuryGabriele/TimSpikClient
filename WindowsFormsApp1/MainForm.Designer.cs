namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.closeBtn = new System.Windows.Forms.Button();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.pannelloUtenti = new System.Windows.Forms.FlowLayoutPanel();
            this.minimizeBtn = new System.Windows.Forms.Button();
            this.chatPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.invioMsg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.joinBtn = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.muteAudioBtn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Transparent;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.closeBtn.Location = new System.Drawing.Point(979, 2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(74, 62);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "Chiudi";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // logoBox
            // 
            this.logoBox.BackColor = System.Drawing.Color.Transparent;
            this.logoBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logoBox.BackgroundImage")));
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.logoBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("logoBox.InitialImage")));
            this.logoBox.Location = new System.Drawing.Point(1, 0);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(263, 62);
            this.logoBox.TabIndex = 1;
            this.logoBox.TabStop = false;
            this.logoBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logoBox_MouseDown);
            this.logoBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logoBox_MouseMove);
            this.logoBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.logoBox_MouseUp);
            // 
            // topPanel
            // 
            this.topPanel.Location = new System.Drawing.Point(263, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(630, 62);
            this.topPanel.TabIndex = 2;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseMove);
            this.topPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseUp);
            // 
            // pannelloUtenti
            // 
            this.pannelloUtenti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.pannelloUtenti.Location = new System.Drawing.Point(12, 107);
            this.pannelloUtenti.Name = "pannelloUtenti";
            this.pannelloUtenti.Size = new System.Drawing.Size(263, 412);
            this.pannelloUtenti.TabIndex = 3;
            // 
            // minimizeBtn
            // 
            this.minimizeBtn.BackColor = System.Drawing.Color.Transparent;
            this.minimizeBtn.FlatAppearance.BorderSize = 0;
            this.minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeBtn.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.minimizeBtn.Location = new System.Drawing.Point(899, 2);
            this.minimizeBtn.Name = "minimizeBtn";
            this.minimizeBtn.Size = new System.Drawing.Size(74, 62);
            this.minimizeBtn.TabIndex = 4;
            this.minimizeBtn.Text = "Leva";
            this.minimizeBtn.UseVisualStyleBackColor = false;
            this.minimizeBtn.Click += new System.EventHandler(this.minimizeBtn_Click);
            // 
            // chatPanel
            // 
            this.chatPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.chatPanel.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.chatPanel.Location = new System.Drawing.Point(284, 68);
            this.chatPanel.Name = "chatPanel";
            this.chatPanel.Size = new System.Drawing.Size(759, 451);
            this.chatPanel.TabIndex = 4;
            // 
            // txtBox
            // 
            this.txtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.txtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox.ForeColor = System.Drawing.Color.White;
            this.txtBox.Location = new System.Drawing.Point(8, 9);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(662, 67);
            this.txtBox.TabIndex = 0;
            this.txtBox.Text = "Insulta i tuoi amici...";
            this.txtBox.GotFocus += new System.EventHandler(this.clearMsgBox);
            this.txtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            this.txtBox.LostFocus += new System.EventHandler(this.fillPlaceholder);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.panel2.Controls.Add(this.invioMsg);
            this.panel2.Controls.Add(this.txtBox);
            this.panel2.Location = new System.Drawing.Point(284, 525);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(759, 86);
            this.panel2.TabIndex = 5;
            // 
            // invioMsg
            // 
            this.invioMsg.BackColor = System.Drawing.Color.Transparent;
            this.invioMsg.FlatAppearance.BorderSize = 0;
            this.invioMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invioMsg.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invioMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.invioMsg.Location = new System.Drawing.Point(676, 3);
            this.invioMsg.Name = "invioMsg";
            this.invioMsg.Size = new System.Drawing.Size(80, 80);
            this.invioMsg.TabIndex = 6;
            this.invioMsg.Text = "Invio";
            this.invioMsg.UseVisualStyleBackColor = false;
            this.invioMsg.Click += new System.EventHandler(this.invioMsg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Brutta gente:";
            // 
            // joinBtn
            // 
            this.joinBtn.FlatAppearance.BorderSize = 0;
            this.joinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.joinBtn.ForeColor = System.Drawing.Color.White;
            this.joinBtn.Location = new System.Drawing.Point(148, 74);
            this.joinBtn.Name = "joinBtn";
            this.joinBtn.Size = new System.Drawing.Size(116, 23);
            this.joinBtn.TabIndex = 0;
            this.joinBtn.Text = "Entra";
            this.joinBtn.UseVisualStyleBackColor = true;
            this.joinBtn.Click += new System.EventHandler(this.joinBtn_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.settingsBtn.FlatAppearance.BorderSize = 0;
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.settingsBtn.Location = new System.Drawing.Point(12, 525);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(80, 80);
            this.settingsBtn.TabIndex = 7;
            this.settingsBtn.Text = "Opzioni";
            this.settingsBtn.UseVisualStyleBackColor = false;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // muteAudioBtn
            // 
            this.muteAudioBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.muteAudioBtn.FlatAppearance.BorderSize = 0;
            this.muteAudioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.muteAudioBtn.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.muteAudioBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.muteAudioBtn.Location = new System.Drawing.Point(104, 525);
            this.muteAudioBtn.Name = "muteAudioBtn";
            this.muteAudioBtn.Size = new System.Drawing.Size(80, 80);
            this.muteAudioBtn.TabIndex = 8;
            this.muteAudioBtn.Text = "Muta audio";
            this.muteAudioBtn.UseVisualStyleBackColor = false;
            this.muteAudioBtn.Click += new System.EventHandler(this.muteBtn_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.button3.Location = new System.Drawing.Point(195, 525);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 80);
            this.button3.TabIndex = 9;
            this.button3.Text = "Mutati";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1055, 623);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.muteAudioBtn);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.joinBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.chatPanel);
            this.Controls.Add(this.minimizeBtn);
            this.Controls.Add(this.pannelloUtenti);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.closeBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "VBANSpeak";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.FlowLayoutPanel pannelloUtenti;
        private System.Windows.Forms.Button minimizeBtn;
        private System.Windows.Forms.FlowLayoutPanel chatPanel;
        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button invioMsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button joinBtn;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Button muteAudioBtn;
        private System.Windows.Forms.Button button3;
    }
}

