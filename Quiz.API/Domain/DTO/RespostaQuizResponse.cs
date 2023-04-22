using Quizzes.API.Domain.Entity;

namespace Quizzes.API.Domain.DTO
{
    public class RespostaQuizResponse
    {
        public RespostaQuizResponse(Respostas resposta)
        {
            Descricao = resposta.Descricao;
            EhCorreta = resposta.EhCorreta;
        }
        public string Descricao { get; set; }


        public bool EhCorreta { get; set; }
    }
}
