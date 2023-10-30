namespace IdentityBank.Domain.Models;

public class UserDeletedEvent
{
    public Guid UserId { get; set; }

    public DateTime DeletedAt { get; set; }
}