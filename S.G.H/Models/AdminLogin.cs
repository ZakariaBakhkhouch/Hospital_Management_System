using System.ComponentModel.DataAnnotations;

namespace S.G.H.Models
{
    public class AdminLogin
    {


        [Required(ErrorMessage = "* Le champ Mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name ="Enter Password")]
        public string Password { get; set; }



        [Required(ErrorMessage = "* Le champ Email est obligatoire")]
        [EmailAddress]
        [Display(Name ="Enter Email")]
        public string Email { get; set; }



    }
}
