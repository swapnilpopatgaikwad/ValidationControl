using Android.App;
using Android.Content;
using Android.Content.Res;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ValidationControl.CustomControl;
using ValidationControl.Extensions;

namespace ValidationControl.Handler
{
	public partial class CMDatePickerHandler
	{
		DatePickerDialog? _dialog;
		protected override void ConnectHandler(MauiDatePicker platformView)
		{
			base.ConnectHandler(platformView);

			Mapper.Add(nameof(CMDatePicker.NullableDate), MapNullableDate);
			Mapper.ReplaceMapping<CMDatePicker, CMDatePickerHandler>(nameof(CMDatePicker.Date), MapDate);
			Mapper.ReplaceMapping<CMDatePicker, CMDatePickerHandler>(nameof(CMDatePicker.Format), MapFormat);
			Mapper.AppendToMapping<CMDatePicker, CMDatePickerHandler>(nameof(CMDatePicker.NoUnderline), MapNoUnderline);

			var control = (CMDatePicker)VirtualView;

			platformView.Focusable = false;

			control.BatchBegin();
			this.SetDateText(VirtualView);
			this.SetDate(VirtualView);
			control.BatchCommit();
		}

		private void MapNullableDate(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler?.SetDateText(datePicker);
			handler?.SetDate(datePicker);
		}

		public static new void MapDate(IDatePickerHandler handler, IDatePicker datePicker) { }

		public static new void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler?.SetDateText(datePicker);
		}

		public static new void MapNoUnderline(IDatePickerHandler handler, IDatePicker picker)
		{
			handler?.UpdateUnderline(picker);
		}

		protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
		{
			CMDatePicker view = (CMDatePicker)VirtualView;

			_dialog = new DatePickerDialog(Context, callBack: OnDateSelected, year, month, day);

			_dialog.SetCanceledOnTouchOutside(true);

			SetMinimumDate();
			SetMaximumDate();

			_dialog.SetButton("Ok", (sender, e) =>
			{
				view.NullableDate = _dialog.DatePicker.DateTime;

				if (view.SelectCommand?.CanExecute(view.CommandParameter) == true)
					view.SelectCommand.Execute(view.CommandParameter);
			});

			_dialog.SetButton((int)DialogButtonType.Negative, "Cancel", (sender, e) =>
			{
				PlatformView?.ClearFocus();
			});

			return _dialog;
		}

		private void OnDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
		{
			CMDatePicker view = (CMDatePicker)VirtualView;
			view.NullableDate = e.Date;

			if (view.SelectCommand?.CanExecute(view.CommandParameter) == true)
				view.SelectCommand.Execute(view.CommandParameter);

			((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
			PlatformView.ClearFocus();

			_dialog = null;
		}

		private void SetMinimumDate()
		{
			if (_dialog != null)
			{
				var control = (CMDatePicker)VirtualView;
				_dialog.DatePicker.MinDate = (long)control.MinimumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
			}
		}

		private void SetMaximumDate()
		{
			if (_dialog != null)
			{
				var control = (CMDatePicker)VirtualView;
				_dialog.DatePicker.MaxDate = (long)control.MaximumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
			}
		}
	}
}
