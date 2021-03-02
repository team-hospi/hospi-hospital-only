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
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reception reception = new Reception();
            reception.HospitalID = Convert.ToInt32(textBox1.Text);
            reception.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Office office = new Office();
            office.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
