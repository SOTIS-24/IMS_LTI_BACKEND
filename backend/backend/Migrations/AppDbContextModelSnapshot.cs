﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("backend.Model.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("QuestionResultId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuestionResultId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("backend.Model.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("backend.Model.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("backend.Model.QuestionResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Passed")
                        .HasColumnType("boolean");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.Property<long>("TestResultId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestResultId");

                    b.ToTable("QuestionResult");
                });

            modelBuilder.Entity("backend.Model.Test", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("backend.Model.TestResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<string>("StudentUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TestId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("TestResult");
                });

            modelBuilder.Entity("backend.Model.Answer", b =>
                {
                    b.HasOne("backend.Model.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Model.QuestionResult", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionResultId");
                });

            modelBuilder.Entity("backend.Model.Question", b =>
                {
                    b.HasOne("backend.Model.Test", null)
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Model.QuestionResult", b =>
                {
                    b.HasOne("backend.Model.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Model.TestResult", null)
                        .WithMany("QuestionResults")
                        .HasForeignKey("TestResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("backend.Model.Test", b =>
                {
                    b.HasOne("backend.Model.Course", null)
                        .WithMany("Tests")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Model.Course", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("backend.Model.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("backend.Model.QuestionResult", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("backend.Model.Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("backend.Model.TestResult", b =>
                {
                    b.Navigation("QuestionResults");
                });
#pragma warning restore 612, 618
        }
    }
}
