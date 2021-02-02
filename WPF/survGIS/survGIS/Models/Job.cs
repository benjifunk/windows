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
        private string sec, blk, lot;
        private string client;
        private bool active, r, f, o, i;
        private int est, rec1, rec2, total;


        public Job(int jobno, int town, string sec, string blk, string lot, string client, bool active = false, bool r = false, bool f=false, bool o = false, bool i = false, int est = 0, int rec1 = 0, int rec2 = 0, int tot = 0)
        {
            this.jobNo = jobno;
            this.town = (Muni)town;
            this.sec = sec;
            this.blk = blk;
            this.lot = lot;
            this.client = client;
            this.r = r;
            this.f = f;
            this.o = o;
            this.i = i;
            this.est = est;
            this.rec1 = rec1;
            this.rec2 = rec2;
            this.total = tot;
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
        public string Client
        {
            get { return client; }
            set
            {
                if (client != value)
                {
                    client = value;
                    OnPropertyChanged("Client");
                }
            }
        }
        public bool Active
        {
            get { return active; }
            set
            {
                if (active != value)
                {
                    active = value;
                    OnPropertyChanged("Active");
                }
            }
        }
        public bool Recon
        {
            get { return r; }
            set
            {
                if (r != value)
                {
                    r = value;
                    OnPropertyChanged("Recon");
                }
            }
        }
        public bool Field
        {
            get { return f; }
            set
            {
                if (f != value)
                {
                    f = value;
                    OnPropertyChanged("Field");
                }
            }
        }
        public bool Office
        {
            get { return o; }
            set
            {
                if (o != value)
                {
                    o = value;
                    OnPropertyChanged("Field");
                }
            }
        }
        public bool Invoice
        {
            get { return i; }
            set
            {
                if (i != value)
                {
                    i = value;
                    OnPropertyChanged("Invoice");
                }
            }
        }
        public int Est
        {
            get { return est; }
            set
            {
                if (est != value)
                {
                    est = value;
                    OnPropertyChanged("Est");
                }
            }
        }
        public int Deposit
        {
            get { return rec1; }
            set
            {
                if (rec1 != value)
                {
                    rec1 = value;
                    OnPropertyChanged("Rec1");
                }
            }
        }
        public int Final
        {
            get { return rec2; }
            set
            {
                if (rec2 != value)
                {
                    rec2 = value;
                    OnPropertyChanged("Rec2");
                }
            }
        }
        public int Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged("Total");
                }
            }
        }
        public static ObservableCollection<Job> Jobs
        {
            get
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
}
