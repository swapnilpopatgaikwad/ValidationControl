using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Documents;
using ValidationControl.CustomControl;

namespace ValidationControl.Extensions
{
	public static partial class CMDatePickerExtensions
	{

		public static void UpdateUnderline(this IDatePickerHandler handler, CMDatePicker cMDatePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			bool noUnderline = cMDatePicker.NoUnderline; // Check if underline should be removed

			if (handler.PlatformView is Microsoft.UI.Xaml.Controls.CalendarDatePicker platformView)
			{
				if (noUnderline)
				{
					platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0); // Removes the underline
				}
				else
				{
					platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(1); // Adds the underline
				}
			}
		}
	}
}
