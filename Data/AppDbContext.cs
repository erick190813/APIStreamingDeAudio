using APIStreamingDeAudio.Models;
using Microsoft.EntityFrameworkCore;

namespace APIStreamingDeAudio.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<FaixaAudio> FaixasAudio { get; set; }
}