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
    }
}
