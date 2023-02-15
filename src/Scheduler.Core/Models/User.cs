using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a user in the scheduler.
/// </summary>
/// <remarks>
/// Without this class, <see cref="IdentityUser{TKey}"/> would need to be referenced everywhere.
/// </remarks>
public sealed class User : IdentityUser<Guid>
{
}
