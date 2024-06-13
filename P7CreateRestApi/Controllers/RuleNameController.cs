using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Services;
using Serilog;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        // Service de gestion des opérations CRUD
        private readonly IRuleNameService _ruleNameService;
        public RuleNameController(IRuleNameService ruleNameService)
        {
            _ruleNameService = ruleNameService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize(policy: "User")]
        public IActionResult Home()
        {
            Log.Information("Récupération de la liste des 'RuleName'");
            try
            {
                return Ok(_ruleNameService.List());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération des listes 'RuleName'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(policy: "User")]
        public IActionResult Get([FromRoute] int id)
        {
            Log.Information("Récupération de 'RuleName' avec l'id : {id}", id);
            try
            {
                var ruleName = _ruleNameService.Get(id);
                if (ruleName is not null)
                {
                    return Ok(ruleName);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'RuleName' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add")]
        [Authorize(policy: "User")]
        public IActionResult AddRuleName([FromBody] RuleNameInputModel inputModel)
        {
            Log.Information("Ajout d'une nouvelle 'RuleName'");
            try
            {
                return Ok(_ruleNameService.Create(inputModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de l'ajout d'une nouvelle 'RuleName'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult ShowUpdateForm(int id)
        {
            Log.Information("Récupération sur la route 'update/id' de 'RuleName' avec l'id : {id}", id);
            try
            {
                var ruleName = _ruleNameService.Get(id);
                if (ruleName is not null)
                {
                    return Ok(ruleName);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'RuleName' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult UpdateById([FromRoute] int id, [FromBody] RuleNameInputModel inputModel)
        {
            Log.Information("Mise à jour de 'RuleName' avec l'id : {id}", id);
            try
            {
                var ruleName = _ruleNameService.Update(id, inputModel);
                if (ruleName is not null)
                {
                    return Ok(_ruleNameService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la mise à jour de 'RuleName' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(policy: "User")]
        public IActionResult DeleteRuleName([FromRoute] int id)
        {
            Log.Information("Suppression de 'RuleName' avec l'id : {id}", id);
            try
            {
                var ruleName = _ruleNameService.Delete(id);
                if (ruleName is not null)
                {
                    return Ok(_ruleNameService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la suppression de 'RuleName' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }
    }
}