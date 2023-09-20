using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication_byWpf
{
    public class Message
    {

        [Key]
        public string User_name {
           get;
           set;
        }
       
        
        public string Message_text { get; set; }
        
        private DateTime time;
        public DateTime Message_time { get; set; }
        public DateTime Last_Message_Time { get; set; }
        
        public static List<string> messages = new List<string>();
       

        static public void writeText(string text)
        {
            string file = Path.Combine(Environment.CurrentDirectory, "chattfileee.txt");
            File.AppendAllText(file, text + Environment.NewLine);
            using (FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(text);
                    sw.Flush();
                }
            }

        }
        public static List<string> readText()
        {


            string file = Path.Combine(Environment.CurrentDirectory, "chattfileee.txt");

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {


                    string text;

                    while ((text = sr.ReadLine()) != null)
                    {

                        if (!messages.Contains(text))
                        {
                            Console.WriteLine(text);
                            messages.Add(text);

                        }


                    }
                }
            }
            return messages;
        }
       
    }
}
        