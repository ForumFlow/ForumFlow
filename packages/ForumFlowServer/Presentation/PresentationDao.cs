using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using PresentationApp;




namespace PresentationDao{
    public class PresentationDao{


        private static SqliteConnection connection = new SqliteConnection("Data Source=forum.db");

        public bool PresentationExists(int presentationId){
            connection.Open();
            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT COUNT(*) FROM Presentation WHERE presentationId = @presentationId";
                command.Parameters.AddWithValue("@presentationId", presentationId);
                var count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count > 0;
            }
        }


        public void CreatePresentation(int presentationId, string title, string description, int authorId){
            
            
            connection.Open();
            try {
                using (var command = connection.CreateCommand()){
                    command.CommandText = "INSERT INTO Presentation (title, description, authorId) VALUES (@title, @description, @authorId)";
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);              
                    command.Parameters.AddWithValue("@authorId", authorId);
                    command.ExecuteNonQuery();
                }
            }

            finally {
                connection.Close();
            }
        }


        public Presentation GetPresentation(int presentationId){
            Presentation presentation = null;


            connection.Open();


            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT presentationId, title, description, authorId FROM Presentation WHERE presentationId = @presentationId";
                command.Parameters.AddWithValue("@presentationId", presentationId);
                using (var reader = command.ExecuteReader()){
                    if (reader.Read()){
                        presentation = new Presentation{
                            presentationId = reader.GetInt32(0),
                            // Title = reader.GetString(1),
                            title = "Testing",
                            description = reader.GetString(2),
                            authorId = reader.GetInt32(3)
                        };
                    }
                }
                connection.Close();
            }
            return presentation;
         
        }

        public void DeletePresentation(int presentationId){


            connection.Open();
            using (var command = connection.CreateCommand()){
                command.CommandText = "DELETE FROM Presentation WHERE presentationId = @presentationId";
                command.Parameters.AddWithValue("@presentationId", presentationId);
                command.ExecuteNonQuery();
            }
            connection.Close();


        }


        public void UpdatePresentation(int presentationId, string title, string description, int authorId){
            connection.Open();


            using (var command = connection.CreateCommand()){
                command.CommandText = "UPDATE Presentation SET title = @title, description = @description, authorID = @authorId WHERE presentationId = @presentationId";
                command.Parameters.AddWithValue("@presentationId", presentationId);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@authorId", authorId);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }


    }




}
