using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using ProjetoDDD.Services.Interfaces;
using ProjetoDDD.WebApi.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace ProjetoDDD.WebApi.Provider
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IProdutoService _produtoService;
        public ApplicationOAuthProvider(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext c)
        {
            c.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext c)
        {
            //Regra de autenticação
            c.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //var produto = ValidarAcesso(c.UserName, c.Password);

            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, c.UserName));
                identity.AddClaim(new Claim("role", "user"));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    /* 
                     {
                         "empresa", produto.Codigo.ToString()
                     },
                     {
                         "funcionario", produto.Descricao.ToString()
                     }*/
                });
                var ticket = new AuthenticationTicket(identity, props);
                c.Validated(ticket);
            }
            catch (Exception)
            {
                // The ClaimsIdentity could not be created by the UserManager.
                c.Rejected();
                c.SetError("server_error");
            }

            return Task.FromResult<object>(null);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

    }
}