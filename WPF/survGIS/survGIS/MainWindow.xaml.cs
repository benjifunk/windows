using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private JobView jobView;
        private MapperView mapperView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            jobView = new JobView();
            mapperView = new MapperView();
            

            updateButton.Click += mapperView.UpdateExtents;
            jobView.jobDataGrid.SelectionChanged += mapperView.SelectionChanged;

        }

        public JobView JobView { get { return this.jobView; } }
        public MapperView MapperView { get { return this.mapperView; } }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
    public enum Muni
    {
        Austerlitz,
        Chatham,
        ChathamVillage,
        Claverack,
        Ghent,
        GhentVillage,
        Hudson,
        Kinderhook,
        KinderhookVillage,
        NewLeabanon,
        Stockport,
        Stuyvesant,
        Valatie
    }

}
