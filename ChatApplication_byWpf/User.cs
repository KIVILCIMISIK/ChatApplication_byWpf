using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private DateTime loginTime;
        public DateTime logInTime
        {
            get { return loginTime; }
            set { loginTime = value; }
        }
    
}
}
