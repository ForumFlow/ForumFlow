using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace ForumFlowServer.CreateTables
{
    public class SqlUtil
    {
        // private readonly string dbSource = "Data Source=forumflow.db";
        private readonly string createTablesCmd = "SqlUtils/createTables.txt";

        private readonly string createTestDataCmd = "SqlUtils/DummyData.txt";
        //private readonly string createTestDataCmd = "TestingPresentation.txt";

        private static SqliteConnection connection = new SqliteConnection("Data Source=forumflow.db");

        /// <summary>
        /// Initializes new sql tables in our db && will delete all current data in db also
        /// </summary>

        public void createTables()
        {
            string sqlCommand = File.ReadAllText(createTablesCmd);
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sqlCommand;
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        public void createTestinData()
        {
            // using (var connection = new SqliteConnection(dbSource))
            // {
            string sqlCommand = File.ReadAllText(createTestDataCmd);
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sqlCommand;
                command.ExecuteNonQuery();
                connection.Open();
            }

            // }
        }

        public void CreatePresentation(string title, string description, int authorId)
        {


            connection.Open();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Presentation (title, description, authorId) VALUES (@title, @description, @authorId)";
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@authorId", authorId);
                    command.ExecuteNonQuery();
                }
            }

            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Retrieves and displays information about all users from the database.
        /// // string sqlCommand = "SELECT * FROM Users";
        // </summary>
        public void showAllUsers()
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT username, passwordSalt, passwordHash, firstName, lastName, ID FROM Users";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var username = reader.GetString(0);
                            var passwordSalt = reader.GetString(1);
                            Console.WriteLine($"userName: {username}\n passwordSalt: {passwordSalt}\n passwordHash: {reader.GetString(2)}\n firstName: {reader.GetString(3)}\n lastName: {reader.GetString(4)}\n ID: {reader.GetInt32(5)}");
                            Console.WriteLine("-------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No users found.");
                    }
                }
            }
        }
        public void showAllFaqs()
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT presentationId, question, answer, displayOrder FROM Faq";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var faqID = reader.GetInt32(0);
                            var question = reader.GetString(1);
                            var answer = reader.GetString(2);
                            var displayOrder = reader.GetInt32(3);
                            Console.WriteLine($"FaqID: {faqID}\nQuestion: {question}\nAnswer: {answer}\nDisplay Order: {displayOrder}");
                            Console.WriteLine("-------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No FAQs found.");
                    }
                }
            }

            connection.Close();
        }
        public void showAllPresentations()
        {

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT authorId, presentationId, title, description FROM Presentation";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var authorID = reader.GetInt32(0);
                            var presentationID = reader.GetInt32(1);
                            var title = reader.GetString(2);
                            var description = reader.GetString(3);


                            Console.WriteLine($"PresentationID: {presentationID}\n Title: {title}\n Description: {description}\n AuthorID: {authorID}");
                            Console.WriteLine("-------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No presentations found.");
                    }
                }
            }

        }







        // public void ExecuteNonQuery(string query)
        // {
        //     using (var connection = new SqliteConnection(_connectionString))
        //     {
        //         connection.Open();


        //         using (var command = connection.CreateCommand())
        //         {
        //             command.CommandText = query;
        //             command.ExecuteNonQuery();
        //         }
        //     }
        // }
    }

}






