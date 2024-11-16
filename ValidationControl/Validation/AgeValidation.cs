using ValidationControl.Interface;

namespace ValidationControl.Validation
{
    public class AgeValidation : BaseValidation
    {
        public static readonly BindableProperty MinAgeProperty =
            BindableProperty.Create(nameof(MinAge), typeof(int), typeof(AgeValidation), 0);
        public static readonly BindableProperty MaxAgeProperty =
            BindableProperty.Create(nameof(MaxAge), typeof(int), typeof(AgeValidation), 150);

        public int MinAge
        {
            get => (int)GetValue(MinAgeProperty);
            set => SetValue(MinAgeProperty, value);
        }

        public int MaxAge
        {
            get => (int)GetValue(MaxAgeProperty);
            set => SetValue(MaxAgeProperty, value);
        }

        protected override string DefaultMessage => "Invalid age";

        public override bool Validate(object value)
        {
            if (value is string stringValue && int.TryParse(stringValue, out int age))
            {
                return age >= MinAge && age <= MaxAge;
            }

            return false;
        }
    }

}
