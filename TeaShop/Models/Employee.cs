using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaShop.Repository;

namespace TeaShop.Models
{
    public class Employee
    {
        int _employeeID;
        string _employeeName;
        string _emptype;
        string _contact;

        public int employeeID
        {
            get { return _employeeID; }
            //set { _employeeID = value; }
        }

        public string employeeName
        {
            get { return _employeeName;}
            //set { _employeeName = value; }
        }

        public string emptype
        {
            get { return _emptype; }
            //set { _emptype = value; }
        }

        public string contact
        {
            get { return _contact; }
            //set { _contact = value; }
        }


        public Employee(string employeeName, string empType, string Contact)
        {
            this._employeeName = employeeName;
            this._emptype = empType;
            this._contact = Contact;
        }

        public Employee(int Id, string employeeName, string empType, string Contact)
        {
            this._employeeID = Id;
            this._employeeName = employeeName;
            this._emptype = empType;
            this._contact = Contact;
        }

        public Employee() 
        { 
        }

        
        public void AddEmployee(Employee employee)
        {
            EmployeeRepository.employeeList.Add(employee);
            Console.WriteLine("Employee details added successfully.");
        }

        public void UpdateEmployee(Employee employee)
        {
            var CurrentEmployee = EmployeeRepository.employeeList.FirstOrDefault(x => x.employeeID == employee.employeeID);
            int value = EmployeeRepository.employeeList.IndexOf(CurrentEmployee);
            EmployeeRepository.employeeList.RemoveAt(value);
            if(CurrentEmployee != null)
            {
                EmployeeRepository.employeeList.Insert(value, employee);
            }
        }

        public Employee GetEmployee(int id)
        {
            Employee currentObj = new Employee();
            var CurrentEmployee = EmployeeRepository.employeeList.FirstOrDefault(x=>x.employeeID == id);
            if(CurrentEmployee != null)
            {
                currentObj = CurrentEmployee;
                //Console.WriteLine(CurrentEmployee.employeeID+"\t"+CurrentEmployee.employeeName+"\t"+CurrentEmployee.emptype+"\t"+"\t"+CurrentEmployee.contact);
            }
            return currentObj;
        }

        public void DeleteEmployee(int id)
        {
            var currentEmployee = EmployeeRepository.employeeList.FirstOrDefault( x => x.employeeID == id);
            if(currentEmployee != null)
            {
                string Name = currentEmployee.employeeName;
                EmployeeRepository.employeeList.Remove(currentEmployee);
                Console.WriteLine($"{Name}, employee details from the list.");
            }
        }
    }
}
