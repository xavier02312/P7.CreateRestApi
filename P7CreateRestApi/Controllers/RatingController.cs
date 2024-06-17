using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Services;
using Serilog;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        // Service de gestion des opérations CRUD
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService) 
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize(policy: "User")]
        public IActionResult Home()
        {
            Log.Information("Récupération de la liste des 'Rating'");
            try
            {
                return Ok(_ratingService.List());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération des listes 'Rating'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(policy: "User")]
        public IActionResult Get([FromRoute] int id)
        {
            Log.Information("Récupération de 'Rating' avec l'id : {id}", id);
            try
            {
                var ratingService = _ratingService.Get(id);
                if (ratingService is not null)
                {
                    return Ok(ratingService);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'Rating' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("add")]
        [Authorize(policy: "User")]
        public IActionResult AddRating([FromBody] RatingInputModel inputModel)
        {
            Log.Information("Ajout d'un 'Rating'");
            try
            {
                return Ok(_ratingService.Create(inputModel));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de l'ajout d'un 'Rating'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpGet]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult ShowUpdateForm(int id)
        {
            Log.Information("Récupération sur la route 'update/id' de 'Rating' avec l'id : {id}", id);
            try
            {
                var rating = _ratingService.Get(id);
                if (rating is not null)
                {
                    return Ok(rating);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'Rating' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPost]
        [Route("update/{id}")]
        [Authorize(policy: "User")]
        public IActionResult UpdateById([FromRoute] int id, [FromBody] RatingInputModel inputModel)
        {
            Log.Information("Mise à jour de 'Rating' avec l'id : {id}", id);
            try
            {
                var rating = _ratingService.Update(id, inputModel);
                if (rating is not null)
                {
                    return Ok(_ratingService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la mise à jour de 'Rating' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(policy: "User")]
        public IActionResult DeleteRating([FromRoute] int id)
        {
            Log.Information("Suppression de 'Rating' avec l'id : {id}", id);
            try
            {
                var rating = _ratingService.Delete(id);
                if (rating is not null)
                {
                    return Ok(_ratingService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la suppression de 'Rating' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }
    }
}