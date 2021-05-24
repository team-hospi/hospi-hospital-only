﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Microsoft.Toolkit.Uwp.Notifications;

namespace hospi_hospital_only
{
    [FirestoreData]
    class ReceptionList
    {
        [FirestoreProperty]
        public string department { get; set; }
        [FirestoreProperty]
        public string doctor { get; set; }
        [FirestoreProperty]
        public string hospitalID { get; set; }
        [FirestoreProperty]
        public string hospitalName { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string office { get; set; }
        [FirestoreProperty]
        public string patient { get; set; }
        [FirestoreProperty]
        public string receptionDate { get; set; }
        [FirestoreProperty]
        public int status { get; set; }
        [FirestoreProperty]
        public int waitingNumber { get; set; }
        [FirestoreProperty]
        public string receptionTime { get; set; }



        public string today = DateTime.Now.ToString("yyyy-MM-dd");
        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        public FirestoreDb fs;
        public string documentName;

        public List<ReceptionList> list = new List<ReceptionList>();

        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }

        async public void TodayReceptionOpen(string hospitalId, string department)
        {
            Query qref = fs.Collection("receptionList").WhereEqualTo("hospitalId", hospitalId).WhereEqualTo("department", department).WhereEqualTo("receptionDate", today);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                ReceptionList fp = docsnap.ConvertTo<ReceptionList>();
                if (docsnap.Exists)
                {
                    ReceptionList receptionlist = fp;
                    list.Add(receptionlist);

                }
            }
        }
        //예약 접수
        public void ReceptionAccept(string depart, string id, string name, string Date,string Time, int number)
        {
            CollectionReference coll = fs.Collection("receptionList");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"department", depart },
                {"doctor","의사이름" },
                {"hospitalId", DBClass.hospiID },
                {"hospitalName", DBClass.hospiname },
                {"id", id },
                {"office", "진료1실" },
                {"patient", name },
                {"receptionDate", Date },
                {"receptionTime", Time },
                {"status", 0 },
                {"waitingNumber", number }
             };
            coll.AddAsync(data1);
        }

        async public void watingNumberUpdate(int waitingNumber)
        {
            DocumentReference docref = fs.Collection("receptionList").Document(documentName);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"waitingNumber", waitingNumber},
            };
            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.UpdateAsync(data);
            }
        }

        //문서 이름찾기
        async public void FindDocument(string hospitalId, string receptionDate, string receptionTime, string id, string department)
        {
            Query qref = fs.Collection("receptionList").WhereEqualTo("hospitalId", hospitalId).WhereEqualTo("receptionDate", receptionDate).WhereEqualTo("receptionTime", receptionTime).WhereEqualTo("id", id).WhereEqualTo("department", department);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                ReceptionList fp = docsnap.ConvertTo<ReceptionList>();
                if (docsnap.Exists)
                {
                    documentName = docsnap.Id;

                }
            }
        }


    }
}
