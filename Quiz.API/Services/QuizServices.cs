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
        public QuizServices(AppDbContext dbContext, RespostasServices respostas)
        {
            _dbContext = dbContext;
            _respostas = respostas;

        }

        public ServiceResponse<QuizResponse> CadastrarNovo(QuizRequest model)
        {
            //Regra
            if (model.IdTema == 0)
            {
                return new ServiceResponse<QuizResponse>("Tema é obirgatório");
            }
            if (string.IsNullOrWhiteSpace(model.Pergunta))
            {
                return new ServiceResponse<QuizResponse>("Pergunta é obirgatória");
            }
            if (model.Respostas?.Where( r => r.EhCorreta == true).ToList().Count > 1)
            {
                return new ServiceResponse<QuizResponse>("Apenas 1 Respostas pode ser verdadeira");
            }

            //tudo certo, só cadastrar
            var novoQuiz= new Quiz()
            {
                Pergunta = model.Pergunta,
                IdTema = model.IdTema,
            };

            _dbContext.Add(novoQuiz);
            _dbContext.SaveChanges();

            if(model.Respostas?.Count > 0 || model.Respostas != null)
            {
               foreach(var item in model.Respostas)
                {
                    var novaResposta = new RespostasRequest()
                    {
                        IdQuiz = novoQuiz.Id,
                        Descricao = item.Descricao,
                        EhCorreta = item.EhCorreta,
                    };
                    
                    _respostas.CadastrarNovo(novaResposta);

                }
            }

            var resultado = PesquisarPorId(novoQuiz.Id).ObjetoRetorno;

            return new ServiceResponse<QuizResponse>(resultado);

        }

        public IEnumerable<QuizResponse> ListarTodos()
        {
            var retornoDoBanco = _dbContext.Quiz.Include(x => x.Tema).Include(x => x.Respostas).ToList();

            IEnumerable<QuizResponse> lista = retornoDoBanco.Select(x => new QuizResponse(x));

            return lista;
        }


        public ServiceResponse<QuizResponse> PesquisarPorId(int id)
        {
            var resultado = _dbContext.Quiz.Include(x => x.Tema).Include(x => x.Respostas).FirstOrDefault(x => x.Id == id);
            if (resultado == null)
                return new ServiceResponse<QuizResponse>("Não encontrado!");
            else
                return new ServiceResponse<QuizResponse>(new  QuizResponse(resultado));

        }

        public ServiceResponse<IEnumerable<QuizResponse>> PesquisarPorNome(string nome)
        {
            var resultado = _dbContext.Quiz
                            .Include(x => x.Tema)
                            .Include(x => x.Respostas)
                            .Where(x => x.Pergunta.Contains(nome))
                            .ToList();

            if (resultado == null)
                return new ServiceResponse<IEnumerable<QuizResponse>>("Não encontrado!");
            else
                return new ServiceResponse<IEnumerable<QuizResponse>>(resultado.Select(x => new QuizResponse(x)));
        }

        public ServiceResponse<QuizResponse> Editar(int id, QuizUpdateRequest model)
        {
            var resultado = _dbContext.Quiz.FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<QuizResponse>("Quiz não encontrado!");

            resultado.IdTema = model.IdTema;
            resultado.Pergunta = model.Pergunta;

            _dbContext.Quiz.Add(resultado).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return new ServiceResponse<QuizResponse>(new QuizResponse(resultado));
        }

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
