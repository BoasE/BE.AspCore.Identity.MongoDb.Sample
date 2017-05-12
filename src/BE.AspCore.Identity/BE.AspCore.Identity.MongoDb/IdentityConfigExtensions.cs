// ==========================================================================
// IdentityConfigExtensions.cs
// ==========================================================================
// All rights reserved.
// ==========================================================================

using Microsoft.Extensions.Configuration;

namespace BE.IdentityServer
{
    public static class IdentityConfigExtensions
    {
        public const string Section = "Identity";

        public static string ReadAuthority(this IConfigurationRoot configuration)
        {
            return configuration[$"{Section}:Authority"];
        }
    }
}