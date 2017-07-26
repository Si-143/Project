using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class _Default : Page
    {

        static List<DateTime> sTimeList = new List<DateTime>();
        static List<DateTime> eTimeList = new List<DateTime>();
        static List<double> dur = new List<double>();
        static List<double> dur2 = new List<double>();
        static double different = 0;
        Task newTask = new Task();
        static List<Task> taskList = new List<Task>();



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
        //public string selectFromDatabase()
        //{

        //    SqlConnection connect = new SqlConnection("Data Source=SQL-SERVER;Initial Catalog=ts348;Integrated Security=True");

        //    string myQuery = "SELECT TaskDes, PLevel FROM Project_Table";
        //    SqlCommand myCommand = new SqlCommand(myQuery, connect);

        //    try
        //    {
        //        connect.Open();
        //        //SqlDataReader read = myCommand.ExecuteReader();
        //        string A = "hello";


        //        Chart1.Series[A].XValueMember = "ProductName";
        //        Chart1.Series["Series1"].YValueMembers = "UnitsInStock";
        //        Chart1.DataBind();
        //        return TestBox.Text = A;

        //    }

        //    finally
        //    {
        //        connect.Close();

        //    }


        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            selectData();
        }

        //protected void Button2_Click1(object sender, EventArgs e)
        //    {
        //        ShortJob(dur,sTimeList);
        //    }



        public void selectData()// choosing project based on what the user have searched for. 
        {
            using (SqlConnection connect = new SqlConnection("Data Source=SQL-SERVER;Initial Catalog=ts348;Integrated Security=True"))
            {
                string myQuery = "SELECT TaskDes, PLevel, StartTime, ENDTime, Task, Path FROM Project_Table";
                SqlCommand myCommand = new SqlCommand(myQuery, connect);
                try
                {
                    connect.Open();
                    SqlDataReader myRead = myCommand.ExecuteReader();
                    Series chart = Chart1.Series["Series1"];
                    while (myRead.Read())
                    {

                        DateTime STime = Convert.ToDateTime(myRead["StartTime"]);
                        DateTime ETime = Convert.ToDateTime(myRead["ENDTime"]);
                        // need more work -------- zzzzzz 
                        different = (ETime - DateTime.Today).TotalDays;

                        int P = Convert.ToInt16(myRead["Plevel"]);

                        TimeSpan duration = ETime - STime;


                        // testing using a list class to hold more than one values, making it easier to manage the task infomation. 
                        Task TaskInfo = new Task();
                        TaskInfo.Name = myRead["Task"].ToString();
                        TaskInfo.ST = STime;
                        TaskInfo.ET = ETime;
                        TaskInfo.D = duration.TotalDays;
                        TaskInfo.P = P;
                        string Pa = myRead["Path"].ToString();

                        if (Pa.Length != 0)
                        {
                            TaskInfo.Path = Pa;
                        }

                        // need to fix
                        // double days = ETime.Subtract(DateTime.Today).TotalDays;

                        taskList.Add(TaskInfo);

                        sTimeList.Add(STime);
                        eTimeList.Add(ETime);

                        if (DropDownList1.SelectedItem.Text == "Week")
                        {

                            // plot the task on the chart 
                            chart.Points.AddXY(myRead["Task"].ToString(), myRead["StartTime"], myRead["ENDTime"]);



                            chart.Label = myRead["TaskDes"].ToString();

                            //  calculate the duration of each of the task. 
                            double Dur = duration.TotalDays;// measure in days 
                            dur.Add(Dur);




                            //  set the max and min of the Y axis. 
                            DateTime max = DateTime.Now.AddDays(+7);
                            DateTime min = DateTime.Now;


                            Chart1.ChartAreas[0].AxisY.Minimum = min.ToOADate();
                            Chart1.ChartAreas[0].AxisY.Maximum = max.ToOADate();

                        }

                        else if (DropDownList1.SelectedItem.Text == "Month")
                        {

                            chart.Points.AddXY(myRead["Task"].ToString(), myRead["StartTime"], myRead["ENDTime"]);

                            double Dur = duration.TotalDays;

                            dur.Add(Dur);

                            DateTime max = DateTime.Now.AddMonths(+1);
                            DateTime min = DateTime.Now;
                            //DateTime current = max;
                            Chart1.ChartAreas[0].AxisY.Minimum = min.ToOADate();
                            Chart1.ChartAreas[0].AxisY.Maximum = max.ToOADate();


                        }

                    }
                    CritPath();

                }

                finally
                {

                    connect.Close();

                }

            }
        }
        public void CritPath()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Start", typeof(Double)));
            dt.Columns.Add(new DataColumn("Finish", typeof(Double)));
            dt.Columns.Add(new DataColumn("Late Start", typeof(Double)));
            dt.Columns.Add(new DataColumn("Late Finish", typeof(Double)));


            double EFT = 0;
            double q = 0;
            double K = 0;
            double EST = 0;
            int i = 0;
            string Check = "";
            double checkD = 0;
            //double totalDur = 0;

            var duplicates = taskList.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();

            foreach (var dup in duplicates)
            {

                string name = dup.Name;


            }

            var result = from em in taskList
                         where em.Path != null && em.Path.ToString() != ""      
                         //orderby em.D ascending
                         select em;


            //foreach (var T in result)
            //{
            //    different = (T.ET - DateTime.Today).TotalDays;
            //    //double F = K + 1;// early start time
            //    //K = T.D + F - 1; // ear  
            //    //q = EFT;


            //}
            foreach (var D in result)
            {
                //if (D.Path.Contains(Check))
                //{
                //    if ()


                //}
                //else
                //{
                //    Check = D.Path;
                //    double checkD = D.D;
                //    double t = D.D;
                //    dur2.Add(t);


                //}
               

            }
          
            
            dur.Reverse();
            double t = 0;
            //Loop// usong
            // check of dupicates 
            foreach (var test in result)
            {
                    if (test.Path.Equals(Check))
                         {
                             if (test.D < checkD)
                             {

                                 dur2.Remove(dur2.Last());
                                 dur2.Add(test.D);

                                 Check = test.Path;
                                 t = test.D;

                             }
                             else { continue; }
                }
                else
                {
                    Check = test.Path;
                    checkD = test.D;
                    t = test.D;
                    dur2.Add(t);


                }
            }
                    foreach (var Test2 in result)
                    {

                        string s = Test2.Path;

                        EST = EFT + 1;// early start time
                        EFT = Test2.D + EST - 1; // ear       


                        // uses a list, not the object class 
                        //totalDur = totalDur + totalDur;
                        double active = dur2[i];
                        double LS = different - active - 1;
                        double LF = LS - active + 1;
                        different = different - active;

                        DataRow dr = dt.NewRow();
                        dr["Start"] = EST;
                        dr["Finish"] = EFT;
                        dr["Late Start"] = LS;
                        dr["Late Finish"] = LF;

                        dt.Rows.Add(dr);
                        TimeGrid.DataSource = dt;
                        TimeGrid.DataBind();

                    }
            }  
    

            //result.Reverse();

            //for (int i = 0; i < result.Count(); i++)
            //{
            //    double active = dur[i];
            //    double LS = totalDur - active - 1;
            //    double LF = LS - active + 1;
            //    totalDur = totalDur - active;
                

             
            //}

      
        //old code

        //for (int i = 0; i < dur.Count; i++)
        //{
        //    // calulating th EFT (early finish time)
        //    double EST = S;
        //    EST = EFT + 1;// early start time
        //    EFT = dur[i] + EST - 1; // early finish time

        //    //  create the row 






        //}
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
        private void ShortJob()
        {
            //List<double> shortJ = new List<double>();
            //int sPoint = 1;
            //dur.Sort();

            clearChart();
            //shortJ.Add(dur[sPoint]);
            int i = 0;
            var result = from em in taskList
                         orderby em.D ascending
                         select em;

            int size = taskList.Count();

            foreach (var Task in result)
            {
                DateTime end = sTimeList[i].AddDays(Task.D);
                string name = Task.Name;


                Series chart = Chart1.Series["Series1"];
                chart.Points.AddXY(name, sTimeList[i], end);

                i++;
            }




        }
        //for (int i = 0; i < size; )
        //{
        //    DateTime end = sTimeList[i].AddDays(dur[i]);
        //    string test = ;


        //    Series chart = Chart1.Series["Series1"];
        //    chart.Points.AddXY(test, sTimeList[i], end);
        //    i++;
        //}


        //if (dur[i] < dur[sPoint])
        //{

        //    sPoint = i;
        //    shortJ.Insert(0, dur[i]);

        //}



        //}



        private void PLevelHighest()
        {
            int i = 0;
            //Linq
            var result = from em in taskList
                         orderby em.P ascending
                         select em;

            foreach (var Task in result)
            {
                DateTime end = sTimeList[i].AddDays(Task.D);
                string name = Task.Name;


                Series chart = Chart1.Series["Series1"];
                chart.Points.AddXY(name, sTimeList[i], end);

                i++;

            }

        }

        protected void arrange_Click(object sender, EventArgs e)
        {
            //var Plist = taskList.OrderBy(a => a.P);
            // ShortJob(dur, sTimeList);//  find the shortest job. 
            adaptive();


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //CritPath();
            //latestTime(dur);

            ShortJob();
        }
        private void clearChart()
        {

            foreach (var series in Chart1.Series)
            {
                series.Points.Clear();
            }
        }

        private void adaptive() // adaptive 
        {// break time finish later
            int sumP = taskList.Sum(item => item.P);
            double D = taskList.Sum(info => info.D);
            int count = taskList.Count;

            if (sumP > count)
            {
                // shortest job first 



            }
            else if (count > different)
            {
                // priority first


            }
            else
            {

                // first come
            }

        }
        //private void dup()
        //{

        //    var query = (from s in taskList
        //                 group s by new { s.Path }
        //                     into g
        //                     where g.Count() > 1
        //                     select g).SelectMany(g => g);
        //}

    }
}
