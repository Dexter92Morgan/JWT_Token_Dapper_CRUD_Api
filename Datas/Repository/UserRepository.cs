using Dapper;
using Datas.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Repository
{
    public class UserRepository
    {

        private string conStr;
        public UserRepository()
        {
            //for Docker
            //conStr = @"Host=host.docker.internal;Port=5432;User ID=postgres; Password=Sa123;Database=postgres;Pooling=true;";

            //for Local
            conStr = @"Host=localhost;Port=5432;User ID=postgres; Password=Sa123;Database=postgres;Pooling=true;";

        }
        public IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(conStr);

            }

        }

        //GET BY ID
        public UserInfo GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM userinfo WHERE userid=@userid ";
                dbConnection.Open();
                return dbConnection.Query<UserInfo>(sql, new { UserId = id }).FirstOrDefault();
            }
        }
        //GET ALL

        public IEnumerable<UserInfo> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM userinfo ";
                dbConnection.Open();
                return dbConnection.Query<UserInfo>(sql);
            }
        }

        //INSERT
        public void Add(UserInfo user)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"INSERT INTO userinfo (username,email,password,role) VALUES (@username,@email,@password,@role)";
                dbConnection.Open();
                dbConnection.Execute(sql, user);

            }
        }

        //GET BY ID
        public UserInfo GetByuser(string username, string password)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM userinfo WHERE username=@username and password=@password ";
                dbConnection.Open();
                return  dbConnection.Query<UserInfo>(sql, new { UserName = username, Password = password }).FirstOrDefault();
            }
        }

        //UPDATE

        public void Update(UserInfo userdata)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"UPDATE userinfo SET username=@username, email=@email, password=@password and role=@role WHERE userid=@userid ";
                dbConnection.Open();
                dbConnection.Query(sql, userdata);
            }

        }

        //DELETE

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM userinfo WHERE userid=@userid";
                dbConnection.Open();
                dbConnection.Query(sql, new { UserId = id });
            }

        }

    }
}
