using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Models;

namespace TeaShop.Repository
{
    public class CustomerRepository
    {
        static string connectionString = "DATA SOURCE=ARUNKUMAR\\SQLEXPRESS; INITIAL CATALOG=TeaShop; Integrated security=True; TrustServerCertificate=True; ";
        public static List<Customer> customerList = new List<Customer>();
        public SqlConnection connection = new SqlConnection(connectionString);

        public void LoadCustomer()
        {
            string query = "SELECT * FROM Customer;";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        int Id = Convert.ToInt32(dr["CustId"]);
                        string Name = dr["CustName"].ToString();
                        string Type = dr["CustType"].ToString();
                        string Contact = dr["Contact"].ToString();
                        Customer customer = new Customer(Id, Name, Type, Contact);
                        customerList.Add(customer);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void AddNewCustomer(Customer customer)
        {
            string query = "INSERT INTO Customer VALUES('"+customer.customerName+"', '"+customer.customerType+"', '"+customer.contact+"');";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("New customer added in the database.");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            string query = "UPDATE Customer SET CustName = '" + customer.customerName + "', CustType = '" + customer.customerType + "', Contact = '"+customer.contact+"' WHERE CustId = "+customer.customerID+" ;";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Customer details updated in the database.");
                }
                catch(Exception ex)
                {
                    throw new Exception (ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void DeleteCustomer(int id)
        {
            string query = "DELETE FROM Customer WHERE CustId = "+id+";";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Customer details removed in the database.");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }
    }
}
