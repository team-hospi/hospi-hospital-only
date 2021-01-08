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
    public partial class Office : Form
    {
        public Office()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Office_Load(object sender, EventArgs e)
        {
            // 로드시 포커스
            this.ActiveControl = button7;

            // 처방일 라벨
            label8.Text = "처방일 : "+ DateTime.Now.ToString("yyyy-MM-dd");

            // 리스트뷰 속성
            listView1.GridLines = true;
            listView2.GridLines = true;
            listView3.GridLines = true;
            listView1.FullRowSelect = true;
            listView2.FullRowSelect = true;
            listView3.FullRowSelect = true;
        }
    }
}
