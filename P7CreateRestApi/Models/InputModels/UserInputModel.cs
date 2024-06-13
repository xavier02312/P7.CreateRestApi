using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models.InputModel
{
    public class UserInputModel
    {
        [Required(ErrorMessage = "Le champs UserName est requis")]
        [MinLength(4, ErrorMessage = "Le champs UserName doit avoir au moins 4 caractères")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Le champs Password est requis")]
        [MinLength(8, ErrorMessage = "Le champs Password doit avoir au moins 8 caractères")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s]).*$", ErrorMessage = "Le champs Password doit contenir au moins une lettre majuscule, un chiffre et un symbole")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Le champs FullName est requis")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Le champs Role est requis")]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "Le rôle doit être 'Admin' ou 'User'")]
        public string Role { get; set; }
    }
}


