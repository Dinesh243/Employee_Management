using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAppCore.Core.Models
{
    public class LogIn_Details
    {
        public int UserId { get; set; }      
        public string UserName { get; set; }       
        public string Password { get; set; }
        public bool IsAdminUser { get; set; }
      
    }
}
