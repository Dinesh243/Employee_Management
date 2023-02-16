using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAppCore.Core.Models
{
    public class EmployeeDetails
    {
        public int WorkerId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }       
        public string SecondName { get; set; }        
        public DateTime DateOfJoining { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public int ContactNumber { get; set; }     
        public string Adress { get; set; }
        public int LocationId { get; set; }       
        public string Location { get; set; }        
        public bool EnableEditButton { get; set; }
    }
}
