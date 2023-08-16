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

namespace ChatApplication_byWpf
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public Messages messagesPage;
        User user;
      
        public MainWindow()
        {
            
            user = new User();
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textbox.Text))
            {
                user.Name = textbox.Text;
                user.logInTime = DateTime.Now;
                
            }
            Message.writeText(user.Name + " logged in!");
            messagesPage = new Messages(user);
            MainWindowFrame.Content = messagesPage;
            label.Content = null;
        }

        private void MainWindowFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        
    }
}
