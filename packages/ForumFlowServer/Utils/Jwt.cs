using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ForumFlowServer.JWT
{
    public class JWT
    {
        public static string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

        public static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with '='s
            {
                case 0: break; // No padding needed.
                case 2: output += "=="; break; // Two pad chars needed
                case 3: output += "="; break; // One pad char needed
            }
            return Convert.FromBase64String(output);
        }

        public static string CreateToken(string headerJson, string payloadJson, string secret)
        {
            var header = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));
            var payload = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));
            var signature = GenerateSignature(header, payload, secret);
            return $"{header}.{payload}.{signature}";
        }
        private static string GenerateSignature(string header, string payload, string secret)
        {
            using (var sha256 = new HMACSHA256(Encoding.ASCII.GetBytes(secret)))
            {
                var input = $"{header}.{payload}";
                var hash = sha256.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Base64UrlEncode(hash);
            }
        }

        public static bool ValidateToken(string token, string secret)
        {
            var parts = token.Split('.');
            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];

            var expectedSignature = GenerateSignature(header, payload, secret);
            return expectedSignature == signature;
        }

    }
}

// class Program
// {
//     static void Main()
//     {
//         var header = JsonConvert.SerializeObject(new { alg = "HS256", typ = "JWT" });
//         var payload = JsonConvert.SerializeObject(new { sub = "1234567890", name = "John Doe", iat = 1516239022 });

//         var secret = "your-256-bit-secret";
//         var token = JWT.CreateToken(header, payload, secret);
//         Console.WriteLine("JWT: " + token);

//         var isValid = JWT.ValidateToken(token, secret);
//         Console.WriteLine("Is Token Valid? " + isValid);
//     }
// }
