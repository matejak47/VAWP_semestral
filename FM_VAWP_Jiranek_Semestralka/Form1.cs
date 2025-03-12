using FM_VAWP_Jiranek_Semestralka.Data;
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
        private string drawMode = "zatáèení"; // nebo "rychlost"

        public Form1()
        {
            InitializeComponent();
            context = new VAPW_PS_DrivesContext(ConfigurationManager.ConnectionStrings["Pripojeni"].ConnectionString);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadRecordings();
        }

        /// <summary>
        /// Naète záznamy jízd do ComboBoxu
        /// </summary>
        private async Task LoadRecordings()
        {
            var recordings = await context.Recordings.ToListAsync();

            comboBoxRecordings.DataSource = recordings;
            comboBoxRecordings.DisplayMember = "StartDateTime";
            comboBoxRecordings.ValueMember = "Id";
        }

        /// <summary>
        /// Reakce na zmìnu výbìru jízdy v ComboBoxu
        /// </summary>
        private async void ComboBoxRecordings_SelectedIndexChanged(object sender, EventArgs e)
        {
       
            if (comboBoxRecordings.SelectedValue is int selectedRecordingId)
            {
                using (var dbContext = new VAPW_PS_DrivesContext(ConfigurationManager.ConnectionStrings["Pripojeni"].ConnectionString))
                {
                    var driveData = await dbContext.DriveData
                                                   .Where(d => d.RecordingId == selectedRecordingId)
                                                   .ToListAsync();

                    listView1.Items.Clear();
                    currentDriveData = driveData; // Uložit pro vykreslení

                    foreach (var item in driveData)
                    {
                        ListViewItem row = new ListViewItem(item.Id.ToString());
                        row.SubItems.Add(item.Lat.ToString());
                        row.SubItems.Add(item.Lon.ToString());
                        row.SubItems.Add(item.Speed.ToString());
                        listView1.Items.Add(row);
                    }

                    panelDraw.Invalidate(); // Pøekreslit trasu
                }
            }
        }

        private List<DriveData> currentDriveData = new List<DriveData>();

        private void panelDraw_Paint(object sender, PaintEventArgs e)
        {
            if (currentDriveData.Count < 2) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            // Získání minimálních a maximálních hodnot souøadnic
            float minX = (float)currentDriveData.Min(d => d.LonRec.GetValueOrDefault());
            float minY = (float)currentDriveData.Min(d => d.LatRec.GetValueOrDefault());
            float maxX = (float)currentDriveData.Max(d => d.LonRec.GetValueOrDefault());
            float maxY = (float)currentDriveData.Max(d => d.LatRec.GetValueOrDefault());

            // **Pøidáme okraj (margin) kolem trasy**
            float margin = 10f;

            // **Zmenšíme trasu (zvìtšíme mìøítko)**
            float scaleX = (panelDraw.Width - margin * 2) / (maxX - minX + 0.00001f);
            float scaleY = (panelDraw.Height - margin * 2) / (maxY - minY + 0.00001f);
            float scale = Math.Min(scaleX, scaleY) * 0.5f; // **Zmenšíme trasu na 80 % panelu**


            // **Posuneme trasu doleva tím, že dáme menší offsetX**
            float offsetX = 50;  // **Negativní hodnota posouvá trasu doleva**
            float offsetY = (panelDraw.Height - (maxY - minY) * scale) / 2;

            Pen linePen = new Pen(Color.Blue, 2);

            for (int i = 1; i < currentDriveData.Count; i++)
            {
                float x1 = ((float)currentDriveData[i - 1].LonRec.GetValueOrDefault() - minX) * scale + offsetX;
                float y1 = panelDraw.Height - (((float)currentDriveData[i - 1].LatRec.GetValueOrDefault() - minY) * scale + offsetY);
                float x2 = ((float)currentDriveData[i].LonRec.GetValueOrDefault() - minX) * scale + offsetX;
                float y2 = panelDraw.Height - (((float)currentDriveData[i].LatRec.GetValueOrDefault() - minY) * scale + offsetY);

                g.DrawLine(linePen, x1, y1, x2, y2);
            }

            // Zvýraznìní prvního a posledního bodu
            g.FillEllipse(Brushes.Green, ((float)currentDriveData[0].LonRec.GetValueOrDefault() - minX) * scale + offsetX - 5,
                                       panelDraw.Height - (((float)currentDriveData[0].LatRec.GetValueOrDefault() - minY) * scale + offsetY) - 5, 10, 10);
            g.FillEllipse(Brushes.Red, ((float)currentDriveData[^1].LonRec.GetValueOrDefault() - minX) * scale + offsetX - 5,
                                     panelDraw.Height - (((float)currentDriveData[^1].LatRec.GetValueOrDefault() - minY) * scale + offsetY) - 5, 10, 10);
        }




        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Bude implementováno pozdìji
        }
    }
}
