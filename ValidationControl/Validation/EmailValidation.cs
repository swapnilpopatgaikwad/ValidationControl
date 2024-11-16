namespace ValidationControl.Validation
{
    public class EmailValidation : BaseValidation
    {
        protected override string DefaultMessage => "Invalid email address";

        public override bool Validate(object value)
        {
            if (value is string stringValue)
            {
                var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return System.Text.RegularExpressions.Regex.IsMatch(stringValue, emailRegex);
            }

            return false;
        }
    }
}
