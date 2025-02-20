using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 2,
                Name = "Luxury Retreat",
                Details = "Spacious and modern villa with ocean views",
                Image = "",
                Occupancy = 8,
                Rate = 350,
                Sqft = 3000,
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 3,
                Name = "Mountain Escape",
                Details = "Cozy villa nestled in the mountains",
                Image = "",
                Occupancy = 4,
                Rate = 250,
                Sqft = 1500,
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 4,
                Name = "Beachfront Paradise",
                Details = "Villa with direct access to the beach",
                Image = "",
                Occupancy = 6,
                Rate = 400,
                Sqft = 2500,
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 5,
                Name = "Urban Oasis",
                Details = "Modern villa in the heart of the city",
                Image = "",
                Occupancy = 3,
                Rate = 300,
                Sqft = 1200,
                CreatedDate = DateTime.Now
            },
            new Villa()
            {
                Id = 6,
                Name = "Countryside Charm",
                Details = "Rustic villa surrounded by nature",
                Image = "",
                Occupancy = 7,
                Rate = 275,
                Sqft = 1800,
                CreatedDate = DateTime.Now
            }
            );
    }
}