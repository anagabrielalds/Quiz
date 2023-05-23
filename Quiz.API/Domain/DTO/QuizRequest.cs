using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class QuizRequest
    {
        public string Titulo { get; set; }

        public int IdTema { get; set; }

        public List<PerguntasRequest> Questions { get; set; }
    }
}
