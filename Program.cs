using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Sistema de Gerenciamento de Clientes para uma Oficina Mecânica ");

// Clientes

app.MapGet("/clientes", async (AppDbContext db) => 
    await db.Clientes.ToListAsync());

app.MapGet("/clientes/{id}", async (int id, AppDbContext db) => 
    await db.Clientes.FindAsync(id)
    is Cliente cliente
        ? Results.Ok(cliente)
            : Results.NotFound());

app.MapPost("/clientes", async (Cliente cliente, AppDbContext db) => {
    db.Clientes.Add(cliente);
        await db.SaveChangesAsync();
        return Results.Created($"/tarefas/{cliente.Id}", cliente);
        });

app.MapPut("/clientes/{id}", async (int id, Cliente clienteAlterado, AppDbContext db) =>
    {
        var cliente = await db.Clientes.FindAsync(id);
        if (cliente is null) return Results.NotFound();
        
        cliente.Nome = clienteAlterado.Nome;
        clienteAlterado.Telefone = cliente.Telefone;
        clienteAlterado.Email = cliente.Email;

        await db.SaveChangesAsync();
        return Results.NoContent();
    });

app.MapDelete("/cliente/{id}", async (int id, AppDbContext db) => 
{
    if (await db.Clientes.FindAsync(id) is Cliente cliente) {
        db.Clientes.Remove(cliente);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.Run();
