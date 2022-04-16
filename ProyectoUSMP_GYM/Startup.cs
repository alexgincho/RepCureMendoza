using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoUSMP_GYM.Models.ModelDB;
using ProyectoUSMP_GYM.Models.Services;
using ProyectoUSMP_GYM.Models.Services.Interfaces;
using ProyectoUSMP_GYM.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoUSMP_GYM
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
            // Auth Session
            services.AddSession();
            // Inyeccion de Dependencias Modelos
            services.AddTransient<IPersonalService, PersonalService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProveedorService, ProveedorService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVentaService, VentaService>();
            // Inyectando Fluent Validation
            services.AddMvc().AddFluentValidation();
            // Agregando Inyeccion de Dependencias FluentValidation
            services.AddTransient<IValidator<Personaladm>, PersonalValidator>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession(); // Session Auth
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
