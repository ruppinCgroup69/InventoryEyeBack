

using inventoryeyeback;

namespace inventoryEyeBack;

public class PostWithUser : Post
{

    public PostWithUser(){}
     public PostWithUser(Post postCreation, User user)
    {
        this.AddressLatitude = postCreation.AddressLatitude;
        this.AddressLongtitude = postCreation.AddressLongtitude;
        this.PostContent = postCreation.PostContent;
        this.Category = postCreation.Category;
        this.UserId = postCreation.UserId;
        this.Tags = postCreation.Tags;
        this.ImageUrl = postCreation.ImageUrl;
        this.PostOwnerEmail = user.Email;
        this.PostOwnerName = user.FullName;
        this.PostOwnerImage = user.ImageUrl;
    }
    public string PostOwnerName { get; set; } = string.Empty;
    public string PostOwnerEmail { get; set; } = string.Empty;
    public string PostOwnerImage { get; set; } = string.Empty;

    public static List<PostWithUser> Read()
    {
        PostDBservices dbs = new PostDBservices();
        return dbs.ReadPostsWithUsers();
    }

    public static List<PostWithUser> ReadByUser(int userId)
    {
        PostDBservices dbs = new PostDBservices();
        return dbs.GetPostsByUserId(userId);
    }
    public static List<PostWithUser> ReadByCategory(int category)
    {
        PostDBservices dbs = new PostDBservices();
        return dbs.GetPostsByCategory(category);
    }
    public static PostWithUser ReadById(int postId)
    {
        PostDBservices dbs = new PostDBservices();
        return dbs.GetPostById(postId);
    }
}