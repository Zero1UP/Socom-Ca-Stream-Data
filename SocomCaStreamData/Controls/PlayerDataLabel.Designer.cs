namespace SocomCaStreamData.Controls
{
    partial class PlayerDataLabel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_PlayerName = new System.Windows.Forms.Label();
            this.hb_PlayerHealth = new SocomCaStreamData.Controls.HealthBar();
            this.SuspendLayout();
            // 
            // lbl_PlayerName
            // 
            this.lbl_PlayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lbl_PlayerName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_PlayerName.Font = new System.Drawing.Font("Arial", 8.5F, System.Drawing.FontStyle.Bold);
            this.lbl_PlayerName.ForeColor = System.Drawing.Color.White;
            this.lbl_PlayerName.Location = new System.Drawing.Point(0, 0);
            this.lbl_PlayerName.Name = "lbl_PlayerName";
            this.lbl_PlayerName.Size = new System.Drawing.Size(100, 19);
            this.lbl_PlayerName.TabIndex = 0;
            this.lbl_PlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_PlayerName.Click += new System.EventHandler(this.lbl_PlayerName_Click);
            // 
            // hb_PlayerHealth
            // 
            this.hb_PlayerHealth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hb_PlayerHealth.healthSet = false;
            this.hb_PlayerHealth.Location = new System.Drawing.Point(0, 17);
            this.hb_PlayerHealth.Name = "hb_PlayerHealth";
            this.hb_PlayerHealth.playerHealth = 100;
            this.hb_PlayerHealth.Size = new System.Drawing.Size(100, 2);
            this.hb_PlayerHealth.TabIndex = 1;
            // 
            // PlayerDataLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.hb_PlayerHealth);
            this.Controls.Add(this.lbl_PlayerName);
            this.Name = "PlayerDataLabel";
            this.Size = new System.Drawing.Size(100, 19);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_PlayerName;
        private HealthBar hb_PlayerHealth;
    }
}
