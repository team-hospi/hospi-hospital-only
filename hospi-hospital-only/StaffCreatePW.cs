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
    public partial class StaffCreatePW : Form
    {
        string pw;
        string createYn = "N";
        string staffID;

        DBClass dbc = new DBClass();

        public string Pw
        {
            get { return pw; }
            set { pw = value; }
        }
        public string CreateYn
        {
            get { return createYn; }
            set { createYn = value; }
        }
        public string StaffID
        {
            get { return staffID; }
            set { staffID = value; }
        }

        public StaffCreatePW()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            dbc.Staff_open();
            dbc.StaffTable = dbc.DS.Tables["staff"];

            for(int i=0; i<dbc.StaffTable.Rows.Count; i++)
            {
                if(dbc.StaffTable.Rows[i]["staffID"].ToString() == staffID)
                {
                    DataRow upRow = null;
                    upRow = dbc.StaffTable.Rows[i];
                    upRow.BeginEdit();
                    upRow["staffPW"] = textBoxPW2.Text;
                    upRow.EndEdit();
                    dbc.DBAdapter.Update(dbc.DS, "staff");
                    dbc.DS.AcceptChanges();
                    
                    createYn = "Y";
                    Dispose();
                }
            }
        }

        private void StaffCreatePW_Load(object sender, EventArgs e)
        {
        }
    }
}
