using System;
using System.Collections.Generic;

namespace CBooking.Models
{
    public partial class User
    {
        public User()
        {
            Librarian = new HashSet<Librarian>();
            Manage = new HashSet<Manage>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }

        public ICollection<Librarian> Librarian { get; set; }
        public ICollection<Manage> Manage { get; set; }
    }
}
