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
    public partial class Hospital_SignUp : Form
    {
        int listBoxIndex;
        int iDCheck; // 디폴트0 , 중복되지 않을경우1

        public Hospital_SignUp()
        {
            InitializeComponent();
        }

        private void buttonSubjectAdd_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBoxEtc.Text);
            textBoxEtc.Clear();
        }
        
        // 기타과 삭제버튼
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBoxIndex);
        }

        // 기타과 리스트박스 클리
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxIndex = listBox1.SelectedIndex;
        }

        private void Hospital_SignUp_Load(object sender, EventArgs e)
        {
            this.ActiveControl = textBoxHospitalID;
        }
        
        // PW1
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPw1.Text.Length >= 4)
            {
                pwLabel1.Text = "✓";
                pwLabel1.ForeColor = Color.Green;
            }
            else if (textBoxPw1.Text.Length < 4)
            {
                pwLabel1.Text = "X";
                pwLabel1.ForeColor = Color.Red;
            }

            if(textBoxPw1.Text != textBoxPw2.Text)
            {
                pwLabel2.Text = "X";
                pwLabel2.ForeColor = Color.Red;
            }
            else if(textBoxPw1.Text == textBoxPw2.Text)
            {
                pwLabel2.Text = "✓";
                pwLabel2.ForeColor = Color.Green;
            }
        }

        //PW2
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPw2.Text == textBoxPw1.Text && pwLabel1.Text == "✓")
            {
                pwLabel2.Text = "✓";
                pwLabel2.ForeColor = Color.Green;
            }
            else if (textBoxPw2.Text != textBoxPw1.Text || pwLabel1.Text == "X")
            {
                pwLabel2.Text = "X";
                pwLabel2.ForeColor = Color.Red;
            }
        }

        // 등록완료 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            // iDCheck 값이 중복확인후 1로 변경되어야 진행
            if(iDCheck==1 && pwLabel1.Text == "✓" && pwLabel2.Text == "✓")
            {
                if(textBoxHospitalName.Text != "" && comboBox17.Text != "" && textBoxTell1.Text != "" && textBoxTell2.Text != "" && textBoxTell3.Text != "" && textBoxHospitalAddress.Text != "") 
                {
                    if(comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && comboBox4.Text != "" && comboBox6.Text != "" && comboBox7.Text != "" && comboBox8.Text != "" && comboBox9.Text != "")
                    {
                        if(comboBox15.Text != "" && comboBox16.Text != "" & comboBox11.Text != "" && comboBox12.Text != "" && comboBox13.Text != "" && comboBox14.Text != "" && comboBox5.Text != "" && comboBox10.Text != "")
                        {
                            
                        }
                    }
                }
            }
        }

        // ID 중복확인 버튼
        private void button4_Click(object sender, EventArgs e)
        {
            // DB 조회후 중복되지 않을경우 iDCheck 값 1로 변경
        }
    }
}
