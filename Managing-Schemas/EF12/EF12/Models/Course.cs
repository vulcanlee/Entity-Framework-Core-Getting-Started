namespace EF12.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<StudentGrade> StudentGrades { get; set; }=
            new HashSet<StudentGrade>();
    }
}
