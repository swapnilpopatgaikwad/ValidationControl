namespace ValidationControl.Validation
{
    public class RequiredValidation : BaseValidation
    {
        protected override string DefaultMessage => "This field is required";

        public override bool Validate(object value)
        {
            return value is string stringValue ? !string.IsNullOrWhiteSpace(stringValue) : value != null;
        }
    }
}
