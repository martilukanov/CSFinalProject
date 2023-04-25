using System;
using System.Collections.Generic;
using System.Data;
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

namespace CSFinalProject
{
    /// <summary>
    /// Interaction logic for EnterOrders.xaml
    /// </summary>
    public partial class EnterOrders : Window
    {
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");
        public EnterOrders()
        {
            InitializeComponent();

            SqlCon.Open();
            SqlCommand loadMenuItems = new SqlCommand($"Select dishName from MenuInfo", SqlCon);
            SqlDataReader readerloadMenuItems;
            readerloadMenuItems = loadMenuItems.ExecuteReader();
            while (readerloadMenuItems.Read())
            {
                menuItemBox.Items.Add(readerloadMenuItems["dishName"].ToString());
            }
            SqlCon.Close();

            SqlCon.Open();
            SqlCommand fillBox = new SqlCommand($"Select orderId, dishName as[Dish], orderTime as [Time of Order],[table] as [Table],name from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId ", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(fillBox);
            DataTable dt = new DataTable("Menu");
            sda.Fill(dt);
            Box.ItemsSource = dt.DefaultView;
            SqlCon.Close();
        }

        
        
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
            
            int selectedDishId = 0;
            SqlCon.Open();
            SqlCommand loadMenuItems = new SqlCommand($"Select dishId from MenuInfo where dishName = '{menuItemBox.SelectedValue}'", SqlCon);
            SqlDataReader readerloadMenuItems;
            readerloadMenuItems = loadMenuItems.ExecuteReader();
            while (readerloadMenuItems.Read())
            {
                selectedDishId = Convert.ToInt32(readerloadMenuItems["dishId"]);

            }
            SqlCon.Close();

            
            currentUser currentUser = new currentUser();
        

            SqlCon.Open();
            SqlCommand cmdAddToBoxTeam = new SqlCommand($"Insert into OrderInfo (dishId, orderTime, userId, [table]) values ({selectedDishId}, '{DateTime.Now}',{currentUser.UserID},'{table.Text.ToString()}')", SqlCon);
            cmdAddToBoxTeam.ExecuteNonQuery();
            SqlCon.Close();

            SqlCon.Open();
            SqlCommand fillBox = new SqlCommand($"Select orderId, dishName as[Dish], orderTime as [Time of Order],[table] as [Table],name from OrderInfo Left join MenuInfo on MenuInfo.dishId = OrderInfo.dishId left Join UserInfo on UserInfo.userId = OrderInfo.userId ", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(fillBox);
            DataTable dt = new DataTable("Menu");
            sda.Fill(dt);
            Box.ItemsSource = dt.DefaultView;
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
    }
}
