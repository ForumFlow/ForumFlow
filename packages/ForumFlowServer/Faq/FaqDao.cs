
// using Microsoft.Data.Sqlite;
// // accesc Pattern
// // insert a new Faq into a db based on the presentations ID insert (data) where presentationID = presentationID

// delete all Faqs from a db based on the presentations ID delete where presentationID = presentationID
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;


namespace FaqDao
{
    public class FaqDao
    {

        private static SqliteConnection connection = new SqliteConnection("Data Source=forum.db");

        public bool FaqExists(int presentationId)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Faq WHERE presentationId = @presentationId";
                command.Parameters.AddWithValue("@presentationId", presentationId);
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

        public void CreateFaq(int ID, int presentationId, string question, string answer, int displayOrder)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Faq (presentationId, question, answer, displayOrder) VALUES (@ID, @presentationId, @question, @answer, @createdDate)";
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@presentationId", presentationId);
                command.Parameters.AddWithValue("@question", question);
                command.Parameters.AddWithValue("@answer", answer);
                command.Parameters.AddWithValue("@displayOrder", displayOrder);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public string GetFaq(int ID)
        {
            var faq = "";

            connection.Open();

            using (var command = connection.CreateCommand())
            {
            
                command.CommandText = "SELECT presentationId, question, answer, displayOrder FROM Faqs WHERE FaqID = @faqID";
                command.Parameters.AddWithValue("@ID", ID);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                        faq = reader.GetString(0);
                    }
                }
            }

            connection.Close();
            return faq;
        }

        // public List<Faq> GetFaqs()
        // {
        //     List<Faq> faqs = new List<Faq>();

        //     connection.Open();

        //     using (var command = connection.CreateCommand())
        //     {
        //         command.CommandText = "SELECT FaqID, Question, Answer, CreateDate FROM Faqs";
        //         using (var reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 faqs.Add(new Faq
        //                 {
        //                     FaqID = reader.GetInt32(0),
        //                     Question = reader.GetString(1),
        //                     Answer = reader.GetString(2),
        //                     CreateDate = reader.GetDateTime(3)
        //                 });
        //             }
        //         }
        //     }
        // }
    }
}

//             }
//         }
//     }
// }