using Flunt.Notifications;

namespace Store.Domain.Entities;

public class Entity : Notifiable<Notification>, IComparable
{
    public Guid Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public int CompareTo(object? other)
    {
        return this.CompareTo(other);
    }
}
