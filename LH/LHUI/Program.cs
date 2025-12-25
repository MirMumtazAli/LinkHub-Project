using LHDAL.DAos;
using LHDAL.Data;
using LHDAL.Repository;
using LHDAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LHDbContext>(
    x => x.UseSqlServer(configuration.GetSection("LH:ProdconStr").Value)
    );


//builder.Services.AddDbContext<LHDbContext>(
//    x=>x.UseSqlServer("Data Source=DESKTOP-RFG12VT\\SQLEXPRESS;Initial Catalog=LHDb;Integrated Security=True;Trust Server Certificate=True")    
//    );

//builder.Services.AddScoped<ICategoryDb, CategoryDb>();
//builder.Services.AddSingleton<ICategoryDb, CategoryDb>();

//builder.Services.AddTransient<ICategoryDb,CategoryDb>();
//builder.Services.AddTransient<ILHUrlDB, LHUrlDB>();

builder.Services.AddTransient<ILHDB, LHDB>();


builder.Services.AddIdentity<LHUser, IdentityRole>()
                .AddEntityFrameworkStores<LHDbContext>()
                .AddDefaultTokenProviders();


//app.MapGet("/", () => "Hello World!");
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    #region Creating Roles

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] rolNames = configuration.GetSection("LH:Roles").GetChildren().Select(x => x.Value).ToArray();

    foreach (var rolName in rolNames)
    {
        var rolExits = roleManager.RoleExistsAsync(rolName).Result;

        if (!rolExits)
        {
            var rolResulr = roleManager.CreateAsync(new IdentityRole(rolName)).Result;
        }
    }


    #endregion


    #region Creating Admin

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<LHUser>>();
    var adminUser = configuration.GetSection("LH:AdminUser").Value;
    var adminPassword = configuration.GetSection("LH:AdminPassword").Value;
    var adminDefaultRole = configuration.GetSection("LH:DefaultRole").Value;

    var userExits = userManager.FindByNameAsync(adminUser).Result;
    if (userExits == null)
    {
        var user = new LHUser() { UserName = adminUser, Email = adminUser };
        var userResult = userManager.CreateAsync(user,adminPassword).Result;
        var assignRoleResult = userManager.AddToRoleAsync(user, adminDefaultRole).Result;
    }



    #endregion
}



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Common/Error/Index");
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Common/Error/NotFound";
        await next();
    }
});


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{area:exists=Common}/{controller=Home}/{action=Index}/{id?}");

app.Run();
