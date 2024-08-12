// accesc Pattern
// insert a new Faq into a db based on the presentations ID insert (data) where presentationID = presentationID

// delete all Faqs from a db based on the presentations ID delete where presentationID = presentationID


namespace FaqDao
{
    public class FaqDao
    {

        private static SqliteConnection connection = new SqliteConnection("Data Source=forum.db");

        public bool FaqExists(int faqID)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Faqs WHERE FaqId = @faqId";
                command.Parameters.AddWithValue("@faqId", faqID);
                var count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count > 0;
            }
        }

        public void CreateFaq(int faqID, string question, string answer, DateTime createdDate)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Faqs (FaqID, Question, Answer, CreateDate) VALUES (@faqID, @question, @answer, @createdDate)";
                command.Parameters.AddWithValue("@faqID", faqID);
                command.Parameters.AddWithValue("@question", question);
                command.Parameters.AddWithValue("@answer", answer);
                command.Parameters.AddWithValue("@createdDate", createdDate);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public Faq GetFaq(int faqID)
        {
            Faq faq = null;

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT FaqID, Question, Answer, CreateDate FROM Faqs WHERE FaqID = @faqID";
                command.Parameters.AddWithValue("@faqID", faqID);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        faq = new Faq
                        {
                            FaqID = reader.GetInt32(0),
                            Question = reader.GetString(1),
                            Answer = reader.GetString(2),
                            CreateDate = reader.GetDateTime(3)
                        };
                    }
                }
            }

            connection.Close();
            return faq;
        }

        public List<Faq> GetFaqs()
        {
            List<Faq> faqs = new List<Faq>();

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT FaqID, Question, Answer, CreateDate FROM Faqs";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        faqs.Add(new Faq
                        {
                            FaqID = reader.GetInt32(0),
                            Question = reader.GetString(1),
                            Answer = reader.GetString(2),
                            CreateDate = reader.GetDateTime(3)
                        });
                    }
                }
