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

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "PW초기화";

            DBGrid.Columns.Add("staffId", "ID");
            DBGrid.Columns.Add("staffNm", "직원명");
            DBGrid.Columns.Add("docYn", "의사여부");
            DBGrid.Columns.Add("useYn", "사용여부");
            DBGrid.Columns.Add(checkBoxColumn);
            DBGrid.Columns[0].Width = 120;
            DBGrid.Columns[1].Width = 100;
            DBGrid.Columns[2].Width = 65;
            DBGrid.Columns[3].Width = 65;
            DBGrid.Columns[4].Width = 70;
            DBGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
                DBGrid.Rows.Add(dr[0], dr[2], dr[3], dr[4]);
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
            if (e.ColumnIndex == 2 )
            {
                if (DBGrid.Rows[e.RowIndex].Cells[2].Value.ToString() == "Y")
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Value = "N";
                }
                else if (DBGrid.Rows[e.RowIndex].Cells[2].Value.ToString() == "N")
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Value = "Y";
                }

                if(DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor == Color.Empty)
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Red;
                }
                else if(DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor == Color.Red)
                {
                    DBGrid.Rows[e.RowIndex].Cells[2].Style.ForeColor = Color.Empty;
                }
            }

            if (e.ColumnIndex == 3 )
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
            DBGrid.Rows[e.RowIndex].Cells[4].Value = true;
        }
    }
}
