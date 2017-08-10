using System.Globalization;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Resources.Helpers.Validation
{

    public class DoubleValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);

            if (value == null) return new ValidationResult(false, Properties.Resources.FieldMustNotBeEmpty);
            var success = double.TryParse(value.ToString(), out double _);

            return !success ? new ValidationResult(false, Properties.Resources.IllegalCharacters) : validationResult;
        }
    }
}