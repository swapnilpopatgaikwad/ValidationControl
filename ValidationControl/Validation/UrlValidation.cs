namespace ValidationControl.Validation
{
    public class UrlValidation : BaseValidation
    {
        protected override string DefaultMessage => "Invalid URL";

        public override bool Validate(object value)
        {
            if (value is string stringValue)
            {
                Uri uriResult;
                return Uri.TryCreate(stringValue, UriKind.Absolute, out uriResult) &&
                       (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }

            return false;
        }
    }

}
