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



public class UserDBservices
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
    //     Class AuthUser
    //--------------------------------------------------------------------------------------------------


    //--------------------------------------------------------------------------------------------------
    // This method inserts a new user to the users table (register)
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(string FullName, UserType userType, string Email, string Password, double AddressLatitude, double AddressLongtitude,long BirthDate)
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

        cmd = CreateInsertUserWithStoredProcedure("SP_InsertUser", con, FullName, userType, Email, Password, AddressLatitude, AddressLongtitude, BirthDate); // create the command

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
    // Create the SqlCommand using a stored procedure - InsertUser
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertUserWithStoredProcedure(String spName, SqlConnection con, string FullName, UserType userType, string Email, string Password, double AddressLatitude, double AddressLongtitude, long BirthDate)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@fullName",FullName);
        cmd.Parameters.AddWithValue("@userType",userType);
        cmd.Parameters.AddWithValue("@email", Email);
        cmd.Parameters.AddWithValue("@lastSeen", DateTime.Now.Millisecond);
        cmd.Parameters.AddWithValue("@password", Password); 
        cmd.Parameters.AddWithValue("@addressLatitude",AddressLatitude);
        cmd.Parameters.AddWithValue("@addressLongitude", AddressLongtitude);
        cmd.Parameters.AddWithValue("@birthDate", BirthDate);
        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // This method Delete a user from the userTable 
    //--------------------------------------------------------------------------------------------------

    public int DeleteUser(string email)
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

        cmd = CreateDeleteUserWithStoredProcedure("SP_DeleteUser", con, email); // create the command

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
    // Create the SqlCommand using a stored procedure - Delete User
    //---------------------------------------------------------------------------------
    private SqlCommand CreateDeleteUserWithStoredProcedure(String spName, SqlConnection con, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method Update an existed user in the users table 
    //--------------------------------------------------------------------------------------------------

    public int UpdateUser(string fullName,UserType userType, string  email, string password,double  addressLatitude,double addressLongitude, long birthDate)
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

        cmd = CreateUpdateUserWithStoredProcedure("SP_UpdatetUser", con, fullName, userType, email, password, addressLatitude, addressLongitude, birthDate);

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
    // Create the SqlCommand using a stored procedure - UpdateUser
    //---------------------------------------------------------------------------------
    private SqlCommand CreateUpdateUserWithStoredProcedure(String spName, SqlConnection con, string fullName, UserType userType, string email, string password, double addressLatitude, double addressLongitude, long birthDate)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@fullName",fullName);
        cmd.Parameters.AddWithValue("@userType", userType);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@lastSeen", DateTime.Now.Millisecond);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@addressLatitude", addressLatitude);
        cmd.Parameters.AddWithValue("@addressLongitude", addressLongitude);
        cmd.Parameters.AddWithValue("@birthDate", birthDate);


        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // This method Read a user from the userTable 
    //--------------------------------------------------------------------------------------------------


    public List<User> ReadUsers()
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
        List<User> users = new List<User>();

        // create a Command with the connection to use, name of stored procedure and its parameters
        cmd = buildReadUsersStoredProcedureCommand(con, "SP_ReadUsers");

        // call the stored procedure (using the cmd) and get results to DataReader
        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        // iterate over the results, next moves to the next record
        while (dataReader.Read())
        {
            
            User u = new User();
            u.UserId = int.Parse(dataReader["UserId"].ToString()); ;
            u.FullName = dataReader["FullName"].ToString();
            u.UserType = (UserType)Enum.Parse(typeof(UserType), dataReader["UserType"].ToString());
            u.Email = dataReader["Email"].ToString();
            u.Password = dataReader["Password"].ToString();
            u.AddressLatitude = double.Parse(dataReader["AddressLatitude"].ToString());
            u.AddressLongtitude = double.Parse(dataReader["AddressLongtitude"].ToString());
            u.BirthDate = long.Parse(dataReader["BirthDate"].ToString());
            u.LastSeen = long.Parse(dataReader["LastSeen"].ToString());


            users.Add(u);
        }

        if (con != null)
        {
            // close the db connection
            con.Close();
        }

        return users;

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure - ReadUsers
    //---------------------------------------------------------------------------------
    private SqlCommand buildReadUsersStoredProcedureCommand(SqlConnection con, String spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }


}

