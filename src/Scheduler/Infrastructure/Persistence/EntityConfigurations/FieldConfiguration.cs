using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Models;

namespace Scheduler.Infrastructure.Persistence.EntityConfigurations;

public class FieldConfiguration : IEntityTypeConfiguration<Field>
{
	public void Configure(EntityTypeBuilder<Field> builder)
	{
		builder
			.Property(f => f.Name)
			.IsRequired()
			.HasMaxLength(32);

		builder
			.Metadata
			.FindNavigation(nameof(Field.Events))
			?.SetPropertyAccessMode(PropertyAccessMode.Field);

		builder
			.HasMany(f => f.Events)
			.WithOne(e => e.Field)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
