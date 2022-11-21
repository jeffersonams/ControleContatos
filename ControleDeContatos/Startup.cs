using ControleDeContatos.Data;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } //ATRAVES DO CONFIGURATION, CONSEGUE PEGAR TUDO QUE ESTA DENTRO DO APPSETTINGS

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //CONFIGURANDO O BANCO DE DADOS - FALANDO QUE QUANDO O SISTEMA RODAR PRECISA CONHECER O BANCOCONTEXT, SABER  QUAL VAI SER A STRING DE CONEXAO
            services.AddEntityFrameworkSqlServer().AddDbContext<BancoContext>(O => O.UseSqlServer(Configuration.GetConnectionString("DataBase"))); 
            services.AddScoped<IContatoRepositorio, ContatoRepositorio>(); //TODA VEZ QUE A INTERFACE FOR INVOCADA A INJECAO DE DEPENDENCIA VAI USAR TUDO DO CONTATOREPOSITORIO   
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
