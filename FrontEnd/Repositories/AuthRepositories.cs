using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Repositories
{
    public class AuthRepositories
    {
        private readonly string conn;
        // private readonly NpgsqlConnection conn;
        public AuthRepositories(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("DefaultConnection");
            // conn = new NpgsqlConnection(_conn);
        }

        public List<AuthModel> GetAllCity()
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {
                List<AuthModel> cities = new List<AuthModel>();
                try
                {
                    _conn.Open();
                    var query = "SELECT * FROM t_city";
                    using (var cmd = new NpgsqlCommand(query, _conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuthModel city = new AuthModel
                            {
                                c_cityid = Convert.ToInt32(reader["c_cityid"]),
                                c_cityname = reader["c_cityname"].ToString(),
                            };
                            cities.Add(city);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    _conn.Close();
                }
                return cities;
            }
        }

        public List<AuthModel> GetAllDept()
        {
            List<AuthModel> dept = new List<AuthModel>();
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {
                try
                {
                    _conn.Open();
                    var query = "SELECT * FROM t_department";
                    using (var cmd = new NpgsqlCommand(query, _conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AuthModel dt = new AuthModel
                            {
                                c_departmentid = Convert.ToInt32(reader["c_departmentid"]),
                                c_deptname = reader["c_deptname"].ToString(),
                            };
                            dept.Add(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    _conn.Close();
                }
                return dept;
            }
        }

        public void Register(AuthModel register)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {
                _conn.Open();

                using var command = new NpgsqlCommand("Insert into t_kendoregister (c_name, c_email, c_gender, c_dob, c_hobby, c_password, c_photo, c_cityid, c_departmentid) values (@c_name, @c_email, @c_gender, @c_dob, @c_hobby, @c_password, @c_photo, @c_cityid, @c_departmentid)", _conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@c_name", register.c_name);
                command.Parameters.AddWithValue("@c_email", register.c_email);
                command.Parameters.AddWithValue("@c_gender", register.c_gender);
                command.Parameters.AddWithValue("@c_dob", register.c_dob);
                command.Parameters.AddWithValue("@c_hobby", register.c_hobby);
                command.Parameters.AddWithValue("@c_password", register.c_password);
                command.Parameters.AddWithValue("@c_photo", register.c_photo);
                command.Parameters.AddWithValue("@c_cityid", register.c_cityid);
                command.Parameters.AddWithValue("@c_departmentid", register.c_departmentid);

                command.ExecuteNonQuery();

                _conn.Close();
            }
        }

        public List<AuthModel> GetAllData()
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {

                var records = new List<AuthModel>();

                _conn.Open();

                using var command = new NpgsqlCommand("select * from t_kendoregister join t_city on t_kendoregister.c_cityid = t_city.c_cityid join t_department on t_kendoregister.c_departmentid = t_department.c_departmentid", _conn);
                command.CommandType = CommandType.Text;

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var record = new AuthModel
                    {
                        c_empid = Convert.ToInt32(reader["c_empid"]),
                        c_name = reader["c_name"].ToString(),
                        c_email = reader["c_email"].ToString(),
                        c_gender = reader["c_gender"].ToString(),
                        c_dob = Convert.ToDateTime(reader["c_dob"]),
                        c_hobby = reader["c_hobby"].ToString(),
                        c_password = reader["c_password"].ToString(),
                        c_photo = reader["c_photo"].ToString(),
                        c_cityname = reader["c_cityname"].ToString(),
                        c_deptname = reader["c_deptname"].ToString(),
                        c_cityid = Convert.ToInt32(reader["c_cityid"]),
                        c_departmentid = Convert.ToInt32(reader["c_departmentid"]),
                    };
                    records.Add(record);
                }
                _conn.Close();
                return records;
            }
        }

        public AuthModel ShowData(int id)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {

                _conn.Open();

                using var command = new NpgsqlCommand("select * from t_kendoregister where c_empid = @id", _conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", id);

                using var reader = command.ExecuteReader();

                var record = new AuthModel();

                if (reader.Read())
                {
                    record.c_empid = Convert.ToInt32(reader["c_empid"]);
                    record.c_name = reader["c_name"].ToString();
                    record.c_email = reader["c_email"].ToString();
                    record.c_gender = reader["c_gender"].ToString();
                    record.c_dob = Convert.ToDateTime(reader["c_dob"]);
                    record.c_hobby = reader["c_hobby"].ToString();
                    record.c_password = reader["c_password"].ToString();
                    record.c_photo = reader["c_photo"].ToString();
                    record.c_cityid = Convert.ToInt32(reader["c_cityid"]);
                    record.c_departmentid = Convert.ToInt32(reader["c_departmentid"]);
                }
                _conn.Close();
                return record;
            }
        }

        public bool UpdateData(AuthModel updatedRecord)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {

                try
                {
                    _conn.Open();

                    using var command = new NpgsqlCommand("UPDATE t_kendoregister SET c_name = @c_name, c_email = @c_email, c_gender = @c_gender, c_dob = @c_dob, c_hobby = @c_hobby, c_password = @c_password, c_photo = @c_photo, c_cityid = @c_cityid, c_departmentid = @c_departmentid WHERE c_empid = @id", _conn);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@id", updatedRecord.c_empid);
                    command.Parameters.AddWithValue("@c_name", updatedRecord.c_name);
                    command.Parameters.AddWithValue("@c_email", updatedRecord.c_email);
                    command.Parameters.AddWithValue("@c_gender", updatedRecord.c_gender);
                    command.Parameters.AddWithValue("@c_dob", updatedRecord.c_dob);
                    command.Parameters.AddWithValue("@c_hobby", updatedRecord.c_hobby);
                    command.Parameters.AddWithValue("@c_password", updatedRecord.c_password);
                    command.Parameters.AddWithValue("@c_photo", updatedRecord.c_photo);
                    command.Parameters.AddWithValue("@c_cityid", updatedRecord.c_cityid);
                    command.Parameters.AddWithValue("@c_departmentid", updatedRecord.c_departmentid);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating record: " + ex.Message);
                    return false;
                }
                finally
                {
                    _conn.Close();
                }
            }
        }

        public void DeleteData(int id)
        {
            using (NpgsqlConnection _conn = new NpgsqlConnection(conn))
            {

                _conn.Open();

                using var command = new NpgsqlCommand("delete from t_kendoregister where c_empid = @c_empid", _conn);
                command.Parameters.AddWithValue("@c_empid", id);
                command.CommandType = CommandType.Text;

                command.ExecuteNonQuery();

                _conn.Close();
            }
        }
    }
}