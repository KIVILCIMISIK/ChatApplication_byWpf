using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication_byWpf
{
   public class User
    {
        private string nickName;
        public string Name
        {
            get { return nickName; }
            set { nickName = value; }
        }

        public DateTime logInTime { get; set; }
    
}
}
