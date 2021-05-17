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
    public partial class Notice : Form
    {
        DBClass dbc = new DBClass();
        string[] user;

        public string[] User
        {
            get { return user; }
            set { user = value; }
        }

        public Notice()
        {
            InitializeComponent();
        }

        private void Notice_Load(object sender, EventArgs e)
        {
            textBoxStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            for (int i = 0; i < user.Length; i++)
            {
                comboBox1.Items.Add(user[i]);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dateTimePickerEndDate.Enabled = false;
            }
            else if (checkBox1.Checked == false)
            {
                dateTimePickerEndDate.Enabled = true;
            }
        }

        private void textBoxTitle_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text == "제목을 입력하세요.")
            {
                textBoxTitle.SelectAll();
            }
        }

        private void textBoxInfo_Click(object sender, EventArgs e)
        {
            if (textBoxInfo.Text == "내용을 입력하세요.")
            {
                textBoxInfo.SelectAll();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text == "제목을 입력하세요.")
            {
                MessageBox.Show("제목을 입력해주세요.", "알림");
            }
            else if (textBoxInfo.Text == "내용을 입력하세요.")
            {
                MessageBox.Show("내용을 입력해주세요.", "알림");
            }
            else if (comboBox1.Text == "선택")
            {
                MessageBox.Show("게시자를 선택해주세요.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("공지사항을 등록하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        dbc.Notice_Open();
                        //dbc.DBAdapter.Fill(dbc.DS, "notice");
                        dbc.NoticeTable = dbc.DS.Tables["notice"];
                        DataRow newRow = dbc.NoticeTable.NewRow();

                        newRow["NoticeID"] = dbc.NoticeTable.Rows.Count;
                        newRow["NoticeTitle"] = textBoxTitle.Text;
                        newRow["NoticeInfo"] = textBoxInfo.Text;
                        newRow["NoticeStartDate"] = textBoxStartDate.Text.Substring(2, 2) + textBoxStartDate.Text.Substring(5, 2) + textBoxStartDate.Text.Substring(8, 2);
                        if (checkBox1.Checked == true)
                        {
                            newRow["NoticeEndDate"] = "999999";
                        }
                        else if (checkBox1.Checked == false)
                        {
                            newRow["NoticeEndDate"] = dateTimePickerEndDate.Value.ToString("yyMMdd");
                        }
                        newRow["NoticeWriter"] = comboBox1.Text;

                        dbc.NoticeTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "Notice");
                        dbc.DS.AcceptChanges();

                        Dispose();
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
        }
    }
}
