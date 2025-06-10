using AutoMapper;
using CORE.DTOs.ApplicationUser;
using CORE.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEBUI.EmailServices;
using WEBUI.Models;

namespace WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,IMapper mapper,IEmailSender emailSender)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            ModelState.AddModelError("", "Lanet olsun sana, Lanet olsun aşkına, seni çok sevmiştim");
            return View(model);
        }

        public IActionResult Register()
        {
            return View(new CreateApplicaitonUserDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateApplicaitonUserDTO model)
        {
            ModelState.Remove("UserName");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.UserName = model.Email;

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //GenerateToken
                //EmailConfirmed

                var generateToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                string callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    token = generateToken,
                    userId = user.Id
                });

                string body = $"Lüften Email adresinize gönderilen doğrulama bağlantısına tıklayınız:<a href='{callbackUrl}'>Email Doğrula</a>";

                await _emailSender.SendEmailAsync(user.Email, "Üyelik Onayı", body);
            }




            return View();
        }

        public IActionResult ConfirmEmail(string token, string userId)
        {
            return View();
        }
    }
}

