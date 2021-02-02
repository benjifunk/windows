using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace survGIS
{
    class Job : INotifyPropertyChanged
    {
        private int jobNo;
        private Muni town;
        private string sec;
        private string blk;
        private string lot;


        public Job(int jobno, int town, string sec, string blk, string lot)
        {
            this.jobNo = jobno;
            this.town = (Muni)town;
            this.sec = sec;
            this.blk = blk;
            this.lot = lot;
        }

        public Job()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int JobNo
        {
            get { return jobNo; }
            set
            {
                if (jobNo != value)
                {
                    jobNo = value;
                    OnPropertyChanged("JobNo");
                }
            }
        }
        public Muni Town
        {
            get { return town; }
            set
            {
                if (town != value)
                {
                    town = value;
                    OnPropertyChanged("Town");
                }
            }
        }

        public string Sec
        {
            get { return sec; }
            set
            {
                if (sec != value)
                {
                    sec = value;
                    OnPropertyChanged("Sec");
                }
            }
        }

        public string Blk
        {
            get { return blk; }
            set
            {
                if (blk != value)
                {
                    blk = value;
                    OnPropertyChanged("Blk");
                }
            }
        }

        public string Lot
        {
            get { return lot; }
            set
            {
                if (lot != value)
                {
                    lot = value;
                    OnPropertyChanged("Lot");
                }
            }
        }

        public static ObservableCollection<Job> GetJobs()
        {
            var jobs = new ObservableCollection<Job>();
            DatabaseManager db = new DatabaseManager();
            db.Begin();
            jobs = db.getTable();

            //jobs.Add(new Job() { JobNo = 1, Town = Muni.Kinderhook, Sec = 4200, Blk = 1, Lot = 3214 });
            return jobs;
        }
    }
}
