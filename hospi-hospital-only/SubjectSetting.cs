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
        List<string> depart = new List<string>();
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

            DBGrid.DefaultCellStyle.ForeColor = Color.Red;

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
            for(int j=0; j<DBGrid.Rows.Count; j++)
            {
                for(int i=0;i<DBGrid.Columns.Count; i++)
                {
                    if(DBGrid.Rows[j].Cells[i].Style.ForeColor == Color.Red)
                    {
                        DialogResult ok = MessageBox.Show("변경사항이 존재합니다. \r저장하지않고 종료하시겠습니까?", "알림", MessageBoxButtons.YesNo);
                        {
                            if (ok == DialogResult.Yes)
                            {
                                Dispose();
                            }
                            return;
                        }
                    }
                }
            }
            Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult ok = MessageBox.Show("저장하시겠습니까?", "알림", MessageBoxButtons.YesNo);

            if (ok == DialogResult.Yes)
            {
                SubjectAdd(); // 진료과(depart)List에 사용여부Y로 표기된 진료과 저장
                dbc.HospitalSubject_Update(); // 진료과 파이어베이스에 업데이트
                UpdateSubjectSetting();

                DBGrid.Rows.Clear();
                setSubjectGrid();

                label1.Visible = false;
                btnDocSet.Enabled = true;
                SetColor();
            }
        }

        private void SubjectSetting_Load(object sender, EventArgs e)
        {
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            tmpSubjectTable = dbc.SubjectTable.Copy();

            dbc.FireConnect();
            dbc.Delay(200);
            dbc.FindDocument(DBClass.hospiID);
            setSubjectGrid();
            SetColor();
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

            tmpSubjectTable = subjectAdd.TmpTable;

            if (subjectAdd.IsChanged)
            {
                DBGrid.Rows.Clear();
                foreach (DataRow dr in tmpSubjectTable.Rows)
                {
                    DBGrid.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
                }
                DBGrid.CurrentCell = DBGrid.Rows[DBGrid.Rows.Count - 1].Cells[1];

                EnableDocSet();
            }

            SetColor();
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
                SetColor();
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

                    EnableDocSet();
                }
            }
            else
            {
                MessageBox.Show("이름을 변경하실 진료과를 선택해주세요", "알림");
            }
            SetColor();
        }

        private void DBGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor == Color.Black)
                DBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Black;
            else 
                DBGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionForeColor = Color.Red;
        }

        /// <summary>
        /// 메서드 -----------------------------------------------------------------------------------------------------------------------------------
        /// </summary>

        // 변경사항 최종 업데이트 (진료과 추가, 진료과명 변경, 사용여부 변경)
        private void UpdateSubjectSetting()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(tmpSubjectTable);

            DBClass dbc2 = new DBClass();
            dbc2.Subject_Open();
            dbc2.DS = ds;
            dbc2.DBAdapter.Update(dbc2.DS.Tables[0]);
            dbc2.DS.AcceptChanges();
        }

        // 폼로드시 db에서 테이블가져오기
        private void setSubjectGrid()
        {
            foreach (DataRow dr in tmpSubjectTable.Rows)
            {
                DBGrid.Rows.Add(dr[0], dr[1], dr[2], dr[3]);
            }
        }

        private void UpdateUseYn(DataGridViewCellEventArgs e)
        {
            DataRow upRow;

            if (DBGrid.Rows[e.RowIndex].Cells[3].Value.ToString() == "Y")
            {
                DBGrid.Rows[e.RowIndex].Cells[3].Value = "N";
                upRow = tmpSubjectTable.Rows[e.RowIndex];
                upRow.BeginEdit();
                upRow["useYn"] = "N";
                upRow.EndEdit();
            }
            else if (DBGrid.Rows[e.RowIndex].Cells[3].Value.ToString() == "N")
            {
                DBGrid.Rows[e.RowIndex].Cells[3].Value = "Y";
                upRow = tmpSubjectTable.Rows[e.RowIndex];
                upRow.BeginEdit();
                upRow["useYn"] = "Y";
                upRow.EndEdit();
            }

            if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Black)
            {
                DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Red;
                DBGrid.Rows[e.RowIndex].Cells[3].Style.SelectionForeColor = Color.Red;
            }
            else if (DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor == Color.Red)
            {
                DBGrid.Rows[e.RowIndex].Cells[3].Style.ForeColor = Color.Black;
                DBGrid.Rows[e.RowIndex].Cells[3].Style.SelectionForeColor = Color.Black;
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

                EnableDocSet();

            }
        }

        private void SetColor()
        {
            for(int i=0; i<dbc.SubjectTable.Rows.Count; i++)
            {
                for(int j=0; j<dbc.SubjectTable.Columns.Count; j++)
                {
                    if(dbc.SubjectTable.Rows[i][j].ToString() == tmpSubjectTable.Rows[i][j].ToString())
                    {
                        DBGrid.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        DBGrid.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;
                    }
                }
            }

            if(tmpSubjectTable.Rows.Count > dbc.SubjectTable.Rows.Count)
            {
                for (int i = tmpSubjectTable.Rows.Count - 1; i >= dbc.SubjectTable.Rows.Count; i--)
                {
                    for (int j = 0; j < dbc.SubjectTable.Columns.Count; j++)
                    {
                        DBGrid.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;
                    }
                }
            }
        }

        private void EnableDocSet()
        {
            btnDocSet.Enabled = false;
            label1.Visible = true;
        }

        private void SubjectAdd()
        {
            for(int i =0; i< DBGrid.Rows.Count; i++)
            {
                if (DBGrid.Rows[i].Cells[3].Value.ToString() == "Y")
                    depart.Add(DBGrid.Rows[i].Cells[1].Value.ToString());
            }
            DBClass.hospidepartment = depart.ToArray();
            depart.Clear();
        }
    }
}
    
