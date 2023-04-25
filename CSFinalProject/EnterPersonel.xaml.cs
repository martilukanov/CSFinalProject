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
    /// Interaction logic for EnterPersonel.xaml
    /// </summary>
    public partial class EnterPersonel : Window
    {
        public EnterPersonel()
        {
            InitializeComponent();

            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");
            SqlCon.Open();
            SqlCommand cmdAddToBoxTeam = new SqlCommand($"Select * from UserInfo", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmdAddToBoxTeam);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            Box.ItemsSource = dt.DefaultView;


            SqlCommand cmdAddToListTeam = new SqlCommand("Select username from UserInfo", SqlCon);
            SqlDataReader readerAddToListTeam;
            readerAddToListTeam = cmdAddToListTeam.ExecuteReader();
            while (readerAddToListTeam.Read())
            {
                userBox.Items.Add(readerAddToListTeam["username"].ToString());
            }

            readerAddToListTeam.Close();
            SqlCon.Close();

        }

        
        
        
        
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{userBox.SelectedValue}");
            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");
            SqlCon.Open();
            SqlCommand cmd = new SqlCommand($"Update UserInfo Set job = '{jobBox.SelectedValue}', contact = '{contactBox.Text.ToString()}', allowedToWork = '{canWorkBox.SelectedValue}' where username = '{userBox.SelectedValue}'", SqlCon);
            cmd.ExecuteNonQuery();
            SqlCon.Close();


            SqlCon.Open();
            SqlCommand cmdAddToBoxTeam = new SqlCommand($"Select * from UserInfo", SqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(cmdAddToBoxTeam);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            Box.ItemsSource = dt.DefaultView;
        }

        private void ManagerMenu_Click(object sender, RoutedEventArgs e)
        {
            ManagerMenu menu = new ManagerMenu();   
            menu.Show();
            this.Close();
        }
    }
}
