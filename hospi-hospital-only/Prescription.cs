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
    public partial class Prescription : Form
    {
        DBClass dbc = new DBClass();

        string patientID;
        string receptionTime;
        string receptionDate;

        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }
        public string ReceptionTime
        {
            get { return receptionTime; }
            set { receptionTime = value; }
        }
        public string ReceptionDate
        {
            get { return receptionDate; }
            set { receptionDate = value; }
        }

        public Prescription()
        {
            InitializeComponent();
        }

        private void Prescription_Load(object sender, EventArgs e)
        {
            dbc.Presctiption_Select(patientID, receptionDate, receptionTime);
            dbc.PrescriptionTable = dbc.DS.Tables["prescription"];
            dataGridView1.DataSource = dbc.PrescriptionTable.DefaultView;
        }
    }
}
