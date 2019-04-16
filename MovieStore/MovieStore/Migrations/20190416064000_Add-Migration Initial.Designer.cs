﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieStore.Data;

namespace MovieStore.Migrations
{
    [DbContext(typeof(MovieStoreDbContext))]
    [Migration("20190416064000_Add-Migration Initial")]
    partial class AddMigrationInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MovieStore.Models.Actor", b =>
                {
                    b.Property<Guid>("ActorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ActorID");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("MovieStore.Models.Address", b =>
                {
                    b.Property<Guid>("AddressID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CityID");

                    b.Property<int?>("CountryID");

                    b.Property<string>("Line1");

                    b.Property<string>("Line2");

                    b.Property<int?>("LocalityID");

                    b.Property<int?>("PostCodeID");

                    b.Property<int?>("RegionID");

                    b.HasKey("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.HasIndex("LocalityID");

                    b.HasIndex("PostCodeID");

                    b.HasIndex("RegionID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("MovieStore.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CityID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("MovieStore.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CapitalCityID");

                    b.Property<string>("Name");

                    b.HasKey("CountryID");

                    b.HasIndex("CapitalCityID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MovieStore.Models.Director", b =>
                {
                    b.Property<Guid>("DirectorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("DirectorID");

                    b.ToTable("Director");
                });

            modelBuilder.Entity("MovieStore.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("GenreID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MovieStore.Models.Locality", b =>
                {
                    b.Property<int>("LocalityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("RegionID");

                    b.HasKey("LocalityID");

                    b.HasIndex("RegionID");

                    b.ToTable("Locality");
                });

            modelBuilder.Entity("MovieStore.Models.Movie", b =>
                {
                    b.Property<Guid>("MovieID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<string>("Title");

                    b.HasKey("MovieID");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieStore.Models.MovieActor", b =>
                {
                    b.Property<Guid>("MovieActorID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ActorID");

                    b.Property<Guid?>("MovieID");

                    b.HasKey("MovieActorID");

                    b.HasIndex("ActorID");

                    b.HasIndex("MovieID");

                    b.ToTable("MovieActor");
                });

            modelBuilder.Entity("MovieStore.Models.MovieDirector", b =>
                {
                    b.Property<Guid>("MovieDirectorID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DirectorID");

                    b.Property<Guid?>("MovieID");

                    b.HasKey("MovieDirectorID");

                    b.HasIndex("DirectorID");

                    b.HasIndex("MovieID");

                    b.ToTable("MovieDirector");
                });

            modelBuilder.Entity("MovieStore.Models.MovieGenre", b =>
                {
                    b.Property<Guid>("MovieGenreID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GenreID");

                    b.Property<Guid?>("MovieID");

                    b.HasKey("MovieGenreID");

                    b.HasIndex("GenreID");

                    b.HasIndex("MovieID");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("MovieStore.Models.MovieProducer", b =>
                {
                    b.Property<Guid>("MovieProducerID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MovieID");

                    b.Property<Guid?>("ProducerID");

                    b.HasKey("MovieProducerID");

                    b.HasIndex("MovieID");

                    b.HasIndex("ProducerID");

                    b.ToTable("MovieProducer");
                });

            modelBuilder.Entity("MovieStore.Models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Closed");

                    b.Property<DateTime>("Creation");

                    b.Property<string>("CustomerId");

                    b.Property<int>("Status");

                    b.Property<double>("Total");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("MovieStore.Models.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MovieID");

                    b.Property<Guid?>("OrderID");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderItemID");

                    b.HasIndex("MovieID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("MovieStore.Models.PostCode", b =>
                {
                    b.Property<int>("PostCodeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code");

                    b.HasKey("PostCodeID");

                    b.ToTable("PostCode");
                });

            modelBuilder.Entity("MovieStore.Models.Producer", b =>
                {
                    b.Property<Guid>("ProducerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ProducerID");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("MovieStore.Models.Region", b =>
                {
                    b.Property<int>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CapitalCityID");

                    b.Property<int?>("CountryID");

                    b.Property<string>("Name");

                    b.HasKey("RegionID");

                    b.HasIndex("CapitalCityID");

                    b.HasIndex("CountryID");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("MovieStore.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<Guid?>("AddressID");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasIndex("AddressID");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MovieStore.Models.Address", b =>
                {
                    b.HasOne("MovieStore.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID");

                    b.HasOne("MovieStore.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("MovieStore.Models.Locality", "Locality")
                        .WithMany()
                        .HasForeignKey("LocalityID");

                    b.HasOne("MovieStore.Models.PostCode", "PostCode")
                        .WithMany()
                        .HasForeignKey("PostCodeID");

                    b.HasOne("MovieStore.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionID");
                });

            modelBuilder.Entity("MovieStore.Models.Country", b =>
                {
                    b.HasOne("MovieStore.Models.City", "Capital")
                        .WithMany()
                        .HasForeignKey("CapitalCityID");
                });

            modelBuilder.Entity("MovieStore.Models.Locality", b =>
                {
                    b.HasOne("MovieStore.Models.Region")
                        .WithMany("Localities")
                        .HasForeignKey("RegionID");
                });

            modelBuilder.Entity("MovieStore.Models.MovieActor", b =>
                {
                    b.HasOne("MovieStore.Models.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorID");

                    b.HasOne("MovieStore.Models.Movie", "Movie")
                        .WithMany("Actors")
                        .HasForeignKey("MovieID");
                });

            modelBuilder.Entity("MovieStore.Models.MovieDirector", b =>
                {
                    b.HasOne("MovieStore.Models.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorID");

                    b.HasOne("MovieStore.Models.Movie", "Movie")
                        .WithMany("Directors")
                        .HasForeignKey("MovieID");
                });

            modelBuilder.Entity("MovieStore.Models.MovieGenre", b =>
                {
                    b.HasOne("MovieStore.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreID");

                    b.HasOne("MovieStore.Models.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieID");
                });

            modelBuilder.Entity("MovieStore.Models.MovieProducer", b =>
                {
                    b.HasOne("MovieStore.Models.Movie", "Movie")
                        .WithMany("Producers")
                        .HasForeignKey("MovieID");

                    b.HasOne("MovieStore.Models.Producer", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerID");
                });

            modelBuilder.Entity("MovieStore.Models.Order", b =>
                {
                    b.HasOne("MovieStore.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("MovieStore.Models.OrderItem", b =>
                {
                    b.HasOne("MovieStore.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieID");

                    b.HasOne("MovieStore.Models.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("MovieStore.Models.Region", b =>
                {
                    b.HasOne("MovieStore.Models.City", "Capital")
                        .WithMany()
                        .HasForeignKey("CapitalCityID");

                    b.HasOne("MovieStore.Models.Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryID");
                });

            modelBuilder.Entity("MovieStore.Models.User", b =>
                {
                    b.HasOne("MovieStore.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID");
                });
#pragma warning restore 612, 618
        }
    }
}
