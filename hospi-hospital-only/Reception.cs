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
    public partial class Reception : Form
    {
        DBClass dbc = new DBClass();
        DBClass dbc2 = new DBClass();
        DBClass dbc3 = new DBClass();
        int hospitalID, old;
        string test;

        public Reception()
        {
            InitializeComponent();
        }

        // 프로퍼티 
        public int HospitalID // Main폼에서 입력된 병원코드를 받아옴
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        // 진료정보의 접수일, 접수시간 초기화 & 현재 체크박스 체크
        public void TimeNow()
        {
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            comboBox2.Text = DateTime.Now.ToString("HH");
            comboBox3.Text = DateTime.Now.ToString("mm");
            checkBox2.Checked = true;
        }

        // 접수 현황 버튼 ▶◀ 삭제
        public void ButtonClearL()
        {
            button2.Text = "접수환자";
            button5.Text = "진료보류";
        }

        // 수납 현황 버튼 ▶◀ 삭제
        public void ButtonClearR()
        {
            button8.Text = "수납대기";
            button13.Text = "수납완료";
        }

        // 재진조회, 진료정보 텍스트박스 비우기
        public void TextBoxClear()
        {
            // 재진조회
            textBox24.Clear();
            textBoxB1.Clear();
            textBoxB2.Clear();
            phone1.Clear();
            phone2.Clear();
            phone3.Clear();
            textBoxADDR.Clear();
            label13.Text = "성별/나이";
            textBox16.Clear();
            // 이전진료
            listView2.Items.Clear();
            // 진료정보
            textBox19.Clear();
        }

        // 성별/나이 라벨 수정
        public void GenderAgeLabel()
        {
            String year = DateTime.Now.ToString("yyyy");
            if (textBoxB2.Text.Substring(0, 1) == "1" || textBoxB2.Text.Substring(0, 1) == "2")
            {
                old = Convert.ToInt32(year) - Convert.ToInt32(textBoxB1.Text.Substring(0, 2)) - 1899;
            }
            else if (textBoxB2.Text.Substring(0, 1) == "3" || textBoxB2.Text.Substring(0, 1) == "4")
            {
                old = Convert.ToInt32(year) - Convert.ToInt32(textBoxB1.Text.Substring(0, 2)) - 1999;
            }

            if (textBoxB2.Text.Substring(0, 1) == "1" || textBoxB2.Text.Substring(0, 1) == "3")
            {
                label13.Text = "남/" + old.ToString() + "세";
            }
            else if (textBoxB2.Text.Substring(0, 1) == "2" || textBoxB2.Text.Substring(0, 1) == "4")
            {
                label13.Text = "여/" + old.ToString() + "세";
            }
        }

        // 이전진료기록 조회 (ListVIew2), 최초/최종 내원일
        public void ReceptionBefore()
        {
            listView2.Items.Clear();

            dbc2.Reception_Before(hospitalID, textBox24.Text); // 병원코드, 차트번호
            dbc2.ReceptionTable = dbc2.DS.Tables["Reception"];

            for (int i = 0; i < dbc2.ReceptionTable.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = dbc2.ReceptionTable.Rows[i][0].ToString();
                item.SubItems.Add(dbc2.ReceptionTable.Rows[i][1].ToString().Substring(0,2)+" : "+dbc2.ReceptionTable.Rows[i][1].ToString().Substring(2,2));
                item.SubItems.Add(dbc2.ReceptionTable.Rows[i][2].ToString());
                item.SubItems.Add(dbc2.ReceptionTable.Rows[i][3].ToString());
                listView2.Items.Add(item);
            }

            if(dbc2.ReceptionTable.Rows.Count != 0)
            {
                textBox17.Text = "20"+dbc2.ReceptionTable.Rows[0]["receptionDate"].ToString();
                textBox20.Text = "20"+dbc2.ReceptionTable.Rows[dbc2.ReceptionTable.Rows.Count-1]["receptionDate"].ToString();
            }
        }

        // 접수 조회 ( listView )
        public void ReceptionUpdate()
        {
            string date = dateTimePicker1.Value.ToString("yy-MM-dd");
            dbc3.Reception_Update(hospitalID, date);
            dbc3.ReceptionTable = dbc3.DS.Tables["Reception"];

            for (int i = 0; i < dbc3.ReceptionTable.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (listView1.Items.Count + 1).ToString();
                item.SubItems.Add(dbc3.ReceptionTable.Rows[i][0].ToString().Substring(0, 2) + " : " + dbc3.ReceptionTable.Rows[i][0].ToString().Substring(2, 2));
                item.SubItems.Add(dbc3.ReceptionTable.Rows[i][1].ToString());
                item.SubItems.Add(dbc3.ReceptionTable.Rows[i][2].ToString());
                // Age
                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7,1) == "1" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7,1) == "2")
                {
                    item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1899).ToString());
                }
                else if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7,1) == "3" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7,1) == "4")
                {
                    item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1999).ToString());
                }
                item.SubItems.Add(dbc3.ReceptionTable.Rows[i][4].ToString());
                item.SubItems.Add(dbc3.ReceptionTable.Rows[i][5].ToString());
                listView1.Items.Add(item);
            }
        }

        // 종료
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 새로고침 버튼 이미지
        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ImageIndex = 1;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ImageIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.ImageIndex = 0;
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            // 폼 로드시 버튼 클릭
            button2_Click(sender, e);
            button8_Click(sender, e);

            // 폼 로드시 수신자명 포커스
            this.ActiveControl = patientName;

            // 접수시간 (현재)
            TimeNow();

            // 기본접수자 지정
            textBox1.Text = "관리자";

            // DB오픈 ( Hospital, SubjectName, Reception ) 
            // 병원정보 가져오고 과목명 comboBox에 추가
            dbc.Reception_Open();
            dbc.Hospital_Open(hospitalID);
            dbc.HospitalTable = dbc.DS.Tables["hospital"];
            DataRow subjectRow = dbc.HospitalTable.Rows[0];
            int subjectCount = Convert.ToInt32(subjectRow["subjectCount"]);  // 해당 병원정보의 SubjectCount를 가져와서 SubjectCount만큼 반목문으로 query생성

            for (int i = 1; i <= subjectCount; i++)
            {
                if (i != subjectCount)
                {
                    test += "subject" + i + ",";
                }
                else if (i == subjectCount)
                {
                    test += "subject" + i;
                }
            }

            string query = "select " + test + " from SubjectName where hospitalID = " + hospitalID;  // ex) select subject1, subject2, subject3 from SubjectName where hospitalID = 1; 
            dbc.Hopital_Subject(query);                                                                                              // 위 query 문자열 자체를 commandString 으로 대입
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];

            for(int i=0; i<dbc.SubjectTable.Columns.Count; i++)     // comboBox1에 과목명 추가
            {
                comboBox1.Items.Add(dbc.SubjectTable.Rows[0][i]);
            }
            comboBox1.Text = dbc.SubjectTable.Rows[0][0].ToString();    // 최상위 과목명을 기본 텍스트로 지정

            ReceptionUpdate(); // 접수로드
        }

        // 초진등록
        private void button7_Click(object sender, EventArgs e)
        {
            Reception_First receipt_First = new Reception_First();
            receipt_First.HospitalID = hospitalID;
            receipt_First.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // (현재) 접수 체크변경
            if (checkBox2.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                TimeNow();
            }
           else if (checkBox2.Checked == false)
            {
                dateTimePicker1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
        }

        // 접수환자 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClearL();
            button2.Text = "▶ " + button2.Text + " ◀";
        }

        // 진료보류 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            ButtonClearL();
            button5.Text = "▶ " + button5.Text + " ◀";
        }

        // 수납대기 버튼
        private void button8_Click(object sender, EventArgs e)
        {
            ButtonClearR();
            button8.Text = "▶ " + button8.Text + " ◀";
        }

        // 수납완료 버튼
        private void button13_Click(object sender, EventArgs e)
        {
            ButtonClearR();
            button13.Text = "▶ " + button13.Text + " ◀";
        }

        // 병원정보설정 버튼
        private void button16_Click(object sender, EventArgs e)
        {
            Hospital_Setting hospital_Setting = new Hospital_Setting();
            hospital_Setting.HospitalID = hospitalID;
            hospital_Setting.ShowDialog();
        }

        // 접수된 예약 버튼
        private void button10_Click(object sender, EventArgs e)
        {
        }

        // 수진자 조회 버튼
        private void button9_Click(object sender, EventArgs e)
        {
            // 재진조회 그룹박스 정보 넣기
            TextBoxClear();

            dbc.Visitor_Name(hospitalID, patientName.Text);
            dbc.VisitorTable = dbc.DS.Tables["visitor"];

            if (dbc.VisitorTable.Rows.Count == 0)
            {
                MessageBox.Show("등록된 정보가 존재하지 않습니다.", "알림");
                TextBoxClear();
                patientName.Clear();
            }
            else if (dbc.VisitorTable.Rows.Count == 1)
            {
                // 재진조회 그룹박스, 특이사항
                DataRow vRow = dbc.VisitorTable.Rows[0];
                textBox24.Text = vRow["patientID"].ToString();
                textBoxB1.Text = vRow["patientBirthCode"].ToString().Substring(0, 6);
                textBoxB2.Text = vRow["patientBirthCode"].ToString().Substring(7, 7);
                phone1.Text = vRow["patientPhone"].ToString().Substring(0, 3);
                phone2.Text = vRow["patientPhone"].ToString().Substring(3, 4);
                phone3.Text = vRow["patientPhone"].ToString().Substring(7, 4);
                textBoxADDR.Text = vRow["patientAddress"].ToString();
                textBox16.Text = vRow["patientMemo"].ToString();

                // 이전 진료기록 listView (dbc2 객체로 Reception DB받아옴) , 최초/최종 내원일
                ReceptionBefore();

                // 성별/나이 라벨 수정
                GenderAgeLabel();
                textBox19.Focus(); // 내원목적 포커스
            }

            // GirdView 띄우기
            DBGrid.DataSource = dbc.DS.Tables["visitor"].DefaultView;

            // GirdView 속성 ▼
            DBGrid.CurrentCell = null; // 로딩시 첫번째열 자동선택 없애기 
            // 색상변경
            for (int i = 1; i < DBGrid.Rows.Count; i++)
            {
                if (i % 2 != 0)
                {
                    DBGrid.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240);
                }
                else
                {
                    DBGrid.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
            // 정렬 막기
            foreach (DataGridViewColumn item in DBGrid.Columns)
            {
                item.SortMode=DataGridViewColumnSortMode.NotSortable;
            }
            // 컬럼 속성
            DBGrid.Columns[0].HeaderText = "상품 번호";
            DBGrid.Columns[1].HeaderText = "차트번호";
            DBGrid.Columns[2].HeaderText = "수진자명";
            DBGrid.Columns[3].HeaderText = "주민등록번호";
            DBGrid.Columns[1].Width = 85;
            DBGrid.Columns[2].Width = 85;
            DBGrid.Columns[3].Width = 105;
            DBGrid.Columns[0].Visible = false;
            DBGrid.Columns[4].Visible = false;
            DBGrid.Columns[5].Visible = false;
            DBGrid.Columns[6].Visible = false;
            DBGrid.Columns[7].Visible = false;
        }

        // 수진자 조회 셀 더블클릭    ( 조회된 수진자의 수가 1보다 많을경우 )      
        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 재진조회 그룹박스 정보 넣기
            DataRow vRow = dbc.VisitorTable.Rows[e.RowIndex];
            patientName.Text = vRow["patientName"].ToString();
            textBox24.Text = vRow["patientID"].ToString();
            textBoxB1.Text = vRow["patientBirthCode"].ToString().Substring(0, 6);
            textBoxB2.Text = vRow["patientBirthCode"].ToString().Substring(7, 7);
            phone1.Text = vRow["patientPhone"].ToString().Substring(0, 3);
            phone2.Text = vRow["patientPhone"].ToString().Substring(3, 4);
            phone3.Text = vRow["patientPhone"].ToString().Substring(7, 4);
            textBoxADDR.Text = vRow["patientAddress"].ToString();
            textBox16.Text = vRow["patientMemo"].ToString();

            // 이전 진료기록 listView , 최초/최종 내원일
            ReceptionBefore();

            // 성별/나이 라벨 수정
            GenderAgeLabel();
            textBox19.Focus();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(textBox24.Text == "")
            {
                MessageBox.Show("수진자 정보를 확인하세요.", "알림");
            }
            else if (textBox19.Text == "")
            {
                MessageBox.Show("내원 목적이 작성되지 않았습니다.", "알림");
            }
            else
            {
                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                DataRow newRow = dbc.ReceptionTable.NewRow();
                newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                newRow["HospitalID"] = hospitalID; // **
                newRow["PatientID"] = textBox24.Text;
                newRow["ReceptionTime"] = comboBox2.Text + comboBox3.Text;
                newRow["ReceptionDate"] = dateTimePicker1.Value.ToString("yy/MM/dd");
                newRow["SubjectName"] = comboBox1.Text;
                newRow["Receptionist"] = textBox1.Text;
                newRow["ReceptionInfo"] = textBox19.Text;
                newRow["ReceptionType"] = 1;

                dbc.ReceptionTable.Rows.Add(newRow);
                dbc.DBAdapter.Update(dbc.DS, "Reception");
                dbc.DS.AcceptChanges();

                MessageBox.Show("접수 완료.", "알림");
                TextBoxClear();
                patientName.Clear();
                comboBox1.Text = comboBox1.Items[0].ToString();
                DBGrid.DataSource = null;
                patientName.Focus();
            }
        }

        // 접수자 변경 버튼    
        private void button6_Click(object sender, EventArgs e)
        {
            // 접수자 변경 메뉴 
            Receptionist receptionist = new Receptionist();
            receptionist.HospitalID = hospitalID;
            receptionist.ReceptionistName = textBox1.Text;
            receptionist.ShowDialog();
            textBox1.Text = receptionist.ReceptionistName;
        }

        // 수진자명 조회 엔터 이벤트
        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button9_Click(sender, e);
            }
        }
    }
}
