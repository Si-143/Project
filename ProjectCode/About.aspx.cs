using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ProjectCode
{
    public partial class About : Page
    {
        static List<DateTime> sTimeList;
        static List<DateTime> eTimeList;
        static List<double> dur;



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }



        }
        protected void Page_Init(object source, System.EventArgs e)
        {

        }

        //protected void ChartExample_DataBound(object sender, EventArgs e)// no data 
        //{
        //    // If there is no data in the series, show a text annotation
        //    if (TestChart.Series[0].Points.Count == 0)
        //    {
        //        System.Web.UI.DataVisualization.Charting.TextAnnotation annotation =
        //            new System.Web.UI.DataVisualization.Charting.TextAnnotation();
        //        annotation.Text = "No data for this period";
        //        annotation.X = 5;
        //        annotation.Y = 5;
        //        annotation.Font = new System.Drawing.Font("Arial", 12);
        //        annotation.ForeColor = System.Drawing.Color.Red;
        //        TestChart.Annotations.Add(annotation);
        //    }


        //}
        public string selectFromDatabase()
        {

            SqlConnection connect = new SqlConnection("Data Source=SQL-SERVER;Initial Catalog=ts348;Integrated Security=True");

            string myQuery = "SELECT TaskDes, PLevel FROM Project_Table";
            SqlCommand myCommand = new SqlCommand(myQuery, connect);

            try
            {
                connect.Open();
                //SqlDataReader read = myCommand.ExecuteReader();
                string A = "hello";


                Chart1.Series[A].XValueMember = "ProductName";
                Chart1.Series["Series1"].YValueMembers = "UnitsInStock";
                Chart1.DataBind();
                return TestBox.Text = A;

            }

            finally
            {
                connect.Close();

            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            selectData();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ShortJob(dur);
        }



        public void selectData()// choosing project based on what the user have searched for. 
        {
            SqlConnection connect = new SqlConnection("Data Source=SQL-SERVER;Initial Catalog=ts348;Integrated Security=True");

            string myQuery = "SELECT TaskDes, PLevel, StartTime, ENDTime, Task FROM Project_Table";
            SqlCommand myCommand = new SqlCommand(myQuery, connect);
            sTimeList = new List<DateTime>();
            eTimeList = new List<DateTime>();
            dur = new List<double>();

            try
            {
                connect.Open();
                SqlDataReader myRead = myCommand.ExecuteReader();
                Series chart = Chart1.Series["Series1"];
                while (myRead.Read())
                {


                    DateTime STime = Convert.ToDateTime(myRead["StartTime"]);
                    DateTime ETime = Convert.ToDateTime(myRead["ENDTime"]);

                    TimeSpan duration = ETime - STime;
                    // need to fix
                    // double days = ETime.Subtract(DateTime.Today).TotalDays;


                    sTimeList.Add(STime);
                    eTimeList.Add(ETime);
                    
                    if (DropDownList1.SelectedItem.Text == "Week")
                    {

                        // plot the task on the chart 
                        chart.Points.AddXY(myRead["Task"].ToString(), myRead["StartTime"], myRead["ENDTime"]);
                       
                        
                        
                        chart.Label = myRead["TaskDes"].ToString();
                       
                        //  calculate the duration of each of the task. 
                        double D = duration.TotalDays;// measure in days 
                        dur.Add(D);




                        //  set the max and min of the Y axis. 
                        DateTime max = DateTime.Now.AddDays(+7);
                        DateTime min = DateTime.Now;


                        Chart1.ChartAreas[0].AxisY.Minimum = min.ToOADate();
                        Chart1.ChartAreas[0].AxisY.Maximum = max.ToOADate();

                    }

                    else if (DropDownList1.SelectedItem.Text == "Month")
                    {

                        chart.Points.AddXY(myRead["Task"].ToString(), myRead["StartTime"], myRead["ENDTime"]);

                        double D = duration.TotalDays;

                        dur.Add(D);

                        DateTime max = DateTime.Now.AddMonths(+1);
                        DateTime min = DateTime.Now;
                        //DateTime current = max;
                        Chart1.ChartAreas[0].AxisY.Minimum = min.ToOADate();
                        Chart1.ChartAreas[0].AxisY.Maximum = max.ToOADate();


                    }

                }


            }

            finally
            {
                CritPath(dur);
                latestTime(dur);
              
                connect.Close();

            }

        }
        public void CritPath(List<double> dur)
        {


            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Start", typeof(Double)));
            dt.Columns.Add(new DataColumn("Finish", typeof(Double)));
            dt.Columns.Add(new DataColumn("Late Start", typeof(Double)));
            dt.Columns.Add(new DataColumn("Late Finish", typeof(Double)));
         
            double EFT = 0;
            int S = 1;

            for (int i = 0; i < dur.Count; i++)
            {

                // calulating th EFT (early finish time)

                double EST = S;
                EST = EFT + 1;// early start time
                EFT = dur[i] + EST - 1; // early finish time

            
               
                //  create the row 
               
                DataRow dr = dt.NewRow();
                dr["Start"] = EST;
                dr["Finish"] = EFT;
                dt.Rows.Add(dr);
                TimeGrid.DataSource = dt;
                TimeGrid.DataBind();
            

                // note use this to calulate http://www.tutorialspoint.com/operating_system/os_process_scheduling_algorithms.htm


            }
            //double totalDur = dur.Sum();
            //dur.Reverse();

            //for (int i = 0; i < dur.Count; i++)
            //{
            //    double active = dur[i];
            //    double LS = totalDur - active - 1;
            //    double LF = LS - active + 1;
            //    totalDur = totalDur - active;
            //    DataRow dr = dt.NewRow();
            //    dr["Late Start"] = LS;
            //    dr["Late Finish"] = LF;
            //    dt.Rows.Add(dr);
            //    TimeGrid.DataSource = dt;
            //    TimeGrid.DataBind();
            //}




        }

        private void latestTime(List<double> dur)
        {
            //List<double> dur = new List<double>();

            double totalDur = dur.Sum();
            dur.Reverse();

            for (int i = 0; i < dur.Count; i++)
            {
                double active = dur[i];
                double LS = totalDur - active - 1;
                double LF = LS - active + 1;
                totalDur = totalDur - active;

            }

        }
        private void ShortJob( List<double> dur)
        {
            List<double> shortJ = new List<double>();
            //int sPoint = 1;
            dur.Sort();



            //foreach (var series in Chart1.Series)
            //{
            //    series.Points.Clear();
            //}
            ////shortJ.Add(dur[sPoint]);

            //for (int i = 0; i < dur.Count; i++)
            //{
            //    Series chart = Chart1.Series["Series1"];
            //    chart.Points.AddXY("test", dur[i], dur[i]);


                //if (dur[i] < dur[sPoint])
                //{

                //    sPoint = i;
                //    shortJ.Insert(0, dur[i]);

                //}



            //}


        }
    }
}
