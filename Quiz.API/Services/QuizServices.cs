using Microsoft.EntityFrameworkCore;
using Quizzes.API.DAL;
using Quizzes.API.Domain.DTO;
using Quizzes.API.Domain.Entity;
using Quizzes.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizzes.API.Services
{
    public class QuizServices
    {
        private readonly AppDbContext _dbContext;
        public  RespostasServices _respostas;
        public PerguntasServices _perguntas;
        public QuizServices(AppDbContext dbContext, RespostasServices respostas, PerguntasServices perguntas)
        {
            _dbContext = dbContext;
            _respostas = respostas;
            _perguntas = perguntas;
        }

        public ServiceResponse<QuizResponse> CadastrarNovo(QuizRequest model)
        {
            //Regra
            if (model.IdTema == 0)
            {
                return new ServiceResponse<QuizResponse>("Tema é obirgatório");
            }
            if (string.IsNullOrEmpty(model.Titulo))
            {
                return new ServiceResponse<QuizResponse>("Titulo é obirgatória");
            }
            

            //tudo certo, só cadastrar
            var novoQuiz= new Quiz()
            {
                Titulo = model.Titulo,
                IdTema = model.IdTema,
            };

            _dbContext.Add(novoQuiz);
            _dbContext.SaveChanges();

            if (model.Questions?.Count > 0 || model.Questions != null)
            {
                foreach (var item in model.Questions)
                {
                    var novaPergunta = new PerguntasRequest()
                    {
                        IdQuiz = novoQuiz.Id,
                        Pergunta = item.Pergunta,
                    };

                    var retornoPerguntas = _perguntas.CadastrarNovo(novaPergunta);

                    foreach(var res in item.Respostas)
                    {
                        res.IdPergunta = retornoPerguntas.ObjetoRetorno.Id;
                        _respostas.CadastrarNovo(res);

                    }

                }
            }

            var resultado = PesquisarPorId(novoQuiz.Id).ObjetoRetorno;

            return new ServiceResponse<QuizResponse>(resultado);

        }

        public IEnumerable<QuizResponse> ListarTodos()
        {
            List<Quiz> retornoDoBanco = (List<Quiz>)_dbContext.Quiz.Include(x => x.Tema).Include(x => x.Perguntas)
                .ThenInclude(p => p.Respostas)
                .ToList();
                

 
            IEnumerable<QuizResponse> lista = retornoDoBanco.Select(x => new QuizResponse(x));

            return lista;
        }


        public ServiceResponse<QuizResponse> PesquisarPorId(int id)
        {
            var resultado = _dbContext.Quiz
                .Include(x => x.Tema)
                .Include(x => x.Perguntas)
                .ThenInclude(p => p.Respostas)
                .FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<QuizResponse>("Não encontrado!");
            else
                return new ServiceResponse<QuizResponse>(new QuizResponse(resultado));

        }

        public ServiceResponse<IEnumerable<QuizResponse>> PesquisarPorIdTema(int idTema)
        {
            var resultado = _dbContext.Quiz
                .Include(x => x.Tema)
                .Include(x => x.Perguntas)
                .ThenInclude(p => p.Respostas)
                .Where(x => x.Tema.Id == idTema)
                .ToList();

            if (resultado == null)
                return new ServiceResponse<IEnumerable<QuizResponse>>("Não encontrado!");
            else return new ServiceResponse<IEnumerable<QuizResponse>>(resultado.Select(x => new QuizResponse(x)));

        }

        public ServiceResponse<IEnumerable<QuizResponse>> PesquisarPorNome(string nome)
        {
            List<Quiz> resultado = _dbContext.Quiz
                       .Include(x => x.Tema)
                       .Include(x => x.Perguntas)
                       .ThenInclude(p => p.Respostas)
                       .Where(x => x.Titulo.Contains(nome))
                       .ToList();

            if (resultado == null || resultado?.Count == 0)
                return new ServiceResponse<IEnumerable<QuizResponse>>("Não encontrado!");
            else return new ServiceResponse<IEnumerable<QuizResponse>>(resultado.Select(x => new QuizResponse(x)));
        }

        //public ServiceResponse<QuizResponse> Editar(int id, QuizUpdateRequest model)
        //{
        //    var resultado = _dbContext.Quiz.FirstOrDefault(x => x.Id == id);

        //    if (resultado == null)
        //        return new ServiceResponse<QuizResponse>("Quiz não encontrado!");

        //    resultado.IdTema = model.IdTema;
        //    resultado.Perguntas = model.Perguntas;

        //    _dbContext.Quiz.Add(resultado).State = EntityState.Modified;
        //    _dbContext.SaveChanges();

        //    return new ServiceResponse<QuizResponse>(new QuizResponse(resultado));
        //}

        public ServiceResponse<bool> Deletar(int id)
        {
            var resultado = _dbContext.Quiz.FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<bool>("Quiz não encontrado!");

            _dbContext.Quiz.Remove(resultado);
            _dbContext.SaveChanges();

            return new ServiceResponse<bool>(true);
        }

    }
}
