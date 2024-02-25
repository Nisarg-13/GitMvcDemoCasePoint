using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using Npgsql;

namespace FrontEnd.Repositories
{
    public class AuthRepositories : IAuthRepositories
    {
        private readonly string conn;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepositories(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("DefaultConnection");
            _httpContextAccessor = httpContextAccessor;
        }

        public void Register(AuthModel register)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {
                _conn.Open();

                using var command = new NpgsqlCommand("Insert into t_auth(c_name, c_email, c_password) Values(@c_name, @c_email, @c_password)", _conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@c_name", register.c_name);
                command.Parameters.AddWithValue("@c_email", register.c_email);
                command.Parameters.AddWithValue("@c_password", register.c_password);

                command.ExecuteNonQuery();

                _conn.Close();
            }

        }

        public AuthModel Login(AuthModel login)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {
                try
                {
                    _conn.Open();

                    using var command = new NpgsqlCommand("SELECT * FROM t_auth WHERE c_email = @c_email AND c_password = @c_password", _conn);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@c_email", login.c_email);
                    command.Parameters.AddWithValue("@c_password", login.c_password);

                    var dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        var session = _httpContextAccessor.HttpContext.Session;
                        _httpContextAccessor.HttpContext.Session.SetString("c_userid", dr["c_userid"].ToString());
                        session.SetString("Name", dr.GetString(dr.GetOrdinal("c_name")));

                        AuthModel loggedInUser = new AuthModel
                        {
                            c_userid = Convert.ToInt32(dr["c_userid"]),
                            c_name = dr["c_name"].ToString(),
                            c_email = dr["c_email"].ToString(),
                            c_password = dr["c_password"].ToString(),
                            c_role = Convert.ToInt32(dr["c_role"]),
                        };

                        return loggedInUser;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                    _conn.Close();
                }

                return null;
            }
        }

        public bool IsEmailExists(string c_email)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {
                try
                {
                    _conn.Open();
                    var query = "SELECT c_userid, c_name, c_email, c_password, c_role FROM t_auth WHERE c_email = @c_email";
                    var cmd = new NpgsqlCommand(query, _conn);
                    cmd.Parameters.AddWithValue("@c_email", c_email);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
                finally
                {
                    _conn.Close();
                }
            }

        }
    }
}