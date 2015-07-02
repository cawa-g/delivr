using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivr.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        public string Nom { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }

    [Table("MenuItem")]
    public class MenuItem
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int Prix { get; set; }
        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; }
    }

    public class EditMenuModel
    {
        [ScaffoldColumn(false)]
        public int? MenuId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        public string Nom { get; set; }

        [ScaffoldColumn(false)]
        public int RestaurantId { get; set; }

        [Display(ResourceType = typeof(Resources.Menu), Name = "MenuItems")]
        public ICollection<EditMenuItemModel> MenuItemModels { get; set; }
    }

    public class EditMenuItemModel
    {
        [ScaffoldColumn(false)]
        public int? MenuItemId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessageResourceType = typeof(Resources.General), ErrorMessageResourceName = "StringLengthError")]
        public string Nom { get; set; }

        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0, 1000, ErrorMessageResourceType = typeof(Resources.Menu), ErrorMessageResourceName = "PriceErrorMessage")]
        public decimal Prix { get; set; }
    }
}