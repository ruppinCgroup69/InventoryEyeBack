using inventoryeyeback;

namespace inventoryEyeBack;


public class PostCreation
{
    public int UserId { get; set; }
    public string ImageUrl {get; set;} = string.Empty;
    public string PostContent { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;

    public CategoryType Category { get; set; }
    public double AddressLatitude { get; set; }
    public double AddressLongtitude { get; set; }
}