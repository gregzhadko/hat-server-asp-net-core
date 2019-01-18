﻿// <auto-generated />
using System;
using HatServer.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HatServer.Migrations.GameDb
{
    [DbContext(typeof(GameDbContext))]
    [UsedImplicitly]
    internal class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Entities.DeviceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Device");

                    b.Property<Guid>("DeviceGuid");

                    b.Property<string>("DeviceModel");

                    b.Property<string>("OsName");

                    b.Property<string>("OsVersion");

                    b.Property<string>("PushToken");

                    b.Property<int>("TimeStamp");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("DeviceInfos");
                });

            modelBuilder.Entity("Model.Entities.DownloadedPacksInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("DeviceId");

                    b.Property<DateTime>("DownloadedTime");

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

                    b.Property<Guid>("DeviceId");

                    b.Property<string>("InGameId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Model.Entities.GamePack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("GamePackIconId");

                    b.Property<string>("Language")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Paid");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("GamePacks");
                });

            modelBuilder.Entity("Model.Entities.GamePackIcon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GamePackId");

                    b.Property<byte[]>("Icon");

                    b.HasKey("Id");

                    b.HasIndex("GamePackId")
                        .IsUnique();

                    b.ToTable("GamePackIcons");
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

            modelBuilder.Entity("Model.Entities.InGamePhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BadItalic");

                    b.Property<int>("GameId");

                    b.Property<int>("InGameId");

                    b.Property<int>("PackId");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("InGamePhrase");
                });

            modelBuilder.Entity("Model.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InGameId");

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

                    b.Property<Guid>("DeviceId");

                    b.Property<string>("GameGUID");

                    b.Property<int>("GameId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("RoundNumber");

                    b.Property<int?>("SettingsId");

                    b.Property<int>("Stage");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Time");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("Model.Entities.RoundPhrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoundId");

                    b.Property<int>("State");

                    b.Property<int>("Time");

                    b.Property<int>("WordId");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

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

            modelBuilder.Entity("Model.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId");

                    b.Property<int>("InGameId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Model.Entities.DownloadedPacksInfo", b =>
                {
                    b.HasOne("Model.Entities.GamePack", "GamePack")
                        .WithMany()
                        .HasForeignKey("GamePackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.GamePackIcon", b =>
                {
                    b.HasOne("Model.Entities.GamePack", "GamePack")
                        .WithOne("GamePackIcon")
                        .HasForeignKey("Model.Entities.GamePackIcon", "GamePackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.GamePhrase", b =>
                {
                    b.HasOne("Model.Entities.GamePack", "GamePack")
                        .WithMany("Phrases")
                        .HasForeignKey("GamePackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Model.Entities.InGamePhrase", b =>
                {
                    b.HasOne("Model.Entities.Game", "Game")
                        .WithMany("Words")
                        .HasForeignKey("GameId")
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
                    b.HasOne("Model.Entities.Settings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");
                });

            modelBuilder.Entity("Model.Entities.RoundPhrase", b =>
                {
                    b.HasOne("Model.Entities.Round", "Round")
                        .WithMany("Words")
                        .HasForeignKey("RoundId")
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
