namespace training_final.Data
{
    public class Book
    {
        public int Id { get; set; }         //  THE ID OF BOOK

        public string Name { get; set; }    //  THE NAME OF BOOK

        public string Title { get; set; }   //  THE TITLE OF BOOK FOR MORE DETAIL

        public string Catogry { get; set; } //  THE CATOGRY OF BOOK

        public int Countity { get; set; }   //  THE COUNTITY OF BOOK

        public int Day { get; set; }        //  THE DAYS YOU MUST RETURN BOOK BEFOR THE DAY END

        public List<Customer> customers { get; set; }
    }
}
