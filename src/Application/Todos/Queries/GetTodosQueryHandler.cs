using Domain.Abstractions.Repositories;
using MediatR;
using Shared.DTOs;

namespace Application.Todos.Queries;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<TodoDto>>
{
    private readonly ITodoRepository _repo;

    public GetTodosQueryHandler(ITodoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _repo.GetAllAsync(cancellationToken);
        return todos.Select(t => new TodoDto(t.Id, t.Title, t.IsCompleted, t.CreatedUtc)).ToList();
    }
}
