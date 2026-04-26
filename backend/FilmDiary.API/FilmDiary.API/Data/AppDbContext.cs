using FilmDiary.API.Models;
using Microsoft.EntityFrameworkCore;
namespace FilmDiary.API.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmActor>()
                .HasKey(fa => new { fa.FilmId, fa.ActorId });

            modelBuilder.Entity<FilmActor>()
                .HasOne(fa => fa.Film)
                .WithMany(f => f.FilmActors)
                .HasForeignKey(fa => fa.FilmId);

            modelBuilder.Entity<FilmActor>()
                .HasOne(fa => fa.Actor)
                .WithMany(a => a.FilmActors)
                .HasForeignKey(fa => fa.ActorId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Film)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FilmId);

            modelBuilder.Entity<Film>()
                .Property(f => f.ImdbRating)
                .HasPrecision(3, 1);

        }
    }
}
