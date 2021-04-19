using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Reserve
    {
        [FirestoreProperty]
        public string additionalContent { get; set; }
        [FirestoreProperty]
        public string department { get; set; }
        [FirestoreProperty]
        public string hospitalId { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string reservationDate { get; set; }
        [FirestoreProperty]
        public int reservationStatus { get; set; }
        [FirestoreProperty]
        public string reservationTime { get; set; }
        [FirestoreProperty]
        public long timestamp { get; set; }

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        FirestoreDb fs;
        public static int count;

        public List<Reserve> list = new List<Reserve>(); // 문의내역 리스트
        
        //Firestore 연결
        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }

        async public void ReserveOpen(string hospitalID)
        {
            list.Clear();
            Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    Reserve reserve = fp;

                    list.Add(reserve);
                }
            }
        }

        
    }
}
