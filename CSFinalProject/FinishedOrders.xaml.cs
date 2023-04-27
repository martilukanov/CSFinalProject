using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Telegram.Bot;

namespace CSFinalProject
{
    /// <summary>
    /// Interaction logic for FinishedOrders.xaml
    /// </summary>
    public partial class FinishedOrders : Window
    {
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");
        public FinishedOrders()
        {
            InitializeComponent();


            ////foreach (DataGridRow row in ordersBox.SelectedItems)
            ////{
            ////    System.Data.DataRow MyRow = (System.Data.DataRow)row.Item;
            ////    string value = MyRow[""].ToString();
            ////    MessageBox.Show(value);
            ////}


            ////List<User> users = new List<User>();
            ////users.Add(new User() { Id = 5, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
            ////users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            ////users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

            ////ordersBox.ItemsSource = users;

            try
            {
                SqlCon.Open();
                SqlCommand cmdLoadData = new SqlCommand($"Select orderId, dishName, orderTime,name from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId", SqlCon);
                SqlDataAdapter sda = new SqlDataAdapter(cmdLoadData);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                ordersBox.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex) 
            {
                
            }
            finally
            {
                SqlCon.Close(); 
            }

            



            //MessageBox.Show(DateTime.Now.ToString());
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var row = sender as DataGridRow;
            //var emp = row.DataContext as TextBlock;

            //MessageBox.Show(emp.ToString());

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlCon.Close();
            SqlCon.Open();
            string ID = "";
            bool thereaAreOrders = false;
            SqlCommand loadMenuItems = new SqlCommand($"Select orderId, dishName as[Dish], orderTime as [Time of Order],name from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId", SqlCon);
            SqlDataReader readerloadMenuItems;
            readerloadMenuItems = loadMenuItems.ExecuteReader();
            if (readerloadMenuItems.Read())
            {
                thereaAreOrders=true;
            }

            try 
            {
                object item = ordersBox.SelectedItem;
                if (thereaAreOrders )
                {
                    
                    ID = (ordersBox.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    MessageBox.Show(ID);
                    ID.ToString();
                }
                
            }
            catch{ }

            SqlCon.Close();
            SqlCon.Open();
            SqlCommand completedOrder = new SqlCommand($"Select Top 1 orderId, dishName , orderTime ,contact, [table] from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId where orderId = '{ID}'", SqlCon);
            SqlDataReader readercompletedOrder;
            readercompletedOrder = completedOrder.ExecuteReader();
            while (readercompletedOrder.Read())
            {
                sendMessage($"{readercompletedOrder["contact"]}",$"Your order {readercompletedOrder["orderId"].ToString()} - {readercompletedOrder["dishName"].ToString()} for table {readercompletedOrder["table"]} is ready to be served");
            }

            SqlCon.Close();
            SqlCon.Open();
            SqlCommand cmdDeleteOrder = new SqlCommand($"Delete from OrderInfo where orderId = '{ID}'", SqlCon);
            cmdDeleteOrder.ExecuteNonQuery();
            SqlCon.Close();

            SqlCon.Open();
            SqlCommand cmdLoadData = new SqlCommand($"Select orderId, dishName as[Dish], orderTime as [Time of Order],name from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmdLoadData);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            ordersBox.ItemsSource = dt.DefaultView;
            SqlCon.Close(); 

        }

        private void ManagerMenu_Click(object sender, RoutedEventArgs e)
        {
            currentUser currentUser = new currentUser();
            if (currentUser.Job == "Manager")
            {
                ManagerMenu menu = new ManagerMenu();
                menu.Show();
                this.Close();
            }
        }

        public async Task sendMessage(string destID, string text)
        {
            try
            {
                var bot = new Telegram.Bot.TelegramBotClient("6213451114:AAGY9JZ7qF544ws-KbCJq5FUFRVsY08vjoM");
                await bot.SendTextMessageAsync(destID, text);
            }
            catch (Exception e)
            {
                Console.WriteLine("err");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            SqlCon.Close();
            SqlCon.Open();
            SqlCommand cmdLoadData = new SqlCommand($"Select orderId, dishName as[Dish], orderTime as [Time of Order],name from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmdLoadData);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            ordersBox.ItemsSource = dt.DefaultView;
            SqlCon.Close();
        }
    }

}
