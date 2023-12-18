using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Repository;

namespace TeaShop.Models
{
    public class Product
    {
        int _productID;
        string _productName;
        double _unitPrice;
        double _netQty;

        public int productId
        {
            get { return _productID; }
        }

        public string productName
        {
            get { return _productName; }
        }

        public double unitPrice
        {
            get { return _unitPrice; }
        }

        public double netQty
        {
            get { return _netQty; }
        }

        public Product(int productid, string productName, double unitPrice, double Qty)
        {
            this._productID = productid;
            this._productName = productName;
            this._unitPrice = unitPrice;
            this._netQty = Qty;
        }

        public Product(string productName, double unitPrice, double Qty)
        {
            this._productName = productName;
            this._unitPrice = unitPrice;
            this._netQty = Qty;
        }
        public Product() { }

        public void AddNewProduct(Product product)
        {
            ProductRepository.ProductList.Add(product);
            Console.WriteLine("New product added successfully in collection.");
        }

        public void UpdateProduct(Product product)
        {
            var currentProduct = ProductRepository.ProductList.FirstOrDefault(x => x.productId == product.productId);
            if(currentProduct != null)
            {
                int ProductIndex = ProductRepository.ProductList.IndexOf(currentProduct);
                ProductRepository.ProductList.RemoveAt(ProductIndex);
                ProductRepository.ProductList.Insert(ProductIndex, product);
            }
            else
            {
                Console.WriteLine("Product details not available in the current list.");
            }
        }

        public Product GetProductById(int Id)
        {
            Product ProductById = new Product();
            var currentProduct = ProductRepository.ProductList.FirstOrDefault(x=>x.productId == Id);
            if(currentProduct != null)
            {
                ProductById = currentProduct;
            }
            else
            {
                Console.WriteLine("Product details not available in the current list.");
            }
            return ProductById;
        }

        public void DeleteProduct(int Id)
        {
            var currentProduct = ProductRepository.ProductList.FirstOrDefault(x=>x.productId == Id);
            if (currentProduct != null)
            {
                ProductRepository.ProductList.Remove(currentProduct);
            }
            else
            {
                Console.WriteLine("Product details is not available in the current list.");
            }
        }
    }
}
