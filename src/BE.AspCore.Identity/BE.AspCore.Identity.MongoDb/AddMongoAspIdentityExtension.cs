// ==========================================================================
// AddMongoAspIdentityExtension.cs
// ==========================================================================
// All rights reserved.
// ==========================================================================

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BE.Identity
{
    public static class AddMongoAspIdentityExtension
    {
        public static IdentityBuilder AddMongoIdentityStores<TIdentity, TRole>(this IServiceCollection services, IMongoDatabase database) where TIdentity : IdentityUser where TRole : IdentityRole
        {
            services.AddSingleton<IUserStore<TIdentity>>(x =>
            {
                IMongoCollection<TIdentity> usersCollection = database.GetCollection<TIdentity>("Identity_Users");

                IndexChecks.EnsureUniqueIndexOnNormalizedEmail(usersCollection);
                IndexChecks.EnsureUniqueIndexOnNormalizedUserName(usersCollection);

                return new UserStore<TIdentity>(usersCollection);
            });

            services.AddSingleton<IRoleStore<TRole>>(x =>
            {
                var rolesCollection = database.GetCollection<TRole>("Identity_Roles");

                IndexChecks.EnsureUniqueIndexOnNormalizedRoleName(rolesCollection);

                return new RoleStore<TRole>(rolesCollection);
            });

            return services.AddIdentity<TIdentity, TRole>();

        }
    }
}