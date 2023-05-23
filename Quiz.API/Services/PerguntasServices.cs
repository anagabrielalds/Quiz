using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizzes.API.DAL;
using Quizzes.API.Domain.DTO;
using Quizzes.API.Domain.Entity;

namespace Quizzes.API.Services
{
    public class PerguntasServices
    {

        private readonly AppDbContext _dbContext;
        public PerguntasServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResponse<Perguntas> CadastrarNovo(PerguntasRequest model)
        {

            if (string.IsNullOrEmpty(model.Pergunta))
            {
                return new ServiceResponse<Perguntas>("A Pergunta é obrigatório");
            }
            if (string.IsNullOrEmpty(model.IdQuiz.ToString()))
            {
                return new ServiceResponse<Perguntas>("O idQuiz é obrigatório");
            }


            var novaPergunta = new Perguntas()
            {
                IdQuiz= model.IdQuiz,   
                Pergunta = model.Pergunta

            };

            _dbContext.Add(novaPergunta);
            _dbContext.SaveChanges();

            return new ServiceResponse<Perguntas>(novaPergunta);
        }


    }
}
