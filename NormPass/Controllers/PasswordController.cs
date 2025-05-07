using Microsoft.AspNetCore.Mvc;
using NormPass.Entities;
using NormPass.Services;
using NormPass.Utilities;
using System.Drawing;

namespace NormPass.Controllers
{
   
    public class PasswordController : Controller
    {
        private readonly PasswordGenService _passwordGenService;

        

        public PasswordController()
        {

            _passwordGenService = new PasswordGenService();
        }

        [HttpPost]
        public IActionResult GeneratePassword([FromBody] PasswordGen request)
        {
            var password = _passwordGenService.GeneratePassword(request);
            var strength = _passwordGenService.CalculateStrength(password);
            var crackTime = _passwordGenService.PasswordCrackTime(password);



            return Json(new {
                Password = password,
                PasswordStrength = strength,
                CrackTime = crackTime
            }) ;

        }

    }
}
