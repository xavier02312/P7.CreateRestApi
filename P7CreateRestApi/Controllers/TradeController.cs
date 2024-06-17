using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Services;
using Serilog;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        // Service de gestion des opérations CRUD
        private readonly ITradeService _tradeService;
        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize(policy: "User")]
        public IActionResult Home()
        {
            Log.Information("Récupération de la liste des 'Trade'");
            try
            {
                return Ok(_tradeService.List());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération des listes 'Trade'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(policy: "User")]
        public IActionResult Get([FromRoute] int id)
        {
            Log.Information("Récupération de 'Trade' avec l'id : {id}", id);
            try
            {
                var trade = _tradeService.Get(id);
                if (trade is not null)
                {
                    return Ok(trade);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'Trade' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add")]
        [Authorize(policy: "User")]
        public IActionResult AddTrade([FromBody] TradeInputModel inputModel)
        {
            Log.Information("Ajout d'un 'Trade'");
            try
            {
                return Ok(_tradeService.Create(inputModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de l'ajout d'un 'Trade'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult ShowUpdateForm(int id)
        {
            Log.Information("Récupération sur la route 'update/id' de 'Trade' avec l'id : {id}", id);
            try
            {
                var ruleName = _tradeService.Get(id);
                if (ruleName is not null)
                {
                    return Ok(ruleName);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'Trade' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPut]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult UpdateById([FromRoute] int id, [FromBody] TradeInputModel inputModel)
        {
            Log.Information("Mise à jour de 'Trade' avec l'id : {id}", id);
            try
            {
                var trade = _tradeService.Update(id, inputModel);
                if (trade is not null)
                {
                    return Ok(_tradeService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la mise à jour de 'Trade' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(policy: "User")]
        public IActionResult DeleteTrade([FromRoute] int id)
        {
            Log.Information("Suppression de 'Trade' avec l'id : {id}", id);
            try
            {
                var trade = _tradeService.Delete(id);
                if (trade is not null)
                {
                    return Ok(_tradeService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la suppression de 'Trade' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }
    }
}