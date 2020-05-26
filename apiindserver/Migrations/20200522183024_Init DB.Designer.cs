﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using apiindserver.Models;

namespace apiindserver.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200522183024_Init DB")]
    partial class InitDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("apiindserver.Models.Criteria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Color");

                    b.Property<double?>("MaxDiffPercent");

                    b.Property<double?>("MinDiffPercent");

                    b.Property<string>("Name");

                    b.Property<long?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Criterias");
                });

            modelBuilder.Entity("apiindserver.Models.LogRecord", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<long?>("ProductID");

                    b.Property<long?>("ProjectID");

                    b.Property<TimeSpan>("ResponseTime");

                    b.Property<long?>("TesterId");

                    b.Property<string>("URL");

                    b.Property<string>("Version");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("TesterId");

                    b.ToTable("LogRecords");
                });

            modelBuilder.Entity("apiindserver.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<long>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("apiindserver.Models.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("apiindserver.Models.ProjectTester", b =>
                {
                    b.Property<long>("ProjectId");

                    b.Property<long>("TesterId");

                    b.HasKey("ProjectId", "TesterId");

                    b.HasIndex("TesterId");

                    b.ToTable("ProjectTester");
                });

            modelBuilder.Entity("apiindserver.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("apiindserver.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("apiindserver.Models.UserRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("apiindserver.Models.Criteria", b =>
                {
                    b.HasOne("apiindserver.Models.Project", "Project")
                        .WithMany("Criterias")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("apiindserver.Models.LogRecord", b =>
                {
                    b.HasOne("apiindserver.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("apiindserver.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");

                    b.HasOne("apiindserver.Models.User", "Tester")
                        .WithMany()
                        .HasForeignKey("TesterId");
                });

            modelBuilder.Entity("apiindserver.Models.Product", b =>
                {
                    b.HasOne("apiindserver.Models.Project", "Project")
                        .WithMany("Products")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("apiindserver.Models.ProjectTester", b =>
                {
                    b.HasOne("apiindserver.Models.Project", "Project")
                        .WithMany("Testers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("apiindserver.Models.User", "Tester")
                        .WithMany("Projects")
                        .HasForeignKey("TesterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("apiindserver.Models.UserRole", b =>
                {
                    b.HasOne("apiindserver.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("apiindserver.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}