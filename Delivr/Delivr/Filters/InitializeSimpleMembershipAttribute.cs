using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using Delivr.Models;

namespace Delivr.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // S'assurer qu'ASP.NET Simple Membership est initialisé une seule fois par démarrage d'application
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<DelivrContext>(null);

                try
                {
                    using (var context = new DelivrContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Créer la base de données SimpleMembership sans schéma de migration Entity Framework
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    

                    InitializeRoles();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Impossible d'initialiser la base de données ASP.NET Simple Membership. Pour plus d'informations, consultez la page http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            protected void InitializeRoles()
            {

                var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;
                var membership = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;

                if (!roles.RoleExists("Admin"))
                    roles.CreateRole("Admin");

                if (!roles.RoleExists("User"))
                    roles.CreateRole("User");

                if (!roles.RoleExists("Restaurateur"))
                    roles.CreateRole("Restaurateur");

               // System.Web.Security.Roles.AddUserToRole("admin@admin.com", "Admin");

            }
        }
    }
}
