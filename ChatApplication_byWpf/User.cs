using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication_byWpf
{
    
    public class User
    {
        
        [Key]  public string User_name
        { get; set; }

        public DateTime Login_Time
        {
            get; set;
        }

    }
}
