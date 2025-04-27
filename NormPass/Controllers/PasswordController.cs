using Microsoft.AspNetCore.Mvc;
using NormPass.Entities;
using NormPass.Services;

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
            return Json(new { Password = password });
            
        }
    }
}
