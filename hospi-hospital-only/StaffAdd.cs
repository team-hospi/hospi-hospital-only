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
    public partial class StaffAdd : Form
    {
        DBClass dbc = new DBClass();
        string staffId;

        public string StaffId
        {
            get { return staffId; }
            set { staffId = value; }
        }

        public StaffAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            if (textBoxId.Text != "" && textBoxId.Text != " ")
            {
                for (int i = 0; i < dbc.StaffTable.Rows.Count; i++)
                {
                    if (dbc.StaffTable.Rows[i]["staffId"].ToString() == textBoxId.Text)
                    {
                        MessageBox.Show("사용중인 ID입니다.. \r\n다른 ID를 입력해주세요.", "알림");
                        textBoxId.Focus();
                        return;
                    }
                }
                MessageBox.Show("사용 가능한 ID입니다.", "알림");
                buttonCheck.Enabled = false;
            }
            else
            {
                MessageBox.Show("ID를 입력해주세요", "알림");
            }
        }

        private void MasterAdd_Load(object sender, EventArgs e)
        {
            dbc.Staff_open();
            dbc.StaffTable = dbc.DS.Tables["staff"];
        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            textBoxId.SelectAll();
        }


        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            buttonCheck.Enabled = true;
        }

        // 완료 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            if (buttonCheck.Enabled == true)
            {
                MessageBox.Show("ID 중복확인을 먼저 진행해주세요.", "알림:");
            }
            else
            {
                    DataRow newRow = dbc.StaffTable.NewRow();
                    newRow["staffId"] = textBoxId.Text;
                    newRow["staffPw"] = string.Empty;
                    newRow["staffNm"] = textBoxName.Text;
                    newRow["docYn"] = "N";
                    newRow["useYn"] = "Y";

                    dbc.StaffTable.Rows.Add(newRow);
                    dbc.DBAdapter.Update(dbc.DS, "staff");
                    dbc.DS.AcceptChanges();

                    MessageBox.Show("ID : " + textBoxId.Text + "\r\n등록이 완료되었습니다.", "알림");
                    Dispose();
            }
        }

    }
}
