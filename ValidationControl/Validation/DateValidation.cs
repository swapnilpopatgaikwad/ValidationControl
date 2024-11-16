namespace ValidationControl.Validation
{
    public class DateValidation : BaseValidation
    {
        protected override string DefaultMessage => "Invalid date format";

        public override bool Validate(object value)
        {
            return value is string stringValue && DateTime.TryParse(stringValue, out _);
        }
    }

}
