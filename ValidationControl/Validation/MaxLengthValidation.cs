namespace ValidationControl.Validation
{
    public class MaxLengthValidation : BaseValidation
    {
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(MaxLengthValidation), int.MaxValue);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        protected override string DefaultMessage => "Maximum length exceeded";

        public override bool Validate(object value)
        {
            if (value is string stringValue)
            {
                return stringValue.Length <= MaxLength;
            }

            return true;
        }
    }

}
