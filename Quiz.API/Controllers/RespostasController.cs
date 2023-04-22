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
    public class RespostasController : ControllerBase
    {

        private readonly RespostasServices respostasService;

        public RespostasController(RespostasServices respostasService)
        {
            this.respostasService = respostasService;
        }

        [HttpGet]
        public IEnumerable<RespostasResponse> Get()
        {
            return respostasService.ListarTodos();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var retorno = respostasService.PesquisarPorId(id);

            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno);
            }
        }

        [HttpGet("{idQuiz}")]
        public IActionResult GetByIdQuiz(int id)
        {
            var retorno = respostasService.PesquisarPorIdQuiz(id);

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
        public IActionResult Post([FromBody] RespostasRequest postModel)
        {
            //Validação modelo de entrada
            if (ModelState.IsValid)
            {
                var retorno = respostasService.CadastrarNovo(postModel);
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
        public IActionResult Put(int id, [FromBody] RespostasUpdateRequest putModel)
        {
            //Validação modelo de entrada
            if (ModelState.IsValid)
            {
                var retorno = respostasService.Editar(id, putModel);
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
            var retorno = respostasService.Deletar(id);
            if (!retorno.Sucesso)
                return BadRequest(retorno.Mensagem);
            return Ok();

        }

    }
}

