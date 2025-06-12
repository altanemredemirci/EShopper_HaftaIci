using AutoMapper;
using CORE.DTOs.ApplicationUser;
using CORE.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEBUI.EmailServices;
using WEBUI.Models;

namespace WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,IMapper mapper,IEmailSender emailSender,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Email ile kayıtlı bir kullanıcı bulunamadı");
                    return View(model);
                }

                await _signInManager.PasswordSignInAsync(user, model.Password, model.IsPersient, true);

                return RedirectToAction("Index","Home");
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

                string body = $"Lüften Email adresinize gönderilen doğrulama bağlantısına tıklayınız:<a href='http://localhost:5038{callbackUrl}'>Email Doğrula</a>";
                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "Üyelik Onayı", body);
                    TempData["message"] = "Lütfen email adresinize gönderilen 'Üyelik Onay Linkine tıklayınız.'";
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Onay maili gönderilirken bir hata oluştu");

                    return View(model);
                }
            

            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            if(token==null || userId == null)
            {
                TempData["message"] = "Kullanıcı Id veya token anahtarı hatalı!!";
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["message"] = "Kullanıcı bulunamadı!";
                return RedirectToAction("Login");
            }

            await _userManager.ConfirmEmailAsync(user, token);
            TempData["message"] = "Üyelik hesabınız onaylandı.";
            return RedirectToAction("Login");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                TempData["message"] = "Email ile kayıtlı bir kullanıcı bulunamadı";
                return View();
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            string callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                token = resetToken,
                userId = user.Id
            });

            string body = $"Şifre yenileme maili gönderildi. Lütfen emailinizdeki resetleme linkine tıklayınız.<a href='http://localhost:5038{callbackUrl}'>Şifre Yenileme</a>";
            try
            {
                await _emailSender.SendEmailAsync(user.Email, "Şifre Yenileme", body);
                TempData["message"] = "Lütfen email adresinize gönderilen 'Şifre Yenileme Linkine tıklayınız.'";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                TempData["message"] = "Şifre yenileme maili gönderilirken bir hata oluştu";

                return View();
            }
        }

        public IActionResult ResetPassword(string token, string userId)
        {
            return View(new ResetPasswordViewModel()
            {
                token=token,
                userId=userId
            });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.userId);

            if (user == null)
            {
                ModelState.AddModelError("","Kayıtlı bir kullanıcı bulunamadı");
                return View(model);
            }

            try
            {
                await _userManager.ResetPasswordAsync(user, model.token, model.Password);
                TempData["message"] = "Şifreniz yenilendi. Yeni şifre ile giriş yapınız.";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Şifre yenileme işlemi başarısız!");
                return View(model);
            }
          

           
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


    }
}

