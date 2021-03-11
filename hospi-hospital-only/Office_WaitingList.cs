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
    public partial class Office_WaitingList : Form
    {
        DBClass dbc = new DBClass();
        DataTable watingTable;
        string date;
        int subjectID;

        public Office_WaitingList()
        {
            InitializeComponent();
        }

        public DataTable WatingTable
        {
            get { return watingTable; }
            set { watingTable = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public int SubjectID
        {
            get { return subjectID; }
            set { subjectID = value; }
        }

        private void Office_WaitingList_Load(object sender, EventArgs e)
        {
            for(int i=0; i<watingTable.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (listView1.Items.Count + 1).ToString("00");
                item.SubItems.Add(watingTable.Rows[i]["receptionTime"].ToString().Substring(0,2)+ " : " + watingTable.Rows[i]["receptionTime"].ToString().Substring(2, 2)) ;
                item.SubItems.Add(watingTable.Rows[i]["patientID"].ToString());
                item.SubItems.Add(watingTable.Rows[i]["patientName"].ToString());
                // Age
                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "1" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "2")
                {
                    item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString());
                }
                else if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "3" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "4")
                {
                    item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString());
                }
                item.SubItems.Add(watingTable.Rows[i]["receptionistName"].ToString());
                listView1.Items.Add(item);

                timer1.Start();
            }
        }

        // 5초마다 목록 리프레쉬
        private void timer1_Tick(object sender, EventArgs e)
        {
            dbc.Reception_Office(date, subjectID);
            watingTable = dbc.DS.Tables["reception"];
            textBoxReceptionCount.Text = watingTable.Rows.Count.ToString();
            if(listView1.Items.Count != watingTable.Rows.Count)
            {
                for (int i = listView1.Items.Count; i < watingTable.Rows.Count; i++)  
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = (listView1.Items.Count + 1).ToString("00");
                    item.SubItems.Add(watingTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + " : " + watingTable.Rows[i]["receptionTime"].ToString().Substring(2, 2));
                    item.SubItems.Add(watingTable.Rows[i]["patientID"].ToString());
                    item.SubItems.Add(watingTable.Rows[i]["patientName"].ToString());
                    // Age
                    int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                    if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "1" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "2")
                    {
                        item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString());
                    }
                    else if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "3" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "4")
                    {
                        item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString());
                    }
                    item.SubItems.Add(watingTable.Rows[i]["receptionistName"].ToString());
                    listView1.Items.Add(item);
                    textBoxReceptionCount.Text = watingTable.Rows.Count.ToString();
                }
            }
        }
    }
}
