using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Delivr.Models
{
    [Table("Restaurateur")]
    public class Restaurateur : UserProfile
    {
        public Restaurateur()
        {
            this.Restaurants = new List<Restaurant>();
        }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }

    public class CreateModel
    {
        [Required]
        [Display(Name = "Adresse courriel")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Adresse courriel invalide")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Téléphone invalide, utilisez le format xxx-xxx-xxxx")]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Rue")]
        public string Rue { get; set; }

        [Required]
        [Display(Name = "Code Civique")]
        public int NumeroCivique { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessage = "Code postale invalide, utilisez le format H1H 1H1")]
        [Display(Name = "Code Postale")]
        [DataType(DataType.PostalCode)]
        public string CodePostale { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date, ErrorMessage = "Date invalide, utilisez le format JJ/MM/AAAA"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateNaissance { get; set; }

        public int? restaurantId { get; set; }
    }
}