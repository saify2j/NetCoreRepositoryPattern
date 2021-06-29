using MessagingRealtime.EFCore;
using MessagingRealtime.Models;
using MessagingRealtime.Services;
using MessagingRealtime.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingRealtime.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AppUserRepository _userRepository;
        public AuthenticationController(AppUserRepository appUserRepository)
        {
            _userRepository = appUserRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            try
            {
                List<AppUser> users = await _userRepository.GetAll();
                AppUser user = users.Where(x => (x.Email == loginViewModel.Identity || x.UserName == loginViewModel.Identity)).FirstOrDefault();
                if(user is not null)
                {
                    SecurityService.DecryptAndCheck(user.Password, user.SecretSalt, loginViewModel.PlainSecret);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch(Exception ex)
            {
                if(ex is UnauthorizedAccessException)
                {
                    TempData["ERROR"] = ex.Message;
                }
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] AppUserViewModel appUserModel)
        {
            try
            {
                AppUser newAppUser = new();
                newAppUser.FirstName = appUserModel.FullName;
                newAppUser.LastName = appUserModel.FullName;
                newAppUser.UserName = appUserModel.UserName;
                newAppUser.Email = appUserModel.Email;
                newAppUser.SecretSalt = SecurityService.GenerateSalt();
                newAppUser.Password = SecurityService.EncryptSecret(newAppUser.SecretSalt, appUserModel.Password);
                newAppUser.Location = appUserModel.Location;
                newAppUser.BirthDate = DateTime.Now;

                await _userRepository.Add(newAppUser);
                return RedirectToAction("Login", "Authentication");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
