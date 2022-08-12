// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace QS.Authentication
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiResource> ApiResoprces =>
            new ApiResource[]
            {
                new ApiResource("roles", new [] { JwtClaimTypes.Role })
                {
                    Scopes = { "qs" }
                },

                new ApiResource("fullName", new[] { JwtClaimTypes.Name })
                {
                    Scopes = { "qs" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("qs")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                    {
                        ClientId = "postman",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireClientSecret = false,
                        AllowedCorsOrigins = { "https://oauth.pstmn.io" },
                        AllowedScopes = { "qs" },
                        RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                        PostLogoutRedirectUris = { "https://oauth.pstmn.io/v1/callback/" },
                        Enabled = true
                    }
            };
    }
}