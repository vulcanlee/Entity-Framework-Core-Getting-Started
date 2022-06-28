using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation property Returns the Employee Address
        public EmployeeAddress EmployeeAddress { get; set; }
    }

    public class EmployeeAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public int EmployeeID { get; set; }
        //Navigation property Returns the Employee object
        public Employee Employee { get; set; }
    }
}
