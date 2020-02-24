using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokyo2020.Domain.Models
{
    public class DataContext: DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
        public DbSet<Athlete> Athletes { get; set; }

        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
