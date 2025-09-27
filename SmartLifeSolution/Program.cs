
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MQTTnet;
using MQTTnet.Packets;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.BLL.Mapper;
using SmartLifeSolution.BLL.Middleware;
using SmartLifeSolution.DAL.Dao.Settings;
using SmartLifeSolution.DAL.DBContexts;
using SmartLifeSolution.DAL.Enums;
using SmartLifeSolution.DAL.Models;
using System.Runtime;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using SmartLifeSolution.BLL.Repositories.Gateways;
using SmartLifeSolution.BLL.Repositories.SmartBuildings;
using SmartLifeSolution.BLL.Repositories.SubscriptionPlans;
using SmartLifeSolution.BLL.Repositories.UserSubscriptions;
using SmartLifeSolution.BLL.Repositories.Dashboards;
using SmartLifeSolution.BLL.Repositories.Auths;
using SmartLifeSolution.BLL.Repositories.GatewayDevices;
using SmartLifeSolution.BLL.Repositories.Menus;
using SmartLifeSolution.BLL.Repositories.UserDashboards;
using SmartLifeSolution.BLL.Repositories.Templates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


#region Repository Services Configurations

builder.Services.Configure<MqttSettings>(builder.Configuration.GetSection("MqttSettings"));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

builder.Services.AddSingleton<MqttClt>();
builder.Services.AddSingleton<EmailUtility>();
builder.Services.AddSingleton<StripePayment>();
builder.Services.AddSingleton<JwtHelper>();

builder.Services.AddScoped<IGatewayRepository, GatewayRepository>();
builder.Services.AddScoped<IGatewayDeviceRepository, GatewayDeviceRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
//builder.Services.AddScoped<ISmartBuildingRepository, SmartBuildingRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserDashboardRepository, UserDashboardRepository>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

#endregion




var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { UserRoleEnums.ADMIN.ToString(),
        UserRoleEnums.MANAGER.ToString(), UserRoleEnums.USER.ToString() };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = roleName,
            });
        }
    }
}

//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
//    context.Response.Headers.Add("Cross-Origin-Embedder-Policy", "require-corp");
//    await next();
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();
app.UseAuthentication(); 
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

//app.MapStaticAssets();
//app.MapRazorPages().WithStaticAssets();

app.UseMiddleware<CustomMiddleware>(); 


//var clientId = "3cb81efd-868e-46ee-9296-0b12b77c494d";
//var factory = new MqttClientFactory();
//var mqttClient = factory.CreateMqttClient();

//var options = new MqttClientOptionsBuilder()
//    .WithTcpServer("54.254.160.93", 1883)
//    .WithCredentials("test", "test")
//    .WithClientId(clientId)
//   // .WithCleanSession()
//    .Build();

//await mqttClient.ConnectAsync(options, CancellationToken.None);

//await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
//           .WithTopic("/milesight/uplink")
//           .Build());

//mqttClient.ConnectedAsync += e =>
//    {

//        return Task.CompletedTask;
//    };

//mqttClient.ApplicationMessageReceivedAsync += e =>
//{
//    Console.WriteLine("we got data");

//    Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
//    return Task.CompletedTask;
//};

//await mqttClient.DisconnectAsync();

app.Run();


