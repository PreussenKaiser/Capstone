using AccessorData.Core.Aggregates;
using AccessorData.Core.Models;

namespace AccessorData.Core.Services;

/// <summary>
/// Implements read-only query methods for <see cref="Property"/>
/// </summary>
public interface IPropertyService
{
	/// <summary>
	/// Gets a <see cref="Property"/>
	/// </summary>
	/// <param name="id">A value referencing <see cref="Property.Id"/>.</param>
	/// <returns>A possible property.</returns>
	Task<Property?> GetAsync(int id);

	/// <summary>
	/// Gets properties based off of it's location.
	/// </summary>
	/// <param name="location">The search aggregate to use.</param>
	/// <returns>Properties that match the search.</returns>
	Task<IEnumerable<Property>?> SearchAsync(PropertyLocation location);
}
