using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Models;

namespace Scheduler.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Contains configuration for <see cref="Event"/>.
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
	/// <inheritdoc/>
	public void Configure(EntityTypeBuilder<Event> builder)
	{
		builder.UseTptMappingStrategy();

		builder.HasKey(e => e.Id);

		builder
			.Property(e => e.FieldId)
			.IsRequired(false);

		builder
			.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(32);

		builder
			.Property(e => e.StartDate)
			.IsRequired();

		builder
			.Property(e => e.EndDate)
			.IsRequired();

		builder
			.Property(e => e.IsBlackout)
			.HasDefaultValue(false);
	}
}
