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
            Pergunta = quiz.Pergunta;
            Tema = quiz.Tema.TemaDescription;
            Respostas = quiz.Respostas?.Select(x => new RespostaQuizResponse(x)).ToList();
         }

        public int Id { get; set; }

        public string Pergunta { get; set; }

        public string Tema { get; set; }
        public ICollection<RespostaQuizResponse> Respostas { get; set; }
    }
}
