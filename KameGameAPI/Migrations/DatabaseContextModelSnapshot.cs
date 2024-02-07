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

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<string>("race")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("setId")
                        .HasColumnType("int");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("cardId");

                    b.HasIndex("setId")
                        .IsUnique();

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

                    b.HasIndex("loginId")
                        .IsUnique();

                    b.HasIndex("zipCode")
                        .IsUnique();

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

                    b.HasIndex("loginId")
                        .IsUnique();

                    b.ToTable("productManagers");
                });

            modelBuilder.Entity("KameGameAPI.Models.Set", b =>
                {
                    b.Property<int>("setId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("setId"));

                    b.Property<string>("setCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("setName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("setId");

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

                    b.HasIndex("cardId")
                        .IsUnique();

                    b.HasIndex("customerId")
                        .IsUnique();

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
                        .WithOne()
                        .HasForeignKey("KameGameAPI.Models.Card", "setId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("set");
                });

            modelBuilder.Entity("KameGameAPI.Models.Customer", b =>
                {
                    b.HasOne("KameGameAPI.Models.Login", "login")
                        .WithOne()
                        .HasForeignKey("KameGameAPI.Models.Customer", "loginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KameGameAPI.Models.ZipCodeCity", "zipCodeCity")
                        .WithOne()
                        .HasForeignKey("KameGameAPI.Models.Customer", "zipCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("login");

                    b.Navigation("zipCodeCity");
                });

            modelBuilder.Entity("KameGameAPI.Models.ProductManager", b =>
                {
                    b.HasOne("KameGameAPI.Models.Login", "login")
                        .WithOne()
                        .HasForeignKey("KameGameAPI.Models.ProductManager", "loginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("login");
                });

            modelBuilder.Entity("KameGameAPI.Models.TransactionHistory", b =>
                {
                    b.HasOne("KameGameAPI.Models.Card", "card")
                        .WithOne()
                        .HasForeignKey("KameGameAPI.Models.TransactionHistory", "cardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KameGameAPI.Models.Customer", "customer")
                        .WithOne()
                        .HasForeignKey("KameGameAPI.Models.TransactionHistory", "customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("card");

                    b.Navigation("customer");
                });
#pragma warning restore 612, 618
        }
    }
}
