using MediatR;
using Shared.DTOs;

namespace Application.Todos.Commands;

public record CreateTodoCommand(string Title) : IRequest<TodoDto>;
