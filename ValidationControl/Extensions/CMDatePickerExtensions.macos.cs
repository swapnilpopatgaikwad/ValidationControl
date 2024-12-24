using Microsoft.Maui.Handlers;
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

			if (handler.PlatformView is UIKit.UIView platformView)
			{
				if (noUnderline)
				{
					if (platformView.Superview != null)
					{
						platformView.Superview.Layer.BorderWidth = 0; // Removes the underline/border
						platformView.Superview.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
					}
					else
					{
						platformView.Layer.BorderWidth = 0; // Removes the underline/border
						platformView.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
					}
				}
				else
				{
					if (platformView.Superview != null)
					{
						platformView.Superview.Layer.BorderWidth = 1; // Adds the underline/border
						platformView.Superview.Layer.BorderColor = UIKit.UIColor.Black.CGColor; // Example color
					}
					else
					{
						platformView.Layer.BorderWidth = 1; // Adds the underline/border
						platformView.Layer.BorderColor = UIKit.UIColor.Black.CGColor; // Example color
					}
				}
			}
		}
	}
}
