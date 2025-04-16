using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BookRest.Extensions;

public static class HashStringExtension
{
    /// <summary>
    /// Creates hashed version of the string.
    /// Preferred to use for generating passwords.
    /// </summary>
    /// <param name="input"></param>
    /// <returns>Hashed string</returns>
    public static string HashString(this string input)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
        
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: input!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hashed;
    }
}