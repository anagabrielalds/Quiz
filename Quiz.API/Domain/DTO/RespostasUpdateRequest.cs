namespace Quizzes.API.Domain.DTO
{
    public class RespostasUpdateRequest
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public bool EhCorreta { get; set; }
    }
}
