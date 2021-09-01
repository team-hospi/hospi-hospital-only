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
                        MessageBox.Show("ID가 중복됩니다. \r\n다른 ID를 입력해주세요.", "알림");
                        textBoxId.Focus();
                        return;
                    }
                }
                MessageBox.Show("사용 가능한 ID입니다.", "알림");
                buttonCheck.Enabled = false;
                textBoxPW1.Focus();
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

        private void textBoxPW1_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPW1.Text.Length >= 4)
            {
                pwLabel1.Visible = true;
            }
            else if (textBoxPW1.Text.Length < 4)
            {
                pwLabel1.Visible = false;
            }
            if (textBoxPW2.Text == textBoxPW1.Text && textBoxPW2.Text.Length >= 4)
            {
                pwLabel2.Visible = true;
            }
            else if (textBoxPW2.Text != textBoxPW1.Text || textBoxPW2.Text.Length < 4)
            {
                pwLabel2.Visible = false;
            }
        }

        private void textBoxPW2_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPW2.Text == textBoxPW1.Text)
            {
                pwLabel2.Visible = true;
            }
            else if (textBoxPW2.Text != textBoxPW1.Text)
            {
                pwLabel2.Visible = false;
            }
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
                if (pwLabel1.Visible == true && pwLabel2.Visible == true)
                {
                    DialogResult ok = MessageBox.Show("신규 ID를 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ok == DialogResult.Yes)
                    {
                        DataRow newRow = dbc.StaffTable.NewRow();
                        newRow["staffId"] = textBoxId.Text;
                        newRow["staffPw"] = textBoxPW1.Text;
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
    }
}
