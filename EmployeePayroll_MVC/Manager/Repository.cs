using EmployeePayroll_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayroll_MVC.Manager
{
    public class Repository
    {
       
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Employees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

        SqlConnection con;
        /// <summary>
        /// Method For GetAllEmployees
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeModel> GetAllEmployee()
        {
            List<EmployeeModel> empList = new List<EmployeeModel>();
            using (con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeModel emp = new EmployeeModel();
                    emp.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    emp.Name = rdr["Name"].ToString();
                    emp.ProfileImage = rdr["ProfileImage"].ToString();
                    emp.Gender = rdr["Gender"].ToString();
                    emp.Department = rdr["Department"].ToString();
                    emp.Salary = Convert.ToInt32(rdr["Salary"]);
                    emp.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    emp.Notes = rdr["Notes"].ToString();
                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }
        /// <summary>
        /// Adding Employees
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeModel employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        /// <summary>
        /// Updating Employees
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(EmployeeModel employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                con.Open();
                var result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// Deleting Employees
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
                var result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                    return true;
                else
                    return false;
            }
        }

    }

}
