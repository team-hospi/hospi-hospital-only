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
        string receptionDate;
        string receptionTime;

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
                textBoxType.Text = "초진";
            }
            else if (dbc.ReceptionTable.Rows.Count != 1)
            {
                textBoxType.Text = "재진";
            }
            // 병의원타입 추가
            dbc.FireConnect();
            dbc.Hospital_Open(hospitalID);
            if(DBClass.hospikind == "대학")
            {
                textBoxHospiKind.Text = "대학 병원";
                textBox3.Text = "15000";
            }
            else if(DBClass.hospikind == "종합")
            {
                textBoxHospiKind.Text = "종합 병원";
                textBox3.Text = "10000";
            }
            else if(DBClass.hospikind == "의원")
            {
                textBoxHospiKind.Text = "의원";
                textBox3.Text = "5000";
            }
            textBox4.Text = (Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text)).ToString();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void te(object sender, EventArgs e)
        {

        }

        // 카드
        private void button2_Click(object sender, EventArgs e)
        {
            dbc.Reception_Date(receptionDate, receptionTime, patientID);
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            DataRow upRow = dbc.ReceptionTable.Rows[0];

            upRow.BeginEdit();
            upRow["payment"] = "카드";
            upRow["price"] = textBox4.Text;
            upRow["receptionType"] = 3;
            upRow.EndEdit();
            dbc.DBAdapter.Update(dbc.DS, "reception");
            dbc.DS.AcceptChanges();

            DialogResult ok = MessageBox.Show("카드 결제는 별도의 단말에서 진행해주세요. \r\n결제완료 처리 하시겠습니까?", "알림", MessageBoxButtons.YesNo);
            if (ok == DialogResult.Yes)
            {
                Dispose();
            }
        }

        // 현금
        private void button1_Click(object sender, EventArgs e)
        {
            dbc.Reception_Date(receptionDate, receptionTime, patientID);
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            DataRow upRow = dbc.ReceptionTable.Rows[0];

            upRow.BeginEdit();
            upRow["payment"] = "현금";
            upRow["price"] = textBox4.Text;
            upRow["receptionType"] = 3;
            upRow.EndEdit();
            dbc.DBAdapter.Update(dbc.DS, "reception");
            dbc.DS.AcceptChanges();

            DialogResult ok = MessageBox.Show("현금으로 결제완료 처리 하시겠습니까?.", "알림", MessageBoxButtons.YesNo);
            if (ok == DialogResult.Yes)
            {
                Dispose();
            }
        }
    }
}
