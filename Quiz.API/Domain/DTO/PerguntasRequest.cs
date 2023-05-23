using System.Reflection.Metadata.Ecma335;

namespace Quizzes.API.Domain.DTO
{
    public class PerguntasRequest
    {
        public int IdQuiz { get; set; }
        public string Pergunta { get; set; }
        public List<RespostasRequest> Respostas { get; set; }
    }
}
