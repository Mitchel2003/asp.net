using MediatR;
using Shared.DTOs;

namespace Application.Todos.Queries;

public record GetTodosQuery : IRequest<List<TodoDto>>;
