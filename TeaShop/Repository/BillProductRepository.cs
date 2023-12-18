using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Models;

namespace TeaShop.Repository
{
    public class BillProductRepository
    {
        static string connectionString = "DATA SOURCE=ARUNKUMAR\\SQLEXPRESS; INITIAL CATALOG=TeaShop; Integrated security=True; TrustServerCertificate=True; ";
        public static List<BillProduct> BillProductList = new List<BillProduct>();
        public SqlConnection connection = new SqlConnection(connectionString);
        Exception exp = new Exception();
        Bill bill = new Bill();
        BillRepository repo = new BillRepository();

        public void Add_BillProdct(BillProduct billProduct)
        {
            connection.ConnectionString = connectionString;
            string query1 = "INSERT INTO BillProduct VALUES("+bill.billNumber+" ,"+billProduct.product.productId+", '"+billProduct.product.productName+"', "+billProduct.NumOfQty+", "+billProduct.SubTotal+");";
            using (SqlCommand cmd = new SqlCommand(query1, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("New record inserted into bill product database.");
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
