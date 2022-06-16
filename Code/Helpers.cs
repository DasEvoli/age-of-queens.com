
using System;
using System.Linq;
using Ageofqueenscom.Data;
using Microsoft.EntityFrameworkCore;

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

	public static bool isAdmin(string session_id, string username, DataContext dbContext)
	{
		try
		{
			var user = dbContext.Users.FirstOrDefault(s => s.UserName == username && s.Session == session_id);
			if(user != null){
				return true;
			}
			else 
			{
				return false;
			}
		}
		catch(Exception e)
		{
			return false;
		}

		
	}
}

