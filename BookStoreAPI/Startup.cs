using BookStoreAPI.Data;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI
{
    public class Startup
    {
        private object builder;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<BookStoreContext>();
            //second method for connection string

            /*  services.AddDbContext<BookStoreContext>(
                  options => options.UseSqlServer("Server=LAPTOP-5EO3NL9R;Database=BookstoreAPI;Integrated security =True"));*/
            //3rd method from connections string in appsettngs.json
            services.AddDbContext<BookeStoreContext>(
                 options => options.UseSqlServer(Configuration.GetConnectionString("BookStoreDB")));
            services.AddControllers().AddNewtonsoftJson();                                                                                                                                       
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(buider =>
                {
                    buider.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            }
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStoreAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStoreAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
