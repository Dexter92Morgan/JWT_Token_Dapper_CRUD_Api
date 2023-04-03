using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Datas.Repository
{
   public class EmployeeRepository
    {
        private string conStr;

        public EmployeeRepository()
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

        //INSERT
        public void Add(Employee employee)
        {

            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"INSERT INTO employeedapper (empname,designation,department) VALUES (@empname,@designation,@department )";
                dbConnection.Open();
                dbConnection.Execute(sql, employee);

            }
        }

        //GET ALL

        public IEnumerable<Employee> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM employeedapper ";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql);
            }
        }

        //GET BY ID
        public Employee  GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM employeedapper WHERE EmpId=@EmpId ";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql, new {EmpId = id}).FirstOrDefault();
            }
        }

        //UPDATE

        public void Update(Employee employee)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"UPDATE employeedapper SET EmpName=@EmpName,Designation=@Designation,Department=@Department WHERE EmpId=@EmpId ";
                dbConnection.Open();
                dbConnection.Query(sql,employee);
            }

        }

        //DELETE

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM employeedapper WHERE EmpId=@EmpId ";
                dbConnection.Open();
                dbConnection.Query(sql, new { EmpId = id });
            }

        }
    }
}
