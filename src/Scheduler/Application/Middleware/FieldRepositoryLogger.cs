using Scheduler.Domain.Models;
using Scheduler.Domain.Repositories;
using Scheduler.Domain.Specifications;

namespace Scheduler.Application.Middleware;

/// <summary>
/// Provides logging for a <see cref="IFieldRepository"/>.
/// </summary>
public sealed class FieldRepositoryLogger : IFieldRepository
{
	/// <summary>
	/// The repository to log.
	/// </summary>
	private readonly IFieldRepository fieldRepository;

	/// <summary>
	/// The logger to use.
	/// </summary>
	private readonly ILogger<IFieldRepository> logger;

	/// <summary>
	/// Initializes the <see cref="FieldRepositoryLogger"/> class.
	/// </summary>
	/// <param name="fieldRepository">The repository to log.</param>
	/// <param name="logger">The logger to use.</param>
	public FieldRepositoryLogger(
		IFieldRepository fieldRepository,
		ILogger<IFieldRepository> logger)
	{
		this.fieldRepository = fieldRepository;
		this.logger = logger;
	}

	/// <inheritdoc/>
	public async Task AddAsync(Field field)
	{
		try
		{
			await this.fieldRepository.AddAsync(field);

			this.logger.LogDebug($"Field {field.Name} added.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task<IEnumerable<Field>> SearchAsync(
		Specification<Field> searchSpec)
	{
		IEnumerable<Field>? fields = null;

		try
		{
			fields = await this.fieldRepository.SearchAsync(searchSpec);

			this.logger.LogInformation($"{fields.Count()} fields queried.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}

		return fields ?? Enumerable.Empty<Field>();
	}

	/// <inheritdoc/>
	public async Task UpdateAsync(Field field)
	{
		try
		{
			await this.fieldRepository.UpdateAsync(field);

			this.logger.LogInformation($"Field {field.Name} updated.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}

	/// <inheritdoc/>
	public async Task RemoveAsync(Guid id)
	{
		try
		{
			await this.fieldRepository.RemoveAsync(id);

			this.logger.LogInformation($"Fielf with id {id} removed.");
		}
		catch (Exception exception)
		{
			this.logger.LogError(exception.Message);

			throw;
		}
	}
}
