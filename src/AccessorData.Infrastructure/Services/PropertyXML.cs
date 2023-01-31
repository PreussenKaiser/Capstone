using AccessorData.Core.Services;
using AccessorData.Core.Models;
using AccessorData.Core.Aggregates;

namespace AccessorData.Infrastructure.Services;

/// <summary>
/// Queries <see cref="Property"/> from a local XML file.
/// </summary>
public sealed class PropertyXML : IPropertyService
{
	public Task<Property?> GetAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Property>?> SearchAsync(PropertyLocation searchTerms)
	{
		throw new NotImplementedException();
	}
}
