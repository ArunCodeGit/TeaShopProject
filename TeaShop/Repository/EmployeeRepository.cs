using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Models;
using System.Data;
using System.Data.SqlClient;

namespace TeaShop.Repository
{
    public class EmployeeRepository
    {
        static string connectionString = "DATA SOURCE=ARUNKUMAR\\SQLEXPRESS; INITIAL CATALOG=TeaShop; Integrated security=True; TrustServerCertificate=True; ";
        public static List<Employee> employeeList = new List<Employee>();
        public SqlConnection connection = new SqlConnection(connectionString);

        public void LoadEmployee()
        {
            string query = "SELECT * FROM Employee;";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        int Id = Convert.ToInt32(dr["EmpId"]);
                        string Name = dr["EmpName"].ToString();
                        string Type = dr["EmpType"].ToString();
                        string contact = dr["Contact"].ToString();

                        Employee employee = new Employee(Id, Name, Type, contact);

                        EmployeeRepository.employeeList.Add(employee);

                        //EmployeeRepository.employeeList.Add(new Employee()
                        //{

                        //    employeeID = Convert.ToInt32(dr["EmpId"]),
                        //    employeeName = dr["EmpName"].ToString(),
                        //    emptype = dr["EmpType"].ToString(),
                        //    contact = dr["Contact"].ToString(),
                        //});; ;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void AddNewEmp(Employee employee)
        {
            string query = "INSERT INTO Employee VALUES('" + employee.employeeName + "', '" + employee.emptype + "', '" + employee.contact + "')";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void UpdateEmp(Employee employee)
        {
            string query = "UPDATE Employee SET EmpName = '" + employee.employeeName + "', EmpType = '" + employee.emptype + "', Contact = '" + employee.contact + "' WHERE EmpId = " + employee.employeeID + ";";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Record updated successfully.");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void DeleteEmp(int id)
        {
            string query = "DELETE FROM Employee WHERE EmpId = " + id + ";";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Record deleted from the database.");
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }
    }
}
