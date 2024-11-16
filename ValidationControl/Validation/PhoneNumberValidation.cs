namespace ValidationControl.Validation
{
    public class PhoneNumberValidation : BaseValidation
    {
        protected override string DefaultMessage => "Invalid phone number";

        public override bool Validate(object value)
        {
            if (value is string stringValue)
            {
                var phoneRegex = @"^\+?[1-9]\d{1,14}$"; // E.164 format
                return System.Text.RegularExpressions.Regex.IsMatch(stringValue, phoneRegex);
            }

            return false;
        }
    }

}
