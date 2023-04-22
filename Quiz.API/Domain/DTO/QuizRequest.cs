using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class QuizRequest
    {
        public string Pergunta { get; set; }

        public int IdTema { get; set; }

        public  ICollection<RespostasUpdateRequest>? Respostas { get; set; }
    }
}
