namespace inventoryeyeback;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using inventoryEyeBack;

[Table("posts")]
public class Post
{

    public Post() { }

    public Post(PostCreation postCreation)
    {
        this.AddressLatitude = postCreation.AddressLatitude;
        this.AddressLongtitude = postCreation.AddressLongtitude;
        this.PostContent = postCreation.PostContent;
        this.Category = postCreation.Category;
        this.UserId = postCreation.UserId;
        this.Tags = postCreation.Tags;
        this.ImageUrl = postCreation.ImageUrl;
    }
    [Key]
    public int PostId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public long DatePublished { get; set; }
    public string PostContent { get; set; } = string.Empty;
    public long DateUpdated { get; set; }
    public int NumberOfComments { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public double AddressLatitude { get; set; }
    public double AddressLongtitude { get; set; }

    public CategoryType Category { get; set; }

    public string Tags { get; set; } = string.Empty;


    public int NewPost()
    {
        PostDBservices dbServices = new PostDBservices();
        return dbServices.newPostDB(UserId, Tags, ImageUrl, PostContent, NumberOfComments, AddressLatitude, AddressLongtitude, Category);
    }

    public static int Delete(int postId)
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


