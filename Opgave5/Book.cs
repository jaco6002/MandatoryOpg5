using System;

namespace TCPServer
{
    public class Book
    {
        private string _author;
        private int _pages;
        private string _isbn13;

        public Book(string title, string author, int pages, string isbn13 )
        {
            Title = title;
            _author = author;
            _pages = pages;
            _isbn13 = isbn13;
        }

        public string Title { get; set; }
        public string Author
        {
            get { return _author;} 
            set
            {
                if (value.Length >= 2)
                {
                    _author = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("author",value, "author name must contain at least 2 characters");
                }
            }
        }
        public int Pages
        {
            get { return _pages;}
            set
            {
                if (4 <= value && value <= 1000)
                {
                    _pages = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("pages",value, "pages must be between 4 and 1000");
                }
            } }
        public string Isbn13
        {
            get { return _isbn13;}
            set
            {
                if (value.Length==13)
                {
                    _isbn13 = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("isbn13",value, "isbn13 must be exactly 13 characters");
                }
            }

        }

        public override string ToString()
        {
            return "title: "+ Title + "\tauthor: " + Author;
        }
    }
}
