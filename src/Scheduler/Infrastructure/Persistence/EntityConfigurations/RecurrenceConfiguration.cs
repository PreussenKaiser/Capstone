using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Models;

namespace Scheduler.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Provides configuration for the <see cref="Recurrence"/> entity.
/// </summary>
public sealed class RecurrenceConfiguration : IEntityTypeConfiguration<Recurrence>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Recurrence> builder)
	{
		builder.Metadata
			.FindNavigation(nameof(Recurrence.Events))
			?.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}
