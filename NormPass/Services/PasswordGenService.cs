using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NormPass.Entities;
using NormPass.Utilities;
using System.Text.RegularExpressions;

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
        public string PasswordCrackTime(string password)
        {
            int charsetSize = 0;

            if (Regex.IsMatch(password, "[a-z]")) charsetSize += 26;
            if (Regex.IsMatch(password, "[A-Z]")) charsetSize += 26;
            if (Regex.IsMatch(password, "[0-9]")) charsetSize += 10;
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]")) charsetSize += 32;

            //Total combinations
            double combinations = Math.Pow(charsetSize, password.Length);
            //Estimated cracking speed
            double guessesPerSecond = 1e10; //10 billion per second
            //Time to crack in seconds
            double secondsToCrack = combinations / guessesPerSecond;
            //Converts into a time for nice display
            return FormatTime(secondsToCrack);
        }
        //Format Time helper method otherwise the above won't work due to Format Time not returning a value.
        private string FormatTime(double seconds)
        {
            if (seconds < 60)
                return $"{seconds:F2} seconds";

            if (seconds < 3600)
                return $"{seconds / 60:F2} minutes";

            if (seconds < 86400)
                return $"{seconds / 3600:F2} hours";

            if (seconds < 2592000)
                return $"{seconds / 86400:F2} days";

            if (seconds < 31536000)
                return $"{seconds / 2592000:F2} months";

           return
                    $"{(seconds / 31536000):F2} years";

        }
	};
}
