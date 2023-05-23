using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzes.API.Domain.Entity
{
    [Table("Quiz")]
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        public virtual ICollection<Perguntas>? Perguntas { get; set; }

        [Required]
        [ForeignKey("Tema")]
        public int IdTema { get; set; }
       
        public virtual Tema Tema { get; set; }
    }
}
