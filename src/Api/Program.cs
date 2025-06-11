using Application.Todos.Commands;
using Application.Todos.Queries;
using Infrastructure;
using Application;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddSignalR();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "API up & running 🚀");

var todos = app.MapGroup("/api/todos");

todos.MapGet("/", async (IMediator mediator) =>
{
    var list = await mediator.Send(new GetTodosQuery());
    return Results.Ok(list);
});

todos.MapPost("/", async (IMediator mediator, CreateTodoCommand cmd) =>
{
    var created = await mediator.Send(cmd);
    return Results.Created($"/api/todos/{created.Id}", created);
});

app.Run();
