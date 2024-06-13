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
    public class CurveController : ControllerBase
    {
        // Service de gestion des opérations CRUD
        private readonly ICurvePointService _curvePointService;

        public CurveController(ICurvePointService curvePointService) 
        {
            _curvePointService = curvePointService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize(policy: "User")]
        public IActionResult Home()
        {
            Log.Information("Récupération de la liste des 'Curve'");
            try
            {
                return Ok(_curvePointService.List());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Une erreur s'est produite lors de la récupération de la liste des 'Curve'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(policy: "User")]
        public IActionResult Get([FromRoute] int id)
        {
            Log.Information("Récupération de 'Curve' avec l'id : {id}", id);
            try
            {
                var curvePoint = _curvePointService.Get(id);
                if (curvePoint is not null)
                {
                    return Ok(curvePoint);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'Curve' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add")]
        [Authorize(policy: "User")]
        public IActionResult AddCurvePoint([FromBody] CurvePointInputModel inputModel)
        {
            Log.Information("Ajout d'une 'Curve'");
            try
            {
                return Ok(_curvePointService.Create(inputModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Une erreur s'est produite lors de l'ajout d'une 'Curve'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult ShowUpdateForm(int id)
        {
            Log.Information("Récupération sur la route 'update/id' de 'Curve' avec l'id : {id}", id);
            try
            {
                var curvePoint = _curvePointService.Get(id);
                if (curvePoint is not null)
                {
                    return Ok(curvePoint);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'Curve' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult UpdateById([FromRoute] int id, [FromBody] CurvePointInputModel inputModel)
        {
            Log.Information("Mise à jour de 'Curve' avec l'id : {id}", id);
            try
            {
                var curvePoint = _curvePointService.Update(id, inputModel);
                if (curvePoint is not null)
                {
                    return Ok(_curvePointService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la mise à jour de 'Curve' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(policy: "User")]
        public IActionResult DeleteById(int id)
        {
            Log.Information("Suppression de 'Curve' avec l'id : {id}", id);
            try
            {
                var curvePoint = _curvePointService.Delete(id);
                if (curvePoint is not null)
                {
                    return Ok(_curvePointService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la suppression de 'Curve' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }
    }
}