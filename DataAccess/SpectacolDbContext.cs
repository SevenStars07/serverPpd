using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model;

namespace DataAccess;
public class SpectacolDbContext : DbContext
{
    public DbSet<Spectacol> Spectacols { get; set; }
    public DbSet<Vanzare> Vanzari { get; set; }
    public DbSet<Sala> Sali { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=Ppd;Trusted_Connection=True;ConnectRetryCount=0");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var intArrayValueConverter = new ValueConverter<List<int>, string>(
            i => string.Join(",", i),
            s => string.IsNullOrWhiteSpace(s) ? new List<int>() : s.Split(new[] { ',' }).Select(int.Parse).ToList());

        builder.Entity<Vanzare>().Property(nameof(Vanzare.LocuriVandute)).HasConversion(intArrayValueConverter);
        builder.Entity<Spectacol>().Property(nameof(Spectacol.LocuriVandute)).HasConversion(intArrayValueConverter);

        builder.Entity<Sala>().HasData(new List<Sala>
        {
            new()
            {
                Id = 1,
                NrLocuri = 100
            }
        });

        builder.Entity<Spectacol>().HasData(new List<Spectacol>
        {
            new()
            {
                Id = 1,
                PretBilet = 100,
                Titlu = "S1",
                DataSpectacol = new DateTime(2021, 11, 19),
                LocuriVandute = new List<int>(),
                SalaId = 1
            },
            new()
            {
                Id = 2,
                PretBilet = 200,
                Titlu = "S2",
                DataSpectacol = new DateTime(2021, 12, 27),
                LocuriVandute = new List<int>(),
                SalaId = 1
            },
            new()
            {
                Id = 3,
                PretBilet = 150,
                Titlu = "S3",
                DataSpectacol = new DateTime(2022, 1, 3),
                LocuriVandute = new List<int>(),
                SalaId = 1
            }
        });
    }

}