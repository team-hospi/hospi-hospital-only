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
    public partial class StaffLogin : Form
    {
        DBClass dbc = new DBClass();
        string hospitalID;
        
        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }

        public StaffLogin()
        {
            
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffAdd staffAdd = new StaffAdd();
            staffAdd.ShowDialog();
            textBoxStaffId.Text = staffAdd.StaffId;
        }

        private void StaffLogin_Load(object sender, EventArgs e)
        {
            dbc.FireConnect();
            dbc.Delay(200);
            dbc.Hospital_Open(Properties.Settings.Default.ProductKey);
            dbc.Delay(200);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dbc.Staff_open();
            dbc.StaffTable = dbc.DS.Tables["staff"];

            bool login = false;

            for (int i = 0; i < dbc.StaffTable.Rows.Count; i++)
            {
                if (dbc.StaffTable.Rows[i]["useYn"].ToString() == "Y")
                {
                    if (dbc.StaffTable.Rows[i]["staffId"].ToString() == textBoxStaffId.Text)
                    {
                        if (dbc.StaffTable.Rows[i]["staffPw"].ToString() == textBoxPW.Text)
                        {
                            login = true;

                            dbc.Subject_Open();
                            dbc.SubjectTable = dbc.DS.Tables["subjectName"];

                            DataTable tmpTable = SetStaffString(dbc.StaffTable.Rows[i], dbc.SubjectTable);

                            MainMenu mainMenu = new MainMenu();
                            mainMenu.HospitalID = hospitalID;
                            mainMenu.DtDoc = tmpTable;
                            mainMenu.StaffId = dbc.StaffTable.Rows[i]["staffId"].ToString();

                            this.Visible = false;
                            mainMenu.ShowDialog();
                            this.Visible = true;

                            if (mainMenu.DisposeAll)
                            {
                                Dispose();
                            }

                        }
                        else if (dbc.StaffTable.Rows[i]["staffPw"].ToString() == string.Empty)
                        {
                            MessageBox.Show("신규 비밀번호 생성화면으로 이동합니다.", "알림");
                            StaffCreatePW staffCreatePW = new StaffCreatePW();
                            staffCreatePW.Pw = textBoxPW.Text;
                            staffCreatePW.StaffID = dbc.StaffTable.Rows[i]["staffID"].ToString();

                            staffCreatePW.ShowDialog();

                            if (staffCreatePW.CreateYn == "Y")
                            {
                                textBoxPW.Clear();
                                login = true;

                                MessageBox.Show("생성하신 비밀번호로 로그인 해주세요.", "알림");
                            }
                        }
                    }
                }
            }
            if(login == false) { MessageBox.Show("아이디와 패스워드를 확인해주세요", "알림"); }
        }

        private DataTable SetStaffString(DataRow drStaff, DataTable dtSubject)
        {
            DBClass.staffId = drStaff["staffId"].ToString();
            DBClass.staffName = drStaff["staffNm"].ToString();
            if (drStaff["docYn"].ToString() == "Y")
                DBClass.docYn = true;
            if (drStaff["noticeYn"].ToString() == "Y")
                DBClass.noticeYn = true;

            DataTable dtDoc = new DataTable();
            dtDoc.Columns.Add("subjectName");

            if(DBClass.staffId == "master")
            {
                for (int i = 0; i < dtSubject.Rows.Count; i++)
                        dtDoc.Rows.Add(dtSubject.Rows[i]["subjectName"].ToString());
            }
            else
            {
                for (int i = 0; i < dtSubject.Rows.Count; i++)
                {
                    if (dtSubject.Rows[i]["doctorName"].ToString() == DBClass.staffName)
                    {
                        dtDoc.Rows.Add(dtSubject.Rows[i]["subjectName"].ToString());
                    }
                }
            }

            return dtDoc;
        }

        private void textBoxPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }
        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProductKey = "";
            Properties.Settings.Default.ProductKeyValue = "";
            Properties.Settings.Default.Save();
        }
    }
}
