namespace inventoryeyeback;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Table("posts")]
public class Post
{
    [Key]
    public int PostId { get; set; }

    [ForeignKey("users")]
    public int UserId { get; set; }
    public DateTime DatePublished { get; set; }
    public string PostContent { get; set; } = string.Empty;
    public DateTime DateUpdated { get; set; }
    public int NumberOfComments { get; set; }
    public double AddressLatitude { get; set; }
    public double AddressLongtitude { get; set; }

    public CategoryType Category { get; set; }

    // Navigation property for the related User
    public virtual User? User { get; set; }



    public int NewPost()
    {
        PostDBservices dbServices = new PostDBservices();
        return dbServices.newPostDB(UserId, PostContent,NumberOfComments, AddressLatitude, AddressLongtitude, Category);
    }

    public int Delete(int postId)
    {
        PostDBservices dbs = new PostDBservices();
        return dbs.DeletePost(postId);
    }

    public int Update()
    {
        PostDBservices dbs = new PostDBservices();
        return dbs.UpdatePost(PostContent, NumberOfComments, AddressLatitude, AddressLongtitude, Category);
    }








}


