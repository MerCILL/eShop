﻿namespace Catalog.DataAccess;

public class CatalogDbSeed
{
    public static void Seed(ModelBuilder builder)
    {
        var types = new List<TypeEntity>
        {
            new TypeEntity { Id = 1, Title = "Shoes", CreatedAt = DateTimeOffset.Now, UpdatedAt = null },
            new TypeEntity { Id = 2, Title = "Hoodie", CreatedAt = DateTimeOffset.Now, UpdatedAt = null }
        };

        var brands = new List<BrandEntity>
        {
            new BrandEntity { Id = 1, Title = "Nike", CreatedAt = DateTimeOffset.Now, UpdatedAt = null },
            new BrandEntity { Id = 2, Title = "Adidas", CreatedAt = DateTimeOffset.Now, UpdatedAt = null }
        };

        var items = new List<ItemEntity>
        {
            new ItemEntity
            {
                Id = 1,
                Title = "Nike Dunk Low Retro Premium",
                Description = "Created for the hardwood but taken to the streets, " +
                "the '80s b-ball icon returns with " +
                "classic details and throwback hoops flair. ",
                Price = 100,
                PictureFile = "nike-dunk-low-retro-premium.png",
                TypeId = 1,
                BrandId = 1,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },

            new ItemEntity
            {
                Id = 2,
                Title = "Nike Dunk Mid",
                Description = "Created for the hardwood but taken to the streets, " +
                "the '80s b-ball icon returns " +
                "with crisp leather and and classic \"Panda\" color blocking.",
                Price = 120,
                PictureFile = "nike-dunk-mid.png",
                TypeId = 1,
                BrandId = 1,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },

            new ItemEntity
            {
                Id = 3,
                Title = "Jordan Flight Fleece",
                Description = "Meet your new go-to hoodie. Heavyweight fleece feels" +
                " super soft, and the comfy, relaxed fit will " +
                "have you reaching for it again and again.",
                Price = 100,
                PictureFile = "jordan-flight-fleece.png",
                TypeId = 2,
                BrandId = 1,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },

            new ItemEntity
            {
                Id = 4,
                Title = "Forum Low",
                Description = "These are not just sneakers, but a real symbol" +
                " of the era. The adidas Forum model appeared in 1984 and won" +
                " love not only on basketball courts, but also in show business.",
                Price = 110,
                PictureFile = "forum-low.png",
                TypeId = 1,
                BrandId = 2,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },

            new ItemEntity
            {
                Id = 5,
                Title = "ENTRADA 22 SWEAT HOODIE",
                Description = "For your team. For your planet." +
                " Created with grassroots football in mind," +
                " the Entrada 22 range gives you everything " +
                "you need to make your game feel and look more beautiful. ",
                Price = 60,
                PictureFile = "entrada-22-sweat-hoodie.png",
                TypeId = 2,
                BrandId = 2,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },

            new ItemEntity
            {
                Id = 6,
                Title = "Ja 1",
                Description = "Ja Morant became the superstar he is today " +
                "by repeatedly sinking jumpers on crooked rims, " +
                "jumping on tractor tires and dribbling through traffic " +
                "cones in steamy South Carolina summers.",
                Price = 130,
                PictureFile = "ja-1.png",
                TypeId = 1,
                BrandId = 1,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },

            new ItemEntity
            {
                Id = 7,
                Title = "Air Jordan I High G",
                Description = "Feel unbeatable," +
                " from the tee box to the final putt." +
                " Inspired by one of the most iconic sneakers of all time," +
                " the Air Jordan 1 G is an instant classic on the course. ",
                Price = 190,
                PictureFile = "air-jordan-i-high.png",
                TypeId = 1,
                BrandId = 1,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = null,
            },
        };

        builder.Entity<TypeEntity>().HasData(types);
        builder.Entity<BrandEntity>().HasData(brands);
        builder.Entity<ItemEntity>().HasData(items);

    }
}
