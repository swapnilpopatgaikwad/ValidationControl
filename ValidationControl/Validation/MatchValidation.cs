using ValidationControl.Interface;

namespace ValidationControl.Validation
{
    public class MatchValidation : BaseValidation
    {
        public static readonly BindableProperty MatchValueProperty =
            BindableProperty.Create(nameof(MatchValue), typeof(string), typeof(MatchValidation), null);

        public string MatchValue
        {
            get => (string)GetValue(MatchValueProperty);
            set => SetValue(MatchValueProperty, value);
        }

        protected override string DefaultMessage => "Values do not match";

        public override bool Validate(object value)
        {
            return value is string stringValue && stringValue == MatchValue;
        }
    }
}
