using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class RespostasRequest
    {
        public int IdPergunta { get; set; }

        public string Descricao { get; set; }

        public bool EhCorreta { get; set; }
     }
}
