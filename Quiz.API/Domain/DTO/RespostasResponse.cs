using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class RespostasResponse
    {
        public RespostasResponse(Respostas resposta)
        {
            Id = resposta.Id;
            IdPergunta = resposta.IdPergunta;
            Descricao = resposta.Descricao;
            EhCorreta = resposta.EhCorreta;
        }
        public int Id { get; set; }

        public int IdPergunta { get; set; }

        public string Descricao { get; set; }

        public bool EhCorreta { get; set; }
    }
}
