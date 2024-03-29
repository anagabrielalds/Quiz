﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizzes.API.DAL;

#nullable disable

namespace Quizzes.API.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230522182329_Migration02")]
    partial class Migration02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Perguntas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdQuiz")
                        .HasColumnType("int");

                    b.Property<string>("Pergunta")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdQuiz");

                    b.ToTable("Perguntas");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdTema")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("IdTema");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Respostas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("EhCorreta")
                        .IsUnicode(false)
                        .HasColumnType("bit");

                    b.Property<int>("IdPergunta")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdPergunta");

                    b.ToTable("Respostas");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Tema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Imagem")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("TemaDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Tema");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Perguntas", b =>
                {
                    b.HasOne("Quizzes.API.Domain.Entity.Quiz", "Quiz")
                        .WithMany("Perguntas")
                        .HasForeignKey("IdQuiz")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Quiz", b =>
                {
                    b.HasOne("Quizzes.API.Domain.Entity.Tema", "Tema")
                        .WithMany("Quiz")
                        .HasForeignKey("IdTema")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Tema");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Respostas", b =>
                {
                    b.HasOne("Quizzes.API.Domain.Entity.Perguntas", "Perguntas")
                        .WithMany("Respostas")
                        .HasForeignKey("IdPergunta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perguntas");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Perguntas", b =>
                {
                    b.Navigation("Respostas");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Quiz", b =>
                {
                    b.Navigation("Perguntas");
                });

            modelBuilder.Entity("Quizzes.API.Domain.Entity.Tema", b =>
                {
                    b.Navigation("Quiz");
                });
#pragma warning restore 612, 618
        }
    }
}
