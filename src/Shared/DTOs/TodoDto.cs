namespace Shared.DTOs;

public record TodoDto(Guid Id, string Title, bool IsCompleted, DateTime CreatedUtc);
