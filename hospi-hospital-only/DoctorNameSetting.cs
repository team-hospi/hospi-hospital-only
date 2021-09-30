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
    public partial class DoctorNameSetting : Form
    {
        DBClass dbc = new DBClass();
        string subjectNo;   // 과목기본키

        public DoctorNameSetting()
        {
            InitializeComponent();
        }

        private void SetDBGrid()
        {
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];

            DBGrid.Rows.Clear();
            foreach (DataRow dr in dbc.SubjectTable.Rows)
            {
                DBGrid.Rows.Add(dr[0], dr[1], dr[2]);
            }
        }

        private void DoctorNameSetting_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "담당과목 해제";

            DBGrid.Columns.Add("subjectCode", "과목번호");
            DBGrid.Columns.Add("subjectName", "과목명");
            DBGrid.Columns.Add("doctorName", "담당의사");
            DBGrid.Columns.Add(checkBoxColumn);
            DBGrid.Columns[0].Width = 70;
            DBGrid.Columns[1].Width = 150;
            DBGrid.Columns[2].Width = 100;
            DBGrid.Columns[3].Width = 100;
            DBGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DBGrid.CurrentCell = null;
            DBGrid.AllowUserToAddRows = false;
            DBGrid.AllowUserToResizeRows = false;
            DBGrid.AllowUserToResizeColumns = false;

            foreach (DataGridViewColumn item in DBGrid.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SetDBGrid();

            DBGrid.RowHeadersVisible = false;

            SetComboBox();
        }

        private void DBGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3) // PW초기화
            {
                bool isChecked = Convert.ToBoolean(DBGrid.Rows[e.RowIndex].Cells[3].Value);

                if(DBGrid.Rows[e.RowIndex].Cells[2].Value.ToString() != "")
                {
                    switch (isChecked)
                    {
                        case true:
                            DBGrid.Rows[e.RowIndex].Cells[3].Value = false;
                            break;
                        case false:
                            DBGrid.Rows[e.RowIndex].Cells[3].Value = true;
                            break;
                    }
                }
            }
        }

        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string subjectName = DBGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            string doctorName = DBGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxSubjectName.Text = subjectName;
            comboBoxDocName.Text = doctorName;

            subjectNo = DBGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 저장
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateSubejctSetting();
            InsertDocName();
            SetDBGrid();
            SetComboBox();
        }

        private void InsertDocName()
        {
            if(textBoxSubjectName.Text != "" && comboBoxDocName.Text != "")
            {
                DataRow upRow = null;
                upRow = dbc.SubjectTable.Rows[Convert.ToInt32(subjectNo) -1];
                upRow.BeginEdit();
                upRow["doctorName"] = comboBoxDocName.Text;
                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "subjectName");
                dbc.DS.AcceptChanges();
            }
        }
        private void UpdateSubejctSetting()
        {
            DataRow upRow = null;
            bool isChecked;

            foreach (DataGridViewRow dRow in DBGrid.Rows)
            {
                // 담당의 해제
                isChecked = Convert.ToBoolean(dRow.Cells[3].Value);
                {
                    if (isChecked == true)
                    {
                        upRow = dbc.SubjectTable.Rows[dRow.Index];
                        upRow.BeginEdit();
                        upRow["doctorName"] = null;
                        upRow.EndEdit();
                        dbc.DBAdapter.Update(dbc.DS, "subjectName");
                        dbc.DS.AcceptChanges();
                    }
                }
            }
        }

        private void SetComboBox()
        {
            DBClass dbc2 = new DBClass();

            dbc2.Subject_DocName();
            dbc2.SubjectTable = dbc2.DS.Tables["docNm"];

            comboBoxDocName.Items.Clear();
            for (int i = 0; i < dbc2.SubjectTable.Rows.Count; i++)
            {
                comboBoxDocName.Items.Add(dbc2.SubjectTable.Rows[i]["staffNm"].ToString());
            }
        }
    }
}
