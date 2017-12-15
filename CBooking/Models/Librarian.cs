using System;
using System.Collections.Generic;

namespace CBooking.Models
{
    public partial class Librarian
    {
        public int LibrarianId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int ManageId { get; set; }
        public string BorrowDay { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
