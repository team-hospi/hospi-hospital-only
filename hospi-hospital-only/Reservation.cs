using System;
using System.Collections;
using System.Windows.Forms;

namespace hospi_hospital_only
{
    public partial class Reservation : Form
    {
        DBClass dbc = new DBClass();
        Reserve reserve = new Reserve();

        string hospitalID;

        public string HospitalID
        {
            get { return hospitalID; }
            set { hospitalID = value; }
        }
        public Reservation()
        {
            InitializeComponent();
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            reserve.FireConnect();
            reserve.ReserveOpen(hospitalID);
            dbc.Delay(400);
            ReservationListUpdate();
        }

        //리스트뷰 업데이트
        public void ReservationListUpdate()
        {
            listView1.Items.Clear();
            for (int i = 0; i < reserve.list.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = reserve.list[i].id;
                item.SubItems.Add(ConvertDate(reserve.list[i].timestamp).ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(reserve.list[i].reservationDate);
                item.SubItems.Add(reserve.list[i].reservationTime);
                item.SubItems.Add(reserve.list[i].reservationStatus.ToString());
                item.SubItems.Add("이름");
                item.SubItems.Add(reserve.list[i].department);
                listView1.Items.Add(item);

                this.listView1.ListViewItemSorter = new ListviewItemComparer(1, "asc");
                listView1.Sort();
            }
        }

        //timestamp -> DateTime변형 함수
        public DateTime ConvertDate(long timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
            return dtDateTime;

        }

        //리스트뷰 정렬
        class ListviewItemComparer : IComparer
        {
            private int col;
            public string sort = "asc";
            public ListviewItemComparer()
            {
                col = 0;
            }

            public ListviewItemComparer(int column, string sort)
            {
                col = column;
                this.sort = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort == "asc")
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }

    }
}
