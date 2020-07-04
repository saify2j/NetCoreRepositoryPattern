using MessagingRealtime.Data;
using MessagingRealtime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingRealtime.EFCore
{
    public class MessageRepository: EfCoreRepository<Message, MyDbContext>
    {
        public MessageRepository(MyDbContext context): base(context)
        {

        }
    }
}
