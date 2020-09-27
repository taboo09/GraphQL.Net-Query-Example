using Books.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.API.Context;
using Product.API.GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;

namespace Product.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:Default-sqllite"];
            services.AddDbContext<BookContext>(x => x.UseSqlite(connectionString));

            services.AddControllers();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<BookQuery>();
            
            services.AddScoped<BookSchema>();

            services.AddGraphQL()
                .AddSystemTextJson(deserializerSettings => {}, serializerSettings => {})
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseGraphQL<BookSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            // app.UseRouting();

            // app.UseAuthorization();

            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
        }
    }
}
