using System.Security.Cryptography;
using System.Text;

namespace Scheduler.Domain.Utility;

/// <summary>
/// Password utility methods.
/// </summary>
public static class Password
{
	/// <summary>
	/// Generates a pseudo-random temporary password.
	/// </summary>
	/// <returns>The generated password.</returns>
	public static string Random()
	{
		//Some characters that are easily confused for one another are left out.
		//Examples: 0 and O, l and I.
		const int PASSWORD_LENGTH = 8;
		const string LOWER = "qwertyuiopasdfghjkzxcvbnm";
		const string UPPER = "QWERTYUPASDFGHJKLZXCVBNM";
		const string NUMBERS = "123456789";
		const string SPECIAL = "!@#$%^&*_";

		StringBuilder builder = new StringBuilder()
			.Append(GenerateChar(SPECIAL))
			.Append(GenerateChar(NUMBERS))
			.Append(GenerateChar(NUMBERS))
			.Append(GenerateChar(UPPER));

		while (builder.Length < PASSWORD_LENGTH)
			builder.Append(GenerateChar(LOWER));

		return new string(builder.ToString()
			.ToCharArray()
			.OrderBy(s => (new Random().Next(2) % 2) == 0)
			.ToArray());
	}

	/// <summary>
	/// Generates a random <see cref="char"/> from the provided <see cref="string"/>.
	/// </summary>
	/// <param name="availableChars"><see cref="string"/> pool to pull form.</param>
	/// <returns>The random <see cref="char"/>.</returns>
	private static char GenerateChar(string availableChars)
	{
		using var generator = RandomNumberGenerator.Create();
		var byteArray = new byte[1];
		char c;

		do
		{
			generator.GetBytes(byteArray);

			c = (char)byteArray[0];
		}
		while (!availableChars.Any(x => x == c));

		return c;
	}
}
