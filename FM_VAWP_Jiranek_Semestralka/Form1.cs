using FM_VAWP_Jiranek_Semestralka.Data;
using System.Configuration;

namespace FM_VAWP_Jiranek_Semestralka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {var context = new VAPW_PS_DrivesContext(ConfigurationManager.ConnectionStrings["Pripojeni"].ConnectionString);
         var data = context.DriveData.ToList();


        }
    }
}
