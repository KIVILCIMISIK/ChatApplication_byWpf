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
     internal List<Message> Messages { get; set; }


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
        
        public void readChatJson(ChatJson chatjson)
        {
            List<Message> newMessages = chatjson.Messages.Where(m => m.Time > chatjson.logInTime).ToList();
            //  List<Message> newMessages = chatjson.Messages.ToList();
            foreach (Message message in newMessages)
            {
                if (message.Time > chatjson.lastMessageTime)
                {
                    
                     Console.WriteLine(message.Time + "-> " + message.Sender + ": " + message.Text);

                    lastMessageTime = message.Time;
                }
            }

        }
    }
}



