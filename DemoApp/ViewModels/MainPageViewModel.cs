using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ValidationControl.Interface;

namespace DemoApp.ViewModels
{
	public partial class MainPageViewModel : ObservableObject
	{
        public IRelayCommand SubmitCommand { get; }

		private string _password ="";
		public string Password
		{
			get => _password;
			set
			{
				if (_password != value)
				{
					_password = value;
					OnPropertyChanged();
				}
			}
		}
		[ObservableProperty]
		private string _confirmPassword;

        public MainPageViewModel()
        {
            SubmitCommand = new RelayCommand<IFormView>(OnSubmit);
        }

		private void OnSubmit(IFormView? formView)
		{
			if (formView is null || (formView.SubmitAction?.Invoke() ?? false))
				return;
		}
    }
}
