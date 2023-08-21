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



        public Messages(User user)
        {
            chatjson = new ChatJson();

            this.user = user;
            chatjson.logInTime = user.logInTime;
            message = new Message();
            timer = new DispatcherTimer();
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            userBrushes.Add(user.Name, new SolidColorBrush(Colors.Blue));

        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {

          
            
            readMessages();
            chatjson.loadChat();
            clock = $"{DateTime.Now.ToShortTimeString()}";
            textClock.Text = clock;

        }
   
        public void readMessages()
        {
            List<Message> newMessages = chatjson.Messages.Where(m => m.Time > chatjson.logInTime).ToList();

            foreach (Message message in newMessages)
            {
                if (chatjson.lastMessageTime.ToShortDateString() != message.Time.ToShortDateString())
                {
                    string timeText = $"{message.Time.ToShortDateString()}";
                    textblock.Text += timeText + Environment.NewLine;
                }

                if (message.Time > chatjson.lastMessageTime)
                {
                    string newMessageText = $"{message.Time.ToShortTimeString()} -> {message.Sender}: {message.Text}";
                    SolidColorBrush brush;

                    if (message.Sender == user.Name)
                    {
                        if (!userBrushes.ContainsKey(message.Sender))
                        {
                            userBrushes.Add(message.Sender, new SolidColorBrush(Colors.Blue));
                        }
                        brush = userBrushes[message.Sender];
                    }
                    else
                    {
                        brush = new SolidColorBrush(Colors.Black);
                    }

                    textblock.Inlines.Add(new Run(newMessageText) { Foreground = brush });
                    textblock.Inlines.Add(new LineBreak());

                    chatjson.lastMessageTime = message.Time;
                   // scrollViewer.ScrollToBottom();
                }
            }
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
            Message newMessage = new Message();

            newMessage.Sender = user.Name;
            newMessage.Text = textbox_message.Text;
            newMessage.Time = DateTime.Now;

            Message.writeText(newMessage.Time + "-> " + newMessage.Sender + ": " + newMessage.Text);
            chatjson.Messages.Add(newMessage); 

            ChatJson.saveChatJson(chatjson);
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
        private void ComboBoxFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            string newFontSize = (string)comboBoxItem.Content;

            int fontsize;
            if(Int32.TryParse(newFontSize, out fontsize))
                 textblock.FontSize = fontsize;
        }
        private void ComboBoxFontType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;
            string newFontType = (string)comboBoxItem.Content;

            if (newFontType == "Italic")
                textblock.FontStyle = FontStyles.Italic;
            else if (newFontType == "Normal")
            {
                textblock.FontStyle = FontStyles.Normal;
                textblock.FontWeight = FontWeights.Regular;
            }
            else if (newFontType == "Bold")
                textblock.FontWeight = FontWeights.Bold;
        }
    }
}








