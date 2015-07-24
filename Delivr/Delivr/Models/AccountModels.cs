using System;
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
        [Display(ResourceType = typeof(Resources.Account), Name = "UserName")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Account), Name = "CurrentPassword")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Account), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "ConfirmPasswordMismatch")]
        [Display(ResourceType = typeof(Resources.Account), Name = "ConfirmNewPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(ResourceType = typeof(Resources.Account), Name = "EmailAddress")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Account), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(Resources.Account), Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class EditUserModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "PhoneNumberFormatError")]
        [Display(ResourceType = typeof(Resources.Account), Name = "PhoneNumber")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        [Display(ResourceType = typeof(Resources.General), Name = "StreetName")]
        public string Rue { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.General), Name = "CivicNumber")]
        public int? NumeroCivique { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "PostalCodeFormatError")]
        [Display(ResourceType = typeof(Resources.General), Name = "PostalCode")]
        public string CodePostale { get; set; }

        public virtual ICollection<Adresse> Adresses { get; set; }
    }

    public class AddAdresseModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        [Display(ResourceType = typeof(Resources.General), Name = "StreetName")]
        public string Rue { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.General), Name = "CivicNumber")]
        public int? NumeroCivique { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "PostalCodeFormatError")]
        [Display(ResourceType = typeof(Resources.General), Name = "PostalCode")]
        public string CodePostale { get; set; }
    }


    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "EmailAddressError", ErrorMessage = null)]
        [Display(ResourceType = typeof(Resources.Account), Name = "EmailAddress")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        [Display(ResourceType = typeof(Resources.Account), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "ConfirmPasswordMismatch")]
        [Display(ResourceType = typeof(Resources.Account), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Account), Name = "FirstName")]
        public string Prenom { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Account), Name = "LastName")]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "PhoneNumberFormatError")]
        [Display(ResourceType = typeof(Resources.Account), Name = "PhoneNumber")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        [Display(ResourceType = typeof(Resources.General), Name = "StreetName")]
        public string Rue { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.General), Name = "CivicNumber")]
        public int? NumeroCivique { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[A-Z]\d[A-Z] \d[A-Z]\d$", ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "PostalCodeFormatError")]
        [Display(ResourceType = typeof(Resources.General), Name = "PostalCode")]
        public string CodePostale { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "DateFormatError")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(ResourceType = typeof(Resources.Account), Name = "BirthDate")]
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
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "EmailAddressError", ErrorMessage = null)]
        [Display(ResourceType = typeof(Resources.Account), Name = "EmailAddress")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        [Display(ResourceType = typeof(Resources.Account), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "ConfirmPasswordMismatch")]
        [Display(ResourceType = typeof(Resources.Account), Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Account), Name = "FirstName")]
        public string Prenom { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.Account), Name = "LastName")]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(Resources.Account), ErrorMessageResourceName = "PhoneNumberFormatError")]
        [Display(ResourceType = typeof(Resources.Account), Name = "PhoneNumber")]
        public string Telephone { get; set; }

        [Display(ResourceType = typeof(Resources.Account), Name = "Restaurant")]
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
