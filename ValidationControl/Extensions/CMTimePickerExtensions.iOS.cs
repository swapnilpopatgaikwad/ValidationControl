using Foundation;
using System.Globalization;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ValidationControl.CustomControl;

namespace ValidationControl.Extensions
{
	public static partial class CMTimePickerExtensions
	{
		public static void UpdateUnderline(this ITimePickerHandler handler, ITimePicker timePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMTimePicker = timePicker as CMTimePicker;

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

		internal static void SetTimeText(this ITimePickerHandler handler, ITimePicker timePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMTimePicker = timePicker as CMTimePicker;

			if (cMTimePicker.NullableTime.HasValue)
				{
					var cultureInfo = Culture.CurrentCulture;
					handler.PlatformView.Text = cMTimePicker.NullableTime.Value.ToFormattedString(cMTimePicker.Format, cultureInfo);
					handler.SetTimeTextColor(cMTimePicker.TextColor);
				}
				else
				{
					handler.PlatformView.Text = cMTimePicker.PlaceHolder;
					handler.SetTimeTextColor(cMTimePicker.PlaceHolderColor);
				}
		}

		public static void UpdateTime(this ITimePickerHandler handler, ITimePicker timePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMTimePicker = timePicker as CMTimePicker;
			var mauiTimePicker = handler.PlatformView as MauiTimePicker;

			if (cMTimePicker.NullableTime.HasValue)
			{
				mauiTimePicker.Picker.Date = new DateTime(1, 1, 1).Add(cMTimePicker.NullableTime.Value).ToNSDate();
			}
			else
			{
				mauiTimePicker.Picker.Date = new DateTime(1, 1, 1).Add(DateTime.Now.TimeOfDay).ToNSDate();
			}
		}

		internal static void SetTime(this ITimePickerHandler handler, ITimePicker timePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMTimePicker = timePicker as CMTimePicker;
			var mauiTimePicker = handler.PlatformView as MauiTimePicker;

			handler?.UpdateTime(timePicker);
			if (cMTimePicker.NullableTime.HasValue)
			{
				if (handler.VirtualView.Time.CompareTo(cMTimePicker.NullableTime.Value) != 0)
				{
					var cultureInfo = Culture.CurrentCulture;

					if (string.IsNullOrEmpty(timePicker.Format))
					{
						NSLocale locale = new NSLocale(cultureInfo.TwoLetterISOLanguageName);

						if (mauiTimePicker.Picker != null)
							mauiTimePicker.Picker.Locale = locale;
					}

					var time = cMTimePicker.NullableTime.Value;
					var format = timePicker.Format;

					mauiTimePicker.Text = time.ToFormattedString(format, cultureInfo);

					if (format != null)
					{
						if (format.Contains('H', StringComparison.Ordinal))
						{
							var ci = new CultureInfo("de-DE");
							NSLocale locale = new NSLocale(ci.TwoLetterISOLanguageName);

							if (mauiTimePicker.Picker != null)
								mauiTimePicker.Picker.Locale = locale;
						}
						else if (format.Contains('h', StringComparison.Ordinal))
						{
							var ci = new CultureInfo("en-US");
							NSLocale locale = new NSLocale(ci.TwoLetterISOLanguageName);

							if (mauiTimePicker.Picker != null)
								mauiTimePicker.Picker.Locale = locale;
						}
					}

					mauiTimePicker.UpdateCharacterSpacing(timePicker);
				}
			}
		}

		internal static void SetTimeTextColor(this ITimePickerHandler handler, Color? color)
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
