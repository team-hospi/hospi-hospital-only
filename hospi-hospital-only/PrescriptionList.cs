﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;


namespace hospi_hospital_only
{
    [FirestoreData]
    class PrescriptionList
    {
        [FirestoreProperty]
        string departemnt { get; set; }
        [FirestoreProperty]
        string hospitalId { get; set; }
        [FirestoreProperty]
        string hospitalName { get; set; }
        [FirestoreProperty]
        string id { get; set; }
        [FirestoreProperty]
        List<string> medicine { get; set; }
        [FirestoreProperty]
        string opinion { get; set; }
        [FirestoreProperty]
        long timestamp { get; set; }

        public static string DB_NAME = "prescriptionList";

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        public FirestoreDb fs;


        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }

        public void PrescriptionAdd(string department, string patientID, string opinion, List<string> Medicine)
        {
            CollectionReference coll = fs.Collection(DB_NAME);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"department", department },
                {"hospitalId", DBClass.hospiID},
                {"hospitalName", DBClass.hospiname },
                {"id", patientID },
                {"medicine", Medicine },
                {"opinion", opinion },
                {"timestamp", UnixTimeNow() }
            };
            coll.AddAsync(data1);
        }

        public long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));

            return (long)timeSpan.TotalSeconds;
        }
    }
}
