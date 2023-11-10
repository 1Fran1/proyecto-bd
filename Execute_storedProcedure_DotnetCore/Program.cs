using Execute_storedProcedure_DotnetCore.Data;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddTransient<IAuthService, AuthService>();


//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();




//// For Identity  
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<MiApiContext>()
//                .AddDefaultTokenProviders();
//// Adding Authentication  
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})

//// Adding Jwt Bearer  
//            .AddJwtBearer(options =>
//            {
//                options.SaveToken = true;
//                options.RequireHttpsMetadata = false;
//                options.TokenValidationParameters = new TokenValidationParameters()
//                {
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
//                    ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
//                    ClockSkew = TimeSpan.Zero,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
//                };
//            });


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//});


////// Add HttpClientFactory
////builder.Services.AddHttpClient();
////builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
////    policy =>
////    {
////        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
////    }));

////// Add services to the container.
////builder.Services.AddDbContext<MiApiContext>(cnn => cnn.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnect")));






//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseCors("Open");

//app.MapControllers();

//app.Run();









var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var _GetConnectionString = builder.Configuration.GetConnectionString("connMSSQL");
builder.Services.AddDbContext<MiApiContext>(options => options.UseSqlServer(_GetConnectionString));


var _GetConnectionStringUser = builder.Configuration.GetConnectionString("Con_Admin");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(_GetConnectionStringUser));


// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MiApiContext>()
                .AddDefaultTokenProviders();
// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
                    //ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],



                    ValidateIssuer = false,
                    ValidateAudience = false,


                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
                };
            });


builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Open");

app.MapControllers();


#region Inyectar un administrador a la base de datos
// Obtener el contexto del servicio
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MiApiContext>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Asegurar que la base de datos esté creada
    context.Database.EnsureCreated();

    // Verificar si el usuario administrador ya existe
    var adminUser = userManager.FindByNameAsync("Francisco").Result;
    if (adminUser == null)
    {
        // Crear el usuario administrador y asignarle el rol
        adminUser = new ApplicationUser
        {
            UserName = "Francisco",
            Identificacion = 28130341,
            Nombre = "Francisco",
            Apellido1 = "Calvo",
            Apellido2 = "Araya",
            Email = "administrador.una.ac.cr@gmail.com",
        };

        userManager.CreateAsync(adminUser, "UNAContrasenia@2023").Wait();
        userManager.AddToRoleAsync(adminUser, UserRoles.Admin).Wait();

    }
}

#endregion


app.Run();

