using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionatiesForEverything.Model
{
    class ApplicationContext : DbContext
    {
        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<GlossaryItem> GlossaryItems { get; set; }

        public ApplicationContext() 
        { 
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=D:\Projects\DictionatiesForEverything\DictionatiesForEverything\Database\Database.db");
        }
    }
}
    