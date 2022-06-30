using EF08.Models;

namespace EF08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new DataContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}