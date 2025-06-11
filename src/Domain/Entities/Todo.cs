namespace Domain.Entities;

/// <summary>
/// Aggregate root that represents a task item.
/// </summary>
public class Todo
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedUtc { get; private set; } = DateTime.UtcNow;

    public Todo(string title)
    {
        Title = string.IsNullOrWhiteSpace(title)
            ? throw new ArgumentException("Title cannot be empty", nameof(title))
            : title;
    }

    public void Complete() => IsCompleted = true;
}
