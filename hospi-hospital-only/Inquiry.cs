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
        public string hospital_id { get; set; }
        [FirestoreProperty]
        public string hospital_name { get; set; }
        [FirestoreProperty]
        public long timestamp { get; set; }
        [FirestoreProperty]
        public string title { get; set; }

        FirestoreDb fs;

        public static List<Inquiry> list = new List<Inquiry>();
        //문의 받아오기
        public async void Inquiry_Open(string hospitalID)
        {
            Query qref = fs.Collection("inquiry_list").WhereEqualTo("hospital_id", hospitalID);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Dictionary<string, object> inquiry = docsnap.ToDictionary();
                foreach(KeyValuePair<string, object> pair in inquiry)
                {

                }

            }
        }
    }
}
