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
    
    public int newPostDB(int userId, string PostContent, int NumberOfComments, double AddressLatitude, double AddressLongitude, CategoryType Category)
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

        cmd = CreateNewPostWithStoredProcedure("SP_newPost", con, userId, PostContent,NumberOfComments, AddressLatitude, AddressLongitude, Category); // create the command

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
  
    private SqlCommand CreateNewPostWithStoredProcedure(string spName, SqlConnection con, int userId, string PostContent, int NumberOfComments, double AddressLatitude, double AddressLongitude, CategoryType Category)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;          // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userId", userId);
        cmd.Parameters.AddWithValue("@datePublished", DateTime.UtcNow);
        cmd.Parameters.AddWithValue("@postContent", PostContent);
        cmd.Parameters.AddWithValue("@numberOfComments", NumberOfComments);
        cmd.Parameters.AddWithValue("@dateUpdated", DateTime.UtcNow);
        cmd.Parameters.AddWithValue("@addressLatitude", AddressLatitude);
        cmd.Parameters.AddWithValue("@addressLongitude", AddressLongitude);
        cmd.Parameters.AddWithValue("@category", Category);
        return cmd;
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

    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - UpdatePost
    //---------------------------------------------------------------------------------
    private SqlCommand CreateUpdateUserWithStoredProcedure(String spName, SqlConnection con, string PostContent, int NumberOfComments, double AddressLatitude, double AddressLongtitude, CategoryType Category)
    {)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        //cmd.Parameters.AddWithValue("@datePublished", DateTime.UtcNow);
        cmd.Parameters.AddWithValue("@postContent", PostContent);
        cmd.Parameters.AddWithValue("@numberOfComments", NumberOfComments);
        cmd.Parameters.AddWithValue("@dateUpdated", DateTime.UtcNow);
        cmd.Parameters.AddWithValue("@addressLatitude", AddressLatitude);
        cmd.Parameters.AddWithValue("@addressLongitude", AddressLongtitude);
        cmd.Parameters.AddWithValue("@category", Category);


            return cmd;
    }
}