using CSVProssesing.Models.File;
using CSVProssesing.Models.Person;
using Microsoft.EntityFrameworkCore;

namespace CSVProssesing.Data
{
    public class DataContext : DbContext
    {
        public DbSet<FileCSV> Files { get; set; }
        public DbSet<Person> People { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
