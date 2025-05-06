using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NormPass.Entities;
using NormPass.Utilities;

namespace NormPass.Services
{
    public class PasswordGenService
    {
        public readonly Random _random = new Random();

        public string GeneratePassword(PasswordGen request)
        {
            var chars = "";

            if (request.IncludeLowerCase)
                chars += "abcdefghijklmnopqrstuvwxyz";

            if (request.IncludeUpperCase)
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (request.IncludeNumbers)
                chars += "0123456789";

            if (request.IncludeSymbols)
                chars += "!@£$%^&*()+_?";

            if (string.IsNullOrEmpty(chars))
                return "";

            return new string(Enumerable.Repeat(chars, request.Length)
                .Select(l => l[_random.Next(l.Length)]).ToArray());
        }

        public string CalculateStrength(string password)
        {
            int passwordScore = 0;

            if (password.Length >= 10)
                passwordScore++;
            //If the password has at least one uppercase letter (like A, B, C...),+1 point.
            if (System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]"))
                passwordScore++;

            if (System.Text.RegularExpressions.Regex.IsMatch(password, "[0-9]"))
                passwordScore++;
            //If the password has at least one special character then 1+point.
            if (System.Text.RegularExpressions.Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                passwordScore++;

            if (passwordScore == 1)
                return PasswordStrength.Weak;

            if (passwordScore == 2)
                return PasswordStrength.Average;

            if (passwordScore > 2 && passwordScore < 5)
                return PasswordStrength.Strong;

            else
      
            return PasswordStrength.InvalidScore;
        }
    }
}
