using Microsoft.AspNetCore.Identity;

namespace Scheduler.Core.Models.Identity;

/// <summary>
/// Represents a role held by a <see cref="User"/>.
/// </summary>
/// <remarks>
/// Without this class, <see cref="IdentityRole{TKey}"/> would need to be referenced everywhere.
/// </remarks>
public sealed class Role : IdentityRole<Guid>
{
}
