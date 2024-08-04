using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ForumFlowServer.UserDao
{
    public class UserDao
    {
        private static SqliteConnection connection = new SqliteConnection("Data Source=forumflow.db");


        public bool userExists(string username)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE username = @username";
                command.Parameters.AddWithValue("@username", username);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        connection.Close();
                        return true;
                    }
                    connection.Close();
                    return false;
                }
            }
        }


        /// <summary>
        /// will insertUser Function will insert a new user into the database make sure to hash the password before calling this function and check if the user already exists
        /// </summary>
        public void createUser(string username, string passwordSalt, string passwordHash, string firstName, string lastName)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Users (username, passwordSalt, passwordHash, firstName, lastName) VALUES (@username, @passwordSalt, @passwordHash, @firstName, @lastName)";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                command.Parameters.AddWithValue("@passwordHash", passwordHash);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }


        public string getUserSalt(string username)
        {
            var salt = "";
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT passwordSalt FROM Users WHERE username = @username";
                command.Parameters.AddWithValue("@username", username);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                        salt = reader.GetString(0);
                    }
                }
            }
            connection.Close();
            return salt;
        }

        public bool authenticateUser(string username, string passwordHash)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE username = @username AND passwordHash = @passwordHash";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@passwordHash", passwordHash);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        connection.Close();
                        return true;
                    }
                    connection.Close();
                    return false;
                }
            }
        }





    }
}