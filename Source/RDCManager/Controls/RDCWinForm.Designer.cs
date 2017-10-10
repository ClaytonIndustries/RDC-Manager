namespace RDCManager.Controls
{
    partial class RDCWinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RDCWinForm));
            this.axMsTscAxNotSafeForScripting = new AxMSTSCLib.AxMsTscAxNotSafeForScripting();
            ((System.ComponentModel.ISupportInitialize)(this.axMsTscAxNotSafeForScripting)).BeginInit();
            this.SuspendLayout();
            // 
            // axMsTscAxNotSafeForScripting
            // 
            this.axMsTscAxNotSafeForScripting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMsTscAxNotSafeForScripting.Enabled = true;
            this.axMsTscAxNotSafeForScripting.Location = new System.Drawing.Point(0, 0);
            this.axMsTscAxNotSafeForScripting.Name = "axMsTscAxNotSafeForScripting";
            this.axMsTscAxNotSafeForScripting.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMsTscAxNotSafeForScripting.OcxState")));
            this.axMsTscAxNotSafeForScripting.Size = new System.Drawing.Size(262, 260);
            this.axMsTscAxNotSafeForScripting.TabIndex = 0;
            // 
            // RDCWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axMsTscAxNotSafeForScripting);
            this.Name = "RDCWinForm";
            this.Size = new System.Drawing.Size(262, 260);
            ((System.ComponentModel.ISupportInitialize)(this.axMsTscAxNotSafeForScripting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSTSCLib.AxMsTscAxNotSafeForScripting axMsTscAxNotSafeForScripting;
    }
}
