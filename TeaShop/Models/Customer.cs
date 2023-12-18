using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Repository;

namespace TeaShop.Models
{
    public class Customer
    {
        int _customerID;
        string _customerName;
        string _customerType;
        string _contact;

        public int customerID
        {
            get { return _customerID; }
        }

        public string customerName
        {
            get { return _customerName; }
        }

        public string customerType
        {
            get { return _customerType; }
        }

        public string contact
        {
            get { return _contact; }
        }

        public Customer(string customerName, string customerType, string contact)
        {
            this._customerName = customerName;
            this._customerType = customerType;
            this._contact = contact;
        }

        public Customer(int customerID, string customerName, string customerType, string contact)
        {
            this._customerID = customerID;
            this._customerName = customerName;
            this._customerType = customerType;
            this._contact = contact;
        }

        CustomerRepository customerRepo;

        public Customer() 
        {
        }

        public void AddCustomer(Customer customer)
        {
            CustomerRepository.customerList.Add(customer);
            Console.WriteLine("Record added successfully.");
        }

        public void UpdateCustomer(Customer customer)
        {
            var currentCustomer = CustomerRepository.customerList.FirstOrDefault(x=>x.customerID == customer._customerID);
            if (currentCustomer != null)
            {
                int CurrentIndex = CustomerRepository.customerList.IndexOf(currentCustomer);
                CustomerRepository.customerList.RemoveAt(CurrentIndex);
                if(currentCustomer != null)
                {
                    CustomerRepository.customerList.Insert(CurrentIndex, customer);
                }
                else
                {
                    Console.WriteLine("Customer details not found.");
                }
            }
        }

        public Customer GetCustomer(int Id)
        {
            Customer CustById = new Customer();
            var currentCustomer = CustomerRepository.customerList.FirstOrDefault(x=>x.customerID == Id);
            if(currentCustomer != null)
            {
                CustById = currentCustomer;
            }
            else
            {
                Console.WriteLine("Customer details not found.");
            }
            return CustById;
        }


        public void DeleteCustomer(int Id)
        {
            var currentCustomer = CustomerRepository.customerList.FirstOrDefault(x=>x.customerID == Id);
            if(currentCustomer != null)
            {
                CustomerRepository.customerList.Remove(currentCustomer);
            }
            else
            {
                Console.WriteLine("Customer details not found.");
            }
        }
    }
}
