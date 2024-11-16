namespace ValidationControl.Validation
{
    public class ContainsValidation : BaseValidation
    {
        public static readonly BindableProperty SubstringProperty =
            BindableProperty.Create(nameof(Substring), typeof(string), typeof(ContainsValidation), string.Empty);

        public string Substring
        {
            get => (string)GetValue(SubstringProperty);
            set => SetValue(SubstringProperty, value);
        }

        protected override string DefaultMessage => "Value must contain specified substring";

		public override bool Validate(object value)
		{
			return value is string stringValue && stringValue.Contains(Substring, StringComparison.OrdinalIgnoreCase);
		}
	}

}
