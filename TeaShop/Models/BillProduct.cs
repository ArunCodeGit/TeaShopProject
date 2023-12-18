using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TeaShop.Repository;

namespace TeaShop.Models
{
    public class BillProduct
    {
        Product _product;
        double _numOfQty;
        double _subTotal;

        public Product product
        {
            get { return _product; }
        }
        public double NumOfQty
        {
            get { return _numOfQty;}
        }
        public double SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }

        public BillProduct(Product product, double qty)
        {
            this._product = product;
            this._numOfQty = qty;
            this._subTotal = product.unitPrice * qty;
        }

        public BillProduct() { }
        
    }
}
