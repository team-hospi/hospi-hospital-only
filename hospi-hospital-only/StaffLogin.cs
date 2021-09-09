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

                            MainMenu mainMenu = new MainMenu();
                            mainMenu.HospitalID = hospitalID;
                            mainMenu.StaffId = dbc.StaffTable.Rows[i]["staffId"].ToString();

                            this.Visible = false;
                            mainMenu.ShowDialog();
                            this.Visible = true;

                        }
                        else if (dbc.StaffTable.Rows[i]["staffPw"].ToString() == string.Empty)
                        {
                            login = true;

                            DataRow upRow = null;
                            upRow = dbc.StaffTable.Rows[i];
                            upRow.BeginEdit();
                            upRow["staffPW"] = textBoxPW.Text;
                            upRow.EndEdit();
                            dbc.DBAdapter.Update(dbc.DS, "staff");
                            dbc.DS.AcceptChanges();

                            MessageBox.Show("신규 비밀번호 설정 완료!", "알림");

                            MainMenu mainMenu = new MainMenu();
                            mainMenu.HospitalID = hospitalID;
                            mainMenu.StaffId = dbc.StaffTable.Rows[i]["staffId"].ToString();

                            this.Visible = false;
                            mainMenu.ShowDialog();
                            this.Visible = true;
                        }
                    }
                }
            }
            if(login == false) { MessageBox.Show("아이디와 패스워드를 확인해주세요", "알림"); }
        }

        private void textBoxPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }
    }
}
