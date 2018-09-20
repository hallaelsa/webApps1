﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oblig1theAteam.Business.Movies;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.DBModels;
using Oblig1theAteam.DependencyInjectionDemo;
using System;

// NOTE! En av tingene med core er at man må konfigurere hva man skal bruke i startup.cs

namespace Oblig1theAteam
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                // default cookie heter .AspNetCore.Session
                // 10 sekund??? høres lite ut
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });


            // Lagt til slik at UserService opprettes per request.
            // må ha AddScoped for at HomeController og andre klasser skal få tak i disse:

            services.AddDbContext<DBModels.DbService>(options => options.UseSqlServer(Configuration.GetConnectionString("DbService")));
            //services.AddScoped<DBModels.DbService>();

            services.AddScoped<UserService>();
            services.AddScoped<OrderService>();
            services.AddScoped<MovieService>();

            //services.AddSingleton legger til en instanse som gjenbrukes for alle requests
            // EKSEMPEL på singleton. Sjekk ut ved å kjøre Home/Demo flere ganger(refresh).
            // IDemoService er brukt HomeController, men HomeController kjenner ikke til DemoService
            //services.AddSingleton<IDemoService, DemoService>();

            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            InitializeMigrations(app);
        }

        private static void InitializeMigrations(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                DbInit.Initialize(serviceScope);
            }
        }
    }
}
