using System.ComponentModel;

namespace ValidationControl.Interface
{
    public interface IValidatable : INotifyPropertyChanged
	{
        List<IValidation> Validations { get; }

        bool IsValid { get; }

        void ShowValidation();

        void ResetValidation();
    }
}
