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

namespace RML_Paging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM)DataContext).LoadData();
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            IProgress<int> progress = new Progress<int>(completed => {
                GenerationProgress.Value = completed;
            });
            ProgressPanel.Visibility = Visibility.Visible;
            await ((MainWindowVM)DataContext).Generate(progress);
            ProgressPanel.Visibility = Visibility.Collapsed;
        }

        private async void BulkInsert_Click(object sender, RoutedEventArgs e)
        {
            IProgress<int> progress = new Progress<int>(completed => {
                GenerationProgress.Value = completed;
            });
            ProgressPanel.Visibility = Visibility.Visible;
            await ((MainWindowVM)DataContext).Bulk(progress);
            ProgressPanel.Visibility = Visibility.Collapsed;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM)DataContext).PreviousPage();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM)DataContext).NextPage();
        }

        private void Elejen_Checked(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM)DataContext).IsContainsFilter = false;
        }

        private void Barhol_Checked(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM)DataContext).IsContainsFilter = true;
        }
    }
}
