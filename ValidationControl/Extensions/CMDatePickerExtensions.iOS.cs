using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using ValidationControl.CustomControl;

namespace ValidationControl.Extensions
{
	public static partial class CMDatePickerExtensions
	{

		public static void UpdateUnderline(this IDatePickerHandler handler, IDatePicker datePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMDatePicker = datePicker as CMDatePicker;

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

		internal static void SetDateText(this IDatePickerHandler handler, IDatePicker datePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMDatePicker = datePicker as CMDatePicker;

			if (cMDatePicker.NullableDate.HasValue)
			{
				handler.PlatformView.Text = cMDatePicker.NullableDate.Value.ToString(cMDatePicker.Format);
				handler.SetDateTextColor(cMDatePicker.TextColor);
			}
			else
			{
				handler.PlatformView.Text = cMDatePicker.PlaceHolder;
				handler.SetDateTextColor(cMDatePicker.PlaceHolderColor);
			}
		}

		internal static void SetDate(this IDatePickerHandler handler, IDatePicker datePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMDatePicker = datePicker as CMDatePicker;

			if (cMDatePicker.NullableDate.HasValue)
			{
				if (handler.VirtualView.Date.Date.CompareTo(cMDatePicker.NullableDate.Value.Date) != 0)
				{
					handler.VirtualView.Date = cMDatePicker.NullableDate.Value;
				}
			}
			else if (handler.VirtualView.Date.Date.CompareTo(DateTime.Today.Date) != 0)
			{
				handler.VirtualView.Date = DateTime.Today;
			}
		}

		internal static void SetDateTextColor(this IDatePickerHandler handler, Color? color)
		{
			if (color == null)
			{
				handler.PlatformView.TextColor = Colors.Black.ToPlatform();
			}
			else
			{
				handler.PlatformView.TextColor = color.ToPlatform();
			}
		}
	}
}
