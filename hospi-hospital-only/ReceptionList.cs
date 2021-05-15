using System;
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


        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        public FirestoreDb fs;

        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }

        //예약 접수
        public void ReceptionAccept(string depart, string id, string name, string Date, int number)
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
                {"status", 0 },
                {"waitingNumber", number }
             };
            coll.AddAsync(data1);
        }


    }
}
