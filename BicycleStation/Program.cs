using BicycleStation.Dtos;
using BicycleStation.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BicyclesStations") ?? "Data Source=BicyclesStations.db";
builder.Services.AddSqlite<BicycleStationDbContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/stations-data", (BicycleStationDbContext db) =>
{
    return Results.Ok(db.RepairStations.ToList());
});

app.MapGet("/stations-capacity", (BicycleStationDbContext db, int minCapacity) =>
{
    return Results.Ok(db.RepairStations.Where(x => x.Capacity > minCapacity));
});

app.MapPost("/stations", (BicycleStationDbContext db, RepairStation repairStation) =>
{
    db.RepairStations.Add(repairStation);
    db.SaveChanges();
    return db.RepairStations.ToList();
});

app.MapPut("/stations/{id}", (BicycleStationDbContext db, int id, [FromBody] RepairStationDTO updatedStation) =>
{
    var stationToUpdate = db.RepairStations.FirstOrDefault(x => x.Id == id);
    if (stationToUpdate is null)
        return Results.NotFound("Id is null");
    stationToUpdate.Capacity = updatedStation.Capacity;
    stationToUpdate.VisitorCapacity = updatedStation.VisitorCapacity;
    stationToUpdate.HasTools = updatedStation.HasTools;

    db.SaveChanges();
    return Results.Ok(updatedStation);
});

app.MapDelete("/stations/{id}", (BicycleStationDbContext db, int id) =>
{
    var stationToDelete = db.RepairStations.FirstOrDefault(x => x.Id == id);
    if (stationToDelete is null)
        return Results.NotFound($"Station with id of {id} not found");

    db.RepairStations.Remove(stationToDelete);

    db.SaveChanges();
    return Results.Ok("Station deleted succesfully");
});

app.Run();
