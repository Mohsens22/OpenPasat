﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnoTest.Data;

namespace UnoTest.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12");

            modelBuilder.Entity("UnoTest.Models.TestAnswer", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IndentifierId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Input")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("InputSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("InputTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("InputType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PreFragmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TestFragmentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IndentifierId");

                    b.HasIndex("PreFragmentId")
                        .IsUnique();

                    b.HasIndex("TestFragmentId")
                        .IsUnique();

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("UnoTest.Models.TestFragment", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CloseAnswers")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IndentifierId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PreviousAnswer")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("RepresentationTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IndentifierId");

                    b.ToTable("Fragments");
                });

            modelBuilder.Entity("UnoTest.Models.TestIndentifier", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Correction")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ImpulseRate")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RepresentationType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TestCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ValidationString")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("UnoTest.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClinicalHistory")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DrugAbuseHistory")
                        .HasColumnType("TEXT");

                    b.Property<int>("Education")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Job")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaritalStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OtherInfo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("YearBorn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTimeOffset(new DateTime(2021, 3, 10, 11, 16, 56, 287, DateTimeKind.Unspecified).AddTicks(6301), new TimeSpan(0, 0, 0, 0, 0)),
                            Education = 0,
                            FullName = "Public",
                            Gender = 0,
                            MaritalStatus = 0,
                            Username = "public"
                        });
                });

            modelBuilder.Entity("UnoTest.Models.TestAnswer", b =>
                {
                    b.HasOne("UnoTest.Models.TestIndentifier", "Indentifier")
                        .WithMany("Answers")
                        .HasForeignKey("IndentifierId");

                    b.HasOne("UnoTest.Models.TestFragment", "PreFragment")
                        .WithOne("PreFragmentOf")
                        .HasForeignKey("UnoTest.Models.TestAnswer", "PreFragmentId");

                    b.HasOne("UnoTest.Models.TestFragment", "TestFragment")
                        .WithOne("FragmentOf")
                        .HasForeignKey("UnoTest.Models.TestAnswer", "TestFragmentId");
                });

            modelBuilder.Entity("UnoTest.Models.TestFragment", b =>
                {
                    b.HasOne("UnoTest.Models.TestIndentifier", "Indentifier")
                        .WithMany("TestFragments")
                        .HasForeignKey("IndentifierId");
                });

            modelBuilder.Entity("UnoTest.Models.TestIndentifier", b =>
                {
                    b.HasOne("UnoTest.Models.User", "User")
                        .WithMany("Tests")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
