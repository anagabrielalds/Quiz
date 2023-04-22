using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzes.API.Domain.Entity
{
    [Table("Respostas")]
    public class Respostas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IdQuiz { get; set; }
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Required]
        public bool EhCorreta { get; set; }
        [Required]
        public virtual Quiz Quiz { get; set; }
    }
}
