using System;
using System.Collections.Generic;

namespace CBooking.Models
{
    public partial class Book
    {
        public Book()
        {
            Librarian = new HashSet<Librarian>();
            Manage = new HashSet<Manage>();
        }

        public int BookId { get; set; }
        public string Bookname { get; set; }
        public string Category { get; set; }

        public ICollection<Librarian> Librarian { get; set; }
        public ICollection<Manage> Manage { get; set; }
    }
}
