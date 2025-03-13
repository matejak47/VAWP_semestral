using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FM_VAWP_Jiranek_Semestralka.Properties;


namespace FM_VAWP_Jiranek_Semestralka
{
    public partial class SettingsDialog : Form
    {
        public bool AutoRedraw { get; private set; }
        public Color NormalColor { get; private set; }
        public Color SpeedColor { get; private set; }
        public Color CurveColor { get; private set; }

        public SettingsDialog()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Načtení uložených nastavení
            AutoRedraw = Properties.Settings.Default.AutoRedraw;
            NormalColor = Properties.Settings.Default.NormalColor;
            SpeedColor = Properties.Settings.Default.SpeedColor;
            CurveColor = Properties.Settings.Default.CurveColor;
            Properties.Settings.Default.Save();

            // Nastavení prvků UI podle uložených hodnot
            checkBoxAutoRedraw.Checked = AutoRedraw;
            panelNormalColor.BackColor = NormalColor;
            panelSpeedColor.BackColor = SpeedColor;
            panelCurveColor.BackColor = CurveColor;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Uložení nastavení
            Properties.Settings.Default.AutoRedraw = checkBoxAutoRedraw.Checked;
            Properties.Settings.Default.NormalColor = NormalColor;
            Properties.Settings.Default.SpeedColor = SpeedColor;
            Properties.Settings.Default.CurveColor = CurveColor;
            Properties.Settings.Default.Save();  // Uložíme do konfigurace

            DialogResult = DialogResult.OK;  // Označíme, že změny byly úspěšně uloženy
            this.Close();  // Zavřeme dialog
        }


        private void buttonSelectNormalColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                NormalColor = colorDialog1.Color;
                panelNormalColor.BackColor = NormalColor;
            }
        }

        private void buttonSelectSpeedColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                SpeedColor = colorDialog1.Color;
                panelSpeedColor.BackColor = SpeedColor;
            }
        }

        private void buttonSelectCurveColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                CurveColor = colorDialog1.Color;
                panelCurveColor.BackColor = CurveColor;
            }
        }
        private void panelNormalColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelNormalColor.BackColor = colorDialog.Color;
            }
        }

        private void panelSpeedColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelSpeedColor.BackColor = colorDialog.Color;
            }
        }

        private void panelCurveColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelCurveColor.BackColor = colorDialog.Color;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Uložení nového nastavení
            Properties.Settings.Default.AutoRedraw = checkBoxAutoRedraw.Checked;
            Properties.Settings.Default.NormalColor = panelNormalColor.BackColor;
            Properties.Settings.Default.SpeedColor = panelSpeedColor.BackColor;
            Properties.Settings.Default.CurveColor = panelCurveColor.BackColor;
            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }



        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
