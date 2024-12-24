using Microsoft.Maui.Handlers;
using ValidationControl.CustomControl;

namespace ValidationControl.Extensions
{
	public static partial class CMTimePickerExtensions
	{
		public static void UpdateUnderline(this ITimePickerHandler handler, CMTimePicker cMTimePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			bool noUnderline = cMTimePicker.NoUnderline;

			if (handler.PlatformView is UIKit.UIView platformView)
			{
				if (platformView.Superview != null)
				{
					platformView.Superview.Layer.BorderWidth = 0; // Remove underline/border
					platformView.Superview.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
				}
				else
				{
					// If needed, you can also target platformView.Layer directly if Superview does not work.
					platformView.Layer.BorderWidth = 0;
					platformView.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
				}
			}
		}
	}
}
