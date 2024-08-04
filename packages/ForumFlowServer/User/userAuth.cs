using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;

class UserAuthentication
{
    // private static string connectionString = "Data Source=mydatabase.db";

    private static string GetHash(string text)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    private static string GetSalt()
    {
        var randomNumber = new byte[32];
        RandomNumberGenerator.Fill(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private static void Register(string username, string password, string firstName, string lastName)
    {

    }



}