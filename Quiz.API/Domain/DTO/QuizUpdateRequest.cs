using Quizzes.API.Domain.Entity;

namespace Quizzes.API.Domain.DTO
{
    public class QuizUpdateRequest
    {
        public string Titulo { get; set; }

        public int IdTema { get; set; }

        public List<PerguntasUpdateRequest>? Questions { get; set; }

    }
}
