using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Data.Models;

public class User : IdentityUser
{
    public User()
    {
        BorrowedBooks = new List<Book>();
    }
    
    public ICollection<Book> BorrowedBooks { get; set; }
}

