namespace ValidationControl.Validation
{
    public class RegexValidation : BaseValidation
    {
        public static readonly BindableProperty PatternProperty =
            BindableProperty.Create(nameof(Pattern), typeof(string), typeof(RegexValidation), string.Empty);

        public string Pattern
        {
            get => (string)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }

        protected override string DefaultMessage => "Invalid format";

        public override bool Validate(object value)
        {
            if (value is string stringValue && !string.IsNullOrEmpty(Pattern))
            {
                return System.Text.RegularExpressions.Regex.IsMatch(stringValue, Pattern);
            }

            return true;
        }
    }

}
