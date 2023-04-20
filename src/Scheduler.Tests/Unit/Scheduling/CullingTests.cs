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
	/// <summary>
	/// Asserts that, when the time is past 3am, the background service will run.
	/// </summary>
	/// <returns>Whether the taask was completed or not.</returns>
	[Fact]
	public async Task PastThreeAm_Run()
	{
		CancellationToken cancellationToken = CancellationToken.None;
		IScheduleRepository scheduleRepository = new MockScheduleRepository();
		IDateProvider dateProvider = new MockDateProvider();
		ScheduleCullingService cullingService = new ScheduleCullingService(
			scheduleRepository, dateProvider);

		await cullingService.StartAsync(cancellationToken);

		await Task.Delay(1000); // Give it enough time to run.

		await cullingService.StopAsync(cancellationToken);

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
		CancellationToken cancellationToken = CancellationToken.None;
		IScheduleRepository scheduleRepository = new MockScheduleRepository();
		IDateProvider dateProvider = new MockDateProvider(new DateTime(2022, 3, 24, 3, 0, 0));
		ScheduleCullingService cullingService = new ScheduleCullingService(
			scheduleRepository, dateProvider);

		await cullingService.StartAsync(cancellationToken);

		await Task.Delay(1000); // Give it enough time to run.

		await cullingService.StopAsync(cancellationToken);

		IEnumerable<Event> events = await scheduleRepository.SearchAsync(
			new GetAllSpecification<Event>());

		Assert.Empty(events);
	}

	/// <summary>
	/// Asserts that, if the time is before 3am, the background servie won't run.
	/// </summary>
	/// <returns></returns>
	[Fact]
	public async Task BeforeThreeAm_WontRun()
	{
		CancellationToken cancellationToken = CancellationToken.None;
		IScheduleRepository scheduleRepository = new MockScheduleRepository();
		IDateProvider dateProvider = new MockDateProvider(new DateTime(2022, 3, 24, 1, 0, 0));
		ScheduleCullingService cullingService = new ScheduleCullingService(
			scheduleRepository, dateProvider);

		await cullingService.StartAsync(cancellationToken);

		await Task.Delay(1000); // Give it enough time to run.

		await cullingService.StopAsync(cancellationToken);

		IEnumerable<Event> events = await scheduleRepository.SearchAsync(
			new GetAllSpecification<Event>());

		Assert.NotEmpty(events);
	}
}
