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
using System.Windows.Shapes;

namespace CSFinalProject
{
    /// <summary>
    /// Interaction logic for ManagerMenu.xaml
    /// </summary>
    public partial class ManagerMenu : Window
    {
        public ManagerMenu()
        {
            InitializeComponent();
        }

        private void Personel_Click(object sender, RoutedEventArgs e)
        {
            EnterPersonel enterPersonel = new EnterPersonel(); 
            enterPersonel.Show();
            this.Close();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            EnterMenu enterMenu = new EnterMenu();  
            enterMenu.Show();
            this.Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            EnterOrders enterOrders = new EnterOrders(); 
            enterOrders.Show();
            this.Close();
        }

        private void FinishedOrders_Click(object sender, RoutedEventArgs e)
        {
            FinishedOrders finishedOrders = new FinishedOrders();  
            finishedOrders.Show();
            this.Close();
        }
    }
}
