using Microsoft.EntityFrameworkCore;
using System.IO;

namespace WordZone.Models
{

    public class TranslationContext : DbContext
    {
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Packet> Packets { get; set; }

        public TranslationContext(DbContextOptions<TranslationContext> options) : base(options) { }
        public TranslationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "translations.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath};");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //"Translations"
            modelBuilder.Entity<Translation>(entity =>
            {
                entity.HasKey(t => t.ID); 
                entity.Property(t => t.EnglishWord).HasMaxLength(100).IsRequired();
                entity.Property(t => t.PolishTranslation).HasMaxLength(100).IsRequired(); 

                //Translation -> Packet
                entity.HasOne(t => t.Packet)
                      .WithMany(p => p.Translations)
                      .HasForeignKey(t => t.PacketID);
            });

            // "Packet"
            modelBuilder.Entity<Packet>(entity =>
            {
                entity.HasKey(p => p.ID); 
                entity.Property(p => p.PacketName).HasMaxLength(100).IsRequired();
            });
        }
    }
}
