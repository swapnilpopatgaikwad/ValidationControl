using ValidationControl.Interface;

namespace ValidationControl.Validation
{
    public abstract class BaseValidation : BindableObject, IValidation
    {
        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(BaseValidation), null);

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// Provides the default message for the validation.
        /// Derived classes should override this to set their specific message.
        /// </summary>
        protected abstract string DefaultMessage { get; }

        public BaseValidation()
        {
            // Set the default message if not explicitly provided
            if (string.IsNullOrEmpty(Message))
            {
                Message = DefaultMessage;
            }
        }

        /// <summary>
        /// Derived classes must implement this to perform validation logic.
        /// </summary>
        public abstract bool Validate(object value);
    }

}
