using Android.Content.Res;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
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

			if (handler.PlatformView is Android.Widget.EditText platformView)
			{
				if (noUnderline)
				{
					platformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
					platformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
				}
				else
				{
					platformView.BackgroundTintList = ColorStateList.ValueOf(Colors.DarkGray.ToPlatform());
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
			if (color != null)
			{
				handler.PlatformView.SetTextColor(ColorStateList.ValueOf(color.ToPlatform()));
			}
		}
	}
}
