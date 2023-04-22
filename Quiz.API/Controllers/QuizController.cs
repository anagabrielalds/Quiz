using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzes.API.Domain.DTO;
using Quizzes.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {

        private readonly QuizServices quizService;

        public QuizController(QuizServices quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet]
        public IEnumerable<QuizResponse> Get()
        {
            return quizService.ListarTodos();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var retorno = quizService.PesquisarPorId(id);

            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno);
            }
        }
        [HttpGet("nome/{nomeParam}")]
        public IActionResult GetByNome(string nomeParam) // nome do parametro deve ser o mesmo do {}
        {
            var retorno = quizService.PesquisarPorNome(nomeParam);

            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno);
            }
        }

        [HttpPost]
        // FromBody para indicar de o corpo da requisição deve ser mapeado para o modelo
        public IActionResult Post([FromBody] QuizRequest postModel)
        {
            //Validação modelo de entrada
            if (ModelState.IsValid)
            {
                var retorno = quizService.CadastrarNovo(postModel);
                if (!retorno.Sucesso)
                    return BadRequest(retorno.Mensagem);
                else
                    return Ok(retorno);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut("{id}")]
        // FromBody para indicar de o corpo da requisição deve ser mapeado para o modelo
        public IActionResult Put(int id, [FromBody] QuizUpdateRequest putModel)
        {
            //Validação modelo de entrada
            if (ModelState.IsValid)
            {
                var retorno = quizService.Editar(id, putModel);
                if (!retorno.Sucesso)
                    return BadRequest(retorno.Mensagem);
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("{id}")]
        // FromBody para indicar de o corpo da requisição deve ser mapeado para o modelo
        public IActionResult Delete(int id)
        {
            //Validação modelo de entrada
            var retorno = quizService.Deletar(id);
            if (!retorno.Sucesso)
                return BadRequest(retorno.Mensagem);
            return Ok();

        }

    }
}

