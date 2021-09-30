using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class FindRadiography : Form
    {
        DBClass dbc = new DBClass();
        string chartNum;
        string patientName;
        string date;

        Image returnImage;
        Image newImage;

        public string ChartNum
        {
            get { return chartNum; }
            set { chartNum = value; }
        }
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public FindRadiography()
        {
            InitializeComponent();
        }

        // byte[] > image 변환
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = Image.FromStream(ms, true);
            }
            catch { }
            return returnImage;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FindRadiography_Load(object sender, EventArgs e)
        {
            textBoxChart.Text = chartNum;
            textBoxPatientName.Text = patientName;
            textBoxFileName.Text = date + "_"  + patientName;

            dbc.Image_Open(chartNum, date);
            dbc.ImageTable = dbc.DS.Tables["image"];

            byte[] imageByte = (byte[])dbc.ImageTable.Rows[0]["imageSource"];
            newImage = byteArrayToImage(imageByte);

            pictureBox1.Image = newImage;
        }

        // 저장 버튼
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(textBoxFileName.Text == "")
            {
                MessageBox.Show("파일명은 공백일 수 없습니다.", "알림");
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = @"바탕화면";
                saveFileDialog.Title = "의료 영상 저장 위치 지정";
                saveFileDialog.DefaultExt = "jpg";
                saveFileDialog.Filter = "jpg files(*.jpg)|*.jpg";
                saveFileDialog.FileName = textBoxFileName.Text;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    newImage.Save(fileName);
                    Dispose();
                }
                
            }
        }
    }
}
