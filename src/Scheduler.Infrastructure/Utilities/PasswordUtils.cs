using Scheduler.Core.Services;
using System.Security.Cryptography;
using System.Text;

namespace Scheduler.Infrastructure.Utilities;
public static class PasswordUtils
{
		
	public static string GenerateRandomPassword()
	{

		const int PASSWORD_LENGTH = 8;

		string lowerLetters = "qwertyuiopasdfghjklzxcvbnm";
		string upperLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
		string numbers = "0123456789";
		string specialCharacters = "!@#$%^&*_";

		StringBuilder builder = new StringBuilder();

		builder = builder.Append(GenerateChar(specialCharacters));
		builder = builder.Append(GenerateChar(numbers));
		builder = builder.Append(GenerateChar(numbers));
		builder = builder.Append(GenerateChar(upperLetters));
		while (builder.Length < PASSWORD_LENGTH)
		{
			builder = builder.Append(GenerateChar(lowerLetters));
		}

		string randomPassword = builder.ToString();

		Random random = new Random();
		randomPassword = new string(randomPassword.ToCharArray().OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

		return randomPassword;
	}

	private static char GenerateChar(string availableChars)
	{
		using (var generator = RandomNumberGenerator.Create())
		{
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
}
