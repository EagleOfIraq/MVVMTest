using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyValidators
{
    public class StringRangeValidationRule : ValidationRule
    {
        private int minimumLength = 5;
        private int maximumLength = 10;
        private string errorMessage="length most be between 5 and 10";

       
        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();
            if (inputString.Length < this.minimumLength ||
                (this.maximumLength > 0 &&
                 inputString.Length > this.maximumLength))
            {
                result = new ValidationResult(false, this.errorMessage);
            }

            return result;
        }
    }

}
