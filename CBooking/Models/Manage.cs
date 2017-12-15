using System;
using System.Collections.Generic;

namespace CBooking.Models
{
    public partial class Manage
    {
        public int ManageId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string ManageDescription { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
