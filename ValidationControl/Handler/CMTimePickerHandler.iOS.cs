using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ValidationControl.CustomControl;
using ValidationControl.Extensions;

namespace ValidationControl.Handler
{
	public partial class CMTimePickerHandler
	{
		readonly MauiTimePickerProxy _proxy = new();

		protected override MauiTimePicker CreatePlatformView()
		{
			return new MauiTimePicker(_proxy.OnDateSelected);
		}

		internal bool UpdateImmediately { get; set; }

		protected override void ConnectHandler(MauiTimePicker platformView)
		{
			_proxy.Connect(this, VirtualView, platformView);
			platformView.UpdateTime(VirtualView.Time);

			Mapper.Add(nameof(CMTimePicker.NullableTime), MapNullableTime);
			Mapper.ReplaceMapping<CMTimePicker, CMTimePickerHandler>(nameof(CMTimePicker.Time), MapTime);
			Mapper.ReplaceMapping<CMTimePicker, CMTimePickerHandler>(nameof(CMTimePicker.Format), MapFormat);
			Mapper.AppendToMapping<CMTimePicker, CMTimePickerHandler>(nameof(CMTimePicker.NoUnderline), MapNoUnderline);

			//Remove Picker borders
			platformView.BackgroundColor = UIKit.UIColor.Clear;
			platformView.Layer.BorderWidth = 0;
			platformView.BorderStyle = UIKit.UITextBorderStyle.None;
		}

		protected override void DisconnectHandler(MauiTimePicker platformView)
		{
			base.DisconnectHandler(platformView);

			_proxy.Disconnect(platformView);
		}

		private void MapNullableTime(ITimePickerHandler handler, ITimePicker timePicker)
		{
			handler?.SetTimeText(timePicker);
			handler?.SetTime(timePicker);
		}

		public static new void MapTime(ITimePickerHandler handler, ITimePicker timePicker)
		{
		}

		public static new void MapFormat(ITimePickerHandler handler, ITimePicker timePicker)
		{
			handler?.SetTimeText(timePicker);
		}

		public static new void MapNoUnderline(ITimePickerHandler handler, ITimePicker picker)
		{
			handler?.UpdateUnderline(picker);
		}

		void SetVirtualViewTime()
		{
			if (VirtualView == null || PlatformView == null)
				return;

			var datetime = PlatformView.Date.ToDateTime();

			var control = (CMTimePicker)VirtualView;
			control.Time = new TimeSpan(datetime.Hour, datetime.Minute, 0);
			control.SelectCommand?.Execute(control.Time);
			control.NullableTime = control.Time;
		}

		class MauiTimePickerProxy
		{
			WeakReference<CMTimePickerHandler>? _handler;
			WeakReference<ITimePicker>? _virtualView;

			ITimePicker? VirtualView => _virtualView is not null && _virtualView.TryGetTarget(out var v) ? v : null;

			public void Connect(CMTimePickerHandler handler, ITimePicker virtualView, MauiTimePicker platformView)
			{
				_handler = new(handler);
				_virtualView = new(virtualView);

				platformView.EditingDidBegin += OnStarted;
				platformView.EditingDidEnd += OnEnded;
				platformView.ValueChanged += OnValueChanged;
				platformView.Picker.ValueChanged += OnValueChanged;
			}

			public void Disconnect(MauiTimePicker platformView)
			{
				_virtualView = null;

				platformView.EditingDidBegin -= OnStarted;
				platformView.EditingDidEnd -= OnEnded;
				platformView.ValueChanged -= OnValueChanged;
				platformView.Picker.ValueChanged -= OnValueChanged;
				platformView.RemoveFromSuperview();
			}

			void OnStarted(object? sender, EventArgs eventArgs)
			{
				if (VirtualView is not null)
					VirtualView.IsFocused = true;
			}

			void OnEnded(object? sender, EventArgs eventArgs)
			{
				if (VirtualView is not null)
					VirtualView.IsFocused = false;
			}

			void OnValueChanged(object? sender, EventArgs e)
			{
				if (_handler is not null && _handler.TryGetTarget(out var handler) && handler.UpdateImmediately)  // Platform Specific
					handler.SetVirtualViewTime();
			}

			public void OnDateSelected()
			{
				if (_handler is not null && _handler.TryGetTarget(out var handler))
				{
					handler.SetVirtualViewTime();
					handler.PlatformView?.ResignFirstResponder();
				}
			}
		}
	}
}
