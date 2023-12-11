using OneriPlatform.Entities;
using System.Data.Entity;

namespace OneriPlatform.DataAcsessLayer.EntityFreamework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<SuggestionUsers> SuggestionUserss { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
