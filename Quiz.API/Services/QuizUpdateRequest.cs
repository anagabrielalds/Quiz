using Quizzes.API.Domain.DTO;

namespace Quizzes.API.Services
{
    public class QuizUpdateRequest
    {
        public string Pergunta { get; set; }

        public int IdTema { get; set; }

    }
}
