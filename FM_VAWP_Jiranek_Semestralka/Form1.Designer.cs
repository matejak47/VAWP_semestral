namespace FM_VAWP_Jiranek_Semestralka
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox comboBoxRecordings;
        private System.Windows.Forms.Panel panelDraw;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelRoll;
        private System.Windows.Forms.Label labelAcceleration;
        private System.Windows.Forms.Panel panelBike;

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
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            listView1 = new System.Windows.Forms.ListView();
            comboBoxRecordings = new System.Windows.Forms.ComboBox();
            panelDraw = new System.Windows.Forms.Panel();
            panelDetails = new System.Windows.Forms.Panel();
            labelSpeed = new System.Windows.Forms.Label();
            labelRoll = new System.Windows.Forms.Label();
            labelAcceleration = new System.Windows.Forms.Label();
            panelBike = new System.Windows.Forms.Panel();

            RadioButton radioCurves = new RadioButton();
            RadioButton radioSpeed = new RadioButton();
            RadioButton radioNormal = new RadioButton();

            SuspendLayout();
            Button btnSettings = new Button();
            btnSettings.Text = "Nastavení";
            btnSettings.Click += (s, e) =>
            {
                OpenSettingsDialog();
            };

            btnSettings.Dock = DockStyle.Top; // Umístění tlačítka na horní část okna
            this.Controls.Add(btnSettings);
            // 
            // tableLayoutPanel1 (Levý panel)
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            tableLayoutPanel1.Width = 350;
            tableLayoutPanel1.Controls.Add(comboBoxRecordings, 0, 0);
            tableLayoutPanel1.Controls.Add(listView1, 0, 1);

            // 
            // comboBoxRecordings (Výběr jízdy)
            // 
            comboBoxRecordings.Dock = System.Windows.Forms.DockStyle.Top;
            comboBoxRecordings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxRecordings.SelectedIndexChanged += new System.EventHandler(ComboBoxRecordings_SelectedIndexChanged);

            // 
            // listView1 (Seznam bodů jízdy)
            // 
            listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Add("ID", 80);
            listView1.Columns.Add("Lat", 100);
            listView1.Columns.Add("Lon", 100);
            listView1.Columns.Add("Speed", 80);
            listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);

            //
            // Draw panel
            //
            panelDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            panelDraw.BackColor = System.Drawing.Color.White;
            panelDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDraw_Paint);
            panelDraw.AutoScroll = false;
            panelDraw.AutoScrollMinSize = new System.Drawing.Size(800, 800); // Menší vykreslení

            // 
            // Přepínače režimů vykreslování
            // 
            radioCurves.Text = "Zatáčení";
            radioCurves.Checked = true;
            radioCurves.CheckedChanged += (s, e) => { drawMode = "zatáčení"; panelDraw.Invalidate(); };

            radioSpeed.Text = "Rychlost";
            radioSpeed.CheckedChanged += (s, e) => { drawMode = "rychlost"; panelDraw.Invalidate(); };

            radioNormal.Text = "Normální";
            radioNormal.CheckedChanged += (s, e) => { drawMode = "normální"; panelDraw.Invalidate(); };

            tableLayoutPanel1.Controls.Add(radioCurves, 0, 2);
            tableLayoutPanel1.Controls.Add(radioSpeed, 0, 3);
            tableLayoutPanel1.Controls.Add(radioNormal, 0, 4);

            // 
            // tableLayoutPanel2 (Střední panel - vykreslení trasy)
            // 
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Controls.Add(panelDraw);

            // 
            // tableLayoutPanel3 (Pravý panel - detail bodu)
            // 
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            tableLayoutPanel3.Width = 300;
            tableLayoutPanel3.Controls.Add(panelDetails);
            tableLayoutPanel3.Controls.Add(panelBike);

            // 
            // panelDetails (Detail bodu - zobrazení informací)
            // 
            panelDetails.Dock = System.Windows.Forms.DockStyle.Top;
            panelDetails.Height = 100;
            panelDetails.Controls.Add(labelSpeed);
            panelDetails.Controls.Add(labelRoll);
            panelDetails.Controls.Add(labelAcceleration);

            // 
            // labelSpeed (Zobrazení rychlosti)
            // 
            labelSpeed.Text = "Rychlost: 0 km/h";
            labelSpeed.Dock = System.Windows.Forms.DockStyle.Top;

            // 
            // labelRoll (Zobrazení náklonu)
            // 
            labelRoll.Text = "Náklon: 0°";
            labelRoll.Dock = System.Windows.Forms.DockStyle.Top;

            // 
            // labelAcceleration (Zobrazení akcelerace)
            // 
            labelAcceleration.Text = "Akcelerace: 0 m/s²";
            labelAcceleration.Dock = System.Windows.Forms.DockStyle.Top;

            // 
            // panelBike (Vizualizace motocyklu)
            // 
            panelBike.Dock = System.Windows.Forms.DockStyle.Fill;
            panelBike.BackColor = System.Drawing.Color.LightGray;
            panelBike.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBike_Paint);

            // 
            // Form1 (Hlavní okno)
            // 
            ClientSize = new System.Drawing.Size(1400, 800);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Aplikace pro analýzu jízd";
            Load += new System.EventHandler(Form1_Load);

            ResumeLayout(false);
        }
    }
}
