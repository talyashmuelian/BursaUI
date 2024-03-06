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
using System.Windows.Threading;
using BL;

namespace BursaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL bl = BLImp.theInstance();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            initDispatcher();
        }
        private void initDispatcher()
        {
            dispatcherTimer.Tick += GetListOfTradingPair;
            dispatcherTimer.Tick += UpdateTrades;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private async void UpdateTrades(object sender, EventArgs e)
        {
            try
            {
                await bl.SimulateTrades();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void GetListOfTradingPair(object sender, EventArgs e)
        {
            try
            {
                var data =await bl.GetListOfTradingPair();
                dataGrid.ItemsSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // here function for user can choose colomns by right click on table

        private void ToggleColumnVisibility_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                // Find the corresponding column by header
                var columnName = menuItem.Header.ToString();
                var column = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == columnName);
                if (column != null)
                {
                    column.Visibility = menuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
