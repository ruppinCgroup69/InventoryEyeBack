namespace inventoryeyeback.Dto
{ 

    public class UserRegister
    {

        public string FullName { get; set; } = string.Empty;
        public UserType UserType { get; set; } = UserType.NORMAL;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public double AddressLatitude { get; set; }
        public double AddressLongtitude { get; set; }
        public string ImageUrl { get; set; }

        public long BirthDate { get; set; } 


        public int Insert()
        {
            UserDBservices dbs = new UserDBservices();
            return dbs.InsertUser(FullName, ImageUrl, UserType, Email, Password, AddressLatitude, AddressLongtitude, BirthDate);
        }
    }
}


