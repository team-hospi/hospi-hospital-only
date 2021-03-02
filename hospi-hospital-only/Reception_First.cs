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
    public partial class Reception_First : Form
    {
        DBClass dbc = new DBClass();
        int hospitalID; // 병원코드

        public Reception_First()
        {
            InitializeComponent();
        }

        // 병원코드 받아오기위한 프로퍼티
        public int HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        private void Receipt_First_Load(object sender, EventArgs e)
        {
             // 폼 로드시 수신자명 포커스
            this.ActiveControl = textBox1;

            //  접수 시간
            textBox5.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            // 차트번호 DB오픈
            dbc.Visitor_Chart(hospitalID);
            dbc.VisitorTable = dbc.DS.Tables["visitor"];
            DataRow chartRow = dbc.VisitorTable.Rows[0];
            textBox2.Text = (Convert.ToInt32(chartRow["count(*)"])+1).ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBoxB1.Text == "" || textBoxB1.Text == "" || 
                phone1.Text == "" ||  phone2.Text == "" ||  phone3.Text == "" ||  textBoxADD.Text == "")
            {
                MessageBox.Show("인적사항에 공백이 있습니다.", "알림");
            }
            else if(textBoxB1.TextLength != 6 || textBoxB2.TextLength != 7 )
            {
                MessageBox.Show("주민등록번호 형식이 잘못되었습니다.", "알림");
                // 문자열 입력방지 추가해야함
            }
            else if(phone1.TextLength != 3 || phone2.TextLength != 4 || phone3.TextLength != 4)
            {
                MessageBox.Show("전화번호 형식이 잘못되었습니다.", "알림");
                // 문자열 입력방지 추가해야함
            }
            else
            {
                DialogResult ok = MessageBox.Show("수진자 : "+textBox1.Text+"\r접수를 완료하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc.Visitor_Open();
                        dbc.DBAdapter.Fill(dbc.DS, "visitor");
                        dbc.VisitorTable = dbc.DS.Tables["visitor"];
                        DataRow newRow = dbc.VisitorTable.NewRow();

                        // newRow["HospitalID"] = hospitalID;
                        newRow["PatientID"] = textBox2.Text;
                        newRow["PatientName"] = textBox1.Text;
                        newRow["PatientBirthCode"] = textBoxB1.Text +"-"+ textBoxB2.Text;
                        newRow["PatientPhone"] = phone1.Text + phone2.Text + phone3.Text;
                        newRow["PatientAddress"] = textBoxADD.Text;

                        dbc.VisitorTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "visitor");
                        dbc.DS.AcceptChanges();
                    }
                    catch (DataException DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                    catch (Exception DE)
                    {
                        MessageBox.Show(DE.Message);
                    }
                    Dispose();
                }
            }
        }
    }
}
