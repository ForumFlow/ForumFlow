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

        public void showAllPresentations(){

            connection.Open();

            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT title, description, authorId FROM Presentation";
                using (var reader = command.ExecuteReader()){
                    if (reader.HasRows){
                        while (reader.Read()){
                            var presentationID = 0;
                            // var title = reader.GetString(1);
                            var title = "Test Title";
                            // var description = GetString(2);
                            var description = "Test Description";
                            var authorID = reader.GetInt32(2);
                            Console.WriteLine($"PresentationID: {presentationID}\n Title: {title}\n Description: {description}\n AuthorID: {authorID}");
                            Console.WriteLine("-------------------------------------------------");
                        }
                    }
                    else{
                        Console.WriteLine("No presentations found.");
                    }
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







