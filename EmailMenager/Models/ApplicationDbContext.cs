using EmailMenager.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace EmailMenager.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public DbSet<Email> Emails { get; set; }

        public DbSet<Config> Configs { get; set; }
    }

}