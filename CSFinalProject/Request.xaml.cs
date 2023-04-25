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
using Telegram.Bot;

namespace CSFinalProject
{
    /// <summary>
    /// Interaction logic for Request.xaml
    /// </summary>
    public partial class Request : Window
    {
        public Request()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Request_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-0K9CBJP\SQLEXPRESS; Initial Catalog=CSFinalProject; Integrated Security=True");

            try
            {
                currentUser currentUser = new currentUser();
                SqlCon.Open();
                SqlCommand completedOrder = new SqlCommand($"Select Top 1 contact from UserInfo  where Job = 'Manager'", SqlCon);
                SqlDataReader readercompletedOrder;
                readercompletedOrder = completedOrder.ExecuteReader();
                while (readercompletedOrder.Read())
                {
                    sendMessage($"{readercompletedOrder["contact"]}", $"My name is {currentUser.Name}, please review my profile!");
                }
                SqlCon.Close();
            }
            catch
            {

            }
            this.Close();
            
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
    }
}
