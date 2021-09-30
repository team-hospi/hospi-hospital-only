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
    public partial class SubjectAddUpdate : Form
    {
        bool isChanged = false;
        string addOrUpdate = string.Empty;
        string subjectName = string.Empty;
        DataTable tmpTable = new DataTable();

        public bool IsChanged
        {
            get { return isChanged; }
            set { isChanged = value; }
        }
        public string AddOrUpdate
        {
            get { return addOrUpdate; }
            set { addOrUpdate = value; }
        }
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }
        public DataTable TmpTable
        {
            get { return tmpTable; }
            set { tmpTable = value; }
        }

        public SubjectAddUpdate()
        {
            InitializeComponent();
        }

        private void SubjectAdd_Load(object sender, EventArgs e)
        {
            if (addOrUpdate == "ADD")
                groupBox1.Text = "새 진료과 추가";
            else
                groupBox1.Text = "진료과명 변경";

            if (addOrUpdate == "UPDATE")
                txtSubjectName.Text = subjectName;

            this.ActiveControl = txtSubjectName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == "ADD")
                AddNewSubject();
            else if (addOrUpdate == "UPDATE")
                UpdateSubjectName();
        }

        /// <summary>
        /// 메서드 -----------------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        /// 

        // 진료과목 생성
        private void AddNewSubject()
        {
            if (txtSubjectName.Text == string.Empty)
            {
                MessageBox.Show("진료과명은 공백일 수 없습니다", "알림");
                txtSubjectName.Focus();
            }
            else
            {
                DataRow newRow = tmpTable.NewRow();
                newRow["subjectCode"] = tmpTable.Rows.Count + 1;
                newRow["subjectName"] = txtSubjectName.Text;
                newRow["doctorName"] = string.Empty;
                newRow["useYn"] = "Y";

                tmpTable.Rows.Add(newRow);

                isChanged = true;

                Dispose();
            }
        }

        // 진료과명 업데이트
        private void UpdateSubjectName()
        {
            if (txtSubjectName.Text == string.Empty)
            {
                MessageBox.Show("진료과명은 공백일 수 없습니다", "알림");
                txtSubjectName.Focus();
            }
            else if (txtSubjectName.Text == subjectName)
            {
                MessageBox.Show("동일한 이름으로는 변경할 수 없습니다.", "알림");
                txtSubjectName.Focus();
            }
            else
            {
                for (int i = 0; i < tmpTable.Rows.Count; i++)
                {
                    if (tmpTable.Rows[i]["subjectName"].ToString() == subjectName)
                    {
                        DataRow upRow = tmpTable.Rows[i];
                        upRow.BeginEdit();
                        upRow["subjectName"] = txtSubjectName.Text;
                        upRow.EndEdit();

                        isChanged = true;

                        Dispose();
                    }
                }
            }
        }
    }
}
