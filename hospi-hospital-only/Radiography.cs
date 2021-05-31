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
    public partial class Radiography : Form
    {
        DBClass dbc = new DBClass();
        public Radiography()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dbc.Reception_Radiography(dateTimePicker1.Value.ToString());
            dbc.ReceptionTable = dbc.DS.Tables["reception"];

            MessageBox.Show(dbc.ReceptionTable.Rows.Count.ToString());
            /*for (int i = 0; i < dbc.ReceptionTable.Rows.Count; i++)
             {
                 ListViewItem item = new ListViewItem();

                 item.Text = (listView1.Items.Count + 1).ToString();
                 item.SubItems.Add(dbc.ReceptionistTable.Rows[i]["receptionTime"].ToString().Substring(0, 2) + ":" + dbc.ReceptionistTable.Rows[0]["receptionTime"].ToString().Substring(2, 2));
                 item.SubItems.Add(dbc.ReceptionistTable.Rows[i]["patientID"].ToString());
                 item.SubItems.Add(dbc.ReceptionistTable.Rows[i]["patientID"].ToString());
             }*/
        }
    }
}
