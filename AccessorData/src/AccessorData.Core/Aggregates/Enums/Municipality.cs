using System.Xml.Serialization;

namespace AccessorData.Core.Aggregates.Enums;

/// <summary>
/// Municipalities with assessed properties.
/// </summary>
public enum Municipality : ushort
{
	[XmlEnum("City of Stevens Point")]
	City_of_Stevens_Point
}
