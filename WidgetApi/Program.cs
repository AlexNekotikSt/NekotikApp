using Bridge;
using Domain.Widget;
using System.Text.Json.Serialization;
using WidgetApi;

var builder = WebApplication.CreateBuilder(args);

// Конфігурація: URL, режим використання HTTP-сервісу
builder.Configuration["UseHttpWidgetService"] = "false";

// Реєстрація репозиторія та локальної реалізації
builder.Services.AddScoped<IWidgetService, LocalWidgetService>();
// Обгортка мосту
builder.Services.Decorate<IWidgetService, WidgetServiceBridge>();

builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.TypeInfoResolver = WidgetJsonContext.Default;
});

var app = builder.Build();

app.MapGet("/api/widgets", async (IWidgetService svc) =>
{
    return await svc.GetAllAsync();
});
app.MapGet("/api/widgets/{id}", async (int id, IWidgetService svc) =>
{
    return await svc.GetByIdAsync(id);
});
app.MapPost("/api/widgets", async (WidgetBase w, IWidgetService svc) =>
{
    await svc.CreateAsync(w);
});
app.MapPut("/api/widgets/{id}", async (int id, WidgetBase w, IWidgetService svc) => { w.Id = id; await svc.UpdateAsync(w); });
app.MapDelete("/api/widgets/{id}", async (int id, IWidgetService svc) => await svc.DeleteAsync(id));

app.Run();