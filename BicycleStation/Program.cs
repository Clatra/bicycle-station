using AutoMapper;
using BicycleStation.Dtos;
using BicycleStation.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BicyclesStations") ?? "Data Source=BicyclesStations.db";
builder.Services.AddSqlite<BicycleStationDbContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/stations-data", (BicycleStationDbContext db) =>
{
    return Results.Ok(db.RepairStations.ToList());
}).WithTags("Bicycyle Station");

app.MapGet("/stations-capacity", (BicycleStationDbContext db, int minCapacity) =>
{
    return Results.Ok(db.RepairStations.Where(x => x.Capacity > minCapacity));
}).WithTags("Bicycyle Station");

app.MapPost("/stations", (BicycleStationDbContext db, RepairStation repairStation) =>
{
    db.RepairStations.Add(repairStation);
    db.SaveChanges();
    return db.RepairStations.ToList();
}).WithTags("Bicycyle Station");

app.MapPut("/stations/{id}", (BicycleStationDbContext db, int id, [FromBody] EditRepairStationDTO updatedStation) =>
{
    var stationToUpdate = db.RepairStations.FirstOrDefault(x => x.Id == id);
    if (stationToUpdate is null)
        return Results.NotFound("Id is null");
    stationToUpdate.Capacity = updatedStation.Capacity;
    stationToUpdate.VisitorCapacity = updatedStation.VisitorCapacity;
    stationToUpdate.HasTools = updatedStation.HasTools;

    db.SaveChanges();
    return Results.Ok(updatedStation);
}).WithTags("Bicycyle Station");

app.MapDelete("/stations/{id}", (BicycleStationDbContext db, int id) =>
{
    var stationToDelete = db.RepairStations.FirstOrDefault(x => x.Id == id);
    if (stationToDelete is null)
        return Results.NotFound($"Station with id of {id} not found");

    db.RepairStations.Remove(stationToDelete);

    db.SaveChanges();
    return Results.Ok("Station deleted succesfully");
}).WithTags("Bicycyle Station");

app.MapGet("/pump-stations-data", (BicycleStationDbContext db, IMapper mapper) =>
{
    var repairStations = db.RepairStations.Include(b => b.BicyclePumps).ToList();
    var repairStationsDTO = mapper.Map<List<GetRepairStationDTO>>(repairStations);
    return Results.Ok(repairStationsDTO);

}).WithTags("Pump Station");
app.MapGet("/pump-stations-data/{id}", (BicycleStationDbContext db, int id) =>
{
    return Results.Ok(db.BicyclePump.Where(x => x.Id == id));
}).WithTags("Pump Station");


app.MapPost("/pump-stations", (BicycleStationDbContext db, CreateBicyclePumpDTO bicyclePump) =>
{
    // mapping
    var bicyclePumpToCreate = new BicyclePump
    {
        Brand = bicyclePump.Brand,
        Name = bicyclePump.Name,
        Type = bicyclePump.Type,
        Id = bicyclePump.Id,
        RepairStationId = bicyclePump.RepairStationId
    };
    db.BicyclePump.Add(bicyclePumpToCreate);
    db.SaveChanges();
    return db.BicyclePump.ToList();
}).WithTags("Pump Station");

app.MapPut("/pump-stations/{id}", (BicycleStationDbContext db, int id, [FromBody] BicyclePumpDTO bicyclePumpDTO) =>
{
    var pumpToUpdate = db.BicyclePump.FirstOrDefault(x => x.Id == id);
    if (pumpToUpdate is null)
        return Results.NotFound("Id is null");

    pumpToUpdate.Brand = bicyclePumpDTO.Brand;
    pumpToUpdate.Name = bicyclePumpDTO.Name;
    pumpToUpdate.Type = bicyclePumpDTO.Type;

    db.SaveChanges();
    return Results.Ok(pumpToUpdate);
}).WithTags("Pump Station");

app.MapDelete("/pump-stations/{id}", (BicycleStationDbContext db, int id) =>
{
    var pumpToDelete = db.BicyclePump.FirstOrDefault(x => x.Id == id);
    if (pumpToDelete is null)
        return Results.NotFound($"Station with id of {id} not found");

    db.BicyclePump.Remove(pumpToDelete);

    db.SaveChanges();
    return Results.Ok("Pump deleted succesfully");
}).WithTags("Pump Station");

app.Run();
