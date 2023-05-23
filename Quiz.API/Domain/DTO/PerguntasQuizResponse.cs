using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class PerguntasQuizResponse
    {
        public PerguntasQuizResponse(Perguntas perguntas)
        {
            Id = perguntas.Id;
            Pergunta= perguntas.Pergunta;
            Respostas = perguntas.Respostas?.Select(x => new RespostasResponse(x)).ToList();
        }

        public int Id { get; set; }
        public string Pergunta { get; set; }
        public  List<RespostasResponse>? Respostas { get; set; }
    }
}
