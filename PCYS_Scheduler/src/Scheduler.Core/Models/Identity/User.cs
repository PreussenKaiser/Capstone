using Microsoft.AspNetCore.Identity;

namespace Scheduler.Core.Models.Identity;

/// <summary>
/// Represents a user in the scheduler.
/// </summary>
/// <remarks>
/// Without this class, <see cref="IdentityUser{TKey}"/> would need to be referenced everywhere.
/// </remarks>
public sealed class User : IdentityUser<Guid>
{
}
