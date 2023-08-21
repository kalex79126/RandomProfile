using RandomProfileAPI.Models;
using RandomProfileAPI.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace RandomProfile.Data
{
    public static class RandomProfileInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            RandomProfileContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<RandomProfileContext>();
            try
            {
                //Delete the database if you need to apply a new Migration
                //context.Database.EnsureDeleted();
                //Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                // Look for any Doctors.  Since we can't have patients without Doctors.
                if (!context.ProfileImages.Any())
                {
                    context.ProfileImages.AddRange(
                        new ProfileImage
                        {
                            ProfileImageID = 1,
                            Name = "Rabbit",
                            ProfileImageBytes = File.ReadAllBytes("Image/rabbit-cartoon.jpg"),
                            Description = "White cartoon Rabbit"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 2,
                            Name = "Bear",
                            ProfileImageBytes = File.ReadAllBytes("Image/bear-cartoon.jpg"),
                            Description = "White cartoon Bear"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 3,
                            Name = "Brontosaurus",
                            ProfileImageBytes = File.ReadAllBytes("Image/brontosaurus-cartoon.jpg"),
                            Description = "White cartoon Brontosaurus"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 4,
                            Name = "Cat",
                            ProfileImageBytes = File.ReadAllBytes("Image/cat-cartoon.jpg"),
                            Description = "White cartoon Cat"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 5,
                            Name = "Elephant",
                            ProfileImageBytes = File.ReadAllBytes("Image/elephant-cartoon.jpg"),
                            Description = "White cartoon Elephant"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 6,
                            Name = "Hippopotamus",
                            ProfileImageBytes = File.ReadAllBytes("Image/hippopotamus-cartoon.jpg"),
                            Description = "White cartoon Hippopotamus"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 7,
                            Name = "Penguin",
                            ProfileImageBytes = File.ReadAllBytes("Image/penguin-cartoon.jpg"),
                            Description = "White cartoon Penguin"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 8,
                            Name = "Robot",
                            ProfileImageBytes = File.ReadAllBytes("Image/robot-cartoon.jpg"),
                            Description = "White cartoon robot"
                        },
                        new ProfileImage
                        {
                            ProfileImageID = 9,
                            Name = "T-Rex",
                            ProfileImageBytes = File.ReadAllBytes("Image/t-rex-cartoon.jpg"),
                            Description = "White cartoon T-Rex"
                        });
                }
                if (!context.Profiles.Any())
                {
                    context.Profiles.AddRange(
                     new Profile
                     {
                         FirstName = "John",
                         MiddleName = "Doe",
                         LastName = "Smith",
                         Bio = "I am a software developer",
                         NickName = "JD",
                         DOB = new DateTime(1990, 1, 1),
                         ProfileImageID = 1
                     },
                    new Profile
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        DOB = new DateTime(1995, 4, 23),
                        ProfileImageID = 2
                    },
                    new Profile
                    {
                        FirstName = "Mark",
                        LastName = "Johnson",
                        Bio = "I am a student",
                        DOB = new DateTime(2000, 10, 5),
                        ProfileImageID = 3
                    },
                    new Profile
                    {
                        FirstName = "Emily",
                        LastName = "Nguyen",
                        NickName = "Em",
                        DOB = new DateTime(1988, 7, 12),
                        ProfileImageID = 4
                    },
                    new Profile
                    {
                        FirstName = "Adam",
                        LastName = "Garcia",
                        DOB = new DateTime(1979, 12, 30),
                        ProfileImageID = 1
                    });
                    context.SaveChanges();
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
