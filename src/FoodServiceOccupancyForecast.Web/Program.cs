var builder = WebApplication.CreateBuilder(args);

// ТОЛЬКО Razor Pages (без MVC)
builder.Services.AddRazorPages();

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

// ТОЛЬКО Razor Pages, НЕ MapControllerRoute!
app.MapRazorPages();

app.Run();