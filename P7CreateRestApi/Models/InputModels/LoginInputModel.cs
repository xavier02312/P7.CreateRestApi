using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.InputModels
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Le champs UserName est requis")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Le champs Password est requis")]
        public string Password { get; set; }
    }
}
