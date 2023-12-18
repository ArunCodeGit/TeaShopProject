using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Models;

namespace TeaShop.Repository
{
    public class BillRepository
    {
        static string connectionString = "DATA SOURCE=ARUNKUMAR\\SQLEXPRESS; INITIAL CATALOG=TeaShop; Integrated security=True; TrustServerCertificate=True; ";
        public static List<Bill> BillList = new List<Bill>();
        public SqlConnection connection = new SqlConnection(connectionString);
        Exception exp = new Exception();
        Customer customer = new Customer();
        Employee employee = new Employee();
        //BillProduct billProduct = new BillProduct();
        Bill bill;


        public BillRepository()
        {
            Load_Bill();
        }

        public void Add_Bill(Bill bill)
        {
            string query = "INSERT INTO Bill VALUES("+bill.billNumber+", '"+bill.billDate.ToString("yyyy-MM-dd hh:mm:ss")+"', "+bill.BillAmount+", "+bill.customer.customerID+", "+bill.employee.employeeID+");";
            if(bill.BillProductList.Count>0)
            {
                Execute_Non_Query(query);
                Add_BillProductLIst(bill);
            }
            else
            {
                Console.WriteLine("Product list should not be empty.");
            }            
        }

        public void Add_BillProductLIst(Bill bill)
        {
            foreach(BillProduct billProduct in bill.BillProductList.ToList())
            {
                bill.billDate.ToString();
                string query = "INSERT INTO BillProduct VALUES("+bill.billNumber+", "+billProduct.product.productId+", '"+billProduct.product.productName+"', "+billProduct.NumOfQty+", "+billProduct.SubTotal+");";
                Execute_Non_Query(query);
            }
        }

        public void Load_Bill()
        {
            string query = "SELECT * FROM Bill";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DateTime BillDate = Convert.ToDateTime(dr["BillDate"]);
                        int BillNo = Convert.ToInt32(dr["BillNo"]);
                        int custId = Convert.ToInt32(dr["CustID"]);
                        int EmpId = Convert.ToInt32(dr["EmpId"]);
                        int TotalAmt = Convert.ToInt32(dr["TotalAmount"]);
                        customer = customer.GetCustomer(custId);
                        employee = employee.GetEmployee(EmpId);
                        
                        bill = new Bill(BillDate, BillNo, TotalAmt, customer, employee);
                        BillList.Add(bill);
                    }
                }
                catch
                {
                    throw new Exception(exp.Message, exp.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void Execute_Non_Query(string query)
        {
            connection.ConnectionString = connectionString;

            using (SqlCommand cmd = new SqlCommand (query, connection))
            {
                connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception(exp.Message, exp.InnerException);
                }
                finally { connection.Close(); }
            }
        }
    }
}
