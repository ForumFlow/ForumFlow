public class FaqItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
}


public class newFaqPostRequest
{
    public int ID { get; set; }
    public int presentationId { get; set; }
    public string? question { get; set; }
    public string? answer { get; set; }
    public int displayOrder { get; set; }
}