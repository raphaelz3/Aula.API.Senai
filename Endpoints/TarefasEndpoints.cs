using Microsoft.EntityFrameworkCore;
using Tarefas.API.Context;
using Tarefas.API.Model;

namespace Tarefas.API.Endpoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app)
        {
            app.MapGet("/Tarefas", async (TarefasDbContext db) => await db.Tarefas.ToListAsync());

            app.MapGet("/Tarefas/{id}", async (int id, TarefasDbContext db) =>
                await db.Tarefas.FindAsync(id) is Tarefa tarefa
                ? Results.Ok(tarefa)
                : Results.NotFound());

            app.MapPost("/Tarefas", async (Tarefa tarefa, TarefasDbContext db) =>
            {
                if (tarefa == null) return Results.BadRequest("Requisicao invalida");

                db.Tarefas.Add(tarefa);
                await db.SaveChangesAsync();

                return Results.Created($"/Tarefas/{tarefa.Id}", tarefa);
            });
            //Em construcao
        }
    }
}
