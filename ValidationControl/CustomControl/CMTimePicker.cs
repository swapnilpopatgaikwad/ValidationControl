using System.Windows.Input;

namespace ValidationControl.CustomControl
{
	public partial class CMTimePicker : TimePicker, IDisposable
	{
		#region DatePicker Properties

		public static readonly BindableProperty PlaceHolderProperty =
			BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(CMTimePicker), "Select time");

		public string PlaceHolder
		{
			get { return (string)GetValue(PlaceHolderProperty); }
			set
			{
				SetValue(PlaceHolderProperty, value);
			}
		}

		public static readonly BindableProperty PlaceHolderColorProperty =
			BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(CMTimePicker), Colors.Gray);

		public Color PlaceHolderColor
		{
			get { return (Color)GetValue(PlaceHolderColorProperty); }
			set
			{
				SetValue(PlaceHolderColorProperty, value);
			}
		}

		public static readonly BindableProperty SelectCommandProperty =
			BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(CMTimePicker), null);

		public ICommand SelectCommand
		{
			get { return (ICommand)GetValue(SelectCommandProperty); }
			set { SetValue(SelectCommandProperty, value); }
		}

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CMTimePicker), null);

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly BindableProperty NullableTimeProperty =
		BindableProperty.Create(nameof(NullableTime), typeof(TimeSpan?), typeof(CMTimePicker), null, defaultBindingMode: BindingMode.TwoWay);

		public TimeSpan? NullableTime
		{
			get { return (TimeSpan?)GetValue(NullableTimeProperty); }
			set { SetValue(NullableTimeProperty, value); }
		}

		public static readonly BindableProperty Is24HourViewProperty =
		BindableProperty.Create(nameof(Is24HourView), typeof(bool), typeof(CMTimePicker), false, defaultBindingMode: BindingMode.TwoWay);

		public bool Is24HourView
		{
			get { return (bool)GetValue(Is24HourViewProperty); }
			set { SetValue(Is24HourViewProperty, value); }
		}

		public static readonly BindableProperty NoUnderlineProperty =
		BindableProperty.Create(
			nameof(NoUnderline),
			typeof(bool),
			typeof(CMTimePicker),
			false);

		public bool NoUnderline
		{
			get => (bool)GetValue(NoUnderlineProperty);
			set => SetValue(NoUnderlineProperty, value);
		}

		#endregion

		~CMTimePicker()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
