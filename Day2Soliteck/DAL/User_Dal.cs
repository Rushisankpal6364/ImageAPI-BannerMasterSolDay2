namespace Day2Soliteck.DAL;
using System.Data;
using Microsoft.Data.SqlClient;
using Day2Soliteck.Model;


public class User_Dal
{
    SqlConnection _connection = null;
    SqlCommand _command = null;

    public static IConfiguration Configuration { get; set; }

    private String getConnectionString()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        Configuration = builder.Build();
        return Configuration.GetConnectionString("DefaultConnection");
    }

    public List<User> getAll()
    {
        List<User> userList = new List<User>();
        using (_connection = new SqlConnection(getConnectionString()))
        {
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "[DBO].[usp_Get_Users]";
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            
            while (dr.Read())
            {
                User user = new User();
                user.Id = Convert.ToInt32(dr["Id"]);
                user.UserName = dr["UserName"].ToString();
                user.UserEmail = dr["UserEmail"].ToString();
                user.UserPhoneno = dr["UserPhoneno"].ToString();
                user.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                user.RoleId = Convert.ToInt32(dr["RoleId"]);
                userList.Add(user);

            }
            _connection.Close();
        }
        return userList;

    }

    public bool Insert(User model)
    {
        int id = 0;
        using(_connection = new SqlConnection(getConnectionString()))
        {
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "dbo.usp_Insert_Users";
            _command.Parameters.AddWithValue("@UserName", model.UserName);
            _command.Parameters.AddWithValue("@UserEmail", model.UserEmail);
            _command.Parameters.AddWithValue("@UserPhoneno", model.UserPhoneno);
            _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
            _command.Parameters.AddWithValue("@RoleId", model.RoleId);
            _connection.Open();
            id = _command.ExecuteNonQuery();
            _connection.Close();
        }
        return id > 0 ? true : false;
    }


    public User GetById(int id)
    {
        User user = new User();
        using (_connection = new SqlConnection(getConnectionString()))
        {
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "[DBO].[usp_Get_UserById]";
            _command.Parameters.AddWithValue("@Id", id);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();


            while (dr.Read())
            {
                user.Id = Convert.ToInt32(dr["Id"]);
                user.UserName = dr["UserName"].ToString();
                user.UserEmail = dr["UserEmail"].ToString();
                user.UserPhoneno = dr["UserPhoneno"].ToString();
                user.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                user.RoleId = Convert.ToInt32(dr["RoleId"]);
            }
            _connection.Close();
        }
        return user;
    }


    public bool Update(User model)
    {
        int id = 0;
        using (_connection = new SqlConnection(getConnectionString()))
        {
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "dbo.usp_Update_Users";
            _command.Parameters.AddWithValue("@Id", model.Id);
            _command.Parameters.AddWithValue("@UserName", model.UserName);
            _command.Parameters.AddWithValue("@UserEmail", model.UserEmail);
            _command.Parameters.AddWithValue("@UserPhoneno", model.UserPhoneno);
            _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
            _command.Parameters.AddWithValue("@RoleId", model.RoleId);
            _connection.Open();
            id = _command.ExecuteNonQuery();
            _connection.Close();
        }
        return id > 0 ? true : false;
    }

    public bool Delete(int id)
    {
        int id1 = 0;
        using (_connection = new SqlConnection(getConnectionString()))
        {
            _command = _connection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = "[DBO].[usp_Delete_Users]";
            _command.Parameters.AddWithValue("@Id", id);
            _connection.Open();
            id1 = _command.ExecuteNonQuery();
            _connection.Close();
        }
        return id1 > 0 ? true : false;
    }

}

