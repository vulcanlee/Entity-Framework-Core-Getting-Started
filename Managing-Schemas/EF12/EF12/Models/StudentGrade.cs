using Microsoft.EntityFrameworkCore;

namespace EF12.Models
{
    public class StudentGrade
    {
        public int Id { get; set; }
        [Precision(5, 2)]
        public decimal Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
