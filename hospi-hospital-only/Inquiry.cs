using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Toolkit.Uwp.Notifications;

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

        public void UpdateWait(string hospitalid)
        {
            CollectionReference citiesRef = fs.Collection("inquiryList");
            Query query = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalid).WhereEqualTo("checkedAnswer", false);

            
            /*if (ss < 0)
            {
                mm = mm - 1;
                if (mm < 0)
                {
                    hh = hh - 1;
                    mm = mm + 60;
                }
                ss = ss + 60;
            }*/
            //int timenow = Convert.ToInt32(dt.ToString("ddHHmm"+ ss));

            FirestoreChangeListener listener = query.Listen(async snapshot =>
            {
                DateTime dt = DateTime.Now;
                long ss = Convert.ToInt64(dt.AddSeconds(-1).ToString("yyyyMMddHHmmss"));

                Query qref = fs.Collection("inquiryList").WhereEqualTo("hospitalId", hospitalid);
                    QuerySnapshot snap = await qref.GetSnapshotAsync();
                    foreach (DocumentSnapshot docsnap in snap)
                    {
                        Inquiry fp = docsnap.ConvertTo<Inquiry>();
                        if (docsnap.Exists)
                        {
                            if (fp.checkedAnswer == false && Convert.ToInt64(ConvertDate(fp.timestamp).ToString("yyyyMMddHHmmss")) >= ss)
                            {
                                new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText("HOSPI")
                                    .AddText("새로운 문의가 등록 되었습니다!!")
                                    .Show();
                            }
                        }
                    }
                
            });
        }

        public DateTime ConvertDate(long timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
            return dtDateTime;

        }
    }
}
