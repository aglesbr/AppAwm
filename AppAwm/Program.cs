using AppAwm.DAL;
using AppAwm.Respostas;
using AppAwm.Services;
using AppAwm.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSession(s =>
{
    s.IdleTimeout = TimeSpan.FromHours(1);
    s.Cookie.HttpOnly = true;
    s.Cookie.IsEssential = true;
});


builder.Services.AddDbContext<DbCon>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WAConnection"), b => b.MigrationsHistoryTable("__EFMigrationsHistory"));
 
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

builder.Services.AddScoped<IUsuario<UsuarioAnswer>, UsuarioService>();
builder.Services.AddScoped<IEmpresa<EmpresaAnswer>, EmpresaService>();
builder.Services.AddScoped<IAnexo<AnexoAnswer>, AnexoService>();
builder.Services.AddScoped<IFuncionario<FuncionarioAnswer>, FuncionarioService>();
builder.Services.AddScoped<IObra<ObraAnswer>, ObraService>();
builder.Services.AddScoped<ITreinamento<TreinamentoAnswer>, TreinamentoService>();
builder.Services.AddScoped<DepartamentoService>();

builder.Services.AddSession(s =>
{
    s.IdleTimeout = TimeSpan.FromHours(1);
    s.Cookie.HttpOnly = true;
    s.Cookie.IsEssential = true;
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();


var key = Encoding.ASCII.GetBytes(AppAwm.Util.Utility.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    {
        if (!string.IsNullOrEmpty(token))
            context.Request.Headers.Append("Authorization", "Bearer " + token);

        await next();
    }
});

app.UseStatusCodePages(context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized || response.StatusCode == (int)HttpStatusCode.Forbidden)
        response.Redirect("/Home/unauthorized");
    return Task.CompletedTask;
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Start}/{action=Index}/{id?}");

app.Run();