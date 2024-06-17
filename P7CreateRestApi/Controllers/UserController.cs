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
    public class UserController : ControllerBase
    {
        // Service de gestion des opérations CRUD
        private readonly IUserService _userService;

        public UserController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpGet]
        [Route("list")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> Home()
        {
            Log.Information("Récupération de la liste des 'User'");
            try
            {
                return Ok(await _userService.List());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération des listes 'User'");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }

        [HttpPost]
        [Route("add")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserInputModel inputModel)
        {
            Log.Information("Ajout d'un utilisateur");
            try
            {
                var user = await _userService.Create(inputModel);
                if (user is not null)
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de l'ajout d'un utilisateur");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("update/{id}")]
        [Authorize(policy: "Admin")]
        public IActionResult ShowUpdateForm(int id)
        {
            Log.Information("Récupération sur la route 'update/id' de 'User' avec l'id : {id}", id);
            try
            {
                var user = _userService.Get(id);
                if (user is not null)
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération de 'User' avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpPut]
        [Route("update/{id}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserInputModel inputModel)
        {
            Log.Information("Mise à jour de l'utilisateur avec l'id : {id}", id);
            try
            {
                var user = await _userService.Update(id, inputModel);
                if (user is not null)
                {
                    return Ok(await _userService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la mise à jour de l'utilisateur avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(policy: "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Log.Information("Suppression de l'utilisateur avec l'id : {id}", id);
            try
            {
                var user = await _userService.Delete(id);
                if (user is not null)
                {
                    return Ok(await _userService.List());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la suppression de l'utilisateur avec l'id : {id}", id);
                return StatusCode(500, "Une erreur interne s'est produite");
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/secure/article-details")]
        [Authorize(policy: "Admin")]
        public async Task<ActionResult<List<User>>> GetAllUserArticles()
        {
            Log.Information("Récupération de la liste des utilisateurs");
            try
            {
                return Ok(await _userService.List());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur lors de la récupération des listes des utilisateurs");
                return StatusCode(500, "Une erreur interne s'est produite");
            }
        }
    }
}