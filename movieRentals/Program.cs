
using COMP2084Assign2Real.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()  // enable role-based authorization
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// enable Google auth
// let our app read from appsettings or Azure Env Vars
var configuration = builder.Configuration;

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = configuration["Authentication:Google:ClientID"];
        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });

// enable sessions
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    //role management, which adds roles to server
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //roles to add to server
    var roles = new [] { "Administrator" };

    foreach (var role in roles)
    {
        //check if role exists
        if(!await roleManager.RoleExistsAsync(role))
        {
            //create a role
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    // User manager to add user
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // User data
    string email = "rich@gc.ca";
    string password = "Test123$";

    // Check if user exists
    if (await userManager.FindByEmailAsync(email) == null)
    {
        // If not, create it
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true; // Incase email confirmation is required

        // Create user
        await userManager.CreateAsync(user, password);

        // Add admin role to user
        await userManager.AddToRoleAsync(user, "Administrator");
    }
}

// session support
app.UseSession();

app.Run();
