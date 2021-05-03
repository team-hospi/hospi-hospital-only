using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        [FirestoreProperty]
        public string name { get; set; }

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        public FirestoreDb fs;
        public static int count;
        public string patientName;
        public string documentName;

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

        async public void FindPatient(string id)
        {
            Query qref = fs.Collection("userList").WhereEqualTo("email", id);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    patientName = fp.name;
                }
            }
        }

        //문서 이름찾기
        async public void FindDocument(string hospitalID, string time, string id, string Date)
        {
            Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalID).WhereEqualTo("reservationTime", time).WhereEqualTo("id", id).WhereEqualTo("reservationDate", Date);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Inquiry fp = docsnap.ConvertTo<Inquiry>();
                if (docsnap.Exists)
                {
                    documentName = docsnap.Id;

                }
            }
        }

        async public void ReserveAccept()
        {
            try
            {
                DocumentReference docref = fs.Collection("reservationList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservationStatus", 1 }

                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
