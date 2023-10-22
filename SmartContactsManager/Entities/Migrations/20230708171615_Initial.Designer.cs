﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(ContactsDbContext))]
    [Migration("20230708171615_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Contact", b =>
                {
                    b.Property<Guid>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactId");

                    b.ToTable("ContactTable", (string)null);

                    b.HasData(
                        new
                        {
                            ContactId = new Guid("77175a53-aa4f-404b-ae0f-28d95b2ef0c7"),
                            Description = "Student",
                            Email = "sonu@gmail.com",
                            Gender = "FEMALE",
                            Name = "Sonu",
                            Phone = "7057222817"
                        },
                        new
                        {
                            ContactId = new Guid("e3d2d0a6-7c4a-404b-822c-a2a910c52265"),
                            Description = "Teacher",
                            Email = "raj@gmail.com",
                            Gender = "MALE",
                            Name = "Raj",
                            Phone = "8557222817"
                        },
                        new
                        {
                            ContactId = new Guid("7d1afe41-3007-4832-bb94-6ec9d06dc0d4"),
                            Description = "Actor",
                            Email = "ketan@gmail.com",
                            Gender = "MALE",
                            Name = "Ketan",
                            Phone = "9057225817"
                        },
                        new
                        {
                            ContactId = new Guid("1daa30c5-1a0b-40de-b0ed-f703010252ce"),
                            Description = "Actor",
                            Email = "abhijeet@gmail.com",
                            Gender = "MALE",
                            Name = "Abhijeet",
                            Phone = "8557225817"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}