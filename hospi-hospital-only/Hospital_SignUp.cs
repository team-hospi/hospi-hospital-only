using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    public partial class Hospital_SignUp : Form
    {
        string productKeyForSchema;

        public string ProductKeyForSchema
        {
            get { return productKeyForSchema; }
            set { productKeyForSchema = value; }
        }

        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string pw { get; set; }

        int listBoxIndex;
        int iDCheck;
        DBClass dbc = new DBClass();
        string SHApw;

        Boolean holistate;
        Boolean endState;
        Boolean status;
        List<string> department = new List<string>();

        FirestoreDb fs;
        private string path;

        public Hospital_SignUp()
        {
            InitializeComponent();
        }

        private void Hospital_SignUp_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxHospitalName;
            FireConnect();
            dbc.Delay(400);

        }

        //과 추가
        public void AddDepartment()
        {
            if (checkBox1.Checked)
                department.Add(checkBox1.Text);
            if (checkBox2.Checked)
                department.Add(checkBox2.Text);
            if (checkBox3.Checked)
                department.Add(checkBox3.Text);
            if (checkBox4.Checked)
                department.Add(checkBox4.Text);
            if (checkBox5.Checked)
                department.Add(checkBox5.Text);
            if (checkBox6.Checked)
                department.Add(checkBox6.Text);
            if (checkBox7.Checked)
                department.Add(checkBox7.Text);
            if (checkBox8.Checked)
                department.Add(checkBox8.Text);
            if (checkBox9.Checked)
                department.Add(checkBox9.Text);
            if (checkBox10.Checked)
                department.Add(checkBox10.Text);
            if (checkBox11.Checked)
                department.Add(checkBox11.Text);
            if (checkBox12.Checked)
                department.Add(checkBox12.Text);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                department.Add(listBox1.Items[i].ToString());
            }
        }

        //Hospital추가
        public void AddHospital()
        {
            if (HoliState.Text == "휴원")
            { holistate = false; }
            else if (HoliState.Text == "개원")
            { holistate = true; }

            if (EndState.Text == "휴원")
            { endState = false; }
            else if (EndState.Text == "개원")
            { endState = true; }


            CollectionReference coll = fs.Collection("hospitals");
            if (textBoxAddAddress.Text == "" && textBoxAddAddress.Text == " ")
            {
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                {"address", textBoxHospitalAddress.Text},
                {"department", department},
                {"holidayClose", HoliClose1.Text + ":" + HoliClose2.Text },
                {"holidayOpen",  holiOpen1.Text + ":" + holiOpen2.Text},
                {"holidayStatus", holistate},
                {"id", productKeyForSchema},
                {"kind", HospitalType.Text},
                {"lunchTime", lunch1.Text + ":" + lunch2.Text },
                {"name", textBoxHospitalName.Text},
                {"saturdayClose", EndClose1.Text + ":" + EndClose2.Text},
                {"saturdayOpen", EndOpen1.Text + ":" + EndOpen2.Text },
                {"saturdayStatus", endState},
                {"status", true},
                {"tel", textBoxTell1.Text + "-" + textBoxTell2.Text + "-" + textBoxTell3.Text },
                {"todayReservation", true},
                {"weekdayClose", DayClose1.Text + ":" + DayClose2.Text},
                {"weekdayOpen", DayOpen1.Text + ":" + DayOpen2.Text}
                };
                coll.AddAsync(data1);
            }
            else if (textBoxAddAddress.Text != "" && textBoxAddAddress.Text != " ")
            {
                Dictionary<string, object> data1 = new Dictionary<string, object>()
                {
                {"address", textBoxHospitalAddress.Text + " " + textBoxAddAddress.Text},
                {"department", department},
                {"holidayClose", HoliClose1.Text + ":" + HoliClose2.Text },
                {"holidayOpen",  holiOpen1.Text + ":" + holiOpen2.Text},
                {"holidayStatus", holistate},
                {"id", productKeyForSchema},
                {"kind", HospitalType.Text},
                {"lunchTime", lunch1.Text + ":" + lunch2.Text },
                {"name", textBoxHospitalName.Text},
                {"saturdayClose", EndClose1.Text + ":" + EndClose2.Text},
                {"saturdayOpen", EndOpen1.Text + ":" + EndOpen2.Text },
                {"saturdayStatus", endState},
                {"status", true},
                {"tel", textBoxTell1.Text + "-" + textBoxTell2.Text + "-" + textBoxTell3.Text },
                {"todayReservation", true},
                {"weekdayClose", DayClose1.Text + ":" + DayClose2.Text},
                {"weekdayOpen", DayOpen1.Text + ":" + DayOpen2.Text}
                };
                coll.AddAsync(data1);
            }
        }

        //기타 항목 추가
        private void buttonSubjectAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBoxEtc.Text);
            textBoxEtc.Clear();
        }

        //기타 항목 제거
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBoxIndex);
        }

        //선택된 기타 항목
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxIndex = listBox1.SelectedIndex;
        }

        //등록완료 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            //병원정보 확인
            if (textBoxHospitalName.Text != "" && HospitalType.Text != "" && textBoxTell1.Text != "" && textBoxTell2.Text != "" && textBoxTell3.Text != "" && textBoxHospitalAddress.Text != "")
            {
                //영업시간 확인
                if (DayOpen1.Text != "" && DayOpen2.Text != "" && DayClose1.Text != "" && DayClose2.Text != "" && EndClose1.Text != "" && EndClose2.Text != "" && EndOpen1.Text != "" && EndOpen2.Text != "")
                {
                    //영업시간 확인2
                    if (lunch1.Text != "" && lunch2.Text != "" & holiOpen1.Text != "" && holiOpen2.Text != "" && HoliClose1.Text != "" && HoliClose2.Text != "" && HoliState.Text != "" && EndState.Text != "")
                    {
                        AddDepartment();
                        if (department.Count == 0)
                        {
                            MessageBox.Show("진료 과를 선택해주세요.", "알림");
                        }
                        else
                        {
                            if (CreateSchema(productKeyForSchema) == true)
                            {
                                SaveProductKey(productKeyForSchema);
                            }

                            StaffLogin staffLogin = new StaffLogin();
                            staffLogin.HospitalID = DBClass.hospiID;

                            
                            AddHospital();
                            dbc.Delay(200);
                            MessageBox.Show("병원 등록이 완료되었습니다", "알림");
                            staffLogin.ShowDialog();
                            Dispose();
                        }
                    }
                    else { MessageBox.Show("영업시간을 확인해주세요.", "알림"); }
                }
                else { MessageBox.Show("영업시간을 확인해주세요.", "알림"); }
            }
            else { MessageBox.Show("병원정보를 확인해주세요.", " 알림"); }

        }

        // 진료과목 값 저장
        private DataTable GetSubject()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("subjectName");

            if (checkBox1.Checked) dt.Rows.Add(checkBox1.Text);
            if (checkBox2.Checked) dt.Rows.Add(checkBox2.Text);
            if (checkBox3.Checked) dt.Rows.Add(checkBox3.Text);
            if (checkBox4.Checked) dt.Rows.Add(checkBox4.Text);
            if (checkBox5.Checked) dt.Rows.Add(checkBox5.Text);
            if (checkBox6.Checked) dt.Rows.Add(checkBox6.Text);
            if (checkBox7.Checked) dt.Rows.Add(checkBox7.Text);
            if (checkBox8.Checked) dt.Rows.Add(checkBox8.Text);
            if (checkBox9.Checked) dt.Rows.Add(checkBox9.Text);
            if (checkBox10.Checked) dt.Rows.Add(checkBox10.Text);
            if (checkBox11.Checked) dt.Rows.Add(checkBox11.Text);
            if (checkBox12.Checked) dt.Rows.Add(checkBox12.Text);

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                dt.Rows.Add(listBox1.Items[i]);
            }


            return dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        //주소 검색버튼 클릭
        private void button4_Click_1(object sender, EventArgs e)
        {
            SearchAddress frm = new SearchAddress();
            frm.ShowDialog();

            if (frm.Tag == null) { return; }
            DataRow dr = (DataRow)frm.Tag;

            textBoxAdCode.Text = dr["zonecode"].ToString();
            textBoxHospitalAddress.Text = dr["ADDR1"].ToString() + " " + dr["EX"].ToString();
            textBoxAddAddress.Text = "";

            textBoxAddAddress.Focus();
        }

        private bool CreateSchema(string SchemaName)    // 스키마, 테이블 생성, 인서트
        {
            bool success = false;
            try
            {
                DBClass dbc = new DBClass();
                DataTable dt = GetSubject();

                dbc.CreateSchema(SchemaName);
                for (int i = 1; i <= 13; i++)
                {
                    dbc.CreateTableStructure(i, SchemaName);
                }
                dbc.InsertSubjectName(SchemaName, dt);

                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return success;
        }

        private void SaveProductKey(string ProductKeyValue)
        {
            Properties.Settings.Default.ProductKey = ProductKeyValue;
            Properties.Settings.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }


        //Firestore 연결
        public void FireConnect()
        {
            FBKey fbKey = new FBKey();

            try
            {
                fbKey.DownloadFile();
                path = fbKey.TempKeyFilePath;
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

                fs = FirestoreDb.Create("hospi-edcf9");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                fbKey.DeleteTemp();
            }

            fbKey.DeleteTemp();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            textBoxHospitalName.Text = "부천대학병원";
            HospitalType.Text = "대학";
            textBoxTell1.Text = "010";
            textBoxTell2.Text = "1234";
            textBoxTell3.Text = "5678";
            DayOpen1.Text = "08";
            DayOpen2.Text = "00";
            DayClose1.Text = "17";
            DayClose2.Text = "30";
            EndOpen1.Text = "08";
            EndOpen2.Text = "00";
            EndClose1.Text = "13";
            EndClose2.Text = "00";
            lunch1.Text = "12";
            lunch2.Text = "00";
            holiOpen1.Text = "08";
            holiOpen2.Text = "00";
            HoliClose1.Text = "13";
            HoliClose2.Text = "00";
            EndState.Text = "개원";
            HoliState.Text = "휴원";
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
        }
    }
}
