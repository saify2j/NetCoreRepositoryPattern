using MessagingRealtime.Data;
using MessagingRealtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingRealtime.EFCore
{
    public class AppUserRepository : EfCoreRepository<AppUser, MyDbContext>
    {
        public AppUserRepository(MyDbContext context): base(context)
        {

        }
    }
}
