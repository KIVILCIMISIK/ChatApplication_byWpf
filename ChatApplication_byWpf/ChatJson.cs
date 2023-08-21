using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;

namespace ChatApplication_byWpf
{
    public class ChatJson
    {
   
        public List<Message> Messages { get; set; }


        public DateTime logInTime;
        public DateTime lastMessageTime;
        public ChatJson()
        {
            Messages = new List<Message>();

        }

        public void loadChat()
        {
            if (File.Exists("chatjsonn.txt"))
            {

                string jsonData = File.ReadAllText("chatjsonn.txt");
                ChatJson chattjson = JsonConvert.DeserializeObject<ChatJson>(jsonData);
                Messages = chattjson.Messages;



            }

            else
            {
                Messages = new List<Message>();
                logInTime = DateTime.Now;

            }
        }
        public static void saveChatJson(ChatJson chatjson)
        {

            string JsonData = JsonConvert.SerializeObject(chatjson);
            File.WriteAllText("chatjsonn.txt", JsonData);

        }
    }
}
        
        
            

        
    




