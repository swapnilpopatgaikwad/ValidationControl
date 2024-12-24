using Android.App;
using Android.Content;
using Android.Content.Res;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ValidationControl.CustomControl;
using ValidationControl.Extensions;

namespace ValidationControl.Handler
{
	public partial class CMTimePickerHandler
	{
		TimePickerDialog? _dialog;
		protected override void ConnectHandler(MauiTimePicker platformView)
		{
			base.ConnectHandler(platformView);

			Mapper.Add(nameof(CMTimePicker.NullableTime), MapNullableTime);
			Mapper.ReplaceMapping<CMTimePicker, CMTimePickerHandler>(nameof(CMTimePicker.Time), MapTime);
			Mapper.ReplaceMapping<CMTimePicker, CMTimePickerHandler>(nameof(CMTimePicker.Format), MapFormat);
			Mapper.AppendToMapping<CMTimePicker, CMTimePickerHandler>(nameof(CMTimePicker.NoUnderline), MapNoUnderline);

			var control = (CMTimePicker)VirtualView;

			platformView.Focusable = false;

			control.BatchBegin();
			this.SetTimeText(VirtualView);
			this.SetTime(VirtualView);
			control.BatchCommit();
		}

		private void MapNullableTime(ITimePickerHandler handler, ITimePicker picker)
		{
			handler?.SetTimeText(picker);
			handler?.SetTime(picker);
		}

		public static new void MapTime(ITimePickerHandler handler, ITimePicker picker) { }

		public static new void MapFormat(ITimePickerHandler handler, ITimePicker picker)
		{
			handler?.SetTimeText(picker);
		}

		protected override TimePickerDialog CreateTimePickerDialog(int hourOfDay, int minute)
		{
			_dialog = new TimePickerDialog(Context, callBack: OnTimeSelected, hourOfDay, minute, Use24HourView);

			_dialog.SetCanceledOnTouchOutside(true);
			_dialog.SetButton((int)DialogButtonType.Negative, "Cancel", (sender, e) =>
			{
				PlatformView?.ClearFocus();
			});
			return _dialog;
		}

		private void OnTimeSelected(object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			if (VirtualView == null || PlatformView == null)
				return;

			CMTimePicker view = (CMTimePicker)VirtualView;
			view.NullableTime = new TimeSpan(e.HourOfDay, e.Minute, 0);

			if (view.SelectCommand?.CanExecute(view.CommandParameter) == true)
				view.SelectCommand.Execute(view.CommandParameter);

			((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
			PlatformView.ClearFocus();

			_dialog = null;
		}

		public static new void MapNoUnderline(ITimePickerHandler handler, ITimePicker picker)
		{
			handler?.UpdateUnderline(picker);
		}

		bool Use24HourView => VirtualView != null && ((CMTimePicker)VirtualView).Is24HourView;



		
	}
}
