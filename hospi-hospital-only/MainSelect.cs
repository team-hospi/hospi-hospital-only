using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class MainSelect : Form
    {
        DBClass dbc = new DBClass();
        string hospitalID;

        public string HospitalID // Main폼에서 입력된 병원코드를 받아옴
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        public MainSelect()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reception reception = new Reception();
            reception.HospitalID = hospitalID;
            reception.ShowDialog();
            this.Dispose();
        }

        private void MainSelect_Load(object sender, EventArgs e)
        {
            dbc.FireConnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main2 main2 = new Main2();
            main2.HospitalID = hospitalID;
            main2.ShowDialog();
            this.Dispose();
        }
    }
}
