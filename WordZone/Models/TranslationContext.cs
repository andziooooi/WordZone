using Microsoft.EntityFrameworkCore;

namespace WordZone.Models
{

    public class TranslationContext : DbContext
    {
        public DbSet<Translation> Translations { get; set; }

        public TranslationContext(DbContextOptions<TranslationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=translations.db");
        }
    }
}
