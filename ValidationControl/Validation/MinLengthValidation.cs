using ValidationControl.Interface;

namespace ValidationControl.Validation
{
    public class MinLengthValidation : BaseValidation
    {
        public static readonly BindableProperty MinLengthProperty =
            BindableProperty.Create(nameof(MinLength), typeof(int), typeof(MinLengthValidation), 0);

        public int MinLength
        {
            get => (int)GetValue(MinLengthProperty);
            set => SetValue(MinLengthProperty, value);
        }

        protected override string DefaultMessage => "Minimum length not met";

        public override bool Validate(object value)
        {
            if (value is string stringValue)
            {
                return stringValue.Length >= MinLength;
            }

            return false;
        }
    }
}
