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
    public partial class NoticeInfo : Form
    {
        DBClass dbc = new DBClass();
        string a;

        public string A
        {
            get { return a; }
            set { a = value; }
        }

        public NoticeInfo()
        {
            InitializeComponent();
        }

        private void NoticeInfo_Load(object sender, EventArgs e)
        {
            label1.Text = a;
        }
    }
}
