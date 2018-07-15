﻿// <auto-generated />
using System;
using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HatServer.Migrations.StatisticsDb
{
    [DbContext(typeof(StatisticsDbContext))]
    [Migration("20180714194303_StatisticsContextInit")]
    partial class StatisticsContextInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Model.Entities.Pack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Language")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Packs");
                });

            modelBuilder.Entity("Model.Entities.PhraseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClosedBy");

                    b.Property<string>("ClosedById");

                    b.Property<DateTime?>("ClosedDate");

                    b.Property<double?>("Complexity");

                    b.Property<string>("CreatedById")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int>("PackId");

                    b.Property<string>("Phrase")
                        .IsRequired();

                    b.Property<int>("TrackId");

                    b.Property<int>("Version")
                        .IsConcurrencyToken();

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("PackId");

                    b.ToTable("PhraseItems");
                });

            modelBuilder.Entity("Model.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Model.Entities.ReviewState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("PhraseItemId");

                    b.Property<int>("State");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PhraseItemId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewState");
                });

            modelBuilder.Entity("Model.Entities.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PlayerId");

                    b.Property<int>("RoundNumber");

                    b.Property<int>("SettingsId");

                    b.Property<int?>("StageId");

                    b.Property<int>("Time");

                    b.Property<int>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SettingsId");

                    b.HasIndex("StageId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("Model.Entities.RoundPhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Model.Entities.RoundPhraseState", b =>
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

            modelBuilder.Entity("Model.Entities.ServerUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("ServerUser");
                });

            modelBuilder.Entity("Model.Entities.Settings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BadItalicSimulated");

                    b.Property<bool>("CanChangeWord");

                    b.Property<int>("RoundTime");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Model.Entities.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<int>("Number");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("Model.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Model.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Model.Entities.Game", b =>
                {
                    b.HasOne("Model.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.PhraseItem", b =>
                {
                    b.HasOne("Model.Entities.ServerUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.Pack", "Pack")
                        .WithMany("Phrases")
                        .HasForeignKey("PackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Player", b =>
                {
                    b.HasOne("Model.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.ReviewState", b =>
                {
                    b.HasOne("Model.Entities.PhraseItem", "PhraseItem")
                        .WithMany("ReviewStates")
                        .HasForeignKey("PhraseItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.ServerUser", "User")
                        .WithMany("ReviewStates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Round", b =>
                {
                    b.HasOne("Model.Entities.Player", "Player")
                        .WithMany("Rounds")
                        .HasForeignKey("PlayerId");

                    b.HasOne("Model.Entities.Settings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.Stage")
                        .WithMany("Rounds")
                        .HasForeignKey("StageId");
                });

            modelBuilder.Entity("Model.Entities.RoundPhrase", b =>
                {
                    b.HasOne("Model.Entities.PhraseItem", "PhraseItem")
                        .WithMany()
                        .HasForeignKey("PhraseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.Round", "Round")
                        .WithMany("RoundPhrases")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Model.Entities.RoundPhraseState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Model.Entities.Stage", b =>
                {
                    b.HasOne("Model.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Team", b =>
                {
                    b.HasOne("Model.Entities.Game", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
