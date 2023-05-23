using Quizzes.API.Domain.DTO;
using Quizzes.API.Domain.Entity;

namespace Quizzes.API.Services
{
    public class QuizUpdateRequest
    {
        public ICollection<Perguntas>? Perguntas { get; set; }

        public string Titulo { get; set; }


        public int IdTema { get; set; }

    }
}
