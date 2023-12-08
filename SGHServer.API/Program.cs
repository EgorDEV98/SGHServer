using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SGHServer.API.Middleware;
using SGHServer.Application;
using SGHServer.Identy;
using SGHServer.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddProtection();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(x =>
{
    x.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddCors();

builder.Services.AddRouting(x => x.LowercaseUrls = true);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SGHSERVER_JWT:SECRET"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var dataStore = serviceProvider.GetRequiredService<DataStore>();
        DataStoreInitializer.Init(dataStore);
    }
    catch (Exception exception)
    {
        Console.WriteLine("[CONSOLE] " + exception.Message);
    }
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


if (app.Environment.IsDevelopment())
{
    app.UseCors(x =>
    {
        x.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
}
else
{
    app.UseCors(x =>
    {
        x.WithOrigins("http://192.168.3.10:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
}

app.UseGlobalExtentionMiddleware();
app.UseHttpsRedirection();
app.UseAuthenticationMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();