using tasks.Utilities;
using tasks.Controllers;
using tasks.Services;
z
var builder = WebApplication.CreateBuilder(args);



   builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.TokenValidationParameters = FbiTokenService.GetTokenValidationParameters();
        });

     builder.Services.AddAuthorization(cfg =>
        {
            cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
            cfg.AddPolicy("Agent", policy => policy.RequireClaim("type", "Agent"));
            cfg.AddPolicy("ClearanceLevel1", policy => policy.RequireClaim("ClearanceLevel", "1", "2"));
            cfg.AddPolicy("ClearanceLevel2", policy => policy.RequireClaim("ClearanceLevel", "2"));
        });







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
