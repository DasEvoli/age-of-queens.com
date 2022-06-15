
using System;
using System.Linq;

namespace ageofqueenscom.code;

public class Helpers
{
	public static string GetRandomString(int length){
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var stringChars = new char[length];
		var random = new Random();

		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[random.Next(chars.Length)];
		}

		return new String(stringChars);
	}
}

