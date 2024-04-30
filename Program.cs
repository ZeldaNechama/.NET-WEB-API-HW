using tasks.Utilities;
using tasks.Controllers;
using tasks.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Services
     .AddAuthentication(options =>
     {
         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(cfg =>
     {
         cfg.RequireHttpsMetadata = false;
         cfg.TokenValidationParameters = LoginServices.GetTokenValidationParameters();
     });

builder.Services.AddAuthorization(cfg =>
   {
       cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
       cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User"));
       cfg.AddPolicy("ClearanceLevel1", policy => policy.RequireClaim("ClearanceLevel", "1", "2"));
       cfg.AddPolicy("ClearanceLevel2", policy => policy.RequireClaim("ClearanceLevel", "2"));
   });

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
      {
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBI", Version = "v1" });
          c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
          {
              In = ParameterLocation.Header,
              Description = "Please enter JWT with Bearer into field",
              Name = "Authorization",
              Type = SecuritySchemeType.ApiKey
          });
          c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {}
                }
          });
      });




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddTask();
builder.Services.AddUser();



var app = builder.Build();

app.UseMyLogMiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// js
app.UseDefaultFiles();
// app.UseDefaultFiles(new DefaultFilesOptions
// {
//     DefaultFileNames = new List<string> { "login.html" } // Specify the desired default file here
// });
app.UseStaticFiles();
// js

app.UseAuthorization();

app.MapControllers();

app.Run();
