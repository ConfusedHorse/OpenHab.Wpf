using System.Globalization;
using System.Windows.Controls;

namespace OpenHab.Wpf.View.Resources.Helpers.Validation
{
    public class HourValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var validationResult = new ValidationResult(true, null);

            if (value == null) return new ValidationResult(false, Properties.Resources.FieldMustNotBeEmpty);
            var validInt = int.TryParse(value.ToString(), out int validValue);
            var success = validInt && Validate(validValue, 23);

            return !success
                ? new ValidationResult(false, string.Format(Properties.Resources.ValidateHourMinSec, 0, 23))
                : validationResult;
        }

        private static bool Validate(int value, int upper, int lower = 0)
        {
            return value <= upper && value >= lower;
        }
    }
}