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
        private System.Windows.Forms.RadioButton radioCurves;
        private System.Windows.Forms.RadioButton radioSpeed;

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
            radioCurves = new System.Windows.Forms.RadioButton();
            radioSpeed = new System.Windows.Forms.RadioButton();

            SuspendLayout();

            // 
            // tableLayoutPanel1 (Levý panel - výběr jízdy a seznam bodů)
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // ComboBox nahoře
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // ListView zabere zbytek
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // RadioButton 1
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize)); // RadioButton 2
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            tableLayoutPanel1.Width = 350;
            tableLayoutPanel1.Controls.Add(comboBoxRecordings, 0, 0);
            tableLayoutPanel1.Controls.Add(listView1, 0, 1);
            tableLayoutPanel1.Controls.Add(radioCurves, 0, 2);
            tableLayoutPanel1.Controls.Add(radioSpeed, 0, 3);

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
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;

            //
            // panelDraw (Střední část - vykreslení trasy)
            //
            panelDraw = new System.Windows.Forms.Panel();
            panelDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            panelDraw.BackColor = System.Drawing.Color.White;
            panelDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDraw_Paint);
            panelDraw.AutoScroll = false;

            // 
            // radioCurves (Režim zatáčení)
            // 
            radioCurves.Text = "Zatáčení";
            radioCurves.Checked = true;
            radioCurves.CheckedChanged += (s, e) => { drawMode = "zatáčení"; panelDraw.Invalidate(); };

            // 
            // radioSpeed (Režim rychlosti)
            // 
            radioSpeed.Text = "Rychlost";
            radioSpeed.CheckedChanged += (s, e) => { drawMode = "rychlost"; panelDraw.Invalidate(); };

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
