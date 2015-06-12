using AP.PD.Business.Domain;
using AP.PD.Business.Interface;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AP.PD.CLP.Web.AuthenticationServer.API
{
    public class ClpAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //TODO: Inject thru Dipendency Resolver
        private IAuthService _authService = new AuthService();
        //validating the “Client”, 
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //validate the username and password sent to the authorization server’s token endpoint
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //To allow CORS on the token middleware provider we need to add the header “Access-Control-Allow-Origin” 
            //this allows CORS for token middleware provider not for ASP.NET Web AP8I 
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //TODO: Implement user validation using CLP database for 
            //context.UserName
            //context.Password
            //e.g.
            //using (var _repo = new AuthRepository())
            //{
            //using (var authService = new AuthService())

            var user = _authService.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("userId", Convert.ToString(user.Id)));
            identity.AddClaim(new Claim("role", Convert.ToString(user.RoleId)));
            //generating the token happens behind the scenes when we call “context.Validated(identity)”.

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "login_role", Convert.ToString(user.Role.Name)
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            //context.Validated(identity);
            //}
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