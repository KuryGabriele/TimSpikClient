namespace WindowsFormsApp1 {
    partial class UserOptions {
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
            this.close_btn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.optionsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.volumeSlider = new NAudio.Gui.VolumeSlider();
            this.img_container = new System.Windows.Forms.FlowLayoutPanel();
            this.msg_btn = new System.Windows.Forms.Button();
            this.kick_btn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.firstJoinLabel = new System.Windows.Forms.Label();
            this.lastSeenLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.optionsContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // close_btn
            // 
            this.close_btn.BackColor = System.Drawing.Color.Transparent;
            this.close_btn.FlatAppearance.BorderSize = 0;
            this.close_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.close_btn.Location = new System.Drawing.Point(513, 415);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(75, 23);
            this.close_btn.TabIndex = 0;
            this.close_btn.Text = "Chiudi";
            this.close_btn.UseVisualStyleBackColor = false;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optionsContainer);
            this.panel1.Controls.Add(this.img_container);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 397);
            this.panel1.TabIndex = 1;
            // 
            // optionsContainer
            // 
            this.optionsContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.optionsContainer.Controls.Add(this.panel2);
            this.optionsContainer.Controls.Add(this.panel3);
            this.optionsContainer.Location = new System.Drawing.Point(3, 114);
            this.optionsContainer.Name = "optionsContainer";
            this.optionsContainer.Size = new System.Drawing.Size(570, 283);
            this.optionsContainer.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.volumeSlider);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 85);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Volume";
            // 
            // volumeSlider
            // 
            this.volumeSlider.Location = new System.Drawing.Point(86, 15);
            this.volumeSlider.Name = "volumeSlider";
            this.volumeSlider.Size = new System.Drawing.Size(96, 16);
            this.volumeSlider.TabIndex = 0;
            this.volumeSlider.VolumeChanged += new System.EventHandler(this.cambiaVolume);
            // 
            // img_container
            // 
            this.img_container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(54)))));
            this.img_container.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.img_container.Location = new System.Drawing.Point(3, 3);
            this.img_container.Name = "img_container";
            this.img_container.Size = new System.Drawing.Size(570, 105);
            this.img_container.TabIndex = 0;
            this.img_container.MouseDown += new System.Windows.Forms.MouseEventHandler(this.img_container_MouseDown);
            this.img_container.MouseMove += new System.Windows.Forms.MouseEventHandler(this.img_container_MouseMove);
            this.img_container.MouseUp += new System.Windows.Forms.MouseEventHandler(this.img_container_MouseUp);
            // 
            // msg_btn
            // 
            this.msg_btn.BackColor = System.Drawing.Color.Transparent;
            this.msg_btn.FlatAppearance.BorderSize = 0;
            this.msg_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.msg_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.msg_btn.Location = new System.Drawing.Point(432, 415);
            this.msg_btn.Name = "msg_btn";
            this.msg_btn.Size = new System.Drawing.Size(75, 23);
            this.msg_btn.TabIndex = 2;
            this.msg_btn.Text = "Messaggia";
            this.msg_btn.UseVisualStyleBackColor = false;
            // 
            // kick_btn
            // 
            this.kick_btn.BackColor = System.Drawing.Color.Transparent;
            this.kick_btn.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.kick_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kick_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(119)))), ((int)(((byte)(0)))));
            this.kick_btn.Location = new System.Drawing.Point(351, 415);
            this.kick_btn.Name = "kick_btn";
            this.kick_btn.Size = new System.Drawing.Size(75, 23);
            this.kick_btn.TabIndex = 3;
            this.kick_btn.Text = "Kick";
            this.kick_btn.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lastSeenLabel);
            this.panel3.Controls.Add(this.firstJoinLabel);
            this.panel3.Location = new System.Drawing.Point(209, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 85);
            this.panel3.TabIndex = 2;
            // 
            // firstJoinLabel
            // 
            this.firstJoinLabel.AutoSize = true;
            this.firstJoinLabel.BackColor = System.Drawing.Color.Transparent;
            this.firstJoinLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstJoinLabel.ForeColor = System.Drawing.Color.White;
            this.firstJoinLabel.Location = new System.Drawing.Point(3, 10);
            this.firstJoinLabel.Name = "firstJoinLabel";
            this.firstJoinLabel.Size = new System.Drawing.Size(67, 17);
            this.firstJoinLabel.TabIndex = 1;
            this.firstJoinLabel.Text = "First join:";
            // 
            // lastSeenLabel
            // 
            this.lastSeenLabel.AutoSize = true;
            this.lastSeenLabel.BackColor = System.Drawing.Color.Transparent;
            this.lastSeenLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastSeenLabel.ForeColor = System.Drawing.Color.White;
            this.lastSeenLabel.Location = new System.Drawing.Point(3, 38);
            this.lastSeenLabel.Name = "lastSeenLabel";
            this.lastSeenLabel.Size = new System.Drawing.Size(76, 17);
            this.lastSeenLabel.TabIndex = 2;
            this.lastSeenLabel.Text = "Last seen:";
            // 
            // UserOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.kick_btn);
            this.Controls.Add(this.msg_btn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.close_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.panel1.ResumeLayout(false);
            this.optionsContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button msg_btn;
        private System.Windows.Forms.Button kick_btn;
        private System.Windows.Forms.FlowLayoutPanel img_container;
        private System.Windows.Forms.FlowLayoutPanel optionsContainer;
        private NAudio.Gui.VolumeSlider volumeSlider;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lastSeenLabel;
        private System.Windows.Forms.Label firstJoinLabel;
    }
}