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
        [FirestoreProperty]
        public string phone { get; set; }
        [FirestoreProperty]
        public string address { get; set; }
        [FirestoreProperty]
        public string birth { get; set; }
        [FirestoreProperty]
        public Dictionary<string, List<string>> reserveMap{ get; set; }
        [FirestoreProperty]
        public string token { get; set; }
        [FirestoreProperty]
        public string cancelComment { get; set; }

        private static string FBdir = "hospi-edcf9-firebase-adminsdk-e07jk-ddc733ff42.json";
        public FirestoreDb fs;
        public static int count;
        public string patientName;
        public string patientPhone;
        public string patientAddress;
        public string patientBirth;
        public static string documentName;
        public static string reserveDocument;
        public static string UserToken;
        public static string cancelcomment;

        public Dictionary<string, List<string>> reservemap;
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
                    patientPhone = fp.phone;
                    patientAddress = fp.address;
                    patientBirth = fp.birth;
                    UserToken = fp.token;
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
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    documentName = docsnap.Id;

                }
            }
        }

        //예약 시간 문서이름 찾기
        async public void FindReserveDocument(string hospitalID, string department)
        {
            Query qref = fs.Collection("reservedList").WhereEqualTo("hospitalId", hospitalID).WhereEqualTo("department", department);
            QuerySnapshot snap = await qref.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap)
            {
                Reserve fp = docsnap.ConvertTo<Reserve>();
                if (docsnap.Exists)
                {
                    reserveDocument = docsnap.Id;
                    reservemap = fp.reserveMap;
                }
            }
        }

        //예약 승인
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

        //예약 시간 추가
        async public void ReserveTimeAdd()
        {
            try
            {
                DocumentReference docref = fs.Collection("reservedList").Document(reserveDocument);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservedMap", reservemap }

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


        //예약 취소
        async public void ReserveCancel(string Comment)
        {
            try
            {
                DocumentReference docref = fs.Collection("reservationList").Document(documentName);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"reservationStatus", -1 },
                    {"cancelComment", Comment }

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

        public void RemoveReserveTime(string Date, string Time)
        {
            DocumentReference docref = fs.Collection("reservedList").Document(reserveDocument);
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"reservedMap."+Date, FieldValue.ArrayRemove(Time) }
            };
            docref.UpdateAsync(data);
        }

        public void ReserveUpdateWait(string hospitalid)
        {
            CollectionReference citiesRef = fs.Collection("reservationList");
            Query query = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalid).WhereEqualTo("reservationStatus", 0);

            FirestoreChangeListener listener = query.Listen(async snapshot =>
            {
                DateTime dt = DateTime.Now;
                long ss = Convert.ToInt64(dt.AddSeconds(-5).ToString("yyyyMMddHHmmss"));

                Query qref = fs.Collection("reservationList").WhereEqualTo("hospitalId", hospitalid);
                QuerySnapshot snap = await qref.GetSnapshotAsync();
                foreach (DocumentSnapshot docsnap in snap)
                {
                    Reserve fp = docsnap.ConvertTo<Reserve>();
                    if (docsnap.Exists)
                    {
                        if (fp.reservationStatus == 0 && Convert.ToInt64(ConvertDate(fp.timestamp).ToString("yyyyMMddHHmmss")) >= ss)
                        {
                            new ToastContentBuilder()
                                .AddArgument("action", "viewConversation")
                                .AddArgument("conversationId", 9813)
                                .AddText("HOSPI")
                                .AddText("새로운 예약 신청이 있습니다.")
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
