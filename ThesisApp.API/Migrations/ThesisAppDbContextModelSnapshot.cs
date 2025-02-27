﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisApp.API.Data;

#nullable disable

namespace ThesisApp.API.Migrations
{
    [DbContext(typeof(ThesisAppDbContext))]
    partial class ThesisAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ThesisApp.API.Data.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = false,
                            Name = "Device01",
                            RoomId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsActive = false,
                            Name = "Device02",
                            RoomId = 1
                        },
                        new
                        {
                            Id = 3,
                            IsActive = false,
                            Name = "Device03",
                            RoomId = 2
                        },
                        new
                        {
                            Id = 4,
                            IsActive = false,
                            Name = "Device04",
                            RoomId = 2
                        });
                });

            modelBuilder.Entity("ThesisApp.API.Data.DeviceUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("UserId");

                    b.ToTable("DeviceUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DeviceId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("ThesisApp.API.Data.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Room01"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Room02"
                        });
                });

            modelBuilder.Entity("ThesisApp.API.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DeviceId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "mjtcatanaoan@test.com",
                            Name = "Mel-john Catanaoan",
                            PhoneNumber = "+44 (452) 886 09 12"
                        });
                });

            modelBuilder.Entity("ThesisApp.API.Data.Device", b =>
                {
                    b.HasOne("ThesisApp.API.Data.Room", "Room")
                        .WithMany("Devices")
                        .HasForeignKey("RoomId");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("ThesisApp.API.Data.DeviceUser", b =>
                {
                    b.HasOne("ThesisApp.API.Data.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisApp.API.Data.User", "User")
                        .WithMany("DeviceUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ThesisApp.API.Data.Room", b =>
                {
                    b.HasOne("ThesisApp.API.Data.User", null)
                        .WithMany("Rooms")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ThesisApp.API.Data.User", b =>
                {
                    b.HasOne("ThesisApp.API.Data.Device", null)
                        .WithMany("Users")
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("ThesisApp.API.Data.Device", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ThesisApp.API.Data.Room", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("ThesisApp.API.Data.User", b =>
                {
                    b.Navigation("DeviceUsers");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
