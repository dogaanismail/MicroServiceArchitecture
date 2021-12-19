using MicroServiceArchitecture.Gateaway.DelegateHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MicroServiceArchitecture.Gateaway
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<TokenExhangeDelegateHandler>();

            services.AddAuthentication().AddJwtBearer("GateawayAuthenticationSchema", options =>
            {
                options.Authority = Configuration["IdentityServerURL"];
                options.Audience = "resource_gateaway";
                options.RequireHttpsMetadata = false;
            });

            services.AddOcelot().AddDelegatingHandler<TokenExhangeDelegateHandler>();
        }

        async public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            await app.UseOcelot();
        }
    }
}
