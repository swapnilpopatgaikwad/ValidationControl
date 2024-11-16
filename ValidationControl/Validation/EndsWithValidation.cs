namespace ValidationControl.Validation
{
    public class EndsWithValidation : BaseValidation
    {
        public static readonly BindableProperty SuffixProperty =
            BindableProperty.Create(nameof(Suffix), typeof(string), typeof(EndsWithValidation), string.Empty);

        public string Suffix
        {
            get => (string)GetValue(SuffixProperty);
            set => SetValue(SuffixProperty, value);
        }

        protected override string DefaultMessage => "Invalid suffix";

        public override bool Validate(object value)
        {
            return value is string stringValue && stringValue.EndsWith(Suffix);
        }
    }

}
