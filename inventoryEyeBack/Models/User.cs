
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

    public long BirthDate { get; set; }
    public long LastSeen { get; set; } // Unix timestamp

   // public virtual Post Posts { get; set; }


    public int Delete(string email)
    {
        UserDBservices dbs = new UserDBservices();
        return dbs.DeleteUser(email);

    }

    public int Update()
    {
        UserDBservices dbs = new UserDBservices();
        return dbs.UpdateUser(FullName,UserType, Email, Password, AddressLatitude, AddressLongtitude, BirthDate);
    }

  
    public List<User> Read()
    {
        UserDBservices dbs =  new UserDBservices();
        return dbs.ReadUsers();
    }


}

