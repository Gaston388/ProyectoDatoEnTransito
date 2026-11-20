using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Persistencia.Conexiones;
using Persistencia.Repositorios;
using Persistencia.Repositorios.InterfazRepositorios;
using API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ConexionDesarrollador>();

// cuando la api necesite un servicio, .NET sabra como construirlo y que objetos necesita
builder.Services.AddScoped<IPaqueteRedRepositorio, PaqueteRedRepositorio>();
builder.Services.AddScoped<ISimulacionRepositorio, SimulacionRepositorio>();
builder.Services.AddScoped<IAccessPointRepositorio, AccessPointRepositorio>();
builder.Services.AddScoped<IFirewallRepositorio, FirewallRepositorio>();
builder.Services.AddScoped<IRouterRepositorio, RouterRepositorio>();
builder.Services.AddScoped<ISwitchRepositorio, SwitchRepositorio>();


builder.Services.AddScoped<IRouterServicio, RouterServicio>();
builder.Services.AddScoped<ISwitchServicio, SwitchServicio>();
builder.Services.AddScoped<IFirewallServicio, FirewallServicio>();
builder.Services.AddScoped<ISimulacionServicio, SimulacionServicio>();
builder.Services.AddScoped<IPaqueteServicio, PaqueteServicio>();
builder.Services.AddScoped<IAccessPointServicio, AccessPointServicio>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, cancellationToken) =>
    {
        // Verificamos usando los enums nativos de OpenAPI
        if (schema.Type.HasValue)
        {
            var tipo = schema.Type.Value;
            // Si el esquema tiene combinados Integer y String (el bug de .NET 9 con Swagger)
            if (tipo.HasFlag(Microsoft.OpenApi.JsonSchemaType.Integer) && tipo.HasFlag(Microsoft.OpenApi.JsonSchemaType.String))
            {
                schema.Type = Microsoft.OpenApi.JsonSchemaType.Integer;
            }
        }
        return Task.CompletedTask;
    });
}); 
// este bloque esta hecho para forzar a .net a cambiar de tipo de dato que genera en su archivo
// para que swagger lo entienda bien

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/openapi/v1.json" , "v1"));
}

app.UseHttpsRedirection();

app.MapSimulacionEndpoints();

app.MapRouterEndpoints();

app.MapSwitchEndpoints();

app.MapFirewallEndpoints();

app.MapAccessPointEndpoints();
//ruta http , recibe un servicio y nos entrega una instancia, 
app.Run();
