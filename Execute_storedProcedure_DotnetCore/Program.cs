using Execute_storedProcedure_DotnetCore.Data;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var _GetConnectionString = builder.Configuration.GetConnectionString("connMSSQL");
builder.Services.AddDbContext<MiApiContext>(options => options.UseSqlServer(_GetConnectionString));


//var _GetConnectionStringUser = builder.Configuration.GetConnectionString("connMSSQL");
//builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(_GetConnectionStringUser));


var _GetConnectionStringVM = builder.Configuration.GetConnectionString("connMSSQL");
builder.Services.AddDbContext<VM_Context>(options => options.UseSqlServer(_GetConnectionStringVM));


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
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Asegurar que la base de datos esté creada
    context.Database.EnsureCreated();

    // Verificar si el rol "Admin" ya existe
    var roleExists = await roleManager.RoleExistsAsync(UserRoles.Admin);
    if (!roleExists)
    {
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    }

    // Verificar si el usuario administrador ya existe
    var adminUser = await userManager.FindByNameAsync("Francisco");
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

        // Crear el usuario
        var result = await userManager.CreateAsync(adminUser, "UNAContrasenia@2023");

        if (result.Succeeded)
        {
            // Asignar el rol al usuario
            await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
        }
        else
        {
            // Manejar errores en la creación del usuario
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Error creating default user: {errors}");
        }
    }
}
#endregion






app.Run();

