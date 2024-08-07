using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using PresentationApp;

namespace PresentationDao{


    public class PresentationDao{




        private static SqliteConnection connection = new SqliteConnection("Data Source=forum.db");


        public bool PresentationExists(int presentationID){
            connection.Open();
            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT COUNT(*) FROM Presentations WHERE PresentationId = @presentationId";
                command.Parameters.AddWithValue("@presentationId", presentationID);
                var count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count > 0;
            }
        }




        public void CreatePresentation(int presentationID, string title, string description, DateTime createdDate){
            connection.Open();
            using (var command = connection.CreateCommand()){
                command.CommandText = "INSERT INTO Presentations (PresentationID, Title, Description, CreateDate) VALUES (@presentationID, @title, @description, @createdDate)";
                command.Parameters.AddWithValue("@presentationID", presentationID);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@createdDate", createdDate);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }




        public Presentation GetPresenation(int presentationID){
            Presentation presentation = null;


            connection.Open();


            using (var command = connection.CreateCommand()){
                command.CommandText = "SELECT PresentationID, Title, Description, CreateDate FROM Presentations WHERE PresentationID = @presentationID";
                command.Parameters.AddWithValue("@presentationID", presentationID);
                using (var reader = command.ExecuteReader()){
                    if (reader.Read()){
                        presentation = new Presentation{
                            PresentationID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            CreatedDate = reader.GetDateTime(3)
                        };
                    }
                }
                connection.Close();
            }
            return presentation;


           
        }


        public void DeletePresentation(int presentationID){


            connection.Open();
            using (var command = connection.CreateCommand()){
                command.CommandText = "DELETE FROM Presentations WHERE PresentationID = @presentationID";
                command.Parameters.AddWithValue("@presentationID", presentationID);
                command.ExecuteNonQuery();
            }
            connection.Close();


        }


        public void UpdatePresentation(int presentationID, string title, string description, DateTime updateDate){
            connection.Open();


            using (var command = connection.CreateCommand()){
                command.CommandText = "UPDATE Presentation SET Title = @title, Description = @description, UpdateDate = @updateDate WHERE PresentaionID = @presentationID";
                command.Parameters.AddWithValue("@presentationID", presentationID);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@updateDate", updateDate);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }


       




    }




}
