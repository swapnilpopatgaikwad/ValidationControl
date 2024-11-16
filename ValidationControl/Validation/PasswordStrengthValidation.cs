namespace ValidationControl.Validation
{
    public class PasswordStrengthValidation : BaseValidation
    {
        protected override string DefaultMessage => "Password does not meet strength criteria";

        public override bool Validate(object value)
        {
            if (value is string stringValue)
            {
                // At least one uppercase, one lowercase, one number, one special character, and minimum 8 characters
                var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$";
                return System.Text.RegularExpressions.Regex.IsMatch(stringValue, passwordRegex);
            }

            return false;
        }
    }

}
