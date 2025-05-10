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
            app.MapPut("/Tarefas/{id}", async (Guid id, Tarefa tarefaAtualizada, TarefasDbContext db) =>
            {
                var tarefa = await db.Tarefas.FindAsync(id);

                if (tarefa is null)
                {
                    return Results.NotFound();
                }

                tarefa.Nome = tarefaAtualizada.Nome != null ? tarefaAtualizada.Nome : tarefa.Nome;
                tarefa.Detalhe = tarefaAtualizada.Nome != null ? tarefaAtualizada.Detalhe : tarefa.Detalhe;
                tarefa.CategoriaId = tarefaAtualizada.CategoriaId != 0 ? tarefaAtualizada.CategoriaId : tarefa.CategoriaId;
                if (tarefaAtualizada.Concluida)
                {
                    tarefa.ConcluirTarefa();
                }

                await db.SaveChangesAsync();

                return Results.Ok("Tarefa atualizada com sucesso!");
            });

            app.MapPut("/Tarefas/{id}/Concluir", async (Guid id, TarefasDbContext db) =>
            {
                var tarefa = await db.Tarefas.FindAsync(id);

                if (tarefa is null)
                {
                    return Results.NotFound();
                }

                tarefa.Concluida = true;
                tarefa.ConcluirTarefa();

                await db.SaveChangesAsync();

                return Results.Ok("Tarefa concluida com sucesso!");
            });

            app.MapDelete("/Tarefas/{id}", async (Guid id, TarefasDbContext db) =>
            {
                var tarefa = await db.Tarefas.FindAsync(id);

                if (tarefa is null)
                {
                    return Results.NotFound();
                }

                db.Tarefas.Remove(tarefa);

                await db.SaveChangesAsync();

                return Results.Ok("Tarefa Deletada com sucesso!");
            });
        }
    }
}
