using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyValidators
{
    public class IntegerValidationRule : ValidationRule
    {
        private int minimumLength = 1;
        private int maximumLength = 3;
        private string errorMessage = "input most be number of length between 1 and 3";


        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();
            if (!int.TryParse(inputString, out var x) || inputString.Length < this.minimumLength ||
                (this.maximumLength > 0 &&
                 inputString.Length > this.maximumLength))
            {
                result = new ValidationResult(false, this.errorMessage);
            }

            return result;
        }
    }

}
