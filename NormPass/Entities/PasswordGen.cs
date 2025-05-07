using System.ComponentModel.DataAnnotations;

namespace NormPass.Entities
{
    public class PasswordGen
    {
       
        public int Length { get; set; }

        public bool IncludeUpperCase { get; set; }
        public bool IncludeLowerCase { get; set; }
        public bool IncludeNumbers { get; set; }
        public bool IncludeSymbols { get; set; }
        
    }
}
