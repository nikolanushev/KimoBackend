using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Ui.Voyager;
using KimoBackend.Data;
using KimoBackend.GraphQL;
using KimoBackend.GraphQL.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KimoBackend
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        // private readonly IConfiguration Configuration; -- this didn't work

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(opt => opt.UseSqlServer
                    (Configuration.GetConnectionString("KimoBackend")));
            
            services.AddGraphQLServer()
                    .AddQueryType<Query>()
                    .AddType<UserType>()
                    .AddMutationType<Mutation>()
                    .AddProjections()
                    .AddFiltering()
                    .AddSorting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            }, "/graphql-ui");
        }
    }
}
