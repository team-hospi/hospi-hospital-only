using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    class DBClass
    {
        string connectionString = " User Id =" + id + "; Password =" + pass + "; Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = " + host + ")(PORT =" + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = xe )));";
        string commandString;
        OracleDataAdapter dBAdapter;
        DataSet dS;
        OracleCommandBuilder myCommandBuilder;
        DataTable hospitalTable, visitorTable, subjectTable, receptionTable, receptionistTable;

        private static string id = "hospi";
        private static string pass = "hospi";
        private static int port = 1522;
        private static string host = "localhost";

        // 프로퍼티
        public OracleDataAdapter DBAdapter
        {
            get { return dBAdapter; }
            set { dBAdapter = value; }
        }
        public OracleCommandBuilder MyCommandBuilder
        {
            get { return myCommandBuilder; }
            set { myCommandBuilder = value; }
        }
        public DataSet DS
        {
            get { return dS; }
            set { dS = value; }
        }
        public DataTable HospitalTable
        {
            get { return hospitalTable; }
            set { hospitalTable = value; }
        }
        public DataTable VisitorTable
        {
            get { return visitorTable; }
            set { visitorTable = value; }
        }
        public DataTable SubjectTable
        {
            get { return subjectTable; }
            set { subjectTable = value; }
        }
        public DataTable ReceptionTable
        {
            get { return receptionTable; }
            set { receptionTable = value; }
        }
        public DataTable ReceptionistTable
        {
            get { return receptionistTable; }
            set { receptionistTable = value; }
        }

        // 병원정보 조회용
        public void Hospital_Open(int hospitalID)
        {
            try
            {
                commandString = "select H.HOSPITALID, H.SUBJECTCOUNT, H.HOSPITALNAME, T.HOSPITALTYPENAME, H.HOSPITALADDRESS, H.HOSPITALTELL, H.OPENTIME, H.CLOSETIME, H.SUNDAYOPEN, H.WEEKENDOPENTIME, H.WEEKENDCLOSETIME, H.RESERVATION FROM HOSPITAL H, HOSPITALTYPE T WHERE H.HOSPITALTYPE = T.HOSPITALTYPE AND H.HOSPITALID = " + hospitalID;
               // commandString = "select * from hospital where HOSPITALID = " + hospitalID;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "hospital");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 병원정보 수정용 ( 조회용에서는 HospitalTypeName 속성을 HospitalType에서  조인해 오기 떄문에 수정이 불가함 )
        public void Hospital_Update(int hospitalID)
        {
            try
            {
                commandString = "select * from hospital where HOSPITALID = " + hospitalID;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "hospital");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 병원 과목정보 조회용
        public void Hopital_Subject(string query)
        {
            try
            {
                commandString = query;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "SubjectName");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 환자정보 
        public void Visitor_Open()
        {
            try
            {
                commandString = "select * from visitor";
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 초진환자 차트번호 조회용 ( 해당 병원코드의 환자수 +1로 차트번호 부여를 위함 )
        public void Visitor_Chart(int hospitalID)
        {
            try
            {
                commandString = "select count(*) from visitor where hospitalid = " + hospitalID;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 수진자 이름 조회용
        public void Visitor_Name(int hospitalID, string patientName)
        {
            try
            {
                commandString = "select * from visitor where patientName like '%"+patientName+"%' and hospitalID = "+hospitalID;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "visitor");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수 등록
        public void Reception_Open()
        {
            try
            {
                commandString = "select * from Reception";
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수 조회
        public void Reception_Update(int hospitalID, string receptionDate)
        {
            try
            {
                commandString = "select r.receptionTime, r.patientID, v.patientName, v.patientBirthCode, r.subjectName, r.receptionist from Reception r, Visitor v where r.patientID = v.patientID and r.hospitalID = "+hospitalID+" and r.receptionDate like '%"+receptionDate+"%'";
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 접수자 조회
        public void Receptionist_Open(int hospitalID)
        {
            try
            {
                commandString = "select * from Receptionist where HospitalID ="+hospitalID;
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Receptionist");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        // 수진자 이전 내원기록 조회
        public void Reception_Before(int hospitalID, string patientID)
        {
            try
            {
                commandString = "select receptionDate, receptionTime, subjectName, receptionInfo from reception where hospitalid = " + hospitalID + " and patientid = " + patientID +" order by receptionDate desc";
                DBAdapter = new OracleDataAdapter(commandString, connectionString);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);
                dS = new DataSet();
                DBAdapter.Fill(dS, "Reception");
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
    }
}
