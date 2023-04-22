using Quizzes.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace Quizzes.API.Domain.DTO
{
    public class TemaResponse
    {
        public TemaResponse(Tema tema)
        {
            Id = tema.Id;
            TemaDescription = tema.TemaDescription;
        }
        public int Id { get; set; }

        public string TemaDescription { get; set; }
    }
}
