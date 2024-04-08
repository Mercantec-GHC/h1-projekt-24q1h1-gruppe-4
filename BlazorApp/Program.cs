using BlazorApp.Components;
using BlazorApp.Services;
using Domain_Models;
using Microsoft.Extensions.Configuration;
using System.Net.NetworkInformation;

namespace BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //GetAllUsedBooks from the Postgres DB
            IConfiguration Configuration = builder.Configuration;
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DefaultConnection");
            builder.Services.AddSingleton<DatabaseService>(sp => new DatabaseService(connectionString));

            // AuthenticationService Registration
            // Register AuthenticationService as scoped to be consistent with Blazor server's scoped lifecycle for components.
            builder.Services.AddScoped<AuthenticationService>();

            // Register AppState
            builder.Services.AddScoped<AppState>();


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}




