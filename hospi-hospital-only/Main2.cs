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
    public partial class Main2 : Form
    {
        DBClass dbc = new DBClass();
        public Main2()
        {
            InitializeComponent();
        }

        private void Main2_Load(object sender, EventArgs e)
        {
            dbc.Subject_Open();
            dbc.SubjectTable = dbc.DS.Tables["subjectName"];
            for(int i=0; i<dbc.SubjectTable.Rows.Count; i++)
            {
                comboBoxSub.Items.Add(dbc.SubjectTable.Rows[i]["subjectName"]);
            }
            comboBoxSub.Text = dbc.SubjectTable.Rows[0]["subjectName"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = comboBoxSub.SelectedIndex;
            Office office = new Office();
            this.Visible = false;
            office.SubjectID = a+1;
            office.ShowDialog();
            this.Dispose();
        }
    }
}
