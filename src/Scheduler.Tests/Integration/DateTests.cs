using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Scheduler.Core.Models;
using Scheduler.Core.Services;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Scheduler.Tests;

public class DateTests : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly WebApplicationFactory<Program> factory;

	public DateTests(WebApplicationFactory<Program> webApplicationFactory)
	{
		this.factory = webApplicationFactory;
	}

	[Fact]
	public async Task Overlap_Complete()
	{
		// Arrange
		if (this.factory.Services.GetService(typeof(IScheduleService)) is not IScheduleService service)
		{
			Assert.Fail("Could not get IScheduleService.");

			return;
		}

		Event scheduledEvent = new()
		{
			Id = Guid.NewGuid(),
			UserId = Guid.Empty,
			Name = "Event",
			StartDate = new DateTime(2023, 03, 24, 12, 0, 0),
			EndDate = new DateTime(2023, 03, 24, 14, 0, 0),
			IsRecurring = default,
		};

		await service.CreateAsync(scheduledEvent);

		// Act
		IEnumerable<ValidationResult> result = scheduledEvent.Validate(new(scheduledEvent, this.factory.Services, null));
		
		await service.DeleteAsync(scheduledEvent.Id);

		// Assert
		Assert.True(result.Any());
	}
}