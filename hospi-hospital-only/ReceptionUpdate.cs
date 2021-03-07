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
    public partial class ReceptionUpdate : Form
    {
        DBClass dbc = new DBClass();
        int receptionID; //  접수번호

        public int ReceptionID
        {
            get { return receptionID; }
            set { receptionID = value; }
        }

        public ReceptionUpdate()
        {
            InitializeComponent();
        }

        // 폼 로드
        private void ReceptionUpdate_Load(object sender, EventArgs e)
        {
            // 폼 로드시 포커스
            this.ActiveControl = button1;

            // 접수DB
            dbc.Reception_Open();
            dbc.ReceptionTable = dbc.DS.Tables["reception"];
            DataRow row = dbc.ReceptionTable.Rows[receptionID-1];
            dateTimePicker1.Text = "20" + row["receptionDate"].ToString();
            comboBoxTime1.Text = row["receptionTime"].ToString().Substring(0, 2);
            comboBoxTime2.Text = row["receptionTime"].ToString().Substring(2, 2);
            textBoxChartNum.Text = row["patientID"].ToString();
            comboBoxSubjcet.Text = row["subjectCode"].ToString();
            comboBoxReceptionist.Text = row["receptionistCode"].ToString();
            if(row["receptionType"].ToString() == "1")
            {
                textBoxReceptionType.Text = "진료 대기중";
            }else if(row["receptionType"].ToString() == "4")
            {
                textBoxReceptionType.Text = "진료 보류중";
            }

            // 환자DB
            dbc.Visitor_Open();
            dbc.VisitorTable = dbc.DS.Tables["visitor"];
            row = dbc.VisitorTable.Rows[Convert.ToInt32(textBoxChartNum.Text) - 1];
            patientName.Text = row["patientName"].ToString();

            // 과목DB
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            row = dbc.SubjectTable.Rows[Convert.ToInt32(comboBoxSubjcet.Text) - 1];
            comboBoxSubjcet.Text = row["subjectName"].ToString();
            // comboBoxSubject에 과목명 추가
            for (int i = 0; i < dbc.SubjectTable.Columns.Count; i++)  
            {
                comboBoxSubjcet.Items.Add(dbc.SubjectTable.Rows[i][1]);
            }

            // 접수자DB
            dbc.Receptionist_Open();
            dbc.ReceptionistTable = dbc.DS.Tables["receptionist"];
            row = dbc.ReceptionistTable.Rows[Convert.ToInt32(comboBoxReceptionist.Text) - 1];
            comboBoxReceptionist.Text = row["receptionistName"].ToString();
            // comboBoxReceptionist에 접수자명 추가
            for (int i = 0; i < dbc.ReceptionistTable.Columns.Count; i++)  
            {
                comboBoxReceptionist.Items.Add(dbc.ReceptionistTable.Rows[i][1]);
            }
        }

        // 취소버튼
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        // 수정완료 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dbc.Reception_Open();
                dbc.ReceptionTable = dbc.DS.Tables["reception"];
                DataRow upRow = dbc.ReceptionTable.Rows[receptionID - 1];
                upRow.BeginEdit();
                upRow["receptionDate"] = dateTimePicker1.Value.ToString("yy-MM-dd");
                upRow["receptionTime"] = comboBoxTime1.Text + comboBoxTime2.Text;
                for (int i = 0; i < dbc.SubjectTable.Rows.Count; i++)
                {
                    if (dbc.SubjectTable.Rows[i]["subjectName"].ToString() == comboBoxSubjcet.Text)
                    {
                        upRow["subjectCode"] = dbc.SubjectTable.Rows[i]["subjectCode"];
                    }
                }
                for (int i = 0; i < dbc.ReceptionistTable.Rows.Count; i++)
                {
                    if (dbc.ReceptionistTable.Rows[i]["receptionistName"].ToString() == comboBoxReceptionist.Text)
                    {
                        upRow["receptionistCode"] = dbc.ReceptionistTable.Rows[i]["receptionistCode"];
                    }
                }
                upRow.EndEdit();
                dbc.DBAdapter.Update(dbc.DS, "reception");
                dbc.DS.AcceptChanges();

                MessageBox.Show("수정이 완료되었습니다.", "알림");
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
