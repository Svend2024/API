﻿// <auto-generated />
using System;
using KameGameAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KameGameAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KameGameAPI.Models.Card", b =>
                {
                    b.Property<int>("cardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cardId"));

                    b.Property<string>("attribute")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cardCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pictureLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("race")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("setCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("setCode1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cardId");

                    b.HasIndex("setCode1");

                    b.ToTable("cards");
                });

            modelBuilder.Entity("KameGameAPI.Models.Customer", b =>
                {
                    b.Property<int>("customerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customerId"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("loginId")
                        .HasColumnType("int");

                    b.Property<int>("zipCode")
                        .HasColumnType("int");

                    b.HasKey("customerId");

                    b.HasIndex("loginId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("KameGameAPI.Models.Login", b =>
                {
                    b.Property<int>("loginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("loginId"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("loginId");

                    b.ToTable("logins");
                });

            modelBuilder.Entity("KameGameAPI.Models.ProductManager", b =>
                {
                    b.Property<int>("productManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("productManagerId"));

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("loginId")
                        .HasColumnType("int");

                    b.HasKey("productManagerId");

                    b.HasIndex("loginId");

                    b.ToTable("productManagers");
                });

            modelBuilder.Entity("KameGameAPI.Models.Set", b =>
                {
                    b.Property<string>("setCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("setName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("setCode");

                    b.ToTable("sets");
                });

            modelBuilder.Entity("KameGameAPI.Models.TransactionHistory", b =>
                {
                    b.Property<int>("transactionHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("transactionHistoryId"));

                    b.Property<int>("cardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("customerId")
                        .HasColumnType("int");

                    b.HasKey("transactionHistoryId");

                    b.ToTable("transactionHistories");
                });

            modelBuilder.Entity("KameGameAPI.Models.ZipCodeCity", b =>
                {
                    b.Property<int>("zipCode")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("zipCode");

                    b.ToTable("zipCodeCities");
                });

            modelBuilder.Entity("KameGameAPI.Models.Card", b =>
                {
                    b.HasOne("KameGameAPI.Models.Set", "set")
                        .WithMany()
                        .HasForeignKey("setCode1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("set");
                });

            modelBuilder.Entity("KameGameAPI.Models.Customer", b =>
                {
                    b.HasOne("KameGameAPI.Models.Login", "login")
                        .WithMany()
                        .HasForeignKey("loginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("login");
                });

            modelBuilder.Entity("KameGameAPI.Models.ProductManager", b =>
                {
                    b.HasOne("KameGameAPI.Models.Login", "login")
                        .WithMany()
                        .HasForeignKey("loginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("login");
                });
#pragma warning restore 612, 618
        }
    }
}
