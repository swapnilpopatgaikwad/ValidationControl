using System.ComponentModel;

namespace ValidationControl.Interface
{
    public interface IValidatable
    {
        List<IValidation> Validations { get; }

        bool IsValid { get; }

        void ShowValidation();

        void ResetValidation();
    }
}
