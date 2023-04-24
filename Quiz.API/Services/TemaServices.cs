using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Quizzes.API.DAL;
using Quizzes.API.Domain.DTO;
using Quizzes.API.Domain.Entity;
using System.Collections;
using System.IO.Compression;

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

            if (model.Imagem == null)
            {
                return new ServiceResponse<TemaResponse>("A Imagem é obrigatória");
            }
            var extensionFile = Path.GetExtension(model.Imagem.FileName);

            var extensionValidas = new List<string>() { ".jpeg", ".png" , ".jpg"};

            if (!extensionValidas.Contains(extensionFile))
            {
                return new ServiceResponse<TemaResponse>("A extensão do arquivo não é válida. Extensões válidas: .jpeg e .png");
            }
            var tamanhoMaxUpload = 5242880;
            // Upload the file if less than 5 MB
            if (model.Imagem.Length > tamanhoMaxUpload)
            {
                return new ServiceResponse<TemaResponse>("O tamanho máximo permitido para o arquivo é: " + (tamanhoMaxUpload / 1048576) + "MB");
            }

            var novoTema = new Tema()
            {
                TemaDescription = model.TemaDescription,
                Imagem = GetBytesImagem(model.Imagem),
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

        public ServiceResponse<TemaResponse> Editar(int id, TemaRequest model)
        {
            var resultado = _dbContext.Tema.FirstOrDefault(x => x.Id == id);

            if (resultado == null)
                return new ServiceResponse<TemaResponse>("Tema não encontrado!");

            if (model.Imagem.Length > 0)
            {
                var extensionFile = Path.GetExtension(model.Imagem.FileName);

                var extensionValidas = new List<string>() { ".jpeg", ".png", ".jpg" };

                if (!extensionValidas.Contains(extensionFile))
                {
                    return new ServiceResponse<TemaResponse>("A extensão do arquivo não é válida. Extensões válidas: .jpeg e .png");
                }
                var tamanhoMaxUpload = 5242880;
                // Upload the file if less than 5 MB
                if (model.Imagem.Length > tamanhoMaxUpload)
                {
                    return new ServiceResponse<TemaResponse>("O tamanho máximo permitido para o arquivo é: " + (tamanhoMaxUpload / 1048576) + "MB");
                }
                resultado.Imagem = GetBytesImagem(model.Imagem);
            }
            if (string.IsNullOrWhiteSpace(model.TemaDescription))
                resultado.TemaDescription = model.TemaDescription;
            else
                resultado.TemaDescription = resultado.TemaDescription;

            _dbContext.Tema.Add(resultado).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return new ServiceResponse<TemaResponse>(new TemaResponse(resultado));
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
    
        public byte[] GetBytesImagem(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                 file.CopyToAsync(memoryStream);
                 
                return memoryStream.ToArray();
            }
        }
    }
}
