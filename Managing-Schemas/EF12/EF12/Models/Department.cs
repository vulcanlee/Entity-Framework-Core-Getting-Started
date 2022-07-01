namespace EF12.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }=
            new HashSet<Course>();
    }
}
