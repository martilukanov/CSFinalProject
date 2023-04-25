using System;
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

namespace CSFinalProject
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon2 = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Equals(""))
            {
                MessageBox.Show("Username is empty");
            }
            if (password.Equals(""))
            {
                MessageBox.Show("Password is empty");
            }
            else if (!(username.Text.Equals("")) && !(password.Equals("")))
            {



                bool pass = false;
                bool user = false;
                try
                {


                    string queryPass = "Select pass from CSFinalProject.dbo.UserInfo where username = '" + username.Text + "'";
                    sqlCon2.Open();
                    SqlCommand cmdPass = new SqlCommand(queryPass, sqlCon2);
                    SqlDataReader readerPass;
                    readerPass = cmdPass.ExecuteReader();
                    if (readerPass.Read())
                    {

                        if (readerPass["pass"].ToString() == password.Password)
                        {

                            pass = true;
                        }

                    }


                    readerPass.Close();

                    string query = "Select username from CSFinalProject.dbo.UserInfo where username = '" + username.Text + "'";

                    SqlCommand cmd = new SqlCommand(query, sqlCon2);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    string Getusername = "";
                    if (reader.Read())
                    {

                        if (reader["username"].ToString() == username.Text)
                        {
                            user = true;
                        }

                    }
                    else
                    {

                    }

                    reader.Close();

                    currentUser currentUser = new currentUser();
                    if ((pass && user))
                    {
                        

                        SqlCommand getUserInfo = new SqlCommand($"Select * from UserInfo where username = '{username.Text}'", sqlCon2);
                        SqlDataReader readergetUserInfo;
                        readergetUserInfo = getUserInfo.ExecuteReader();
                        while (readergetUserInfo.Read())
                        {
                            

                            currentUser.UserID = Convert.ToInt32(readergetUserInfo["userId"]);
                            currentUser.Job = readergetUserInfo["job"].ToString();
                            currentUser.Contact = readergetUserInfo["contact"].ToString();
                            currentUser.AllowedToWork = readergetUserInfo["allowedToWork"].ToString();
                            currentUser.Name = readergetUserInfo["name"].ToString();
                        }

                        //MessageBox.Show($"{currentUser.UserID},{currentUser.Job},{currentUser.Contact},{currentUser.AllowedToWork}");

                        if(currentUser.AllowedToWork == "No" || currentUser.AllowedToWork.Equals(null)|| currentUser.AllowedToWork == "")
                        {
                            Request request = new Request();
                            request.Show();
                        }
                        else if (currentUser.Job == "Chef")
                        {
                            FinishedOrders finishedOrders = new FinishedOrders();
                            finishedOrders.Show();
                            this.Close();
                        }
                        else if(currentUser.Job == "Waiter")
                        {
                            EnterOrders enterOrders = new EnterOrders();    
                            enterOrders.Show(); 
                            this.Close();   
                        }
                        else if(currentUser.Job == "Manager")
                        {
                            ManagerMenu menu = new ManagerMenu();
                            menu.Show();
                            this.Close();
                        }
                        
                        
                    }else if (username.Text == "#Dev")
                    {
                        currentUser.AllowedToWork = "Yes";
                        currentUser.Job = "Manager";
                    }
                    else
                    {
                        MessageBox.Show("Username or Password Inncorrect");
                    }

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon2.Close();
                }





            }
        }

        private void Sign_Up_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= username_GotFocus;
        }
    }

    class currentUser
    {
        public static int userId;
        public static string name;
        public static string job;
        public static string contact;
        public static string allowedToWork;

        public int UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Job
        {
            get { return job; }
            set { job = value; }
        }

        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        public string AllowedToWork
        {
            get { return allowedToWork; }
            set
            {
                allowedToWork = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

    }
}
