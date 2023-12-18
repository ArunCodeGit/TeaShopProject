using System;
using System.Threading.Channels;
using TeaShop.Models;
using TeaShop.Repository;

namespace TeaShop
{
    class TeaShop
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("***Welcome to Tea Shop inventory***");

            #region Get Employee details 
            Console.Write("Please provide Employee ID: ");
            int EmpID = Convert.ToInt32(Console.ReadLine());
            EmployeeRepository EmployeeRepo = new EmployeeRepository();
            EmployeeRepo.LoadEmployee();
            Employee emp = new Employee();
            emp = emp.GetEmployee(EmpID);
            #endregion

            #region Get Customer Details

            
            Console.Write("Please provide Customer ID: ");
            int CustID = Convert.ToInt32(Console.ReadLine());
            CustomerRepository CustomerRepo = new CustomerRepository();
            CustomerRepo.LoadCustomer();
            Customer customer = new Customer();
            customer = customer.GetCustomer(CustID);
            //Customer cust = new Customer(1, "Arun", "Regular", "8993953231");
            //cust.UpdateCustomer(cust);
            //customerRepository.UpdateCustomer(cust);
            #endregion

            Bill CurrentBill = new Bill();
            BillRepository billRepository = new BillRepository();
            #region Order list of products
            bool IsStatus = true;
            while(IsStatus)
            {
                ProductRepository ProductRepo = new ProductRepository();
                ProductRepo.LoadProduct();
                Product Product = new Product();
                Console.Write("Provide Product ID: ");
                int ProductID = Convert.ToInt32(Console.ReadLine());
                Product = Product.GetProductById(ProductID);

                Console.Write("Quantity: ");
                int Qty = Convert.ToInt32(Console.ReadLine());
                BillProduct billProduct;
                billProduct = new BillProduct(Product, Qty);
                CurrentBill.AddNewProduct(billProduct);

                Console.WriteLine("Add More products? If yes press 1 else 0.");
                int Choice = Convert.ToInt32(Console.ReadLine());
                if(Choice ==1) { IsStatus = true;  }
                else { IsStatus = false; }

                IsStatus = Choice==1?true :false;
            }
            #endregion
            
            //Product product = new Product("Pencil", 4.5, 1);
            //product.AddNewProduct(product);
            //productRepository.AddNewProduct(product);
            //Product product = new Product();
            

            //BillProduct billproduct;
            //Product currentProduct = new Product();
            //currentProduct = currentProduct.GetProductById(10002);
            //billproduct = new BillProduct(currentProduct, 2);
            //CurrentBill.AddNewProduct(billproduct);

            //billproduct = new BillProduct(product.GetProductById(10006), 1);
            //CurrentBill.AddNewProduct(billproduct);

            //Product updateProduct = new Product(10002, "Boost", 30, 1);
            //updateProduct.UpdateProduct(updateProduct);

            //BillProduct UpdateBillProduct = new BillProduct(product.GetProductById(10002), 5);

            //CurrentBill.UpdateExistProduct(UpdateBillProduct);


            double CurrentBillAmt =  CurrentBill.BillAmount;
            //Console.WriteLine(CurrentBill.billNumber +"\t"+CurrentBill.billDate+"\t"+ CurrentBillAmt);
            
            int BillNo = GenerateBillNo();
            var billProductList = CurrentBill.BillProductList.ToList();
            
            //CurrentBill.UpdateExistProduct(UpdateBillProduct);
            CurrentBill = new Bill(DateTime.Now, BillNo, customer, emp, billProductList);
            //CurrentBill.AddNewProduct(billproduct);
            billRepository.Add_Bill(CurrentBill);

            Display(CurrentBill);
            //Bill bill = new Bill(billproduct, emp);
            //bill.AddNewProduct(billproduct);

            //string Name = string.Empty;
            //string type = string.Empty;
            //string contact= string.Empty;
            //string Choice = string.Empty;
            //int id = 0;

            //do
            //{
            //    Console.WriteLine("ADD NEW EMPLOYEE\n");
            //    Console.Write("Enter the employee name: ");
            //    Name = Console.ReadLine();
            //    Console.Write("Enter the employee type: ");
            //    type = Console.ReadLine();
            //    Console.Write("Enter the contact number: ");
            //    contact = Console.ReadLine();

            //    Employee employee = new Employee(Name, type, contact);
            //    employee.AddEmployee(employee);
            //    empRepo.AddNewEmp(employee);

            //    Console.Write("\nDo you want to add employee details(yes/no): ");
            //    Choice = Console.ReadLine();
            //} while (Choice.ToLower() == "yes");

            //do
            //{
            //    Console.WriteLine("UPDATE EMPLOYEE DETAILS\n");

            //    Console.Write("Enter the Employee ID: ");
            //    id = Convert.ToInt32(Console.ReadLine());
            //    Console.Write("Enter the employee name: ");
            //    Name = Console.ReadLine();
            //    Console.Write("Enter the employee type: ");
            //    type = Console.ReadLine();
            //    Console.Write("Enter the contact number: ");
            //    contact = Console.ReadLine();

            //    Employee UpdateEmployee = new Employee(id, Name, type, contact);
            //    emp.UpdateEmployee(UpdateEmployee);
            //    empRepo.UpdateEmp(UpdateEmployee);

            //    Employee GetEmp = emp.GetEmployee(id);

            //    Console.Write("\nDo you want to add employee details(yes/no):");

            //    Choice = Console.ReadLine();
            //} while (Choice.ToLower() == "yes");


            //do
            //{
            //    Console.WriteLine("DELETE EMPLOYEE FROM DATABASE\n");
            //    Console.Write("Enter the Employee ID: ");
            //    id = Convert.ToInt32(Console.ReadLine());
            //    emp.DeleteEmployee(id);
            //    empRepo.DeleteEmp(id);

            //    Console.Write("\nDo you want to delete any employee details(yes/no):");

            //    Choice = Console.ReadLine();
            //} while (Choice.ToLower() == "yes");

        }

        public static int GenerateBillNo()
        {
            //string value = string.Empty;
            int LastID = BillRepository.BillList.Last().billNumber;
            //string day = DateTime.Now.Day.ToString();
            //string month = DateTime.Now.Month.ToString();
            //string year = (DateTime.Now.Year % 100).ToString();
            //value = year + month + day + NewID.ToString();
            return LastID+1;
        }

        public static void Display(Bill CurrentBill)
        {
            Console.WriteLine("Bill Details find below:");
            Console.WriteLine("************************");
            Console.WriteLine($"Bill Numer: {CurrentBill.billNumber}");
            Console.WriteLine($"Customer ID: {CurrentBill.customer.customerID} \t Customer Name: {CurrentBill.customer.customerName}");
            Console.WriteLine($"Employee ID: {CurrentBill.employee.employeeID} \t Employee Name: {CurrentBill.employee.employeeName}");
            foreach(BillProduct billProduct in CurrentBill.BillProductList)
            {
                Console.WriteLine($"Product ID: {billProduct.product.productId}\t Product Name: {billProduct.product.productName} \t UnitPrice: {billProduct.product.unitPrice} \t NumOfQty: {billProduct.NumOfQty} \t Subtotal: {billProduct.SubTotal}");
            }
            Console.WriteLine("_______________________");
            Console.WriteLine($"Total bill Amount: {CurrentBill.BillAmount}");
            Console.WriteLine("_______________________");
            Console.WriteLine("You're Welcome!!!");
            Console.WriteLine("*******************");
        }
    }
}