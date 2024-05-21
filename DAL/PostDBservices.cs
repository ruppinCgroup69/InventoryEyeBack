namespace inventoryeyeback;

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using inventoryeyeback.Dto;
using System.Runtime.InteropServices;
using System.Net;
using inventoryeyeback;
using inventoryEyeBack;

public class PostDBservices
{
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("MySqlConnectionString");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // Class Post
    //--------------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------------
    // This method inserts a new post to the posts table 
    //--------------------------------------------------------------------------------------------------

    public int newPostDB(int UserId, string Tags, string ImageUrl, string PostContent, int NumberOfComments, double AddressLatitude, double AddressLongitude, CategoryType Category)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("MySqlConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateNewPostWithStoredProcedure("SP_newPost", con, UserId, Tags, ImageUrl, PostContent, NumberOfComments, AddressLatitude, AddressLongitude, Category); // create the command

        try
        {
            // 0 - failure, 1 - success
            cmd.ExecuteScalar(); // execute the command
            return 1;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - newPost
    //---------------------------------------------------------------------------------

    private SqlCommand CreateNewPostWithStoredProcedure(string spName,
     SqlConnection con, int UserId, string Tags, string ImageUrl, string PostContent, int NumberOfComments,
     double AddressLatitude, double AddressLongitude, CategoryType Category)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
        cmd.Parameters.AddWithValue("@userId", UserId);
        cmd.Parameters.AddWithValue("@datePublished", DateTime.UtcNow.Millisecond);
        cmd.Parameters.AddWithValue("@postContent", PostContent);
        cmd.Parameters.AddWithValue("@numberOfComments", NumberOfComments);
        cmd.Parameters.AddWithValue("@tags", Tags);
        cmd.Parameters.AddWithValue("@imageUrl", ImageUrl);
        cmd.Parameters.AddWithValue("@dateUpdated", DateTime.UtcNow.Millisecond);
        cmd.Parameters.AddWithValue("@addressLatitude", AddressLatitude);
        cmd.Parameters.AddWithValue("@addressLongitude", AddressLongitude);
        cmd.Parameters.AddWithValue("@category", Category);
        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method gets all a posts from the posts table by category
    //--------------------------------------------------------------------------------------------------

    public List<PostWithUser> GetPostsByCategory(int category)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("MySqlConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateGetPostByCategoryWithStoredProcedure("SP_ReadPostsByCategory", con, category); // create the command

        try
        {
            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<PostWithUser> posts = new List<PostWithUser>();
            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {

                PostWithUser u = new PostWithUser();
                u.UserId = int.Parse(dataReader["UserId"].ToString()); ;
                u.PostOwnerName = dataReader["FullName"].ToString();
                u.PostOwnerImage = dataReader["PostOwnerImage"].ToString();
                u.Category = (CategoryType)Enum.Parse(typeof(CategoryType), dataReader["Category"].ToString());
                u.PostOwnerEmail = dataReader["PostOwnerEmail"].ToString();
                u.Tags = dataReader["Tags"].ToString();
                u.ImageUrl = dataReader["ImageUrl"].ToString();
                u.PostContent = dataReader["PostContent"].ToString();
                u.DatePublished = long.Parse(dataReader["DatePublished"].ToString());
                u.NumberOfComments = int.Parse(dataReader["NumberOfComments"].ToString());

                u.DateUpdated = long.Parse(dataReader["DateUpdated"].ToString());

                u.AddressLatitude = double.Parse(dataReader["AddressLatitude"].ToString());
                u.AddressLongtitude = double.Parse(dataReader["AddressLongtitude"].ToString());
                posts.Add(u);
            }
            return posts;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method gets all a posts from the posts table by userId
    //--------------------------------------------------------------------------------------------------

    public List<PostWithUser> GetPostsByUserId(int userId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("MySqlConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateGetPostByUserIdWithStoredProcedure("SP_ReadPostsByUserId", con, userId); // create the command

        try
        {
            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<PostWithUser> posts = new List<PostWithUser>();
            // iterate over the results, next moves to the next record
            while (dataReader.Read())
            {

                PostWithUser u = new PostWithUser();
                u.UserId = int.Parse(dataReader["UserId"].ToString()); ;
                u.PostOwnerName = dataReader["FullName"].ToString();
                u.PostOwnerImage = dataReader["PostOwnerImage"].ToString();
                u.Category = (CategoryType)Enum.Parse(typeof(CategoryType), dataReader["Category"].ToString());
                u.PostOwnerEmail = dataReader["PostOwnerEmail"].ToString();
                u.Tags = dataReader["Tags"].ToString();
                u.ImageUrl = dataReader["ImageUrl"].ToString();
                u.PostContent = dataReader["PostContent"].ToString();
                u.DatePublished = long.Parse(dataReader["DatePublished"].ToString());
                u.NumberOfComments = int.Parse(dataReader["NumberOfComments"].ToString());

                u.DateUpdated = long.Parse(dataReader["DateUpdated"].ToString());

                u.AddressLatitude = double.Parse(dataReader["AddressLatitude"].ToString());
                u.AddressLongtitude = double.Parse(dataReader["AddressLongtitude"].ToString());
                posts.Add(u);
            }
            return posts;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method gets a post from the posts table  by id
    //--------------------------------------------------------------------------------------------------

    public PostWithUser GetPostById(int postId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("MySqlConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateGetPostByIdWithStoredProcedure("SP_ReadPostById", con, postId); // create the command

        try
        {
            // call the stored procedure (using the cmd) and get results to DataReader
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // iterate over the results, next moves to the next record
            if (dataReader.Read())
            {

                PostWithUser u = new PostWithUser();
                u.UserId = int.Parse(dataReader["UserId"].ToString()); ;
                u.PostOwnerName = dataReader["FullName"].ToString();
                u.PostOwnerImage = dataReader["PostOwnerImage"].ToString();
                u.Category = (CategoryType)Enum.Parse(typeof(CategoryType), dataReader["Category"].ToString());
                u.PostOwnerEmail = dataReader["PostOwnerEmail"].ToString();
                u.Tags = dataReader["Tags"].ToString();
                u.ImageUrl = dataReader["ImageUrl"].ToString();
                u.PostContent = dataReader["PostContent"].ToString();
                u.DatePublished = long.Parse(dataReader["DatePublished"].ToString());
                u.NumberOfComments = int.Parse(dataReader["NumberOfComments"].ToString());

                u.DateUpdated = long.Parse(dataReader["DateUpdated"].ToString());

                u.AddressLatitude = double.Parse(dataReader["AddressLatitude"].ToString());
                u.AddressLongtitude = double.Parse(dataReader["AddressLongtitude"].ToString());
                return u;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
        return null;

    }



    //--------------------------------------------------------------------------------------------------
    // This method Delete a ppst from the posts table 
    //--------------------------------------------------------------------------------------------------

    public int DeletePost(int postId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("MySqlConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateDeleteWithStoredProcedure("SP_DeletePost", con, postId); // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - Get Post by id
    //---------------------------------------------------------------------------------
    private SqlCommand CreateGetPostByIdWithStoredProcedure(String spName, SqlConnection con, int postId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@postId", postId);

        return cmd;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - Get Posts by user id
    //---------------------------------------------------------------------------------
    private SqlCommand CreateGetPostByUserIdWithStoredProcedure(String spName, SqlConnection con, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userId", userId);

        return cmd;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - Get Posts by category
    //---------------------------------------------------------------------------------
    private SqlCommand CreateGetPostByCategoryWithStoredProcedure(String spName, SqlConnection con, int category)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@category", category);

        return cmd;
    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - Delete Post
    //---------------------------------------------------------------------------------
    private SqlCommand CreateDeleteWithStoredProcedure(String spName, SqlConnection con, int postId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@postId", postId);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method Update an existed post in the posts table 
    //--------------------------------------------------------------------------------------------------

    public int UpdatePost(string PostContent, int NumberOfComments, double AddressLatitude, double AddressLongtitude, CategoryType Category)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUpdateUserWithStoredProcedure("SP_UpdatetUser", con, PostContent, NumberOfComments, AddressLatitude, AddressLongtitude, Category); // create the command

        try
        {
            // 0 - failure, 1 - success
            cmd.ExecuteScalar(); // execute the command
            return 1;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }


    public List<PostWithUser> ReadPostsWithUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("MySqlConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        // creat users list
        List<PostWithUser> postsWithUsers = new List<PostWithUser>();

        // create a Command with the connection to use, name of stored procedure and its parameters
        cmd = buildReadPostsWithUsersStoredProcedureCommand(con, "SP_ReadPostsWithUsers");

        // call the stored procedure (using the cmd) and get results to DataReader
        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        // iterate over the results, next moves to the next record
        while (dataReader.Read())
        {

            PostWithUser u = new PostWithUser();
            u.UserId = int.Parse(dataReader["UserId"].ToString()); ;
            u.PostOwnerName = dataReader["PostOwnerName"].ToString();
            u.PostOwnerImage = dataReader["PostOwnerImage"].ToString();
            u.Category = (CategoryType)Enum.Parse(typeof(CategoryType), dataReader["Category"].ToString());
            u.PostOwnerEmail = dataReader["PostOwnerEmail"].ToString();
            u.Tags = dataReader["Tags"].ToString();
            u.ImageUrl = dataReader["ImageUrl"].ToString();
            u.PostContent = dataReader["PostContent"].ToString();
            u.DatePublished = long.Parse(dataReader["DatePublished"].ToString());
            u.NumberOfComments = int.Parse(dataReader["NumberOfComments"].ToString());

            u.DateUpdated = long.Parse(dataReader["DateUpdated"].ToString());

            u.AddressLatitude = double.Parse(dataReader["AddressLatitude"].ToString());
            u.AddressLongtitude = double.Parse(dataReader["AddressLongtitude"].ToString());

            postsWithUsers.Add(u);
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return postsWithUsers;
    }
    private SqlCommand buildReadPostsWithUsersStoredProcedureCommand(SqlConnection con, String spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - UpdatePost
    //---------------------------------------------------------------------------------
    private SqlCommand CreateUpdateUserWithStoredProcedure(String spName, SqlConnection con, string PostContent, int NumberOfComments, double AddressLatitude, double AddressLongtitude, CategoryType Category)
    {
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            //cmd.Parameters.AddWithValue("@datePublished", DateTime.UtcNow.Millisecond);
            cmd.Parameters.AddWithValue("@postContent", PostContent);
            cmd.Parameters.AddWithValue("@numberOfComments", NumberOfComments);
            cmd.Parameters.AddWithValue("@dateUpdated", DateTime.UtcNow.Millisecond);
            cmd.Parameters.AddWithValue("@addressLatitude", AddressLatitude);
            cmd.Parameters.AddWithValue("@addressLongitude", AddressLongtitude);
            cmd.Parameters.AddWithValue("@category", Category);


            return cmd;
        }
    }
}