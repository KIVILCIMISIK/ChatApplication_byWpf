using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ChatApplication_byWpf
{
    /// <summary>
    /// Messages.xaml etkileşim mantığı
    /// </summary>
    public partial class Messages : Page
    {
        private static object locker = new object();
        private User user;
        Message message;
        ChatJson chatjson;
        DispatcherTimer timer;




        public Messages(User user)
        {
            chatjson = new ChatJson();

            this.user = user;
            chatjson.logInTime = user.logInTime;
            message = new Message();
            timer = new DispatcherTimer();
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += Timer_Tick;
            timer.Start();
            // chatjson.loadChat();


        }

        private void Timer_Tick(object sender, EventArgs e)
        {



            readMessages();
            chatjson.loadChat();
            ChatJson.saveChatJson(chatjson);


        }
        public void readMessages()
        {

            List<Message> newMessages = chatjson.Messages.Where(m => m.Time > chatjson.logInTime).ToList();
            //  List<Message> newMessages = chatjson.Messages.ToList();
            foreach (Message message in newMessages)
            {
                if (message.Time > chatjson.lastMessageTime)
                {
                    string newMessageText = $"{message.Time} -> {message.Sender}: {message.Text}";

                    textblock.Text += newMessageText + Environment.NewLine;

                    chatjson.lastMessageTime = message.Time;

                }
            }





            // chatjson.loadChat();
            //ChatJson.saveChatJson(chatjson);


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Message newMessage = new Message();

            newMessage.Sender = user.Name;
            newMessage.Text = textbox_message.Text;
            newMessage.Time = DateTime.Now;

            Message.writeText(newMessage.Time + "-> " + newMessage.Sender + ": " + newMessage.Text);
            chatjson.Messages.Add(newMessage);


            // ChatJson.saveChatJson(chatjson);



            textbox_message.Text = null;

        }







        private void logout_Click(object sender, RoutedEventArgs e)
        {


            ChatJson.saveChatJson(chatjson);
            timer.Stop();
            MessageBox.Show("You logged out!");
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            Window.GetWindow(this).Close();


        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}








