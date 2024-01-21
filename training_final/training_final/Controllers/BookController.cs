using Microsoft.AspNetCore.Mvc;
using training_final.Data;

namespace training_final.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    
    public class BookController
    {
        MostFunctoins most = new MostFunctoins();

        // SHOW ALL BOOKS INFORMATION
        [HttpGet("SHOW ALL BOOKS")]
        public List<Book> GetAllBook() 
        {
            List<Book> books =  most.Context().Book.ToList();

            if(books.Count == 0)
                return null;

            return books;
        }

        // ADD NEW BOOK TO DATABASE
        [HttpPost("ADD NEW BOOK")]
        public Book AddBook(string name, string titel,
            string catogry, int countity, int date) 
        {
            var context = most.Context();

            var book = context.Book.SingleOrDefault(i => i.Name == name);

            if (book != null)
                return null;

            context.Book.Add(new Book {
                Name = name,
                Title = titel,
                Catogry = catogry,
                Countity = countity,
                Day = date
            });

            context.SaveChanges();

            return context.Book.SingleOrDefault(i => i.Name == name);
        }

        //THIS METHOD CAN EDIT A BOOK BY OLD NAME WITH NEW INFORMATION
        [HttpPut("EDIT A BOOK")]
        public string UpdateBook(string name,string newName, string newTitle,
            string newCatogry, int newCountity, int newDate)
        {
            var context = most.Context();

            var book = context.borrowedBooks.SingleOrDefault(i => i.Book_Name == name);

            if (book != null)
                return $"YOU CANNOT EDIT THE BOOK:{name} IT IS IN THE BORROED BOOK LIST";

            var book2 = context.Book.SingleOrDefault(i => i.Name == newName);

            if (book2 != null)
                return $"THERE ARE ALREADY BOOK WITH THIS NAME:{newName}";

            book2 = context.Book.SingleOrDefault(i => i.Name == name);

            if(book2 == null)
                return $"THERE ARE NO BOOK WITH THIS NAME:{name}";

            book2.Name = newName;
            book2.Title = newTitle;
            book2.Catogry = newCatogry;
            book2.Countity = newCountity;
            book2.Day = newDate;

            context.SaveChanges();

            return $"BOOK :{name} EDITs";
        }

        // DELETE A BOOK USEING BOOK NAME FROM DATABASE
        [HttpDelete("DELETE A BOOK BY BOOK NAME")]
        public string DeleteBook(string name)
        {
            var context = most.Context();

            Book t = context.Book.SingleOrDefault(i => i.Name == name);

            if (t == null)
                return $"THERE NO BOOK WITH THIS NAME :{name}";

            var borrowedBook = context.borrowedBooks.SingleOrDefault(i => i.Book_Name == name);

            if (borrowedBook != null)
                return $"YOU CANNOT DELETE THE BOOK NAME:{name} ,IT IS IN THE BORROED BOOK LIST";

            context.Book.Remove(t);

            context.SaveChanges();

            return $"THE BOOK :{name} DELETED";
        }
    }
}
