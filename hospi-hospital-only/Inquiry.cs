using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Inquiry
    {
        [FirestoreProperty]
        public string answer { get; set; }
        [FirestoreProperty]
        public Boolean checkedAnswer { get; set; }
        [FirestoreProperty]
        public string content { get; set; }
        [FirestoreProperty]
        public string documentID { get; set; }
        [FirestoreProperty]
        public string hospitalID { get; set; }
        [FirestoreProperty]
        public string hospitalName { get; set; }
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public long timestamp { get; set; }
        [FirestoreProperty]
        public string title { get; set; }

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        FirestoreDb fs;
        public static int count;

        //Firestore 연결
        public void FireConnect()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @FBdir;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            fs = FirestoreDb.Create("hospi-edcf9");
        }


        public async void checkinquiry(string hospitalid)
        {
            int i = 0;
            Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalid);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Inquiry fp = docsnap.ConvertTo<Inquiry>();
                if (docsnap.Exists)
                {
                    if(fp.checkedAnswer == false)
                    {
                        i++;
                    }
                }
            }
            count = i;
        }
    }
}
