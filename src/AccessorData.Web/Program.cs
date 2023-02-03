using AccessorData.Infrastructure.Util;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
string? xmlFilePath = builder.Configuration.GetConnectionString("XML");

ArgumentNullException.ThrowIfNull(xmlFilePath);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.UseLocalXML(xmlFilePath);

WebApplication app = builder.Build();

// The default HSTS value is 30 days.
// You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
