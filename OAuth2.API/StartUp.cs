using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;
using Owin;
using OAuth2.Data;
using OAuth2.API.Providers;

[assembly: OwinStartup(typeof(OAuth2.API.StartUp))]
namespace OAuth2.API
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            //DBUtility.Migration("AuthConnection");
            //AutoFacConfig.Configure();
            var config = new HttpConfiguration();
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Auth"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
                Provider = new AuthServerProvider(),
                RefreshTokenProvider = new RefreshTokenServerProvider()
            };


            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}