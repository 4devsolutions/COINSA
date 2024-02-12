using NominaNF.Data.Contrato;
using NominaNF.Data.Implementacion;
using NominaNF.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenericData<Proyecto>, ProyectoData>();
builder.Services.AddScoped<IGenericData<Usuario>, UsuarioData>();
builder.Services.AddScoped<IPuestoCmbData<Puesto>, PuestoCmbData>();
builder.Services.AddScoped<IProyectoCmbData<Proyecto>, ProyectoCmbData>();
builder.Services.AddScoped<IUbicacionCmbData<Ubicacion>, UbicacionCmbData>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.Run();
