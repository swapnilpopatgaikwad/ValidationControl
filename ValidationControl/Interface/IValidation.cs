namespace ValidationControl.Interface
{
    public interface IValidation
    {
        string Message { get; }

        bool Validate(object value);
    }
}
