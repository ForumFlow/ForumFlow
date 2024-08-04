public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
}


public class newUsersPostRequest
{
    public string? username { get; set; }
    
    public string? password { get; set; }
    
    public string? firstName { get; set; }
    
    public string? lastName { get; set; }
}
