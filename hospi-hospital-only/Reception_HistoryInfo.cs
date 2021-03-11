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
    public partial class Reception_HistoryInfo : Form
    {
        string receptionInfo; // 내원목적

        public Reception_HistoryInfo()
        {
            InitializeComponent();
        }

        public string ReceptionInfo
        {
            get { return receptionInfo; }
            set { receptionInfo = value; }
        }

        private void Reception_HistoryInfo_Load(object sender, EventArgs e)
        {
            textBox1.Text = receptionInfo;
        }

        // 종료 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            receptionInfo = "";
            Dispose();
        }

        // 내원목적 붙히기 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            receptionInfo = textBox1.Text;
            Dispose();
        }
    }
}
