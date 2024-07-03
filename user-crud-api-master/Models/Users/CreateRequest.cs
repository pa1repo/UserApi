namespace WebApi.Models.Users;

using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

public class CreateRequest
{
    public int user_id { get; set; }
    [Required]
    public string user_name { get; set; }

    [Required]
    public string first_name { get; set; }

    [Required]
    public string last_name { get; set; }

    [Required]
    [EmailAddress]
    public string email { get; set; }

    [Required]
    public string user_status { get; set; }

    public string? department { get; set; }
}