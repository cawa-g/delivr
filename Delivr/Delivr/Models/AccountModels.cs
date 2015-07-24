﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Delivr.Models
{
   

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public int? AdresseDefaultId { get; set; }
        public DateTime? DateNaissance { get; set; }

        public UserProfile()
        {
            this.Restaurants = new List<Restaurant>();
            this.Adresses = new List<Adresse>();
        }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Adresse> Adresses { get; set; }


    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Adresse courriel")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }

    public class EditUserModel 
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Téléphone non valide, utilisez le format xxx-xxx-xxxx")]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Rue")]
        public string Rue { get; set; }

        [Required]
        [Display(Name = "Code Civique")]
        public int? CodeCivique { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessage = "Code postale non valide, utilisez le format H1H 1H1")]
        [Display(Name = "Code Postale")]
        [DataType(DataType.PostalCode)]
        public string CodePostale { get; set; }

        public virtual ICollection<Adresse> Adresses { get; set; }
    }

    public class AddAdresseModel
    {

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 3)]
        [Display(Name = "Rue")]
        public string Rue { get; set; }

        [Required]
        [Display(Name = "Code Civique")]
        public int? CodeCivique { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessage = "Code postale non valide, utilisez le format H1H 1H1")]
        [Display(Name = "Code Postale")]
        [DataType(DataType.PostalCode)]
        public string CodePostale { get; set; }
    }


    public class RegisterModel
    {
        [Required]
        [Display(Name = "Adresse courriel" )]
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
        public int? CodeCivique { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessage = "Code postale invalide, utilisez le format H1H 1H1")]
        [Display(Name = "Code Postale")]
        [DataType(DataType.PostalCode)]
        public string CodePostale { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date, ErrorMessage = "Date invalide, utilisez le format JJ/MM/AAAA"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateNaissance { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    public class RestaurateurModel
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

        public int? restaurantId { get; set; }
    }

    public class EditRestaurateurModel
    {
        public int UserId { get; set; }

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

    }


}
