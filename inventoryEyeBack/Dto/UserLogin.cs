namespace inventoryeyeback.Dto
{
    public class UserLogin
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public static User Login(string email, string password)
        {
            UserDBservices dbs = new UserDBservices();
            List<User> users = dbs.ReadUsers();
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if (user.Email == email && user.Password.Trim() == password)
                {
                    return user;
                }
            }
            return null;

        }

    }


}



