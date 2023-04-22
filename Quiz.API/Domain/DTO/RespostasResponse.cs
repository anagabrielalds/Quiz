using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class RespostasResponse
    {
        public RespostasResponse(Respostas resposta)
        {
            Id = resposta.Id;   
            IdQuiz = resposta.IdQuiz;
            Descricao = resposta.Descricao;
            EhCorreta = resposta.EhCorreta;
        }
        public int Id { get; set; }

        public int IdQuiz { get; set; }

        public string Descricao { get; set; }

        public bool EhCorreta { get; set; }
    }
}
