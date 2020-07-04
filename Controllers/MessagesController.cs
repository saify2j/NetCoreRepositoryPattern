using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessagingRealtime.Data;
using MessagingRealtime.Models;
using MessagingRealtime.EFCore;

namespace MessagingRealtime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : GenericController<Message, MessageRepository>
    {
        public MessagesController(MessageRepository repository) : base(repository)
        {

        }
    }
}
