using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using ValidationControl.CustomControl;
using ValidationControl.Extensions;

namespace ValidationControl.Handler
{
	public partial class CMDatePickerHandler
	{
		//public UIDatePicker? DatePickerDialog { get { return PlatformView?.InputView as UIDatePicker; } }
		protected override void ConnectHandler(MauiDatePicker platformView)
		{
			base.ConnectHandler(platformView);
			Mapper.Add(nameof(CMDatePicker.NullableDate), MapNullableDate);
			Mapper.ReplaceMapping<CMDatePicker, CMDatePickerHandler>(nameof(CMDatePicker.Date), MapDate);
			Mapper.ReplaceMapping<CMDatePicker, CMDatePickerHandler>(nameof(CMDatePicker.Format), MapFormat);
			Mapper.AppendToMapping<CMDatePicker, CMDatePickerHandler>(nameof(CMDatePicker.NoUnderline), MapNoUnderline);


			platformView.InputAccessoryView = CreateToolbar(platformView);
		}

		private UIToolbar CreateToolbar(MauiDatePicker platformView)
		{
			var toolbar = new UIToolbar(new CoreGraphics.CGRect(0, 0, platformView.Frame.Width, 44));
			var flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (s, e) =>
			{
				if (VirtualView is CMDatePicker customPicker)
				{
					SetVirtualViewTime();
				}
				platformView.ResignFirstResponder();
			});
			var cancelButton = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (s, e) =>
			{
				platformView.ResignFirstResponder();
			});

			toolbar.SetItems(new[] { flexibleSpace, cancelButton, doneButton }, false);
			return toolbar;
		}

		void SetVirtualViewTime()
		{
			if (VirtualView == null || PlatformView == null)
				return;
			var control = (CMDatePicker)VirtualView;
			control.SelectCommand?.Execute(control.Date);
			control.NullableDate = control.Date;
		}

		private void MapNullableDate(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler?.SetDateText(datePicker);
			handler?.SetDate(datePicker);
		}

		public static new void MapDate(IDatePickerHandler handler, IDatePicker datePicker)
		{

		}

		public static new void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler?.SetDateText(datePicker);
		}

		public static new void MapNoUnderline(IDatePickerHandler handler, IDatePicker picker)
		{
			handler?.UpdateUnderline(picker);
		}
	}
}
