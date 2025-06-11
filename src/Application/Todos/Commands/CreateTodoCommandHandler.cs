using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using MediatR;
using Shared.DTOs;

namespace Application.Todos.Commands;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDto>
{
    private readonly ITodoRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateTodoCommandHandler(ITodoRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = new Todo(request.Title);
        await _repo.AddAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return new TodoDto(entity.Id, entity.Title, entity.IsCompleted, entity.CreatedUtc);
    }
}
