using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Interaction logic for EnterMenu.xaml
    /// </summary>
    public partial class EnterMenu : Window
    {
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");

        public EnterMenu()
        {
            InitializeComponent();

            SqlCon.Open();
            SqlCommand cmdAddToBoxTeam = new SqlCommand($"Select dishName, dishType from MenuInfo", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmdAddToBoxTeam);
            DataTable dt = new DataTable("Menu");
            sda.Fill(dt);
            menuBox.ItemsSource = dt.DefaultView;
            SqlCon.Close();

            SqlCon.Open();

            SqlCommand loadMenuItems = new SqlCommand($"Select dishName from MenuInfo", SqlCon);
            SqlDataReader readerloadMenuItems;
            readerloadMenuItems = loadMenuItems.ExecuteReader();
            while (readerloadMenuItems.Read())
            {
                menuItemBox.Items.Add(readerloadMenuItems["dishName"].ToString());
            }

            SqlCon.Close();

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SqlCon.Open();
            SqlCommand cmdAddToBoxTeam = new SqlCommand($"Delete from MenuInfo where dishName = '{menuItemBox.SelectedValue}'", SqlCon);
            cmdAddToBoxTeam.ExecuteNonQuery();
            SqlCon.Close();

            SqlCon.Open();
            SqlCommand cmd5 = new SqlCommand($"Select dishName, dishType from MenuInfo", SqlCon);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd5);
            DataTable dt = new DataTable("Menu");
            sda2.Fill(dt);
            menuBox.ItemsSource = dt.DefaultView;
            SqlCon.Close();


            menuItemBox.Items.Clear();

            SqlCon.Open();

            SqlCommand loadMenuItems = new SqlCommand($"Select dishName from MenuInfo", SqlCon);
            SqlDataReader readerloadMenuItems;
            readerloadMenuItems = loadMenuItems.ExecuteReader();
            while (readerloadMenuItems.Read())
            {
                menuItemBox.Items.Add(readerloadMenuItems["dishName"].ToString());
            }

            SqlCon.Close();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            bool dishExists = false;
            SqlCon.Open();
            SqlCommand loadMenuItems = new SqlCommand($"Select dishName from MenuInfo where dishName = '{dishBox.Text}'", SqlCon);
            SqlDataReader readerloadMenuItems;
            readerloadMenuItems = loadMenuItems.ExecuteReader();
            while (readerloadMenuItems.Read())
            {
                MessageBox.Show("Dish already exists");
                dishExists = true;

            }
            SqlCon.Close();

            if (dishExists == false)
            {
                SqlCon.Open();
                SqlCommand cmdAddToBoxTeam = new SqlCommand($"Insert into MenuInfo (dishName, dishType) values ('{dishBox.Text}','{foodtypeBox.SelectedValue}') ", SqlCon);
                cmdAddToBoxTeam.ExecuteNonQuery();
                SqlCon.Close();
            }

            SqlCon.Open();
            SqlCommand cmd5 = new SqlCommand($"Select dishName, dishType from MenuInfo", SqlCon);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd5);
            DataTable dt = new DataTable("Menu");
            sda2.Fill(dt);
            menuBox.ItemsSource = dt.DefaultView;
            SqlCon.Close();


            SqlCon.Open();
            menuItemBox.Items.Clear();  

            SqlCommand loadMenu = new SqlCommand($"Select dishName from MenuInfo", SqlCon);
            SqlDataReader readerloadMenus;
            readerloadMenus = loadMenu.ExecuteReader();
            while (readerloadMenus.Read())
            {
                menuItemBox.Items.Add(readerloadMenus["dishName"].ToString());
            }

            SqlCon.Close();

        }

        private void ManagerMenu_Click(object sender, RoutedEventArgs e)
        {
            ManagerMenu menu = new ManagerMenu();
            menu.Show();
            this.Close();
        }
    }
}
