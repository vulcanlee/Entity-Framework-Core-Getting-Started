using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; }=new HashSet<Student>();
    }

    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }

}
