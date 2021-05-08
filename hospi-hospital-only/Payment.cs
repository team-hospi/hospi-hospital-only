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
    public partial class Payment : Form
    {
        DBClass dbc = new DBClass();

        string patientID;
        string patientName;
        string subjectName;
        string hospitalID;

        public string PatientID
        {
            get { return patientID; }
            set { patientID = value; }
        }
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }
        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            textBoxChartNum.Text = patientID;
            textBoxPatientName.Text = patientName;
            textBoxSubject.Text = subjectName;

            dbc.FirstReception(Convert.ToInt32(patientID));
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            if(dbc.ReceptionTable.Rows.Count == 1)
            {
                radioButton2.Checked = true;    //초진
            }
            else if (dbc.ReceptionTable.Rows.Count != 1)
            {
                radioButton1.Checked = true;    //재진
            }
            // 병의원타입 추가
            dbc.FireConnect();
            dbc.Hospital_Open(hospitalID);
            dbc.HospitalTable = dbc.DS.Tables["hospital"];
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void te(object sender, EventArgs e)
        {

        }
    }
}
