using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    class FBKey
    {
        private string tempFolderPath;
        private string tempKeyFilePath;
        private string tempFileName;
        private DirectoryInfo di;

        public string TempFolderPath
        {
            get { return tempFolderPath; }
        }

        public string TempKeyFilePath
        {
            get { return tempKeyFilePath; }
        }

        public string TempFileName
        {
            get { return tempFileName; }
        }

        public FBKey()
        {
            try
            {
                tempFolderPath = Path.GetTempPath() + Path.GetRandomFileName();
                di = new DirectoryInfo(tempFolderPath);
                if (di.Exists == false) // 폴더가 존재하지 않으면
                {
                    di.Create(); // 폴더 생성
                }
                tempFileName = Path.GetRandomFileName();
                tempKeyFilePath = tempFolderPath + "\\" + tempFileName + ".json";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DownloadFile()
        {
            string fileName = "FBKey";

            DBClass dbc = new DBClass();
            dbc.GetDownloadURL(fileName);
            dbc.DownloadUrlTable = dbc.DS.Tables["downloadUrl"];
            string url = dbc.DownloadUrlTable.Rows[0][0].ToString();

            WebClient webClient = new WebClient();
            webClient.DownloadFile(url, tempKeyFilePath);
        }

        public void DeleteTemp()
        {
            di.Delete(true);
        }
    }
}
