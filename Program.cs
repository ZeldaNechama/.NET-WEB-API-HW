using tasks.Utilities;
using tasks.Controllers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddTask();
builder.Services.AddUser();



var app = builder.Build();

app.UseMyLogMiddleware();


///////////////////////
//  void ConfigureServices(IServiceCollection services)
//         {
//             services
//                 .AddAuthentication(options =>
//                 {
//                     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//                 })
//                 .AddJwtBearer(cfg =>
//                 {
//                     cfg.RequireHttpsMetadata = false;
//                     cfg.TokenValidationParameters = FbiTokenService.GetTokenValidationParameters();
//                 });

//             services.AddAuthorization(cfg =>
//                 {
//                     cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
//                     cfg.AddPolicy("Agent", policy => policy.RequireClaim("type", "Agent"));
//                     cfg.AddPolicy("ClearanceLevel1", policy => policy.RequireClaim("ClearanceLevel", "1", "2"));
//                     cfg.AddPolicy("ClearanceLevel2", policy => policy.RequireClaim("ClearanceLevel", "2"));
//                 });

//             services.AddControllers();
//             services.AddSwaggerGen(c =>
//             {
//                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "FBI", Version = "v1" });
//                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                 {
//                     In = ParameterLocation.Header,
//                     Description = "Please enter JWT with Bearer into field",
//                     Name = "Authorization",
//                     Type = SecuritySchemeType.ApiKey
//                 });
//                 c.AddSecurityRequirement(new OpenApiSecurityRequirement {
//                 { new OpenApiSecurityScheme
//                         {
//                          Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
//                         },
//                     new string[] {}
//                 }
//                 });
//             });
//         }
////////////////////////////////////////////////////

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// js
app.UseDefaultFiles();
app.UseStaticFiles();
// js

app.UseAuthorization();

app.MapControllers();

app.Run();
