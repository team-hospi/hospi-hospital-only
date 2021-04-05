using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace hospi_hospital_only
{
    [FirestoreData]
    class Fcm
    {
        [FirestoreProperty]
        public string token { get; set; }

        FirestoreDb fs;
        public static string Title;
        public static string UserId;



        public void PushNotificationToFCM(string title, string UserToken)
        {
            try
            {
                var applicationID = "AAAAB_lm5NU:APA91bFykKZMyRXlkHYeolFihouf_0EnC5U3yVlIwrvhSB-bDuVAMnfOwBKx2KYxPMRkUjqNAo8Z_s_ex8yqBB_O7WpfErr5_88vI-WxX7UC8yXFEQv3PITbF2dTwQEGgLKbqLfSyYTx";
                var senderId = "34249041109";
                string deviceId = UserToken;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = "["+ title + "]에 대한 답변이 완료되었습니다.",
                        title = "알림"
                    }
                   
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
