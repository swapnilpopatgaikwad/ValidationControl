namespace ValidationControl.Validation
{
    public class StartsWithValidation : BaseValidation
    {
        public static readonly BindableProperty PrefixProperty =
            BindableProperty.Create(nameof(Prefix), typeof(string), typeof(StartsWithValidation), string.Empty);

        public string Prefix
        {
            get => (string)GetValue(PrefixProperty);
            set => SetValue(PrefixProperty, value);
        }

        protected override string DefaultMessage => "Invalid prefix";

        public override bool Validate(object value)
        {
            return value is string stringValue && stringValue.StartsWith(Prefix);
        }
    }

}
