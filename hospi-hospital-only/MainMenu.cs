using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace hospi_hospital_only
{
    public partial class MainMenu : Form
    {
        string hospitalID;

        DBClass dbc = new DBClass();
        Main main = new Main();
        CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");

        public MainMenu()
        {
            InitializeComponent();
            dbc.FireConnect();
        }

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            // 날짜정보
            string date = DateTime.Now.ToString("yyyy-MM-dd ddd요일 ", cultures);
            string time = DateTime.Now.ToString("tt hh:mm", cultures);
            labelDate.Text = date;
            labelTime.Text = time;
            timer1.Start();

            // 접수처
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            for(int i=0; i<dbc.ReceptionistTable.Rows.Count; i++)
            {
                string name = dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString();
                int length = name.Length;
                if (name.Substring(length-1) != ")")
                {
                    comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
            }

            // 진료실
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            for (int i = 0; i < dbc.SubjectTable.Rows.Count; i++)
            {
                string name = dbc.SubjectTable.Rows[i]["subjectName"].ToString();
                int length = name.Length;
                if (name.Substring(length - 1) != ")")
                {
                    comboBoxOffice.Items.Add(dbc.SubjectTable.Rows[i]["subjectName"]);
                }
            }
        }

        private void buttonDispose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("tt hh:mm", cultures);
            labelTime.Text = time;
        }

        // 진료실
        private void buttonOffice_Click(object sender, EventArgs e)
        {
            if(comboBoxOffice.Text != "진료과목 선택")
            {
                Office office = new Office();
                office.SubjectID = comboBoxOffice.Text;
                this.Visible = false;
                office.ShowDialog();
                this.Visible = true;
                comboBoxOffice.Text = "진료과목 선택";
            }
            else
            {
                MessageBox.Show("과목을 선택해주세요.", "알림");
            }
        }

        // 접수처
        private void buttonReception_Click(object sender, EventArgs e)
        {
            if(comboBoxReceptionist.Text != "접수자 선택")
            {
                Reception reception = new Reception();
                reception.HospitalID = hospitalID;
                this.Visible = false;
                reception.ShowDialog();
                this.Visible = true;
                comboBoxReceptionist.Text = "접수자 선택";
            }
            else
            {
                MessageBox.Show("접수자를 선택해주세요.", "알림");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 병원설정변경
        private void button3_Click(object sender, EventArgs e)
        {
            Hospital_Setting hospital_Setting = new Hospital_Setting();
            hospital_Setting.HospitalID = hospitalID;
            hospital_Setting.Show();
        }

        // 문의확인
        private void button4_Click(object sender, EventArgs e)
        {
            InquiryCheck inquiry = new InquiryCheck();
            inquiry.HospitalID = hospitalID;
            inquiry.ShowDialog();
        }
    }
}
