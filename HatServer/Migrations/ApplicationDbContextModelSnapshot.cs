﻿// <auto-generated />
using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace HatServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HatServer.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("HatServer.Models.Pack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Language")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("HatServer.Models.PhraseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Complexity");

                    b.Property<string>("Description");

                    b.Property<int>("PackId");

                    b.Property<string>("Phrase")
                        .IsRequired();

                    b.Property<int>("Version")
                        .IsConcurrencyToken();

                    b.HasKey("Id");

                    b.HasIndex("PackId");

                    b.ToTable("PhraseItems");
                });

            modelBuilder.Entity("HatServer.Models.PhraseState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ClearReviews");

                    b.Property<string>("Comment");

                    b.Property<int>("PhraseItemId");

                    b.Property<int>("ReviewState");

                    b.Property<string>("ServerUserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("PhraseItemId");

                    b.HasIndex("ServerUserId");

                    b.ToTable("PhraseStates");
                });

            modelBuilder.Entity("HatServer.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("HatServer.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PlayerId");

                    b.Property<int>("RoundNumber");

                    b.Property<int>("SettingsId");

                    b.Property<int>("Time");

                    b.Property<int>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SettingsId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("HatServer.Models.RoundPhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PhraseId");

                    b.Property<int>("RoundId");

                    b.Property<int?>("StateId");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.HasIndex("PhraseId");

                    b.HasIndex("RoundId");

                    b.HasIndex("StateId");

                    b.ToTable("RoundPhrases");
                });

            modelBuilder.Entity("HatServer.Models.RoundPhraseState", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("RoundPhraseStates");
                });

            modelBuilder.Entity("HatServer.Models.ServerUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

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
                });

            modelBuilder.Entity("HatServer.Models.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("BadItalicSimulated");

                    b.Property<bool>("CanChangeWord");

                    b.Property<int>("RoundTime");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("HatServer.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("HatServer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Device");

                    b.Property<Guid>("DeviceId");

                    b.Property<string>("DeviceModel");

                    b.Property<string>("Os");

                    b.Property<string>("OsVersion");

                    b.Property<string>("PushToken");

                    b.Property<int>("TimeStamp");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("GameUsers");
                });

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
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("HatServer.Models.Game", b =>
                {
                    b.HasOne("HatServer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HatServer.Models.PhraseItem", b =>
                {
                    b.HasOne("HatServer.Models.Pack", "Pack")
                        .WithMany("Phrases")
                        .HasForeignKey("PackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HatServer.Models.PhraseState", b =>
                {
                    b.HasOne("HatServer.Models.PhraseItem", "PhraseItem")
                        .WithMany("PhraseStates")
                        .HasForeignKey("PhraseItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HatServer.Models.ServerUser")
                        .WithMany("PhraseStates")
                        .HasForeignKey("ServerUserId");
                });

            modelBuilder.Entity("HatServer.Models.Player", b =>
                {
                    b.HasOne("HatServer.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HatServer.Models.Round", b =>
                {
                    b.HasOne("HatServer.Models.Player", "Player")
                        .WithMany("Rounds")
                        .HasForeignKey("PlayerId");

                    b.HasOne("HatServer.Models.Settings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HatServer.Models.RoundPhrase", b =>
                {
                    b.HasOne("HatServer.Models.PhraseItem", "PhraseItem")
                        .WithMany()
                        .HasForeignKey("PhraseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HatServer.Models.Round", "Round")
                        .WithMany("RoundPhrases")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HatServer.Models.RoundPhraseState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("HatServer.Models.Team", b =>
                {
                    b.HasOne("HatServer.Models.Game", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("HatServer.Models.ServerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HatServer.Models.ServerUser")
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

                    b.HasOne("HatServer.Models.ServerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HatServer.Models.ServerUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
