
namespace ValidationControl.Validation
{
    public class NumericValidation : BaseValidation
    {
        protected override string DefaultMessage => "Only numeric values allowed";

        public override bool Validate(object value)
        {
            return value is string stringValue && double.TryParse(stringValue, out _);
        }
    }

}
