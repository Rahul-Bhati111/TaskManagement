using TaskManagement.Application;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages
builder.Services.AddRazorPages();

// Read database connection string
var connectionString =
	builder.Configuration.GetConnectionString("DefaultConnection")
	?? throw new InvalidOperationException(
		"DefaultConnection is not configured.");

// Register application and infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();