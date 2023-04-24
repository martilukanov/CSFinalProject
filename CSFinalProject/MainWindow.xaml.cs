using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Twilio;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Telegram.Bot;

namespace CSFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();


            sendMessage("-923845949","This is so cool");
            
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
