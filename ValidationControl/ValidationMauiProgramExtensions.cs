using ValidationControl.CustomControl;
using ValidationControl.Handler;

namespace ValidationControl
{
	public static class ValidationMauiProgramExtensions
	{
		public static MauiAppBuilder UseValidationControl(this MauiAppBuilder builder)
		{
			builder.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler(typeof(CMDatePicker), typeof(CMDatePickerHandler));
				handlers.AddHandler(typeof(CMTimePicker), typeof(CMTimePickerHandler));
			});

			return builder;
		}
	}
}
