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
using System.Diagnostics;

namespace ChatApplication_byWpf
{
    /// <summary>
    /// Messages.xaml etkileşim mantığı
    /// </summary>
    public partial class Messages : Page
    {
        private User user;
        Message message;
        ChatJson chatjson;
        DispatcherTimer timer;
        string clock;
        private Dictionary<string, SolidColorBrush> userBrushes = new Dictionary<string, SolidColorBrush>();



        public Messages(User user,ChatJson chatjson)
        {
            chatjson = new ChatJson();

            this.user = user;
            this.chatjson = chatjson;
            chatjson.logInTime = user.Login_Time;
            message = new Message();
            timer = new DispatcherTimer();
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            userBrushes.Add(user.User_name, new SolidColorBrush(Colors.Blue));

        }

        private void Timer_Tick(object sender, EventArgs e)
        {



            readMessages();
         
            clock = $"{DateTime.Now.ToShortTimeString()}";
            textClock.Text = clock;

        }

        public void readMessages()
        {

            using (var dbContext = new MyDbContext())
            {
                var Messages = dbContext.Messages.ToList();
                var NewMessage = dbContext.Messages.Where(m => m.Message_time >= m.Last_Message_Time).ToList();
                
                foreach(var message in NewMessage)
                {
                    textblock.Text += $"{message.User_name}: {message.Message_text}\n";

                   


                }


            }
       /*     List<Message> newMessages = chatjson.Messages.Where(m => m.Message_time > chatjson.logInTime).ToList();

            foreach (Message message in newMessages)
            {
                if (chatjson.lastMessageTime.ToShortDateString() != message.Message_time.ToShortDateString())
                {
                    string timeText = $"{message.Message_time.ToShortDateString()}";
                    textblock.Text += timeText + Environment.NewLine;
                }

                if (message.Message_time > chatjson.lastMessageTime)
                {
                    string newMessageText = $"{message.Message_time.ToShortTimeString()} -> {message.User_name}: {message.Message_text}";
                    SolidColorBrush brush;

                    if (message.User_name == user.User_name)
                    {
                        if (!userBrushes.ContainsKey(message.User_name))
                        {
                            userBrushes.Add(message.User_name, new SolidColorBrush(Colors.Blue));
                        }
                        brush = userBrushes[message.User_name];
                    }
                    else
                    {
                        brush = new SolidColorBrush(Colors.Black);
                    }

                    textblock.Inlines.Add(new Run(newMessageText) { Foreground = brush });
                    textblock.Inlines.Add(new LineBreak());

                    chatjson.lastMessageTime = message.Message_time;
                    // scrollViewer.ScrollToBottom();
                }
            }*/
        }
        private void TextboxMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            using (var dbContext = new MyDbContext())
            {
                var newMessage = new Message
                {
                    User_name = user.User_name,
                    
                    Message_time = DateTime.Now,
                    Message_text = textbox_message.Text,
                    Last_Message_Time=DateTime.Now,

                };
                
                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
            }
            
            /*Message newMessage = new Message();

            newMessage.Sender = user.User_name;
            newMessage.Text = textbox_message.Text;
            newMessage.Time = DateTime.Now;

            Message.writeText(newMessage.Time + "-> " + newMessage.Sender + ": " + newMessage.Text);
            chatjson.Messages.Add(newMessage);

            ChatJson.saveChatJson(chatjson);
            textbox_message.Text = null; */
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
        private void ComboBoxFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            string newFontSize = (string)comboBoxItem.Content;

            int fontsize;
            if (Int32.TryParse(newFontSize, out fontsize))
                textblock.FontSize = fontsize;
        }
        private void ComboBoxFontType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            string newFontType = (string)comboBoxItem.Content;

            if (newFontType == "Italic")
            {
                textblock.FontStyle = FontStyles.Italic;
                

            }
            else if (newFontType == "Normal")
            {
                textblock.FontStyle = FontStyles.Normal;
                textblock.FontWeight = FontWeights.Regular;

            }
            else if (newFontType == "Bold")
            {
                textblock.FontWeight = FontWeights.Bold;
            }
         
        }
        private void emojiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            ListBoxItem selectedItem = (ListBoxItem)listBox.SelectedItem;
            if (selectedItem != null)
            {
                string selectedEmoji = selectedItem.Content.ToString();
                textbox_message.Text += selectedEmoji;
            }


        }
    }

}






