﻿// <auto-generated />
using System;
using DotNetCoreApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetCoreApp1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DotNetCoreApp1.Models.Types.DataDto", b =>
                {
                    b.Property<Guid>("DataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DataId");

                    b.ToTable("Data");
                });

            modelBuilder.Entity("DotNetCoreApp1.Models.Types.DocumentDto", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DataId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("DotNetCoreApp1.Models.Types.PasswordDto", b =>
                {
                    b.Property<Guid>("PasswordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PasswordId");

                    b.ToTable("Passwords");

                    b.HasData(
                        new
                        {
                            PasswordId = new Guid("c4a45cb8-225c-4151-b1e1-504642c9eec9"),
                            PasswordValue = "AdminHeslo123#",
                            UserId = new Guid("d3329a1f-d3b1-4989-9352-cbfa76767b68")
                        });
                });

            modelBuilder.Entity("DotNetCoreApp1.Models.Types.UserDto", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("d3329a1f-d3b1-4989-9352-cbfa76767b68"),
                            FirstName = "Iam",
                            RoleId = "Admin",
                            Surname = "BigBoss",
                            UserName = "Admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
