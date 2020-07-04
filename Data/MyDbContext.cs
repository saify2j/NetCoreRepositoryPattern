using MessagingRealtime.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingRealtime.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options ):base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
    }
}
