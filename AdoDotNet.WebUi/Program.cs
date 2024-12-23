using AdoDotNet.WebUi.Helpers;
using AdoDotNet.WebUi.Repositories;
using Scalar.AspNetCore;

namespace AdoDotNet.WebUi;

public class Program
{
    public static void Main(string[] args)
    {
        //-----------------------------------------------//
        //              SERVICES SECTION
        //-----------------------------------------------//
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Added OpenAPI Support
        builder.Services.AddOpenApi();

        // Check For Database Existence
        DatabaseHelper.EnsureDatabase();
        
        // Added a Scoped Service of ProductRepository
        builder.Services.AddScoped<ProductRepository>();

        //-----------------------------------------------//
        //              MIDDLEWARE-PIPELINE SECTION
        //-----------------------------------------------//

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}





