using Junior.SharedModels.DomainModels;
using System.Data.Entity;

namespace Junior.DataAccessLayer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {
            Configuration.LazyLoadingEnabled = false;

        }

        public DbSet<Compound> Compounds { get; set; }

        public DbSet<Element> Elements { get; set; }

        public DbSet<CompoundType> CompoundTypes { get; set; }

        public DbSet<CompoundElement> CompoundElements { get; set; }
    }
}
