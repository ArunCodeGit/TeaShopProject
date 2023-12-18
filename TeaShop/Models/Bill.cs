using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Repository;

namespace TeaShop.Models
{
    public class Bill
    {
        DateTime _billDate;
        int _billNumber;
        public List<BillProduct> BillProductList = new List<BillProduct>();

        //public Bill(List<BillProduct> billProductList)
        //{
        //    BillProductList = billProductList;
        //}

        Customer _customer;
        Employee _employee;
        double _billAmount;

        public DateTime billDate
        {
            get { return _billDate; }
        }
        public int billNumber
        {
            get { return _billNumber; }
        }
        
        public Customer customer
        {
            get { return _customer; }
        }
        public Employee employee
        {
            get { return _employee;}
        }
        public double BillAmount
        {
            get { return _billAmount; }
        }

        public Bill(DateTime date, int billno, double billAmount, Customer customer, Employee employee)
        {
            this._billDate = date;
            this._billNumber = billno;
            this._billAmount = billAmount;
            this._customer = customer;
            this._employee = employee;
        }

        public Bill(DateTime date, int billno, Customer customer, Employee employee, List<BillProduct> billProductList)
        {
            this._billDate = date;
            this._billNumber = billno;
            this._customer = customer;
            this._employee = employee;
            this.BillProductList = billProductList;
            this._billAmount = GenerateBillAmount();
        }

        public Bill() { }

        public void AddNewProduct(BillProduct billProduct)
        {
            BillProductList.Add(billProduct);
        }

        public void UpdateExistProduct(BillProduct billProduct)
        {
            var UpdateProduct = BillProductList.FirstOrDefault(x => x.product.productId == billProduct.product.productId);
            int Index = BillProductList.IndexOf(UpdateProduct);
            BillProductList.RemoveAt(Index);
            BillProductList.Insert(Index, billProduct);
        }

        public double GenerateBillAmount()
        {
            double TotalAmount = 0;
            foreach(BillProduct bill in BillProductList)
            {
                TotalAmount += bill.SubTotal;
            }
            return TotalAmount;
        }
    }
}
