namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int user_id { get; set; }
    public string user_name { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string user_status { get; set; }

    public string? department { get; set; }
}