using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace survGIS
{
    /// <summary>
    /// Interaction logic for JobView.xaml
    /// </summary>
    public partial class JobView : UserControl
    {
        ObservableCollection<Job> localResults;

        public JobView()
        {
            InitializeComponent();
            jobDataGrid.ItemsSource = Job.Jobs;
            DataContext = new JobViewModel();
        }

        private void jobDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchJobs()
        {
            var results = new ObservableCollection<Job>();
            bool add = false;

            try
            {
                foreach (Job job in Job.Jobs)
                {
                    if (SecTextBox.Text.Length > 0)
                    {
                        if (SecTextBox.IsFocused)
                        {
                            if (job.Sec.Contains(SecTextBox.Text))
                            {
                                if (BlkTextBox.Text.Length > 0)
                                {
                                    if (job.Blk.Contains(BlkTextBox.Text))
                                    {
                                        if (LotTextBox.Text.Length > 0)
                                        {
                                            if (job.Lot.Contains(LotTextBox.Text))
                                            {
                                                add = true;
                                            }
                                        }
                                        else add = true;
                                    }
                                }
                                else add = true;
                            }
                        }
                        else
                        {
                            if (job.Sec.Equals(SecTextBox.Text))
                            {
                                if (BlkTextBox.Text.Length > 0)
                                {
                                    if (job.Blk.Contains(BlkTextBox.Text))
                                    {
                                        if (LotTextBox.Text.Length > 0)
                                        {
                                            if (job.Lot.Contains(LotTextBox.Text))
                                            {
                                                add = true;
                                            }
                                        }
                                        else add = true;
                                    }
                                }
                                else add = true;
                            }
                        }
                    }
                    else add = true;
                    if (OwnTextBox.Text.Length > 0)
                        if (job.Client.ToUpper().Contains(OwnTextBox.Text.ToUpper()))
                            add = true;
                        else
                            add = false;
                    if (add) results.Add(job);
                    add = false;
                }
                jobDataGrid.ItemsSource = results;
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid search terms.");
                throw;
            }

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchJobs();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SecTextBox.Text = null;
            BlkTextBox.Text = null;
            LotTextBox.Text = null;
        }
    }
}

