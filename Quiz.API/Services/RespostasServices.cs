using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizzes.API.DAL;
using Quizzes.API.Domain.DTO;
using Quizzes.API.Domain.Entity;

namespace Quizzes.API.Services
{
    public class RespostasServices
    {

        private readonly AppDbContext _dbContext;
        public RespostasServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResponse<Respostas> CadastrarNovo(RespostasRequest model)
        {

            if (string.IsNullOrEmpty(model.IdPergunta.ToString()))
            {
                return new ServiceResponse<Respostas>("O IdPergunta é obrigatório");
            }

            var novaResposta = new Respostas()
            {
                IdPergunta = model.IdPergunta,
                Descricao = model.Descricao,
                EhCorreta = model.EhCorreta,
            };

            _dbContext.Add(novaResposta);
            _dbContext.SaveChanges();

            return new ServiceResponse<Respostas>(novaResposta);
        }

        public IEnumerable<RespostasResponse> ListarTodos()
        {

            var retornoDoBanco = _dbContext.Respostas.ToList();

            // Conveter para TemaResponse
            IEnumerable<RespostasResponse> lista = retornoDoBanco.Select(x => new RespostasResponse(x));

            return lista;
        }

        public ServiceResponse<RespostasResponse> PesquisarPorId(int id)
        {
            var resultado = _dbContext.Respostas.FirstOrDefault(x => x.Id == id);
            if (resultado == null)
                return new ServiceResponse<RespostasResponse>("Não encontrado!");
            else
                return new ServiceResponse<RespostasResponse>(new RespostasResponse(resultado));

        }
        public ServiceResponse<RespostasResponse> PesquisarPorIdPergunta(int idPergunta)
        {
            var resultado = _dbContext.Respostas.FirstOrDefault(x => x.IdPergunta == idPergunta);
            if (resultado == null)
                return new ServiceResponse<RespostasResponse>("Não encontrado!");
            else
                return new ServiceResponse<RespostasResponse>(new RespostasResponse(resultado));

        }

        public ServiceResponse<Respostas> Editar(int idPergunta, RespostasUpdateRequest res)
        {
            var resultado = _dbContext.Respostas.FirstOrDefault(x => x.Id == res.Id);

            if (resultado == null)
                return new ServiceResponse<Respostas>("Resposta não encontrada para a pergunta " + resultado?.Perguntas.Pergunta + " !");
            if (resultado.IdPergunta != idPergunta)
                return new ServiceResponse<Respostas>("o IdPergunta não pode ser alterado");


            resultado.EhCorreta = res.EhCorreta;
            resultado.Descricao = res.Descricao;

            _dbContext.Respostas.Add(resultado).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return new ServiceResponse<Respostas>(resultado);
        }

        public ServiceResponse<bool> Deletar(int id)
        {
            var resultado = _dbContext.Respostas.FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<bool>("Resposta não encontrada!");

            _dbContext.Respostas.Remove(resultado);
            _dbContext.SaveChanges();

            return new ServiceResponse<bool>(true);
        }
    }
}
