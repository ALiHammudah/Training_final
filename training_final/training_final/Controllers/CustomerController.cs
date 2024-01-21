using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using training_final.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace training_final.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CustomerController
    {
        MostFunctoins most = new MostFunctoins();

        // SHOW ALL CUSTOMER INFORMATOIN
        [HttpGet("SHOW ALL CUSTOMER")]
        public List<Customer> GetAllCustomer()
        {
            List<Customer> customers = most.Context().Customer.ToList();

            if (customers.Count == 0)
                return null;

            return customers;
        }

        // ADD NEW CUSTOMER TO DATABASE
        [HttpPost("ADD NEW CUSTOMER")]
        public Customer AddCustomer(string name, string address, string phone) 
        {
            var context = most.Context();

            var customer = context.Customer.SingleOrDefault(i => i.Name == name);

            if (customer != null)
                return null;

            context.Customer.Add(new Customer
            {
                Name = name,
                Address = address,
                Phone = phone,
            });

            context.SaveChanges();

            return context.Customer.SingleOrDefault(i => i.Name == name);
        }

        //THIS METHOD CAN EDIT A CUSTMER BY OLD NAME WITH NEW INFORMATION
        [HttpPut("EDIT A CUSTOMER")]
        public string UpdateBook(string name, string newName, string newAddress, string newPhone)
        {
            var context = most.Context();

            var customer = context.borrowedBooks.SingleOrDefault(i => i.Customer_Name == name);

            if (customer != null)
                return $"YOU CANNOT EDIT THE CUSTOMER:{name} IT IS IN THE BORROED BOOK LIST";

            var customer2 = context.Customer.SingleOrDefault(i => i.Name == newName);

            if (customer2 != null)
                return $"THERE ARE ALREADY CUSTOMER WITH THIS NAME:{newName}";

            customer2 = context.Customer.SingleOrDefault(i => i.Phone == newPhone);

            if (customer2 != null)
                return $"THERE ARE CUSTOMER WITH THIS PHONE:{newPhone}";

            customer2 = context.Customer.SingleOrDefault(i => i.Name == name);

            if (customer2 == null)
                return $"THERE ARE NO CUSTOMER WITH THIS NAME:{name}";

            customer2.Name = newName;
            customer2.Address = newAddress;
            customer2.Phone = newPhone;

            context.SaveChanges();

            return $"CUSTOMER :{name} EDITS";
        }

        // DELETE A CUSTOMER FROM DATABASE USEING CUSTOMER NAME
        [HttpDelete("DELETE A CUSTOMER BY CUSTOMER NAME")]
        public string DeleteCustomer(string name)
        {
            var context = most.Context();

            Customer t = context.Customer.SingleOrDefault(i => i.Name == name);

            if (t == null)
                return $"THERE NO CUSTOMER WITH THIS NAME :{name}";

            var borrowedBook = context.borrowedBooks.SingleOrDefault(i => i.Customer_Name == name);

            if(borrowedBook != null)
                return $"YOU CANNOT DELETE THE CUSTOMER NAME:{name} ,HE IS IN THE BORROED BOOK LIST";

            context.Customer.Remove(t);

            context.SaveChanges();

            return $"THE CUSTOMER :{name} DELETED";
        }
    }
}
