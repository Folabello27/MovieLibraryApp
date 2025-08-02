using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
// builder.Services.AddDefaultIdentity<IdentityUser>(options =>
//     {
//         options.SignIn.RequireConfirmedAccount = false;
//         options.Password.RequiredLength = 6;
//     })
//     .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // Required for login
app.UseAuthorization();    // Required for roles

app.MapRazorPages();

// Map Identity endpoints
app.MapRazorPages();

app.Run();