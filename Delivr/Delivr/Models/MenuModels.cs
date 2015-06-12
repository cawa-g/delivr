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
        [Required]
        [StringLength(50, ErrorMessage = "La chaîne {0} doit comporter entre {2} et {1} caractères.", MinimumLength = 4)]
        public string Nom { get; set; }
        public int RestaurantId { get; set; }

        public ICollection<EditMenuItemModel> MenuItemModels { get; set; }

        /// <summary>
        /// Builds a Menu edit model from a Menu object.
        /// </summary>
        /// <param name="menu">The Menu this form model represents.</param>
        public EditMenuModel(Menu menu)
        {
            Nom = menu.Nom;
            RestaurantId = menu.RestaurantId;

            MenuItemModels = new List<EditMenuItemModel>();
            foreach (MenuItem item in menu.MenuItems)
            {
                MenuItemModels.Add(new EditMenuItemModel(item));
            }
        }

        /// <summary>
        /// Builds and empty Menu edit model with the given Restaurant ID.
        /// </summary>
        /// <param name="restaurantId">The Restaurant ID the menu is created for.</param>
        public EditMenuModel(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        /// <summary>
        /// Builds an empty Menu edit model.
        /// </summary>
        public EditMenuModel()
        { }
    }

    public class EditMenuItemModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La chaîne {0} doit comporter entre {2} et {1} caractères.")]
        public string Nom { get; set; }

        public string Description { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0, 1000, ErrorMessage = "Le prix doit être un nombre positif inférieur à 1000.")]
        public decimal Prix { get; set; }

        /// <summary>
        /// Builds a MenuItem edit model from a MenuItem object.
        /// </summary>
        /// <param name="item">The MenuItem this form model represents.</param>
        public EditMenuItemModel(MenuItem item)
        {
            Nom = item.Nom;
            Description = item.Description;
            Prix = item.Prix;
        }

        /// <summary>
        /// Builds an empty MenuItem edit model.
        /// </summary>
        public EditMenuItemModel()
        { }
    }
}