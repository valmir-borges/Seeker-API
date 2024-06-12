using Api.Models;
using Api.Repositorios;
using Api.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservacoesController : ControllerBase
    {
        private readonly IObservacoesRepositorio _observacoesRepositorio;

        public ObservacoesController(IObservacoesRepositorio observacoesRepositorio)
        {
            _observacoesRepositorio = observacoesRepositorio;
        }

        [HttpGet("GetAllObservacoes")]
        public async Task<ActionResult<List<ObservacoesModel>>> GetAllObservacoes()
        {
            List<ObservacoesModel> observacoes = await _observacoesRepositorio.GetAll();
            return Ok(observacoes);
        }

        [HttpGet("GetObservacaoId/{id}")]
        public async Task<ActionResult<ObservacoesModel>> GetObservacaoId(int id)
        {
            ObservacoesModel observacao = await _observacoesRepositorio.GetById(id);
            return Ok(observacao);
        }

        [HttpPost("CreateObservacao")]
        public async Task<ActionResult<ObservacoesModel>> InsertObservacao([FromBody] ObservacoesModel observacoesModel)
        {
            ObservacoesModel observacao = await _observacoesRepositorio.InsertObservacao(observacoesModel);
            return Ok(observacao);
        }

        [HttpPut("UpdateObservacao/{id:int}")]
        public async Task<ActionResult<ObservacoesModel>> UpdateObservacao(int id, [FromBody] ObservacoesModel observacoesModel)
        {
            observacoesModel.ObservacoesId = id;
            ObservacoesModel observacao = await _observacoesRepositorio.UpdateObservacao(observacoesModel, id);
            return Ok(observacao);
        }

        [HttpDelete("DeleteObservacao/{id:int}")]

        public async Task<ActionResult<ObservacoesModel>> DeleteObservacao(int id)
        {
            bool deleted = await _observacoesRepositorio.DeleteObservacao(id);
            return Ok(deleted);
        }
    }
}
