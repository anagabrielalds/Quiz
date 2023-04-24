using Microsoft.AspNetCore.Mvc;
using Quizzes.API.Domain.Entity;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Quizzes.API.Domain.DTO
{
    public class TemaResponse
    {
        public TemaResponse(Tema tema)
        {
            Id = tema.Id;
            TemaDescription = tema.TemaDescription;
            Imagem = getFile(tema.Imagem);
        }
        public int Id { get; set; }

        public string TemaDescription { get; set; }

        public FileContentResult Imagem { get; set; }

        public FileContentResult getFile(byte[]? bytes)
        {
            if(bytes.Length > 0)
            {
                return new FileContentResult(bytes, "application/octet-stream");
            }
            return null;
            
        }
    }
}
