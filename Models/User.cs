
namespace inventoryeyeback;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Table("users")]


public class User
{

    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public UserType UserType { get; set; } = UserType.NORMAL;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public double AddressLatitude { get; set; }
    public double AddressLongtitude { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public List<Post> Posts { get; set; } = new List<Post>();

    public long BirthDate { get; set; }
    public long LastSeen { get; set; } // Unix timestamp

    // public virtual Post Posts { get; set; }

    public static int Delete(string email)
    {
        UserDBservices dbs = new UserDBservices();
        return dbs.DeleteUser(email);
    }

    public int Update()
    {
        UserDBservices dbs = new UserDBservices();
        return dbs.UpdateUser(FullName, ImageUrl, UserType, Email, Password, AddressLatitude, AddressLongtitude, BirthDate);
    }


    public static List<User> Read()
    {
        UserDBservices dbs = new UserDBservices();
        return dbs.ReadUsers();
    }


}

