using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.Core.Models;

/// <summary>
/// Represents a role held by a <see cref="User"/> and contains string representations of roles..
/// </summary>
/// <remarks>
/// Without this class, <see cref="IdentityRole{TKey}"/> would need to be referenced everywhere.
/// </remarks>
public sealed class Role : IdentityRole<Guid>
{
	/// <summary>
	/// A string representation of the 'Coach' role.
	/// </summary>
	[NotMapped]
	public const string COACH = "Coach";

	/// <summary>
	/// A string representation of the 'Admin' role.
	/// </summary>
	[NotMapped]
	public const string ADMIN = "Admin";
}
