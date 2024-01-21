using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using training_final.Data;

namespace training_final.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LibraryController
    {
        MostFunctoins most = new MostFunctoins();

        // SHOW ALL BORROWED BOOK INFORMATION
        [HttpGet("SHOW ALL BORROWED BOOK")]
        public List<BorrowedBook> GetAllBorrowedBook()
        {
            List<BorrowedBook> borrowedBooks = most.Context().borrowedBooks.ToList();

            if(borrowedBooks.Count == 0 ) 
                return null;

            return borrowedBooks;
        }

        // SHOW ALL BORROWED BOOK BY CUSTOMER NAME
        [HttpGet("SHOW ALL BORROWED BOOK BY CUSTOMER")]
        public List<BorrowedBook> GetAllBorrowedBookByCustomer(string CustmerName)
        {
            List<BorrowedBook> borrowedBooks = most.Context().borrowedBooks.Where(i => i.Customer_Name == CustmerName).ToList();

            if (borrowedBooks.Count == 0)
                return null;

            return borrowedBooks;
        }

        // SHOW ALL BORROWED BOOK BY BOOK NAME
        [HttpGet("SHOW ALL BORROWED BOOK BY BOOK")]
        public List<BorrowedBook> GetAllBorrowedBookByBook(string BookName)
        {
            List<BorrowedBook> borrowedBooks = most.Context().borrowedBooks.Where(i => i.Book_Name == BookName).ToList();

            if (borrowedBooks.Count == 0)
                return null;

            return borrowedBooks;
        }

        // THIS METHOD LET YOU BORROW A BOOK BY CUSTOMER AND BOOK NAME
        [HttpPost("BORROW A BOOK")]
        public string BorrowBook(string CustomerName, string BookName)
        {
            var context = most.Context();

            Book book = context.Book.SingleOrDefault(i => i.Name == BookName);

            Customer customer = context.Customer.SingleOrDefault(i => i.Name == CustomerName);

            if (book == null)
                return $"THERE ARE NO BOOK WITH THIS NAME:{BookName}";
            else if (customer == null)
                return $"THERE ARE NO CUSTOMER WITH THIS NAME:{CustomerName}";
            else if (book.Countity !<= 1)
                return "THERE NOT ENOUGH BOOK";

            var day = context.Book.SingleOrDefault(i => i.Name == BookName).Day;

            book.Countity--;

            context.borrowedBooks.Add(new BorrowedBook
            {
                Book_Name = BookName,
                Customer_Name = CustomerName,
                Borrow_Date = DateTime.Now.ToString("dd/MM/yyyy/HH:mm"),
                Recive_Date = DateTime.Now.AddDays(day).ToString("dd/MM/yyyy/HH:mm")
            });
            
            context.SaveChanges();

            return $"THE CUSTOMER :{CustomerName} BORROWED THE BOOK :{BookName}";
        }

        // THIS METHOD LET YOU RECIVE A BOOK BY CUSTOMER AND BOOK NAME
        [HttpPost("RECIVE A BOOK")]
        public string ReciveBook(string CustomerName, string BookName)
        {
            var context = most.Context();

            var borroedBook = context.borrowedBooks.SingleOrDefault(i => i.Customer_Name == CustomerName &&
                                                                        i.Book_Name == BookName);

            if (borroedBook == null)
                return $"THERE ARE NO BORROWEDBOOK WITH THIS NAME:{BookName} AND CUSTOMER:{CustomerName}";

            context.borrowedBooks.Remove(borroedBook);
            context.Book.SingleOrDefault(i => i.Name == BookName).Countity++;

            context.SaveChanges();

            return $"THE CUSTOMER :{CustomerName} RECIVED THE BOOK :{BookName}";
        }
    }
}
