using FM_VAWP_Jiranek_Semestralka.Data;
using FM_VAWP_Jiranek_Semestralka.Lib;
using FM_VAWP_Jiranek_Semestralka.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FM_VAWP_Jiranek_Semestralka
{
    public partial class Form1 : Form
    {
        private VAPW_PS_DrivesContext context;
        private Color normalColor;
        private Color speedColor;
        private Color curveColor;
        private string drawMode = "zatáčení"; // nebo "rychlost"
        private List<DriveData> currentDriveData = new List<DriveData>();
        private DriveData selectedDriveData = null;

        public Form1()
        {
            InitializeComponent();
            context = new VAPW_PS_DrivesContext(ConfigurationManager.ConnectionStrings["Pripojeni"].ConnectionString);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadRecordings();
            LoadSettings();
        }

        /// <summary>
        /// Načte záznamy jízd do ComboBoxu
        /// </summary>
        private async Task LoadRecordings()
        {
            var recordings = await context.Recordings.ToListAsync();

            comboBoxRecordings.DataSource = recordings;
            comboBoxRecordings.DisplayMember = "StartDateTime";
            comboBoxRecordings.ValueMember = "Id";
        }

        private void LoadSettings()
        {
            // Načtení barev a dalších nastavení
            normalColor = Properties.Settings.Default.NormalColor;
            speedColor = Properties.Settings.Default.SpeedColor;
            curveColor = Properties.Settings.Default.CurveColor;
        }

        /// <summary>
        /// Reakce na změnu výběru jízdy v ComboBoxu
        /// </summary>
        private async void ComboBoxRecordings_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh();
            if (comboBoxRecordings.SelectedValue is int selectedRecordingId)
            {
                using (var dbContext = new VAPW_PS_DrivesContext(ConfigurationManager.ConnectionStrings["Pripojeni"].ConnectionString))
                {
                    var driveData = await dbContext.DriveData
                                                   .Where(d => d.RecordingId == selectedRecordingId)
                                                   .ToListAsync();

                    listView1.Items.Clear();
                    currentDriveData = driveData; // Uložit pro vykreslení

                    // Výpočet rychlosti a akcelerace
                    for (int i = 1; i < currentDriveData.Count; i++)
                    {
                        double distance = GeoUtils.CalculateDistance(
                            currentDriveData[i - 1].Lat, currentDriveData[i - 1].Lon,
                            currentDriveData[i].Lat, currentDriveData[i].Lon
                        );

                        double timeDiff = (currentDriveData[i].Time - currentDriveData[i - 1].Time) / 1000.0; // Čas v sekundách

                        if (timeDiff > 0)
                        {
                            float speed_mps = (float)(distance / timeDiff); // m/s
                            float acceleration = (speed_mps - currentDriveData[i - 1].Speed) / (float)timeDiff; // m/s²

                            currentDriveData[i].Speed = speed_mps; // Ukládáme rychlost v m/s
                            currentDriveData[i].Acceleration = acceleration; // Ukládáme akceleraci v m/s²
                        }
                        else
                        {
                            currentDriveData[i].Speed = 0;
                            currentDriveData[i].Acceleration = 0;
                        }
                    }

                    // Naplnění ListView
                    foreach (var item in driveData)
                    {
                        ListViewItem row = new ListViewItem(item.Id.ToString());
                        row.SubItems.Add(item.Lat.ToString());
                        row.SubItems.Add(item.Lon.ToString());
                        row.SubItems.Add(item.Speed.ToString("0.00"));
                        listView1.Items.Add(row);
                    }

                    panelDraw.Invalidate(); // Překreslit trasu
                }
            }
        }

        private void panelDraw_Paint(object sender, PaintEventArgs e)
        {
            if (currentDriveData.Count < 2) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            Pen pen = new Pen(normalColor, 2); // Výchozí barva pro normální režim

            // **Získání minimálních a maximálních hodnot souřadnic**
            float minX = (float)currentDriveData.Min(d => d.LonRec.GetValueOrDefault());
            float minY = (float)currentDriveData.Min(d => d.LatRec.GetValueOrDefault());
            float maxX = (float)currentDriveData.Max(d => d.LonRec.GetValueOrDefault());
            float maxY = (float)currentDriveData.Max(d => d.LatRec.GetValueOrDefault());

            // **Zmenšení a posun trasy**
            float margin = 10f;
            float scaleX = (panelDraw.Width - margin * 2) / (maxX - minX + 0.00001f);
            float scaleY = (panelDraw.Height - margin * 2) / (maxY - minY + 0.00001f);
            float scale = Math.Min(scaleX, scaleY) * 0.5f; // **Zmenšujeme na 80 %**
            float offsetX = 50;  // **Posun do leva**
            float offsetY = (panelDraw.Height - (maxY - minY) * scale) / 2;

            // **Vykreslení čar mezi body s ohledem na režim vykreslování**
            for (int i = 1; i < currentDriveData.Count; i++)
            {
                float x1 = ((float)currentDriveData[i - 1].LonRec.GetValueOrDefault() - minX) * scale + offsetX;
                float y1 = panelDraw.Height - (((float)currentDriveData[i - 1].LatRec.GetValueOrDefault() - minY) * scale + offsetY);
                float x2 = ((float)currentDriveData[i].LonRec.GetValueOrDefault() - minX) * scale + offsetX;
                float y2 = panelDraw.Height - (((float)currentDriveData[i].LatRec.GetValueOrDefault() - minY) * scale + offsetY);
                float roll = currentDriveData[i].Roll;  // Hodnota náklonu
                float tolerancer = 1.0f; // **Toleranční pásmo**

                if (drawMode == "zatáčení")
                {
                    if (roll > tolerancer)
                        pen = new Pen(speedColor, 2); // **Levý náklon**
                    else if (roll < -tolerancer)
                        pen = new Pen(curveColor, 2); // **Pravý náklon**
                    else
                        pen = new Pen(normalColor, 2); // **Neutrální (příliš malý náklon)**

                    g.DrawLine(pen, x1, y1, x2, y2);

                    // **Zvýraznění bodu, pokud je v zatáčce (InCurve == true)**
                    if (currentDriveData[i].InCurve == true)
                    {
                        g.FillEllipse(Brushes.Red, x2 - 3, y2 - 3, 6, 6); // Červený bod
                    }
                }
                else if (drawMode == "normální")
                {
                    pen = new Pen(normalColor, 2); // **Černé čáry pro normální režim**
                }
                else if (drawMode == "rychlost")
                {
                    float tolerance = 0.05f;
                    float acceleration = currentDriveData[i].Acceleration;

                    if (acceleration < -tolerance)
                        pen = new Pen(speedColor, 2); // Brzdění
                    else if (acceleration > tolerance)
                        pen = new Pen(curveColor, 2); // Zrychlení
                    else
                        pen = new Pen(normalColor, 2); // Konstantní rychlost
                }
                else
                {
                    pen = new Pen(Color.Blue, 2);
                }

                g.DrawLine(pen, x1, y1, x2, y2);
            }

            // **Zvýraznění prvního a posledního bodu**
            g.FillEllipse(Brushes.Green, ((float)currentDriveData[0].LonRec.GetValueOrDefault() - minX) * scale + offsetX - 5,
                                               panelDraw.Height - (((float)currentDriveData[0].LatRec.GetValueOrDefault() - minY) * scale + offsetY) - 5, 10, 10);
            g.FillEllipse(Brushes.Red, ((float)currentDriveData[^1].LonRec.GetValueOrDefault() - minX) * scale + offsetX - 5,
                                             panelDraw.Height - (((float)currentDriveData[^1].LatRec.GetValueOrDefault() - minY) * scale + offsetY) - 5, 10, 10);
        }


        private void panelBike_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.LightGray);

            // **Rozměry panelu**
            int centerX = panelBike.Width / 2;
            int centerY = panelBike.Height / 2;
            int bikeWidth = 60;
            int bikeHeight = 20;

            // **Aktuální hodnota Roll (náklonu)**
            float rollAngle = selectedDriveData != null ? selectedDriveData.Roll : 0;

            // **Převod na radiány**
            float angleRad = rollAngle * (float)(Math.PI / 180.0);

            // **Vypočítání bodů pro nakloněný motocykl**
            PointF[] bikePoints = {
        new PointF(centerX - bikeWidth / 2, centerY - bikeHeight / 2),
        new PointF(centerX + bikeWidth / 2, centerY - bikeHeight / 2),
        new PointF(centerX + bikeWidth / 2, centerY + bikeHeight / 2),
        new PointF(centerX - bikeWidth / 2, centerY + bikeHeight / 2)
    };

            // **Transformace bodů podle úhlu náklonu**
            for (int i = 0; i < bikePoints.Length; i++)
            {
                float x = bikePoints[i].X - centerX;
                float y = bikePoints[i].Y - centerY;
                bikePoints[i].X = centerX + (float)(x * Math.Cos(angleRad) - y * Math.Sin(angleRad));
                bikePoints[i].Y = centerY + (float)(x * Math.Sin(angleRad) + y * Math.Cos(angleRad));
            }

            // **Vykreslení motocyklu**
            g.FillPolygon(Brushes.Black, bikePoints);
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                selectedDriveData = currentDriveData[index];

                // Aktualizace informací
                labelSpeed.Text = $"Rychlost: {selectedDriveData.Speed:F2} km/h";
                labelRoll.Text = $"Náklon: {selectedDriveData.Roll:F1}°";
                labelAcceleration.Text = $"Akcelerace: {selectedDriveData.Acceleration:F2} m/s²";

                // Překreslení motocyklu
                panelBike.Invalidate();
            }
        }
        private void OpenSettingsDialog()
        {
            using (SettingsDialog settingsDialog = new SettingsDialog())
            {
                settingsDialog.Owner = this;
                if (settingsDialog.ShowDialog() == DialogResult.OK)
                {
                    ApplySettings();
                }
            }
        }




        public void ApplySettings()
        {
            // Načtení nových barev z nastavení
            normalColor = Properties.Settings.Default.NormalColor;
            speedColor = Properties.Settings.Default.SpeedColor;
            curveColor = Properties.Settings.Default.CurveColor;

            // Překreslení všech panelů, aby se změny projevily ihned
            panelDraw.Invalidate();
            panelBike.Invalidate();
        }


    }
}
