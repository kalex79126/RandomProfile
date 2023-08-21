﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomProfileAPI.Data;

#nullable disable

namespace RandomProfileAPI.Data.RandomProfileMigrations
{
    [DbContext(typeof(RandomProfileContext))]
    partial class RandomProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("RandomProfile.Models.Profile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("NickName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("ProfileImageID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProfileImageID");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("RandomProfile.Models.ProfileImage", b =>
                {
                    b.Property<int>("ProfileImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ProfileImageBytes")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("ProfileImageID");

                    b.ToTable("ProfileImages");
                });

            modelBuilder.Entity("RandomProfile.Models.Profile", b =>
                {
                    b.HasOne("RandomProfile.Models.ProfileImage", "ProfileImage")
                        .WithMany("Profiles")
                        .HasForeignKey("ProfileImageID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("RandomProfile.Models.ProfileImage", b =>
                {
                    b.Navigation("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
