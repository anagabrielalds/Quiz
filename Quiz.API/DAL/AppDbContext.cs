using Microsoft.EntityFrameworkCore;
using Quizzes.API.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Quizzes.API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options) { }
        public virtual DbSet<Quiz> Quiz { get; set; }

        public virtual DbSet<Perguntas> Perguntas { get; set; }
        public virtual DbSet<Respostas> Respostas { get; set; }

        public virtual DbSet<Tema> Tema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tema>(entity => {
                entity.Property(x => x.TemaDescription).IsUnicode(false).IsRequired();
                entity.Property(x => x.Imagem).IsUnicode(false).IsRequired();
            });

            modelBuilder.Entity<Perguntas>(entity => {
                entity.Property(x => x.Pergunta).IsUnicode(false).IsRequired();

                entity.HasMany(x => x.Respostas) // entidade virtual que indica relação
                .WithOne(y => y.Perguntas) // coleção na entidade Quiz
                .HasForeignKey(x => x.IdPergunta) //IdAlbum da entidade avaliacao
                .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Respostas>(entity => {
                entity.Property(x => x.Descricao).IsUnicode(false).IsRequired();
                entity.Property(x => x.EhCorreta).IsUnicode(false).IsRequired();
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(x => x.Titulo).IsUnicode(false).IsRequired();

                entity.HasOne(x => x.Tema)
                .WithMany(y => y.Quiz)
                .HasForeignKey(x => x.IdTema)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(x => x.Perguntas) // entidade virtual que indica relação
                .WithOne(y => y.Quiz) // coleção na entidade Quiz
                .HasForeignKey(x => x.IdQuiz) //IdAlbum da entidade avaliacao
                .OnDelete(DeleteBehavior.Cascade); // cascade delete - quando um Quiz for removido suas respostas relacionadas também serão

            });
        }
    }
}