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
                if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "1" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "2" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "0")
                {
                    item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1899).ToString());
                }
                else if (watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "3" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "4" || watingTable.Rows[i]["patientBirthCode"].ToString().Substring(7, 1) == "5")
                {
                    item.SubItems.Add((year - Convert.ToInt32(watingTable.Rows[i]["patientBirthCode"].ToString().Substring(0, 2)) - 1999).ToString());
                }
                item.SubItems.Add(watingTable.Rows[i]["receptionistName"].ToString());
                textBoxReceptionCount.Text = listView1.Items.Count.ToString();
                listView1.Items.Add(item);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
