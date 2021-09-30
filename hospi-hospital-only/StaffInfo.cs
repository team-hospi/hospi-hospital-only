﻿using System;
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
    public partial class StaffInfo : Form
    {
        DBClass dbc = new DBClass();

        public StaffInfo()
        {
            InitializeComponent();
        }

        private void MasterInfomation_Load(object sender, EventArgs e)
        {
            dbc.Staff_open();
            dbc.StaffTable = dbc.DS.Tables["staff"];

            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();

            checkBoxColumn.HeaderText = "PW초기화";

            DBGrid.Columns.Add("staffId", "ID");
            DBGrid.Columns.Add("staffNm", "직원명");
            DBGrid.Columns.Add("docYn", "의사여부");
            DBGrid.Columns.Add("noticeYn", "공지권한");
            DBGrid.Columns.Add("useYn", "사용여부");
            DBGrid.Columns.Add(checkBoxColumn);
            DBGrid.Columns[0].Width = 120;
            DBGrid.Columns[1].Width = 100;
            DBGrid.Columns[2].Width = 65;
            DBGrid.Columns[3].Width = 65;
            DBGrid.Columns[4].Width = 70;
            DBGrid.Columns[5].Width = 70;
            DBGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DBGrid.CurrentCell = null;
            DBGrid.AllowUserToAddRows = false;
            DBGrid.AllowUserToResizeRows = false;
            DBGrid.AllowUserToResizeColumns = false;

            foreach (DataGridViewColumn item in DBGrid.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataRow dr in dbc.StaffTable.Rows)
            {
                DBGrid.Rows.Add(dr[0], dr[2], dr[3], dr[5], dr[4]);
            }

            DBGrid.RowHeadersVisible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 비밀번호 확인 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (DBGrid.Rows[e.RowIndex].Cells[2].Value.ToString() == "Y")
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Value = "N";
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[2].Value.ToString() == "N")
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Value = "Y";
                }

                if (DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor == Color.Empty)
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Red;
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor == Color.Red)
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Empty;
                }
            }

            if (e.ColumnIndex == 3)
            {
                if (DBGrid.Rows[e.RowIndex].Cells[3].Value.ToString() == "Y")
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Value = "N";
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[3].Value.ToString() == "N")
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Value = "Y";
                }

                if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Empty)
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Red;
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Red)
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Empty;
                }
            }

            if (e.ColumnIndex == 4)
            {
                if (DBGrid.Rows[e.RowIndex].Cells[3].Value.ToString() == "Y")
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Value = "N";
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[3].Value.ToString() == "N")
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Value = "Y";
                }

                if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Empty)
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Red;
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Red)
                {
                    DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Empty;
                }
            }
        }

        private void DBGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) // PW초기화
            {
                bool isChecked = Convert.ToBoolean(DBGrid.Rows[e.RowIndex].Cells[5].Value);

                switch (isChecked)
                {
                    case true:
                        DBGrid.Rows[e.RowIndex].Cells[5].Value = false;
                        break;
                    case false:
                        DBGrid.Rows[e.RowIndex].Cells[5].Value = true;
                        break;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)    //저장
        {
            DialogResult ok = MessageBox.Show("저장하시겠습니까?", "알림", MessageBoxButtons.YesNo);

            if (ok == DialogResult.Yes)
            {
                dbc.Staff_open();
                dbc.StaffTable = dbc.DS.Tables["staff"];

                UpdateStaffSetting();

                Dispose();
            }
        }

        private void UpdateStaffSetting()
        {
            DataRow upRow = null;
            bool isChecked;
            string ynValue;

            foreach (DataGridViewRow dRow in DBGrid.Rows)
            {
                // 패스워드 초기화
                isChecked = Convert.ToBoolean(dRow.Cells[5].Value);
                {
                    if (isChecked == true)
                    {
                        upRow = dbc.StaffTable.Rows[dRow.Index];
                        upRow.BeginEdit();
                        upRow["staffPW"] = string.Empty;
                        upRow.EndEdit();
                        dbc.DBAdapter.Update(dbc.DS, "staff");
                        dbc.DS.AcceptChanges();
                    }
                }

                // 의사유무 변경
                if (dRow.Cells[2].Style.ForeColor == Color.Red)
                {
                    if (dRow.Cells[2].Value.ToString() == "Y")
                        ynValue = "Y";
                    else
                        ynValue = "N";

                    upRow = dbc.StaffTable.Rows[dRow.Index];
                    upRow.BeginEdit();
                    upRow["docYn"] = ynValue;
                    upRow.EndEdit();
                    dbc.DBAdapter.Update(dbc.DS, "staff");
                    dbc.DS.AcceptChanges();
                }

                // 사용유무 변경
                if (dRow.Cells[4].Style.ForeColor == Color.Red)
                {
                    if (dRow.Cells[4].Value.ToString() == "Y")
                        ynValue = "Y";
                    else
                        ynValue = "N";

                    upRow = dbc.StaffTable.Rows[dRow.Index];
                    upRow.BeginEdit();
                    upRow["useYn"] = ynValue;
                    upRow.EndEdit();
                    dbc.DBAdapter.Update(dbc.DS, "staff");
                    dbc.DS.AcceptChanges();
                }

                // 공지권한 변경
                if (dRow.Cells[3].Style.ForeColor == Color.Red)
                {
                    if (dRow.Cells[3].Value.ToString() == "Y")
                        ynValue = "Y";
                    else
                        ynValue = "N";

                    upRow = dbc.StaffTable.Rows[dRow.Index];
                    upRow.BeginEdit();
                    upRow["noticeYn"] = ynValue;
                    upRow.EndEdit();
                    dbc.DBAdapter.Update(dbc.DS, "staff");
                    dbc.DS.AcceptChanges();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoctorNameSetting a = new DoctorNameSetting();
            a.ShowDialog();
        }
    }
}
