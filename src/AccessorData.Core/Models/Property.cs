using AccessorData.Core.Aggregates;

namespace AccessorData.Core.Models;

/// <summary>
/// Represents an assessed property.
/// </summary>
/// <param name="Id">The property's unique identifier.</param>
/// <param name="SearchDetails">Property details used for search.</param>
public sealed record Property(
	int Id,
	PropertyLocation SearchDetails);
