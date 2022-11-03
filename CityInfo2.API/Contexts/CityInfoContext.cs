using CityInfo2.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API.Contexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            //comment this if you are using migration to create your db
            //Database.EnsureCreated();

        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "A very buzzling city of the USA"
                },
                new City(){
                    Id = 2,
                    Name = "Lagos",
                    Description = "A very buzzling city of Nigeria"
                },
                new City()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "A very buzzling city of the French"
                });


            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Central Park",
                    Description = "The beautiful greenery park"
                },
                new PointOfInterest()
                {
                    Id = 2,
                    CityId = 1,
                    Name = "Empire State Building",
                    Description = "The 102-storey sky scrapper"
                },
                new PointOfInterest()
                {
                    Id = 3,
                    CityId = 2,
                    Name = "JJT Park",
                    Description = "The beautiful greenery park"
                },
                new PointOfInterest()
                {
                    Id = 4,
                    CityId = 2,
                    Name = "Eko Antlantic city",
                    Description = "The beautiful new well designed and planned City on the Ocean"
                },
                new PointOfInterest()
                {
                    Id = 5,
                    CityId = 3,
                    Name = "Central Park",
                    Description = "The beautiful greenery park"
                },
                new PointOfInterest()
                {
                    Id = 6,
                    CityId=3,
                    Name = "Eiffel Tower",
                    Description = "The High rise iconic portrait of Paris"
                });
            base.OnModelCreating(modelBuilder);
        }

        //below is another way to add coneection string
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionBuilder);
        //}
    }
}
