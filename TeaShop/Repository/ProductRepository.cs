using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Models;

namespace TeaShop.Repository
{
    public class ProductRepository
    {
        static string connectionString = "DATA SOURCE=ARUNKUMAR\\SQLEXPRESS; INITIAL CATALOG=TeaShop; Integrated security=True; TrustServerCertificate=True; ";
        public SqlConnection connection = new SqlConnection(connectionString);
        public static List<Product> ProductList = new List<Product>();

        public void LoadProduct()
        {
            string query = "SELECT * FROM Product";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        int ID = Convert.ToInt32(dr["ProductID"]);
                        string Name = Convert.ToString(dr["ProductName"]);
                        double Price = Convert.ToDouble(dr["UnitPrice"]);
                        double Qty = Convert.ToDouble(dr["NetQty"]);
                        Product product = new Product(ID, Name, Price, Qty);
                        ProductList.Add(product);
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void AddNewProduct(Product product)
        {
            string query = "INSERT INTO Product VALUES('" + product.productName + "', " + product.unitPrice + ", "+product.netQty+");";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product Added");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); }
            }
        }

        public void updateProduct(Product product)
        {
            string query = "UPDATE Product SET ProductName='"+product.productName+"', UnitPrice="+product.unitPrice+", netQty="+product.netQty+" WHERE ProductID="+product.productId+";";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product details updated.");
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                finally { connection.Close(); } 
            }
        }

        public void DeleteProduct(int Id)
        {
            string query = "DELETE FROM Product WHERE ProductID = " + Id + ";";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product details removed.");
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
