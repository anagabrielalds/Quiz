namespace Quizzes.API.Domain.DTO
{
    public class TemaRequest
    {
        public string TemaDescription { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
