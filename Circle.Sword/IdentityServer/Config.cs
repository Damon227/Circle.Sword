using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        // Scope定义，定义要保护的Api资源
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "Circle.Sword",
                    DisplayName = "Circle.Sword Api",
                    Scopes =
                    {
                        new Scope("Circle.Sword.Scope")//这个东西不能少，否则获取token时报invalid_scope
                    },
                    UserClaims = new List<string>
                    {
                        //JwtClaimTypes.Role
                        "admin_role",
                        "readonly"
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Circle.Sword.ClientId",//指定ClientId
                    AllowedGrantTypes = GrantTypes.ClientCredentials,//授权模式为客户端模式
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())//指定ClientSecret
                    },
                    AllowedScopes = { "Circle.Sword.Scope" }//允许访问的Api资源
                },
                new Client
                {
                    ClientId = "Circle.Sword.ClientId.Password",//指定ClientId
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//授权模式为密码模式
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())//指定ClientSecret
                    },
                    AllowedScopes = { "Circle.Sword.Scope" }//允许访问的Api资源
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "01",
                    Username = "joke",
                    Password = "111111",
                    Claims = new List<Claim>
                    {
                        new Claim("admin_role", "admin_role"),
                        new Claim("readonly", "readonly")
                    }
                },
                new TestUser
                {
                    SubjectId = "02",
                    Username = "dadi",
                    Password = "123456",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Role, "super_admin_role")
                    }
                }
            };
        }
    }
}
