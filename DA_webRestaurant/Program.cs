using DAL;
using DAL.Context;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();




builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});
// Add services to the container.

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();




app.UseAuthorization();
app.UseSession();

using var scope = app.Services.CreateScope();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
var employeeRoleExists = await roleManager.RoleExistsAsync("Employee");
if (!adminRoleExists || !employeeRoleExists)
{
    await roleManager.CreateAsync(new IdentityRole("Admin"));
    await roleManager.CreateAsync(new IdentityRole("Employee"));
}

var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
var employeeUser = await userManager.FindByEmailAsync("employee@gmail.com");
if (adminUser == null)
{
    var newAdminUser = new ApplicationUser
    {
        UserName = "admin@gmail.com",
        Fullname = "Admin",
        Email = "admin@gmail.com",
    };

    var newEmployeeUser = new ApplicationUser
    {
        UserName = "employee@gmail.com",
        Fullname = "Emplyee",
        Email = "employee@gmail.com",
    };

    var result = await userManager.CreateAsync(newAdminUser, "123456");
    var result1 = await userManager.CreateAsync(newEmployeeUser, "123456");
    if (result.Succeeded || result1.Succeeded)
    {
        await userManager.AddToRoleAsync(newAdminUser, "Admin");
        await userManager.AddToRoleAsync(newAdminUser, "Employee");
    }
    
}

var rolesToCreate = new List<string> { "Admin", "Customer", "Employee" };

foreach (var roleName in rolesToCreate)
{
    // Check if the role exists
    var roleExists = await roleManager.RoleExistsAsync(roleName);

    // If the role doesn't exist, create it
    if (!roleExists)
    {
        var role = new IdentityRole(roleName);
        var result = await roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
            Console.WriteLine($"Role '{roleName}' created successfully.");
        }
        else
        {
            Console.WriteLine($"Error creating role '{roleName}'.");
            // Handle error
        }
    }
}



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "Employee",
        pattern: "{area:exists}/{controller=Employee}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "Customer",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
