using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class QuizResponse
    {
         public QuizResponse(Quiz quiz)
         {
            Id = quiz.Id;
            Titulo = quiz.Titulo;
            Tema = quiz.Tema.TemaDescription;
            IdTema = quiz.IdTema;
            Questions = quiz?.Perguntas?.Select(x => new PerguntasQuizResponse(x)).ToList();
         }

        public int Id { get; set; }

        public string Tema { get; set; }

        public int IdTema { get; set; }

        public string Titulo { get; set; }
        public List<PerguntasQuizResponse>? Questions { get; set; }

    }
}
