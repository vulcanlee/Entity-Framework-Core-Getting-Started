using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MigrationLabContext : DbContext
    {
        public MigrationLabContext()
        {

        }

        public MigrationLabContext(DbContextOptions<MigrationLabContext> options)
               : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<Student> Student { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
    }
}
