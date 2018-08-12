﻿// <auto-generated />
using System;
using HatServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HatServer.Migrations.GameDb
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20180812204335_GameContextInitialization")]
    partial class GameContextInitialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Entities.DownloadedPacksInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DownloadedTime");

                    b.Property<Guid>("GadgetId");

                    b.Property<int>("GamePackId");

                    b.HasKey("Id");

                    b.HasIndex("GamePackId");

                    b.ToTable("DownloadedPacksInfos");
                });

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

            modelBuilder.Entity("Model.Entities.GamePack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Icon");

                    b.Property<string>("Language")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Paid");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("GamePacks");
                });

            modelBuilder.Entity("Model.Entities.GamePhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Complexity");

                    b.Property<string>("Description");

                    b.Property<int>("GamePackId");

                    b.Property<string>("Phrase")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GamePackId");

                    b.ToTable("GamePhrases");
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

            modelBuilder.Entity("Model.Entities.DownloadedPacksInfo", b =>
                {
                    b.HasOne("Model.Entities.GamePack", "GamePack")
                        .WithMany()
                        .HasForeignKey("GamePackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Game", b =>
                {
                    b.HasOne("Model.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.GamePhrase", b =>
                {
                    b.HasOne("Model.Entities.GamePack", "GamePack")
                        .WithMany("Phrases")
                        .HasForeignKey("GamePackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.Player", b =>
                {
                    b.HasOne("Model.Entities.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
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
                    b.HasOne("Model.Entities.GamePhrase", "PhraseItem")
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
