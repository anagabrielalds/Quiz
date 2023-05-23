using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzes.API.Domain.Entity
{
    public class Perguntas
    {
        public int Id { get; set; }

        
        public string Pergunta { get; set; }
        [Required]
        [ForeignKey("Quiz")]
        public int IdQuiz  { get; set; }

        public virtual ICollection<Respostas>? Respostas { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
