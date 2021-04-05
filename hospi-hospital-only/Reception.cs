using Microsoft.Toolkit.Uwp.Notifications;
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
        DBClass dbc = new DBClass(); // hospital, visitor
        DBClass dbc2 = new DBClass(); // reception, receptionist
        DBClass dbc3 = new DBClass(); // reception 조회 삭제
        Inquiry inquiry = new Inquiry();
        string listViewIndexID1; // 리스트뷰 아이템 클릭시 해당정보의 receptionID를 저장하는 변수
        string listViewIndexPatientName; // 리스트뷰 아이템 클릭시 해당정보의 PatientName을 저장하는 변수
        int listViewModeL, listViewModeR; // 리스트뷰의 현재상태를 저장한 변수     // L ( 진료대기 : 1 , 진료보류 : 2 )      // R ( 수납대기 : 1 , 수납완료 : 2 ) 
        string hospitalID;
        int old;
        string date; // 날짜 변수
        string[] prescriptionArr = new string[3]; // 처방전 조회에 필요한 (patientID, receptionTime, receptionDate 저장)
        DataTable hisTable; // 수진자 정보 조회시 이전 진료기록을 담은 테이블 ( 이전 진료기록 띄울때 사용하고, 이전 진료기록중 내원목적 확인시에 재사용 )
        int selectedListViewItemIndex; // 이전 진료기록 리스트뷰의 선택 인덱스 저장
        string selectedSubjectName; // 접수 수정시 과목명 저장
        string receptionistName; // MainMenu에서 접수자명 받아옴
        int incount;

        public Reception()
        {
            InitializeComponent();
        }

        // 프로퍼티 
        public string HospitalID // Main폼에서 입력된 병원코드를 받아옴
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public string ReceptionistName
        {
            get { return receptionistName; }
            set { receptionistName = value; }
        }

        // 진료정보의 접수일, 접수시간 초기화 & 현재 체크박스 체크
        public void TimeNow()
        {
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            comboBoxTime1.Text = DateTime.Now.ToString("HH");
            comboBoxTime2.Text = DateTime.Now.ToString("mm");
            checkBox2.Checked = true;
        }

        // 접수 현황 버튼 ▶◀ 삭제
        public void ButtonClearL()
        {
            button2.Text = "진료대기";
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
            textBoxChartNum.Clear();
            textBoxB1.Clear();
            textBoxB2.Clear();
            phone1.Clear();
            phone2.Clear();
            phone3.Clear();
            textBoxADDR.Clear();
            labelGenderAge.Text = "성별/나이";
            textBox16.Clear();
            // 이전진료
            listView2.Items.Clear();
            // 진료정보
            textBoxPurpose.Clear();
            // 내원정보
            textBoxFirst.Clear();
            textBoxLast.Clear();
            // 출생년도
            patientBirth.Clear();
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
                labelGenderAge.Text = "남/" + old.ToString() + "세";
            }
            else if (textBoxB2.Text.Substring(0, 1) == "2" || textBoxB2.Text.Substring(0, 1) == "4")
            {
                labelGenderAge.Text = "여/" + old.ToString() + "세";
            }
        }

        // 접수 리스트뷰 ( listView1,3 )
        public void ReceptionUpdate(int receptionType)
        {
            try
            {
                date = dateTimePicker2.Value.ToString("yy-MM-dd");
                dbc3.Reception_Update(date, receptionType);
                dbc3.ReceptionTable = dbc3.DS.Tables["Reception"];

                if (receptionType == 1 || receptionType == 4)
                {
                    listView1.Items.Clear();
                }
                else if (receptionType == 2 || receptionType == 3)
                {
                    listView3.Items.Clear();
                }

                for (int i = 0; i < dbc3.ReceptionTable.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    if (receptionType == 1 || receptionType == 4)
                    {
                        item.Text = (listView1.Items.Count + 1).ToString();
                    }
                    else if (receptionType == 2 || receptionType == 3)
                    {
                        item.Text = (listView3.Items.Count + 1).ToString();
                    }
                    item.SubItems.Add(dbc3.ReceptionTable.Rows[i][0].ToString().Substring(0, 2) + " : " + dbc3.ReceptionTable.Rows[i][0].ToString().Substring(2, 2));
                    item.SubItems.Add(dbc3.ReceptionTable.Rows[i][1].ToString());
                    item.SubItems.Add(dbc3.ReceptionTable.Rows[i][2].ToString());
                    // Age
                    int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                    if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "1" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "2")
                    {
                        item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1899).ToString());
                    }
                    else if (dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "3" || dbc3.ReceptionTable.Rows[i][3].ToString().Substring(7, 1) == "4")
                    {
                        item.SubItems.Add((year - Convert.ToInt32(dbc3.ReceptionTable.Rows[i][3].ToString().Substring(0, 2)) - 1999).ToString());
                    }
                    item.SubItems.Add(dbc3.ReceptionTable.Rows[i][4].ToString());
                    item.SubItems.Add(dbc3.ReceptionTable.Rows[i][5].ToString());
                    item.SubItems.Add(dbc3.ReceptionTable.Rows[i][6].ToString());
                    if (receptionType == 1 || receptionType == 4)
                    {
                        listView1.Items.Add(item);
                        dbc2.Reception_Update(date, 1);
                        dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                        receptionCount1.Text = "진료대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                        dbc2.Reception_Update(date, 4);
                        dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                        receptionCount1.Text = receptionCount1.Text + "\r\n진료보류 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                    }
                    else if (receptionType == 2 || receptionType == 3)
                    {
                        listView3.Items.Add(item);
                        dbc2.Reception_Update(date, 2);
                        dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                        receptionCount2.Text = "수납대기 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                        dbc2.Reception_Update(date, 3);
                        dbc2.ReceptionTable = dbc2.DS.Tables["reception"];
                        receptionCount2.Text = receptionCount2.Text + "\r\n수납완료 : " + dbc2.ReceptionTable.Rows.Count.ToString("00");
                    }
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

        // 수진자 정보 넣기
        public void VisitorText(int rows)
        {
            try
            {
                DataRow vRow = dbc.VisitorTable.Rows[rows];
                textBoxChartNum.Text = vRow["patientID"].ToString();
                textBoxB1.Text = vRow["patientBirthCode"].ToString().Substring(0, 6);
                textBoxB2.Text = vRow["patientBirthCode"].ToString().Substring(7, 7);
                phone1.Text = vRow["patientPhone"].ToString().Substring(0, 3);
                phone2.Text = vRow["patientPhone"].ToString().Substring(3, 4);
                phone3.Text = vRow["patientPhone"].ToString().Substring(7, 4);
                textBoxADDR.Text = vRow["patientAddress"].ToString();
                textBox16.Text = vRow["patientMemo"].ToString();

                // 이전 진료 기록
                dbc.Visitor_Chart(Convert.ToInt32(vRow["patientID"]));
                hisTable = dbc.DS.Tables["visitor"];
                if(hisTable.Rows.Count != 0)
                {
                    for (int i = 0; i < hisTable.Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = hisTable.Rows[i]["receptionDate"].ToString();
                        item.SubItems.Add(hisTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + hisTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                        item.SubItems.Add(hisTable.Rows[i]["subjectName"].ToString());
                        item.SubItems.Add(hisTable.Rows[i]["receptionInfo"].ToString());
                        listView2.Items.Add(item);
                    }
                    textBoxFirst.Text = "20" + hisTable.Rows[hisTable.Rows.Count - 1]["receptionDate"].ToString();
                    textBoxLast.Text = "20" + hisTable.Rows[0]["receptionDate"].ToString();
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

        // 종료
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 폼 로드
        private void Receipt_Load(object sender, EventArgs e)
        {
            dbc.FireConnect();
            dbc2.FireConnect();
            dbc3.FireConnect();
            inquiry.FireConnect();
            incount = Inquiry.count;
            timer1.Start();
            // 폼 로드시 버튼 클릭
            button2_Click(sender, e); // 진료대기버튼
            button8_Click(sender, e); // 진료보류버튼

            // 폼 로드시 수신자명 포커스
            this.ActiveControl = patientName;

            // 접수시간 (현재)
            TimeNow();

            try
            {
                // DB오픈 ( Hospital, SubjectName, Reception ) 
                // 병원정보 가져오고 과목명 comboBox에 추가
                dbc.Reception_Open();
                dbc.Hospital_Open(hospitalID);
                dbc.Delay(400);
                //dbc.HospitalTable = dbc.DS.Tables["hospital"];
                //DataRow subjectRow = dbc.HospitalTable.Rows[0];
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"]; // 접수자 테이블
                textBoxReceptionist.Text = receptionistName;
                dbc.Subject_Open();
                dbc.SubjectTable = dbc.DS.Tables["subjectName"]; // 과목 테이블
                for (int i = 0; i < DBClass.hospidepartment.Length; i++)     // comboBoxSubject에 과목명 추가
                {
                    comboBoxSubjcet.Items.Add(DBClass.hospidepartment[i]);
                }
                comboBoxSubjcet.Text = DBClass.hospidepartment[0];    // 최상위 과목명을 기본 텍스트로 지정
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

        // 초진등록
        private void button7_Click(object sender, EventArgs e)
        {
            Reception_First receipt_First = new Reception_First();
            receipt_First.ShowDialog();
            patientName.Text = receipt_First.VisitorName; // 수진자명 받아와서 텍스트 대입
            if (patientName.Text != "")
            {
                button9_Click(sender, e);                                   // 조회 클릭
                VisitorText(0);                                                     // 수진자 정보 넣기
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // (현재) 접수 체크변경
            if (checkBox2.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                comboBoxTime1.Enabled = false;
                comboBoxTime2.Enabled = false;
                TimeNow();
            }
            else if (checkBox2.Checked == false)
            {
                dateTimePicker1.Enabled = true;
                comboBoxTime1.Enabled = true;
                comboBoxTime2.Enabled = true;
            }
        }

        // 진료대기 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClearL();
            button2.Text = "▶ " + button2.Text + " ◀";
            listViewModeL = 1;

            button22.Text = "진료 보류";

            // 접수로드 (1 = 진료대기)
            ReceptionUpdate(1);
        }

        // 진료보류 버튼
        private void button5_Click(object sender, EventArgs e)
        {
            ButtonClearL();
            button5.Text = "▶ " + button5.Text + " ◀";
            listViewModeL = 2;

            button22.Text = "접수 복구";

            // 접수로드 (4 = 진료보류환자)
            ReceptionUpdate(4);
        }

        // 수납대기 버튼
        private void button8_Click(object sender, EventArgs e)
        {
            ButtonClearR();
            button8.Text = "▶ " + button8.Text + " ◀";
            listViewModeR = 1;

            // 접수로드 (2 = 수납대기)
            ReceptionUpdate(2);
        }

        // 수납완료 버튼
        private void button13_Click(object sender, EventArgs e)
        {
            ButtonClearR();
            button13.Text = "▶ " + button13.Text + " ◀";
            listViewModeR = 2;

            // 접수로드 (3 = 수납완료)
            ReceptionUpdate(3);
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
            if (patientName.Text == "")
            {
                MessageBox.Show("수진자명을 입력하세요.", "알림");
                patientName.Focus();
            }
            else
            {
                try
                {
                    // 재진조회 그룹박스 정보 넣기
                    TextBoxClear();

                    dbc.Visitor_Name(patientName.Text);
                    dbc.VisitorTable = dbc.DS.Tables["visitor"];

                    if (dbc.VisitorTable.Rows.Count == 0)
                    {
                        MessageBox.Show("등록된 정보가 존재하지 않습니다.", "알림");
                        TextBoxClear();
                        patientName.Clear();
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
                        item.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    // DBGrid 컬럼 속성
                    DBGrid.Columns[0].HeaderText = "차트번호";
                    DBGrid.Columns[1].HeaderText = "수진자명";
                    DBGrid.Columns[2].HeaderText = "주민등록번호";
                    DBGrid.Columns[0].Width = 85;
                    DBGrid.Columns[1].Width = 85;
                    DBGrid.Columns[2].Width = 105;
                    DBGrid.Columns[3].Visible = false;
                    DBGrid.Columns[4].Visible = false;
                    DBGrid.Columns[5].Visible = false;
                    DBGrid.Columns[6].Visible = false;

                    // 생년월일 텍스트박스 포커스
                    if (dbc.VisitorTable.Rows.Count != 0 || dbc.VisitorTable.Rows.Count == 1)
                    {
                        patientBirth.Focus();
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

        // 수진자 조회 셀 더블클릭    ( 조회된 수진자의 수가 1보다 많을경우 )      
        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                TextBoxClear();

                // 재진조회 그룹박스, 특이사항 정보 넣기
                //VisitorText(Convert.ToInt32(DBGrid.Rows[e.RowIndex].Cells[1].FormattedValue));
                VisitorText(e.RowIndex);

                // 성별/나이 라벨 수정
                GenderAgeLabel();
                textBoxPurpose.Focus();
            }
        }

        // 접수 (접수등록) 버튼
        private void button11_Click(object sender, EventArgs e)
        {
            if (textBoxChartNum.Text == "")
            {
                MessageBox.Show("수진자 정보를 확인하세요.", "알림");
            }
            else if (textBoxPurpose.Text == "")
            {
                MessageBox.Show("내원 목적이 작성되지 않았습니다.", "알림");
            }
            else
            {
                try
                {
                    dbc.Reception_Open();
                    dbc.ReceptionTable = dbc.DS.Tables["Reception"];
                    DataRow newRow = dbc.ReceptionTable.NewRow();
                    newRow["ReceptionID"] = dbc.ReceptionTable.Rows.Count + 1;
                    newRow["PatientID"] = textBoxChartNum.Text;
                    newRow["ReceptionTime"] = comboBoxTime1.Text + comboBoxTime2.Text;
                    newRow["ReceptionDate"] = dateTimePicker1.Value.ToString("yy/MM/dd");
                    newRow["SubjectName"] = comboBoxSubjcet.Text;
                    for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                    {
                        if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == textBoxReceptionist.Text)
                        {
                            newRow["ReceptionistCode"] = i + 1;
                        }
                    }
                    newRow["ReceptionInfo"] = textBoxPurpose.Text;
                    newRow["ReceptionType"] = 1;

                    dbc.ReceptionTable.Rows.Add(newRow);
                    dbc.DBAdapter.Update(dbc.DS, "Reception");  
                    dbc.DS.AcceptChanges();

                    MessageBox.Show("접수 완료.", "알림");
                    TextBoxClear();
                    patientName.Clear();
                    comboBoxSubjcet.Text = comboBoxSubjcet.Items[0].ToString();
                    DBGrid.DataSource = null;
                    patientName.Focus();

                    if (checkBox2.Checked == false)
                    {
                        checkBox2.Checked = true;
                    }

                    // 접수현황 업데이트
                    ReceptionUpdate(1);
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

        // 접수자 변경 버튼    
        private void button6_Click(object sender, EventArgs e)
        {
            // 접수자 변경 메뉴 
            Receptionist receptionist = new Receptionist();
            receptionist.ReceptionistName = textBoxReceptionist.Text;
            receptionist.ShowDialog();
            textBoxReceptionist.Text = receptionist.ReceptionistName;

            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
        }

        // 날짜정보 금일 버튼
        private void button21_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = DateTime.Now;
            button21.Enabled = false;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button20_Click(sender, e);
            }
        }

        // 수진자 조회 출생년도 검색
        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                dbc.Visitor_BirthName(patientName.Text, patientBirth.Text);
                dbc.VisitorTable = dbc.DS.Tables["visitor"];
                DBGrid.DataSource = dbc.VisitorTable.DefaultView;
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

        // 접수현황 수진자명 검색
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].SubItems[3].Text == textBox2.Text)
                {
                    listView1.Items[i].BackColor = Color.Yellow;
                }
                else
                {
                    listView1.Items[i].BackColor = Color.White;
                }
            }
            textBox2.Clear();
        }
        // 접수현황 수진자명 검색 엔터이벤트
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3_Click(sender, e);
            }
        }

        // 수납현황 수진자명 검색
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView3.Items.Count; i++)
            {
                if (listView3.Items[i].SubItems[3].Text == textBox3.Text)
                {
                    listView3.Items[i].BackColor = Color.Yellow;
                }
                else
                {
                    listView3.Items[i].BackColor = Color.White;
                }
            }
            textBox3.Clear();
        }
        // 수납현황 수진자명 검색 엔터이벤트
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        // 접수취소 버튼
        private void button12_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("취소할 항목이 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("선택된 접수를 취소합니다.\r\n수진자명 : " + listViewIndexPatientName, "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc3.Reception_Open();
                        dbc3.ReceptionTable = dbc3.DS.Tables["reception"];
                        DataColumn[] PrimaryKey = new DataColumn[1];
                        PrimaryKey[0] = dbc3.ReceptionTable.Columns["receptionID"];
                        dbc3.ReceptionTable.PrimaryKey = PrimaryKey;
                        DataRow delRow = dbc3.ReceptionTable.Rows.Find(listViewIndexID1);
                        int rowCount = dbc3.ReceptionTable.Rows.Count; // 삭제전 전체 row 갯수
                        delRow.Delete();
                        int receptionID = Convert.ToInt32(listViewIndexID1);
                        // listViewIndexID1 을 증감시킬경우 for문에 영향을 주므로 변수를 따로 지정해서 사용

                        //  열 하나가 삭제될 경우 열의 인덱스가 삭제 대상보다 높은경우 모두 -1 해줌
                        // ex) 10개열 테이블에서 7번열 삭제시 8ㅡ>7 / 9-ㅡ>8 / 10ㅡ>9
                        for (int i = 0; i < (rowCount - Convert.ToInt32(listViewIndexID1)); i++)
                        {
                            delRow = dbc3.ReceptionTable.Rows[rowCount - (rowCount - receptionID)];
                            delRow.BeginEdit();
                            delRow["receptionID"] = Convert.ToInt32(delRow["receptionID"]) - 1;
                            delRow.EndEdit();
                            receptionID += 1;
                        }
                        dbc3.DBAdapter.Update(dbc3.DS, "reception");
                        dbc3.DS.AcceptChanges();
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
                if (listViewModeL == 1)
                {
                    button2_Click(sender, e); // 진료대기버튼
                }
                else if (listViewModeL == 2)
                {
                    button5_Click(sender, e); // 진료보류버튼
                }
                listViewIndexID1 = null; // 삭제완료후 null값 넣어줌
            }
        }

        // 접수수정 버튼
        private void button19_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("수정할 항목이 선택되지 않았습니다.", "알림");
            }
            else if (listViewIndexID1 != null)
            {
                ReceptionUpdate receptionUpdate = new ReceptionUpdate();
                receptionUpdate.ReceptionID = Convert.ToInt32(listViewIndexID1);
                receptionUpdate.SelectedSubjectName = selectedSubjectName;
                receptionUpdate.ShowDialog();

                if (listViewModeL == 1)
                {
                    button2_Click(sender, e); // 접수내역버튼
                }
                else if (listViewModeL == 2)
                {
                    button5_Click(sender, e); // 진료보류버튼
                }
                listViewIndexID1 = null;
            }
        }

        // 진료보류, 접수복구 버튼
        private void button22_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("보류할 항목이 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("선택된 접수를 보류합니다.\r\n수진자명 : " + listViewIndexPatientName, "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc3.Reception_Open();
                        dbc3.ReceptionTable = dbc3.DS.Tables["reception"];
                        DataRow upRow = dbc3.ReceptionTable.Rows[Convert.ToInt32(listViewIndexID1) - 1];

                        if (listViewModeL == 1)
                        {
                            upRow.BeginEdit();
                            upRow["receptionType"] = 4;
                            upRow.EndEdit();
                            dbc3.DBAdapter.Update(dbc3.DS, "reception");
                            dbc3.DS.AcceptChanges();

                            button2_Click(sender, e); // 진료대기버튼
                        }
                        else if (listViewModeL == 2)
                        {
                            upRow.BeginEdit();
                            upRow["receptionType"] = 1;
                            upRow.EndEdit();
                            dbc3.DBAdapter.Update(dbc3.DS, "reception");
                            dbc3.DS.AcceptChanges();

                            button5_Click(sender, e);
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
                else
                {
                    if (listViewModeL == 1)
                    {
                        button2_Click(sender, e); // 접수내역버튼
                    }
                    else if (listViewModeL == 2)
                    {
                        button5_Click(sender, e); // 진료보류버튼
                    }
                }
            }
            listViewIndexID1 = null;
        }

        // 처방확인 버튼
        private void button15_Click(object sender, EventArgs e)
        {
            Prescription prescription = new Prescription();
            prescription.PatientID = prescriptionArr[0];
            prescription.ReceptionDate = prescriptionArr[1];
            prescription.ReceptionTime = prescriptionArr[2];
            prescription.Show();
        }

        // 수납현황 listView 클릭
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // prescription배열에 ( patientID, receptionDate, receptionTime ) 넣기
            if (listView3.SelectedItems.Count != 0)
            {
                int selectRow = listView3.SelectedItems[0].Index;
                prescriptionArr[0] = listView3.Items[selectRow].SubItems[2].Text;
                prescriptionArr[1] = dateTimePicker1.Value.ToString("yy-MM-dd");
                prescriptionArr[2] = listView3.Items[selectRow].SubItems[1].Text.Substring(0,2)+listView3.Items[selectRow].SubItems[1].Text.Substring(5,2);
            }
        }

        // 접수현황 listView 클릭
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // listViewIndexID1에 7번 컬럼값 넣기
            if (listView1.SelectedItems.Count != 0)
            {
                int selectRow = listView1.SelectedItems[0].Index;
                listViewIndexID1 = listView1.Items[selectRow].SubItems[7].Text;
                listViewIndexPatientName = listView1.Items[selectRow].SubItems[3].Text;

                selectedSubjectName = listView1.Items[selectRow].SubItems[5].Text;
            }
        }

        // 이전 진료기록 더블클릭
        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listView2.SelectedItems.Count > 0)
            {
                for(int i=0; i<listView2.Items.Count-1; i++)
                {
                    if(listView2.Items[i].Selected == true)
                    {
                        selectedListViewItemIndex = i;
                        break;
                    }
                }
            }
            if(selectedListViewItemIndex != -1)
            {
                Reception_HistoryInfo reception_HistoryInfo = new Reception_HistoryInfo();
                reception_HistoryInfo.ReceptionInfo = hisTable.Rows[selectedListViewItemIndex]["receptionInfo"].ToString();
                reception_HistoryInfo.ShowDialog();
                string history = reception_HistoryInfo.ReceptionInfo;
                if(history != "")
                {
                    textBoxPurpose.Text = history;
                }

            }
            selectedListViewItemIndex = -1;
        }

        // 수납완료 버튼
        private void button14_Click(object sender, EventArgs e)
        {
            if (listViewIndexID1 == null)
            {
                MessageBox.Show("보류할 항목이 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("선택된 접수를 보류합니다.\r\n수진자명 : " + listViewIndexPatientName, "알림", MessageBoxButtons.YesNo);
                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc3.Reception_Open();
                        dbc3.ReceptionTable = dbc3.DS.Tables["reception"];
                        DataRow upRow = dbc3.ReceptionTable.Rows[Convert.ToInt32(listViewIndexID1) - 1];

                        if (listViewModeL == 1)
                        {
                            upRow.BeginEdit();
                            upRow["receptionType"] = 4;
                            upRow.EndEdit();
                            dbc3.DBAdapter.Update(dbc3.DS, "reception");
                            dbc3.DS.AcceptChanges();

                            button2_Click(sender, e); // 진료대기버튼
                        }
                        else if (listViewModeL == 2)
                        {
                            upRow.BeginEdit();
                            upRow["receptionType"] = 1;
                            upRow.EndEdit();
                            dbc3.DBAdapter.Update(dbc3.DS, "reception");
                            dbc3.DS.AcceptChanges();

                            button5_Click(sender, e);
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
                else
                {
                    if (listViewModeL == 1)
                    {
                        button2_Click(sender, e); // 접수내역버튼
                    }
                    else if (listViewModeL == 2)
                    {
                        button5_Click(sender, e); // 진료보류버튼
                    }
                }
            }
            listViewIndexID1 = null;
        }

        // 날짜정보 dateTimePicker 변경시
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            button21.Enabled = true;
            button2_Click(sender, e); // 진료대기버튼
            button8_Click(sender, e); // 수납대기버튼
        }

        private void inquirybutton_Click(object sender, EventArgs e)
        {
            InquiryCheck inquiry = new InquiryCheck();
            inquiry.HospitalID = hospitalID;
            inquiry.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            inquiry.checkinquiry(hospitalID);
            dbc.Delay(200);
            if(Inquiry.count > incount)
            {
                new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("HOSPI")
                    .AddText("새로운 문의가 등록 되었습니다!!")
                    .Show();
            }
            incount = Inquiry.count;
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
