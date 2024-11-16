namespace ValidationControl.Validation
{
    public class RangeValidation : BaseValidation
    {
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(double), typeof(RangeValidation), double.MinValue);
        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(RangeValidation), double.MaxValue);

        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        protected override string DefaultMessage => "Value out of range";

        public override bool Validate(object value)
        {
            if (value is double doubleValue)
            {
                return doubleValue >= MinValue && doubleValue <= MaxValue;
            }

            if (value is string stringValue && double.TryParse(stringValue, out double numericValue))
            {
                return numericValue >= MinValue && numericValue <= MaxValue;
            }

            return false;
        }
    }

}
