using FM_VAWP_Jiranek_Semestralka.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace FM_VAWP_Jiranek_Semestralka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var context = new VAPW_PS_DrivesContext(ConfigurationManager.ConnectionStrings["Pripojeni"].ConnectionString);
            var data = context.DriveData.ToList();
            listView1.View = View.Details; 
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Recording ID", 100);
            listView1.Columns.Add("Lat", 100);
            listView1.Columns.Add("Lon", 100);
            listView1.Columns.Add("Speed", 100);
            var list = await context.DriveData.Take(100).ToListAsync();

            foreach (var item in list)
            {
                ListViewItem row =new ListViewItem(item.Id.ToString());
                row.SubItems.Add(item.RecordingId.ToString());
                row.SubItems.Add(item.Lat.ToString());
                row.SubItems.Add(item.Lon.ToString());
                row.SubItems.Add(item.Speed.ToString());
                listView1.Items.Add(row);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
