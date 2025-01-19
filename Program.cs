using FirstWebApi.Database;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

var connectionString = Environment.GetEnvironmentVariable("DefaultConnectionString")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

//app.MapGroup("account").MapIdentityApi<IdentityUser>();

app.MapGet("/search", async (ApplicationDbContext db, [AsParameters] SearchModel model) =>
{
    var searchResult = await db.WastePoints
        .Where(d => 
            string.IsNullOrWhiteSpace(model.Address)
            || d.Name.ToLower().Contains(model.Address.ToLower())).ToListAsync();

    return searchResult;
});

app.MapGet("/get/{id}", async (ApplicationDbContext db, int id) =>
{
    var wastePoint = await db.WastePoints.SingleOrDefaultAsync(d => d.Id == id);

    if (wastePoint is null)
        return new TestDataViewModel { Error = "Nincs adat" };

    return new TestDataViewModel
    { 
        Id = wastePoint.Id,
        Name = wastePoint.Name,
        Description = wastePoint.Description,
        Address = wastePoint.Address,
        Latitude = wastePoint.Latitude,
        Longitude = wastePoint.Longitude
    };
});
app.MapGet("/import-data", async (ApplicationDbContext db, IHttpClientFactory clientFactory) =>
{
    try
    {
        var client = clientFactory.CreateClient();

        var data = new
        {
            coordinates = new { latitude = 0, longitude = 0 },
            wastePointTypes = new List<int>(),
            wasteCategories = new List<int>(),
            hideDrsPoints = false
        };
        var response = await client.PostAsJsonAsync("https://map.mohu.hu/api/Map/SearchWastePoints", data);

        if (!response.IsSuccessStatusCode)
        {
            return Results.StatusCode((int)response.StatusCode);
        }

        var externalData = await response.Content.ReadFromJsonAsync<List<ExternalWastePointModel>>();

        if (externalData == null || !externalData.Any())
        {
            return Results.BadRequest("No data found in the external API.");
        }

        // Map external data to the local WastePoint model
        var wastePoints = externalData.Select(d => new WastePoint
        {
            Id = d.Id, // Ensure the external ID is correctly mapped to your DB schema
            Name = d.Name,
            Latitude = d.Latitude,
            Longitude = d.Longitude,
            Address = d.Address,
            Description = d.Description
        }).ToList();

        // Optional: Avoid duplicate entries based on unique fields (e.g., Id)
        foreach (var point in wastePoints)
        {
            if (!db.WastePoints.Any(existing => existing.Id == point.Id)) // Check if the point already exists in the database
            {
                db.WastePoints.Add(point); // Add the new waste point if it does not exist
            }
        }

        // Save changes to the database
        await db.SaveChangesAsync();
        return Results.Ok("Data imported successfully.");
    }
    catch (Exception ex)
    {
        // If an exception occurs, return the error message
        return Results.Problem($"Error while importing data: {ex.Message}");
    }
});

app.MapPost("/create", async (ApplicationDbContext db, TestDataCreateModel model) =>
{
    var newTestData = new FirstWebApi.Database.WastePoint
    {
        Name = model.Name,
        Description = model.Description,
        Address = model.Address,
        Latitude = model.Latitude,
        Longitude = model.Longitude
    };

    db.WastePoints.Add(newTestData);
    await db.SaveChangesAsync();
});

app.MapPut("/update", async (ApplicationDbContext db, TestDataUpdateModel model) =>
{
    var wastePoint = await db.WastePoints.SingleOrDefaultAsync(d => d.Id == model.Id);
    if (wastePoint is null)
        return Results.NotFound();

    wastePoint.Name = model.Name;
    wastePoint.Description = model.Description;
    wastePoint.Address = model.Address;
    wastePoint.Latitude = model.Latitude;
    wastePoint.Longitude = model.Longitude;

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.MapDelete("/delete/{id}", async (ApplicationDbContext db, int id) =>
{
    var wastePoint = await db.WastePoints.SingleOrDefaultAsync(d => d.Id == id);

    if (wastePoint is null)
        return Results.NotFound();

    db.WastePoints.Remove(wastePoint);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
