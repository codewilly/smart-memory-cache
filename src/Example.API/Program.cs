using Cache;
using Cache.Interfaces;
using Example.API.CacheKeys;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache(); // Don't forget to enable MemoryCache!
builder.Services.AddScoped<ICacheFacade, CacheFacade>();

var app = builder.Build();

app.MapGet("/fruits", ([FromServices] ICacheFacade cache, string fruit) =>
{
    // 1 - Create a cacheKey and set the parameters
    GetFruitCacheKey cacheKey = new(fruit);

    // 2 - Try to get a value from cache
    if (cache.Get(cacheKey) is Guid id)
        return Results.Ok(id); // If exists, then return the cache value

    // 4 - Perform your logic
    id = Guid.NewGuid(); 

    return Results.Ok(cache.Create(cacheKey, id, expiration: 1)); // 5 - Create/Update the cache value.
});

app.Run();
