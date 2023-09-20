using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLog;

namespace ChatApplication_byWpf
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public Messages messagesPage;
        User user;
        ChatJson chatjson;
        Commands command;


        public MainWindow()
        {

            user = new User();
            chatjson = new ChatJson();
            command = new Commands();
            InitializeComponent();
            command.Start();
            command.doSomething();
            command.Stop();



    }
    private void TextboxLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
           
        }
        private void Login()
        {

            
       
                if (!string.IsNullOrWhiteSpace(textbox.Text))
                {
                    user.User_name = textbox.Text;
                    user.Login_Time = DateTime.Now;

                }
   
            using (var dbContext = new MyDbContext())
                {
                    var newUser = new User
                    {
                       
                        User_name = user.User_name,
                        Login_Time = user.Login_Time,

                    };
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();


                }
            
         
            /*  Message.writeText(user.User_name + " logged in!");
            Message loginMessage = new Message();
            loginMessage.Sender = user.User_name;
            loginMessage.Text = "logged in!";
            chatjson.Messages.Add(loginMessage);
            ChatJson.saveChatJson(chatjson);*/

            messagesPage = new Messages(user, chatjson);
            MainWindowFrame.Content = messagesPage;
            label.Content = null;


         
        }

        private void MainWindowFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    
     class Commands
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            public void Start()
            {
                logger.Info("Started");
            }
            public void doSomething()
            {
                logger.Fatal("The service does not work");
                logger.Error("null");
            }
            public void Stop()
            {
                logger.Warn("Transactions not completed");
            }
        }

    }    
    }
