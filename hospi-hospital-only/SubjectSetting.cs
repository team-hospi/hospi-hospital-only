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
    public partial class SubjectSetting : Form
    {
        DBClass dbc = new DBClass();
        DataTable tmpSubjectTable = new DataTable();

        public SubjectSetting()
        {
            InitializeComponent();

            DBGrid.Columns.Add("subjectCode", "No.");
            DBGrid.Columns.Add("subjectName", "진료과명");
            DBGrid.Columns.Add("doctorName", "담당의사");
            DBGrid.Columns.Add("useYn", "사용여부");
            DBGrid.Columns[0].Width = 40;
            DBGrid.Columns[1].Width = 160;
            DBGrid.Columns[2].Width = 140;
            DBGrid.Columns[3].Width = 70;

            DBGrid.CurrentCell = null;
            DBGrid.AllowUserToAddRows = false;
            DBGrid.AllowUserToResizeRows = false;
            DBGrid.AllowUserToResizeColumns = false;

            DBGrid.DefaultCellStyle.SelectionBackColor = Color.White;
            DBGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            DBGrid.Columns[1].DefaultCellStyle.SelectionBackColor = Color.Yellow;
            DBGrid.Columns[3].DefaultCellStyle.SelectionBackColor = Color.Yellow;

            DBGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            foreach (DataGridViewColumn item in DBGrid.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DBGrid.RowHeadersVisible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult ok = MessageBox.Show("저장하시겠습니까?", "알림", MessageBoxButtons.YesNo);

            if (ok == DialogResult.Yes)
            {
                UpdateSubjectSetting();
                Dispose();
            }
        }

        private void SubjectSetting_Load(object sender, EventArgs e)
        {
            setSubjectGrid();
        }

        private void btnDocSet_Click(object sender, EventArgs e)
        {
            DoctorNameSetting docNameSetting = new DoctorNameSetting();
            docNameSetting.ShowDialog();

            if (docNameSetting.IsUpdated)
            {
                DBGrid.Rows.Clear();
                setSubjectGrid();
            }
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            SubjectAddUpdate subjectAdd = new SubjectAddUpdate();
            subjectAdd.AddOrUpdate = "ADD";
            subjectAdd.TmpTable = tmpSubjectTable;
            subjectAdd.ShowDialog();

            DataTable tmpTable = subjectAdd.TmpTable;

            if (subjectAdd.IsChanged)
            {
                DBGrid.Rows.Clear();
                foreach (DataRow dr in tmpTable.Rows)
                {
                    DBGrid.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
                }
                DBGrid.CurrentCell = DBGrid.Rows[DBGrid.Rows.Count - 1].Cells[1];
            }
        }

        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // useYn 변경
            if (e.ColumnIndex == 3)
                UpdateUseYn(e);

            // subjectName 변경
            if (e.ColumnIndex == 1)
            {
                ChangeSubjectName(e);
                DBGrid.CurrentCell = DBGrid.Rows[e.RowIndex].Cells[1];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = DBGrid.CurrentCell.RowIndex;

            if (DBGrid.CurrentCell.ColumnIndex == 1)
            {
                SubjectAddUpdate subjectAdd = new SubjectAddUpdate();
                subjectAdd.AddOrUpdate = "UPDATE";
                subjectAdd.TmpTable = tmpSubjectTable;
                subjectAdd.SubjectName = DBGrid.CurrentCell.Value.ToString();
                subjectAdd.ShowDialog();

                DataTable tmpTable = subjectAdd.TmpTable;

                if (subjectAdd.IsChanged)
                {
                    DBGrid.Rows.Clear();
                    foreach (DataRow dr in tmpTable.Rows)
                    {
                        DBGrid.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
                    }
                    DBGrid.CurrentCell = DBGrid.Rows[rowIndex].Cells[1];
                }
            }
            else
            {
                MessageBox.Show("이름을 변경하실 진료과를 선택해주세요", "알림");
            }

        }
        /// <summary>
        /// 메서드 -----------------------------------------------------------------------------------------------------------------------------------
        /// </summary>

        // 변경사항 최종 업데이트 (진료과 추가, 진료과명 변경, 사용여부 변경)
        private void UpdateSubjectSetting()
        {
            dbc.SubjectTable = tmpSubjectTable;
            dbc.DBAdapter.Update(dbc.DS, "subjectName");
            dbc.DS.AcceptChanges();
        }

        // 폼로드시 db에서 테이블가져오기
        private void setSubjectGrid()
        {
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];

            tmpSubjectTable = dbc.SubjectTable;

            foreach (DataRow dr in dbc.SubjectTable.Rows)
            {
                DBGrid.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }
        }

        private void UpdateUseYn(DataGridViewCellEventArgs e)
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
                DBGrid.Rows[e.RowIndex].Cells[3].Style.SelectionForeColor = Color.Red;
            }
            else if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Red)
            {
                DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Empty;
                DBGrid.Rows[e.RowIndex].Cells[3].Style.SelectionForeColor = Color.Black;
            }

            string ynValue;
            DataRow upRow;

            foreach (DataGridViewRow dRow in DBGrid.Rows)
            {
                if (dRow.Cells[3].Style.ForeColor == Color.Red)
                {
                    if (dRow.Cells[3].Value.ToString() == "Y")
                        ynValue = "Y";
                    else
                        ynValue = "N";

                    upRow = tmpSubjectTable.Rows[dRow.Index];
                    upRow.BeginEdit();
                    upRow["useYn"] = ynValue;
                    upRow.EndEdit();
                }
            }

        }

        private void ChangeSubjectName(DataGridViewCellEventArgs e)
        {
            SubjectAddUpdate subjectAdd = new SubjectAddUpdate();
            subjectAdd.AddOrUpdate = "UPDATE";
            subjectAdd.TmpTable = tmpSubjectTable;
            subjectAdd.SubjectName = DBGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            subjectAdd.ShowDialog();

            DataTable tmpTable = subjectAdd.TmpTable;

            if (subjectAdd.IsChanged)
            {
                DBGrid.Rows.Clear();
                foreach (DataRow dr in tmpTable.Rows)
                {
                    DBGrid.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
                }
            }
        }
    }
}
    
