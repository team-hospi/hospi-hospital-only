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
        string hospitalID;

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public Main2()
        {
            dbc.FireConnect();
            InitializeComponent();
        }

        private void Main2_Load(object sender, EventArgs e)
        {
            dbc.Hospital_Open(hospitalID);
            dbc.Delay(200);
            for(int i=0; i<DBClass.hospidepartment.Length; i++)
            {
                comboBoxSub.Items.Add(DBClass.hospidepartment[i]);
            }
            comboBoxSub.Text = DBClass.hospidepartment[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = comboBoxSub.SelectedIndex;
            Office office = new Office();
            this.Visible = false;
            office.SubjectID = comboBoxSub.Text;
            office.ShowDialog();
            this.Dispose();
        }

        private void settingLabel_Click(object sender, EventArgs e)
        {
            UpdateSubject updateSubject = new UpdateSubject();
            updateSubject.ShowDialog();
        }
    }
}
