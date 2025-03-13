namespace FM_VAWP_Jiranek_Semestralka
{
    partial class SettingsDialog
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckBox checkBoxAutoRedraw;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelNormalColor;
        private System.Windows.Forms.Panel panelSpeedColor;
        private System.Windows.Forms.Panel panelCurveColor;
        private System.Windows.Forms.Label labelNormalColor;
        private System.Windows.Forms.Label labelSpeedColor;
        private System.Windows.Forms.Label labelCurveColor;
        private System.Windows.Forms.ColorDialog colorDialog1;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.checkBoxAutoRedraw = new System.Windows.Forms.CheckBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelNormalColor = new System.Windows.Forms.Panel();
            this.panelSpeedColor = new System.Windows.Forms.Panel();
            this.panelCurveColor = new System.Windows.Forms.Panel();
            this.labelNormalColor = new System.Windows.Forms.Label();
            this.labelSpeedColor = new System.Windows.Forms.Label();
            this.labelCurveColor = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();


            this.SuspendLayout();

            // 
            // checkBoxAutoRedraw
            // 
            this.checkBoxAutoRedraw.AutoSize = true;
            this.checkBoxAutoRedraw.Location = new System.Drawing.Point(20, 20);
            this.checkBoxAutoRedraw.Name = "checkBoxAutoRedraw";
            this.checkBoxAutoRedraw.Size = new System.Drawing.Size(174, 24);
            this.checkBoxAutoRedraw.TabIndex = 0;
            this.checkBoxAutoRedraw.Text = "Automatické překreslení";
            this.checkBoxAutoRedraw.UseVisualStyleBackColor = true;

            // 
            // panelNormalColor
            // 
            this.panelNormalColor.BackColor = System.Drawing.Color.Blue;
            this.panelNormalColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNormalColor.Location = new System.Drawing.Point(200, 60);
            this.panelNormalColor.Name = "panelNormalColor";
            this.panelNormalColor.Size = new System.Drawing.Size(30, 30);
            this.panelNormalColor.TabIndex = 1;
            this.panelNormalColor.Click += new System.EventHandler(this.panelNormalColor_Click);

            // 
            // panelSpeedColor
            // 
            this.panelSpeedColor.BackColor = System.Drawing.Color.Green;
            this.panelSpeedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpeedColor.Location = new System.Drawing.Point(200, 100);
            this.panelSpeedColor.Name = "panelSpeedColor";
            this.panelSpeedColor.Size = new System.Drawing.Size(30, 30);
            this.panelSpeedColor.TabIndex = 2;
            this.panelSpeedColor.Click += new System.EventHandler(this.panelSpeedColor_Click);

            // 
            // panelCurveColor
            // 
            this.panelCurveColor.BackColor = System.Drawing.Color.Red;
            this.panelCurveColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCurveColor.Location = new System.Drawing.Point(200, 140);
            this.panelCurveColor.Name = "panelCurveColor";
            this.panelCurveColor.Size = new System.Drawing.Size(30, 30);
            this.panelCurveColor.TabIndex = 3;
            this.panelCurveColor.Click += new System.EventHandler(this.panelCurveColor_Click);

            // 
            // labelNormalColor
            // 
            this.labelNormalColor.AutoSize = true;
            this.labelNormalColor.Location = new System.Drawing.Point(20, 60);
            this.labelNormalColor.Name = "labelNormalColor";
            this.labelNormalColor.Size = new System.Drawing.Size(140, 20);
            this.labelNormalColor.TabIndex = 4;
            this.labelNormalColor.Text = "Barva pro normální režim";

            // 
            // labelSpeedColor
            // 
            this.labelSpeedColor.AutoSize = true;
            this.labelSpeedColor.Location = new System.Drawing.Point(20, 100);
            this.labelSpeedColor.Name = "labelSpeedColor";
            this.labelSpeedColor.Size = new System.Drawing.Size(162, 20);
            this.labelSpeedColor.TabIndex = 5;
            this.labelSpeedColor.Text = "Barva pro režim rychlosti";

            // 
            // labelCurveColor
            // 
            this.labelCurveColor.AutoSize = true;
            this.labelCurveColor.Location = new System.Drawing.Point(20, 140);
            this.labelCurveColor.Name = "labelCurveColor";
            this.labelCurveColor.Size = new System.Drawing.Size(162, 20);
            this.labelCurveColor.TabIndex = 6;
            this.labelCurveColor.Text = "Barva pro režim zatáčení";

            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(60, 180);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 30);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Uložit";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(150, 180);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Zrušit";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);

            // 
            // SettingsDialog
            // 
            this.ClientSize = new System.Drawing.Size(300, 250);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelCurveColor);
            this.Controls.Add(this.labelSpeedColor);
            this.Controls.Add(this.labelNormalColor);
            this.Controls.Add(this.panelCurveColor);
            this.Controls.Add(this.panelSpeedColor);
            this.Controls.Add(this.panelNormalColor);
            this.Controls.Add(this.checkBoxAutoRedraw);
            this.Name = "SettingsDialog";
            this.Text = "Nastavení";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
    }
}
