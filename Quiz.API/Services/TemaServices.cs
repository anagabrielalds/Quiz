using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Quizzes.API.DAL;
using Quizzes.API.Domain.DTO;
using Quizzes.API.Domain.Entity;
using System.Collections;

namespace Quizzes.API.Services
{
    public class TemaServices
    {

        private readonly AppDbContext _dbContext;
        public TemaServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResponse<TemaResponse> CadastrarNovo(TemaRequest model)
        {

            if (string.IsNullOrEmpty(model.TemaDescription))
            {
                return new ServiceResponse<TemaResponse>("A descrição é obrigatória");
            }

            var novoTema = new Tema()
            {
                TemaDescription = model.TemaDescription
            };

            _dbContext.Add(novoTema);
            _dbContext.SaveChanges();

            return new ServiceResponse<TemaResponse>(new TemaResponse(novoTema));
        }

        public IEnumerable<TemaResponse> ListarTodos()
        {
      
            var retornoDoBanco = _dbContext.Tema.ToList();

            // Conveter para TemaResponse
            IEnumerable<TemaResponse> lista = retornoDoBanco.Select(x => new TemaResponse(x));

            return lista;
        }

        public ServiceResponse<TemaResponse> PesquisarPorId(int id)
        {
            var resultado = _dbContext.Tema.FirstOrDefault(x => x.Id == id);
            if (resultado == null)
                return new ServiceResponse<TemaResponse>("Não encontrado!");
            else
                return new ServiceResponse<TemaResponse>(new TemaResponse(resultado));

        }

        public ServiceResponse<IEnumerable<TemaResponse>> PesquisarPorNome(string nome)
        {

            var consulta = _dbContext.Tema.Where(x => x.TemaDescription.Contains(nome)).ToList();

            if (consulta == null || consulta.Count == 0)
                return new ServiceResponse<IEnumerable<TemaResponse>>("Não encontrado!");
            else
            {
                IEnumerable<TemaResponse> resultado = consulta.Select(x => new TemaResponse(x));
                return new ServiceResponse<IEnumerable<TemaResponse>>(resultado);

            }
        }

        public ServiceResponse<Tema> Editar(int id, TemaRequest model)
        {
            var resultado = _dbContext.Tema.FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<Tema>("Album não encontrado!");

            resultado.TemaDescription = model.TemaDescription;
            _dbContext.Tema.Add(resultado).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return new ServiceResponse<Tema>(resultado);
        }

        public ServiceResponse<bool> Deletar(int id)
        {
            var resultado = _dbContext.Tema.FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<bool>("Album não encontrado!");

            _dbContext.Tema.Remove(resultado);
            _dbContext.SaveChanges();

            return new ServiceResponse<bool>(true);
        }
    }
}
