using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzes.API.Domain.Entity
{
    [Table("Tema")]
    public class Tema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string TemaDescription { get; set; }

        public byte[] Imagem { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}
