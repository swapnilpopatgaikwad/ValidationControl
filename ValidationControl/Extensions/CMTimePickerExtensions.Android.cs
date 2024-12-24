using Android.Content.Res;
using Microsoft.Maui.Controls.Platform;
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

			if (noUnderline)
			{
				handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
				handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
			}
			else
			{
				handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.DarkGray.ToPlatform());
			}
		}

		internal static void SetTimeText(this ITimePickerHandler handler, ITimePicker timePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMTimePicker = timePicker as CMTimePicker;

			if (cMTimePicker.NullableTime.HasValue)
			{
				handler.PlatformView.Text = cMTimePicker.NullableTime.Value.ToFormattedString(timePicker.Format);
				handler.SetTimeTextColor(cMTimePicker.TextColor);
			}
			else
			{
				handler.PlatformView.Text = cMTimePicker.PlaceHolder;
				handler.SetTimeTextColor(cMTimePicker.PlaceHolderColor);
			}
		}

		internal static void SetTime(this ITimePickerHandler handler, ITimePicker timePicker)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			var cMTimePicker = timePicker as CMTimePicker;

			if (cMTimePicker.NullableTime.HasValue)
			{
				if (handler.VirtualView.Time.CompareTo(cMTimePicker.NullableTime.Value) != 0)
				{
					handler.VirtualView.Time = cMTimePicker.NullableTime.Value;
				}
			}
			else if (handler.VirtualView.Time.CompareTo(DateTime.Now.TimeOfDay) != 0)
			{
				handler.VirtualView.Time = DateTime.Now.TimeOfDay;
			}
		}

		internal static void SetTimeTextColor(this ITimePickerHandler handler, Color? color)
		{
			if (color != null)
			{
				handler.PlatformView.SetTextColor(ColorStateList.ValueOf(color.ToPlatform()));
			}
		}
	}
}
