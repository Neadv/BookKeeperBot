using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookKeeperBot.Data;
using BookKeeperBot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BookKeeperBot.Models;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BookKeeperBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));

            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<ICommandStorage, CommandStorage>();

            services.AddDbContext<DataContext>(opts => {
               opts.UseSqlite("Filename=Books.db");
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICommandSelector, CommandSelector>();
            services.AddScoped<IContextFactory, ContextFactory>();

            services.AddLocalization(opts => opts.ResourcesPath = "Resources");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBotService bot)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                bot.SetWebhook();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
