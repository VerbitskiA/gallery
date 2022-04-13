using Gallery.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gallery.Core.Data
{
    public class LocalMsContext : DbContext
    {       

        public LocalMsContext(DbContextOptions<LocalMsContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Соответсвует таблице dbo.Photos.
        /// </summary>
        public DbSet<PhotoEntity> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          

            modelBuilder.Entity<PhotoEntity>().HasData(
                new PhotoEntity[]
                {
                    new PhotoEntity
                    {            
                        PhotoId = 1,
                        Tag = "Mountains",
                        ImagePath = "/images/test1.jpg",
                        Date = new DateTime(2022,04,11)
                    },
                    new PhotoEntity
                    {
                        PhotoId = 2,
                        Tag = "Mountains",
                        ImagePath = "/images/test2.jpg",
                        Date = new DateTime(2022,04,11)
                    },
                    new PhotoEntity
                    {
                        PhotoId = 3,
                        Tag = "Mountains",
                        ImagePath = "/images/test3.jpg",
                        Date = new DateTime(2022,04,11)
                    },
                    new PhotoEntity
                    {
                        PhotoId = 4,
                        Tag = "Forest",
                        ImagePath = "/images/test4.jpg",
                        Date = new DateTime(2022,04,11)
                    },
                    new PhotoEntity
                    {
                        PhotoId = 5,
                        Tag = "Forest",
                        ImagePath = "/images/test5.jpeg",
                        Date = new DateTime(2022,04,11)
                    },
                    new PhotoEntity
                    {
                        PhotoId = 6,
                        Tag = "Forest",
                        ImagePath = "/images/test6.jpg",
                        Date = new DateTime(2022,04,11)
                    },
                    new PhotoEntity
                    {
                        PhotoId = 7,
                        Tag = "Mems",
                        ImagePath = "/images/123.jpg",
                        Date = new DateTime(2022,04,11)
                    }
                });           
        }
    }
}
