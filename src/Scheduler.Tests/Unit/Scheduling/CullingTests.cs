using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Scheduler.Application.Options;
using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Services;
using Scheduler.Domain.Specifications;
using Scheduler.Services;
using Scheduler.Tests.Services;
using Xunit;

namespace Scheduler.Tests.Unit.Scheduling;

/// <summary>
/// Contains tests for <see cref="ScheduleCullingService"/>.
/// </summary>
public sealed class CullingTests
{
	private readonly Mock<IOptions<CullingOptions>> options;

	private readonly IScheduleRepository scheduleRepository;

	public CullingTests()
	{
		CullingOptions options = new() { Time = 3 };
		this.options = new Mock<IOptions<CullingOptions>>();

		this.options
			.Setup(o => o.Value)
			.Returns(options);

		this.scheduleRepository = new MockScheduleRepository();
	}

	/// <summary>
	/// Asserts that, when the time is past 3am, the background service will run.
	/// </summary>
	/// <returns>Whether the taask was completed or not.</returns>
	[Fact]
	public async Task PastThreeAm_Run()
	{
		// Arrange
		CancellationToken cancellationToken = CancellationToken.None;

		IServiceProvider services = new ServiceCollection()
			.AddSingleton<IDateProvider, MockDateProvider>()
			.AddScoped(p => this.scheduleRepository)
			.AddSingleton(this.options.Object)
			.BuildServiceProvider();

		ScheduleCullingService cullingService = new ScheduleCullingService(services);

		// Act
		await cullingService.StartAsync(cancellationToken);
		await cullingService.StopAsync(cancellationToken);

		// Assert
		IEnumerable<Event> events = await scheduleRepository.SearchAsync(
			new GetAllSpecification<Event>());

		Assert.Empty(events);
	}

	/// <summary>
	/// Asserts that, if the time is 3am, the background service will run.
	/// </summary>
	/// <returns></returns>
	[Fact]
	public async Task ThreeAm_Run()
	{
		// Arrange
		CancellationToken cancellationToken = CancellationToken.None;

		IServiceProvider services = new ServiceCollection()
			.AddSingleton<IDateProvider, MockDateProvider>()
			.AddScoped(p => this.scheduleRepository)
			.AddSingleton(this.options.Object)
			.BuildServiceProvider();

		ScheduleCullingService cullingService = new ScheduleCullingService(services);

		// Act
		await cullingService.StartAsync(cancellationToken);
		await cullingService.StopAsync(cancellationToken);

		// Assert
		IEnumerable<Event> events = await scheduleRepository.SearchAsync(
			new GetAllSpecification<Event>());

		Assert.Empty(events);
	}

	/// <summary>
	/// Asserts that, if the time is before 3am, the background servie won't run.
	/// </summary>
	/// <returns>Whether the task was completed or not.</returns>
	[Fact]
	public async Task BeforeThreeAm_WontRun()
	{
		// Arrange
		CancellationToken cancellationToken = CancellationToken.None;
		IDateProvider dateProvider = new MockDateProvider(new DateTime(2022, 3, 24, 0, 0, 0));

		IServiceProvider services = new ServiceCollection()
			.AddSingleton(p => dateProvider)
			.AddScoped(p => this.scheduleRepository)
			.AddSingleton(this.options.Object)
			.BuildServiceProvider();

		ScheduleCullingService cullingService = new ScheduleCullingService(services);

		// Act
		await cullingService.StartAsync(cancellationToken);
		await cullingService.StopAsync(cancellationToken);

		// Assert
		IEnumerable<Event> events = await scheduleRepository.SearchAsync(
			new GetAllSpecification<Event>());

		Assert.NotEmpty(events);
	}
}
