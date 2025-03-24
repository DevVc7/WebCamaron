using LabCamaron.Web.Extensions;
using LabCamaron.Web.Middleware;
using LabCamaronWeb.Infraestructura;
using LabCamaronWeb.Infraestructura.Constantes;
using LabCamaronWeb.Servicios;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
InformacionApi.Nombre = Assembly.GetExecutingAssembly().GetName().Name!;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AgregarFactoryHttp(builder.Configuration);
builder.Services.AgregarAutenticacion(builder.Configuration);
builder.Services.RegistrarServicios();
builder.Services.RegistrarInfraestructura();
builder.Services.ConfigurarSerilog(builder);

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

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LogMiddleware>();
app.UseSession();

app.MapGet("/", context =>
{
    // Verificar si el usuario está autenticado
    if (context.User.Identity?.IsAuthenticated ?? false)
    {
        // Si está autenticado, redirigir a Home/Index
        context.Response.Redirect("/Home/Index");
    }
    else
    {
        // Si no está autenticado, redirigir a Login/Index
        context.Response.Redirect("/Login/Index");
    }

    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();