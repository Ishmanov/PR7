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
using PR7.Pages;

namespace PR7
{

    public class GoodItem
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double Count { get; set; }
    }

    public class ItemsMovements
    {
        public int OperationNumber { get; set; }
        public string Article { get; set; }
        public string OperationType { get; set; }
        public double Count { get; set; }
        public string Reason { get; set; }
    }
    public class SupplierOrder
    {
        public int OrderNumber { get; set; }
        public string Article { get; set; }
        public string Name { get; set; } 
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string UnitOfMeasurement { get; set; } 
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Page1Open_Button(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new GoodsAccounting(); 
        }
        private void Page2Open_Button(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new OrdersToSuppliers();
        }

        private void Page3Open_Button(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AccountingOfStaffWorkingHours(); 
        }

        private void Page4Open_Button(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ExcelReportGenerator(); 
        }
    }

}
