using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace hospi_hospital_only
{
    public partial class Hospital_Setting : Form
    {
        DBClass dbc = new DBClass();
        int hospitalID; // 병원코드

        public Hospital_Setting()
        {
            InitializeComponent();
        }

        // 프로퍼티 
        public int HospitalID // Main폼에서 입력된 병원코드를 Reception을 거쳐서 받아옴
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        // 종료
        private void button1_Click(object sender, EventArgs e)
        {
            // 수정버튼 클릭하지 않고 종료시 바로 종료
            if (comboBox1.Enabled == false)
            {
                Dispose();
            }
            // 수정 상태일 경우 DB업데이트 후 종료
            else if (comboBox1.Enabled == true)
            {
                try
                {
                    DialogResult ok = MessageBox.Show("정보 수정을 완료 하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ok == DialogResult.Yes)
                    {
                        dbc.Hospital_Update(hospitalID);
                        dbc.HospitalTable = dbc.DS.Tables["hospital"];
                        DataRow upRow = dbc.HospitalTable.Rows[0];

                        upRow.BeginEdit();
                        upRow["openTime"] = comboBox1.Text + comboBox2.Text;
                        upRow["closeTime"] = comboBox3.Text + comboBox4.Text;
                        upRow["weekendOpenTime"] = comboBox9.Text + comboBox8.Text;
                        upRow["weekendCloseTime"] = comboBox7.Text + comboBox6.Text;
                        if (comboBox5.Text == "개원")
                        {
                            upRow["sundayOpen"] = 1;
                        }
                        else if (comboBox5.Text == "휴원")
                        {
                            upRow["sundayOpen"] = 0;
                        }
                        upRow.EndEdit();

                        dbc.DBAdapter.Update(dbc.DS, "hospital");
                        dbc.DS.AcceptChanges();

                        Dispose();
                    }
                }
                catch (DataException DE)
                {
                    MessageBox.Show(DE.Message);
                }
                catch (Exception DE)
                {
                    MessageBox.Show(DE.Message);
                }
            }
        }

        private void Hospital_Setting_Load(object sender, EventArgs e)
        {
            // 로드시 포커스 설정
            this.ActiveControl = button1;

            // DB오픈
            dbc.Hospital_Open(hospitalID);
            dbc.HospitalTable = dbc.DS.Tables["hospital"];
            DataRow hosRow = dbc.HospitalTable.Rows[0];

            textBox1.Text = hosRow["hospitalID"].ToString();
            textBox2.Text = hosRow["hospitalName"].ToString();
            textBox3.Text = hosRow["hospitalTypeName"].ToString();
            textBox4.Text = hosRow["hospitalAddress"].ToString();
            textBox5.Text = hosRow["hospitalTell"].ToString();
            comboBox1.Text = hosRow["openTime"].ToString().Substring(0,2);
            comboBox2.Text = hosRow["openTime"].ToString().Substring(2,2);
            comboBox3.Text = hosRow["closeTime"].ToString().Substring(0,2);
            comboBox4.Text = hosRow["closeTime"].ToString().Substring(2,2);
            comboBox9.Text = hosRow["weekendOpenTime"].ToString().Substring(0,2);
            comboBox8.Text = hosRow["weekendOpenTime"].ToString().Substring(2,2);
            comboBox7.Text = hosRow["weekendCloseTime"].ToString().Substring(0,2);
            comboBox6.Text = hosRow["weekendCloseTime"].ToString().Substring(2,2);
            if (Convert.ToInt32(hosRow["sundayopen"]) == 0)
            {
                comboBox5.Text = "휴원";
            }
            else if(Convert.ToInt32(hosRow["sundayopen"]) == 1)
            {
                comboBox5.Text = "개원";
            }
            if (Convert.ToInt32(hosRow["Reservation"]) == 1)
            {
                button18_Click(sender, e);
            }
            else if(Convert.ToInt32(hosRow["Reservation"]) == 0)
            {
                button17_Click(sender, e);
            }
        }

        // 모바일 예약 재개
        private void button18_Click(object sender, EventArgs e)
        {
            // 라벨 
            label13.Text = "금일 모바일 예약 접수중";
            label13.ForeColor = Color.Black;

            // 버튼
            button18.Text = "▶ " + button18.Text + " ◀";
            button17.Text = "모바일 예약 마감";

            //DB
            try
            {
                dbc.Hospital_Update(hospitalID);
                dbc.HospitalTable = dbc.DS.Tables["hospital"];
                DataRow upRow = dbc.HospitalTable.Rows[0];

                upRow.BeginEdit();
                upRow["Reservation"] = 1;
                upRow.EndEdit();

                dbc.DBAdapter.Update(dbc.DS, "hospital");
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
        }

        // 모바일 예약 마감
        private void button17_Click(object sender, EventArgs e)
        {
            // 라벨
            label13.Text = "금일 모바일 예약 마감";
            label13.ForeColor = Color.Red;

            // 버튼
            button17.Text = "▶ " + button17.Text + " ◀";
            button18.Text = "모바일 예약 재개";

            //DB
            try
            {
                dbc.Hospital_Update(hospitalID);
                dbc.HospitalTable = dbc.DS.Tables["hospital"];
                DataRow upRow = dbc.HospitalTable.Rows[0];

                upRow.BeginEdit();
                upRow["Reservation"] = 0;
                upRow.EndEdit();

                dbc.DBAdapter.Update(dbc.DS, "hospital");
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
        }

        // 수정 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Enabled == false)
            {
                // 수정클릭
                button2.Text = "저장 후 종료";
                button3.Enabled = false;
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;
                comboBox7.Enabled = true;
                comboBox8.Enabled = true;
                comboBox9.Enabled = true;
            }
        }
    }
}
