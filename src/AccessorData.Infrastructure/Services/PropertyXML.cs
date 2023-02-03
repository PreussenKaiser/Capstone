using AccessorData.Core.Services;
using AccessorData.Core.Models;
using AccessorData.Core.Aggregates;
using System.Xml.Serialization;
using AccessorData.Infrastructure.Models;

namespace AccessorData.Infrastructure.Services;

/// <summary>
/// Queries <see cref="Property"/> from a local XML file.
/// </summary>
/// <remarks>
/// May want to implement caching.
/// </reamrks>
public sealed class PropertyXML : IPropertyService
{
	/// <summary>
	/// Deserializes XML into <see cref="Property"/>.
	/// </summary>
	private readonly XmlSerializer serializer;

	/// <summary>
	/// The XML document to access.
	/// </summary>
	private readonly string fileName;

	/// <summary>
	/// Initializes the <see cref="PropertyXML"/> class.
	/// </summary>
	public PropertyXML(string fileName)
	{
		this.serializer = new XmlSerializer(typeof(PropertyCollection));
		this.fileName = fileName;
	}

	/// <summary>
	/// Gets a <see cref="Property"/> from the XML document.
	/// </summary>
	/// <param name="id">References <see cref="Property.Id"/>.</param>
	/// <returns>The found property.</returns>
	public Task<Property?> GetAsync(long id)
	{
		Property? property = null;

		using (Stream reader = new FileStream(this.fileName, FileMode.Open))
		{
			var collection = this.serializer.Deserialize(reader) as PropertyCollection;

			property = collection
				?.Properties
				?.FirstOrDefault(p => p.Id == id);
		}

		return Task.FromResult(property);
	}

	/// <summary>
	/// Searches for a <see cref="Property"/> in the XML document via it's <see cref="Location"/>.
	/// </summary>
	/// <param name="location">References <see cref="Property.Location"/>.</param>
	/// <returns>Properties that match the inputted <see cref="Location"/>.</returns>
	public Task<IEnumerable<Property>?> SearchAsync(Location location)
	{
		throw new NotImplementedException();
	}
}
