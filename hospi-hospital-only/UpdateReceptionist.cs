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
    public partial class UpdateReceptionist : Form
    {
        DBClass dbc = new DBClass();

        public UpdateReceptionist()
        {
            InitializeComponent();
        }

        // 폼 로드
        private void UpdateReceptionist_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.Receptionist_Open();
                dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    listBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i]["receptionistName"]);
                }
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

        // 종료버튼
        private void buttonFinish_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 추가 버튼
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("접수자명이 입력되지 않았습니다.", "알림");
            }
            else
            {
                for (int i = 0; i < listBoxReceptionist.Items.Count; i++)
                {
                    if (listBoxReceptionist.Items[i].ToString() == textBoxName.Text)
                    {
                        MessageBox.Show(textBoxName.Text + "는 이미 존재합니다.", "알림");
                        return;
                    }
                }
                DialogResult ok = MessageBox.Show("접수자 '" + textBoxName.Text + "' 을/를 등록합니다.", "알림", MessageBoxButtons.YesNo);

                if(ok == DialogResult.Yes)
                {
                    try
                    {
                        DataRow newRow = dbc.ReceptionistTable.NewRow();
                        newRow["receptionistCode"] = listBoxReceptionist.Items.Count + 1;
                        newRow["receptionistName"] = textBoxName.Text;
                        dbc.ReceptionistTable.Rows.Add(newRow);
                        dbc.DBAdapter.Update(dbc.DS, "receptionist");
                        dbc.DS.AcceptChanges();

                        listBoxReceptionist.Items.Add(textBoxName.Text);
                        listBoxReceptionist.SelectedIndex = listBoxReceptionist.Items.Count - 1;

                        textBoxName.Clear();
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

        // 리스트박스 아이템 클릭
        private void listBoxReceptionist_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxName.Text = listBoxReceptionist.SelectedItem.ToString();
        }

        // 삭제 버튼
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxReceptionist.SelectedIndex == -1)
            {
                MessageBox.Show("삭제할 정보가 선택되지 않았습니다.", "알림");
            }
            else
            {
                DialogResult ok = MessageBox.Show("접수자 '" + listBoxReceptionist.Items[listBoxReceptionist.SelectedIndex].ToString() +"' 을/를 삭제합니다." , "알림", MessageBoxButtons.YesNo);

                if (ok == DialogResult.Yes)
                {
                    try
                    {
                        DataColumn[] PrimaryKey = new DataColumn[1];
                        PrimaryKey[0] = dbc.ReceptionistTable.Columns["receptionistCode"];
                        dbc.ReceptionistTable.PrimaryKey = PrimaryKey;
                        DataRow currRow = dbc.ReceptionistTable.Rows.Find(listBoxReceptionist.SelectedIndex+1);
                        int rowCount = dbc.ReceptionistTable.Rows.Count;  // 전체 행의 개수 (삭제전)
                        currRow.Delete();
                        int select = Convert.ToInt32(listBoxReceptionist.SelectedIndex+1 );  //  SelectedIndex - 1를 증감시킬경우 for문에 영향을 주므로 변수를 따로 지정해서 사용

                        for (int i = 0; i < (rowCount - Convert.ToInt32(listBoxReceptionist.SelectedIndex+1 )); i++)  //  행 하나가 삭제될 경우 행의 인덱스가 상제 대상보다 높은경우 모두 -1 해줌
                        {
                            currRow = dbc.ReceptionistTable.Rows[rowCount - (rowCount - select)];
                            MessageBox.Show(currRow["receptionistName"].ToString());
                            currRow.BeginEdit();
                            currRow["receptionistCode"] = Convert.ToInt32(currRow["receptionistCode"]) - 1;
                            currRow.EndEdit();
                            select += 1;
                        }

                        dbc.DBAdapter.Update(dbc.DS, "receptionist");
                        dbc.DS.AcceptChanges();

                        listBoxReceptionist.Items.Remove(textBoxName.Text);
                        textBoxName.Clear();
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

        // 엔터 이벤트
        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(sender, e);
            }
        }

        // 검색 버튼
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int search = 0;

            for(int i=0; i<listBoxReceptionist.Items.Count; i++)
            {
                if (textBoxName.Text == listBoxReceptionist.Items[i].ToString())
                {
                    listBoxReceptionist.SelectedIndex = i;
                    search = 1;
                }
            }
            if(search == 0)
            {
                MessageBox.Show("검색결과 없음,", "알림");
            }
        }
    }
}
