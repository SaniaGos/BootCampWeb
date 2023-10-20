using Core.Entity;
using Core.Interface;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Service;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddJsonOptions(opts =>
{
	var enumConverter = new JsonStringEnumConverter();
	opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
builder.Services.AddControllers();

builder.Services.AddDbContext<MyAppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));

builder.Services.Configure<AppConf>(builder.Configuration.GetSection(nameof(AppConf)));


builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider
		.GetRequiredService<MyAppDbContext>();

	// Here is the migration executed
	dbContext.Database.Migrate();
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
