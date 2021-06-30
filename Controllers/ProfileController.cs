using MessagingRealtime.EFCore;
using MessagingRealtime.Models;
using MessagingRealtime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingRealtime.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppUserRepository _userRepository;
        public ProfileController(AppUserRepository appUserRepository)
        {
            _userRepository = appUserRepository;
        }
        public async Task<IActionResult> Profile(string UserName)
        {
            var users = await _userRepository.GetAll();
            AppUser user = users.Where(x => x.UserName == UserName).FirstOrDefault();
            AppUserViewModel viewModel = new()
            {
                FullName = user.FirstName,
                UserName = user.UserName,
                Location = user.Location,
                Email = user.Email,
                DateOfBirth = user.BirthDate.Date.ToShortDateString()
            };
            return View(viewModel);
        }
    }
}
