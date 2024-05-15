
namespace inventoryeyeback;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("users")]
[PrimaryKey("UserId")]
public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public UserType UserType { get; set; } = UserType.NORMAL;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public double AddressLatitude { get; set; }
    public double AddressLongtitude { get; set; }

    public long? BirthDate { get; set; }
    public long LastSeen { get; set; } // Unix timestamp

}