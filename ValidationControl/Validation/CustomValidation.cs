using System.Data.SqlTypes;
using ValidationControl.Interface;

namespace ValidationControl.Validation
{
    public class CustomValidation : BaseValidation
    {
        public static readonly BindableProperty ValidateFuncProperty =
            BindableProperty.Create(nameof(ValidateFunc), typeof(Func<object, bool>), typeof(CustomValidation), null);

        public Func<object, bool> ValidateFunc
        {
            get => (Func<object, bool>)GetValue(ValidateFuncProperty);
            set => SetValue(ValidateFuncProperty, value);
        }

        protected override string DefaultMessage => "Validation failed";

        public override bool Validate(object value)
        {
            return ValidateFunc?.Invoke(value) ?? true;
        }
    }

}
