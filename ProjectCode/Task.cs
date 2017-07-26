using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using System.Web;

namespace ProjectCode
{
     class Task 
        
    {
        public Task(){ }
        public Task(string name,string path, DateTime st,DateTime et, double d,int p)
        {

            this.Name = name;
        }

        private string name;
        public string Name
        {

       get { return name;}
        set {name = value;}
         }


        private DateTime st;
        public DateTime ST{
            get {return st;}
            set {st = value;}

        }
        private DateTime et;

        public DateTime ET
        {
            get { return et; }
            set { et = value; }
        }
        private double d;

        public double D
        {
            get { return d; }
            set { d = value; }
        }
        private int p;

        public int P
        {
            get { return p; }
            set { p = value; }
        }
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        } 



        //public int CompareTo(Task other)
        //{
        //    return Name.CompareTo(other.Name);
        //}
    }
}