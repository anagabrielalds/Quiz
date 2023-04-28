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

        public string Imagem { get; set; }

        public string getFile(byte[]? bytes)
        {
            //if(bytes.Length > 0)
            //{
            //    return new FileContentResult(bytes, "application/octet-stream");
            //}
            //return null;
            return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(bytes));
        }
    }
}
