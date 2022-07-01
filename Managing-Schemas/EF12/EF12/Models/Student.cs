namespace EF12.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentAddress Address { get; set; }
        public ICollection<StudentGrade> StudentGrades { get; set; } =
            new HashSet<StudentGrade>();
    }
}
