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
using System.Linq;
using Newtonsoft.Json;

namespace MessagingRealtime.Controllers
{
    [Route("api/user")]
    public class UsersController : Controller
    {
        private readonly AppUserRepository _userRepository;
        public UsersController(AppUserRepository appUserRepository)
        {
            _userRepository = appUserRepository;
        }

        [Produces("application/json")]
        [HttpPost("search")]
        public async Task<IActionResult> Search(string search_keyword)
        {
            try
            {
                List<AppUser> users = await _userRepository.GetAll();
                string term = search_keyword;
                var names = users.Where(p => (p.UserName.Contains(term)) || (p.FirstName.Contains(term)))
                    .Select(p => new { p.UserName ,p.FirstName,p.Location})
                    .ToList();
                return Json(new { data = names });
            }
            catch
            {
                return BadRequest();
            }
        }
        
    }
}
