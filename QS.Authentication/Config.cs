﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace QS.Authentication
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("qsBaseWebApi")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                 new Client
                 {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "qsBaseWebApi" }
                 },

                  new Client
                    {
                        ClientId = "postman",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireClientSecret = false,
                        AllowedCorsOrigins = { "https://oauth.pstmn.io" },
                        AllowedScopes = { "qsBaseWebApi" },
                        RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                        PostLogoutRedirectUris = { "https://oauth.pstmn.io/v1/callback/" },
                        Enabled = true
                    }
            };
    }
}