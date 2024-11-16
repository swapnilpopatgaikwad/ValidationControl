namespace ValidationControl.Validation
{
    public class FutureDateValidation : BaseValidation
    {
        protected override string DefaultMessage => "Date must be in the future";

        public override bool Validate(object value)
        {
            return value is string stringValue && DateTime.TryParse(stringValue, out DateTime date) && date > DateTime.Now;
        }
    }

}
