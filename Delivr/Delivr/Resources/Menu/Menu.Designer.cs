﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Delivr.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Menu {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Menu() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Delivr.Resources.Menu.Menu", typeof(Menu).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ajouter un élément.
        /// </summary>
        public static string AddItem {
            get {
                return ResourceManager.GetString("AddItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Retour aux Restaurants.
        /// </summary>
        public static string BackToRestaurants {
            get {
                return ResourceManager.GetString("BackToRestaurants", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aucune description n&apos;a été fournie..
        /// </summary>
        public static string DescriptionMissingWarning {
            get {
                return ResourceManager.GetString("DescriptionMissingWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Définition du menu complétée avec succès..
        /// </summary>
        public static string MenuDefinitionSuccessMessage {
            get {
                return ResourceManager.GetString("MenuDefinitionSuccessMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Éléments du menu.
        /// </summary>
        public static string MenuItems {
            get {
                return ResourceManager.GetString("MenuItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Définition du menu.
        /// </summary>
        public static string PageTitle {
            get {
                return ResourceManager.GetString("PageTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Le prix doit être un nombre positif non nul inférieur à {2}..
        /// </summary>
        public static string PriceErrorMessage {
            get {
                return ResourceManager.GetString("PriceErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Retirer l&apos;élément.
        /// </summary>
        public static string RemoveItem {
            get {
                return ResourceManager.GetString("RemoveItem", resourceCulture);
            }
        }
    }
}
