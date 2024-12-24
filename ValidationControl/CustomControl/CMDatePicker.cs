using System.Windows.Input;

namespace ValidationControl.CustomControl
{
	public partial class CMDatePicker : DatePicker, IDisposable
	{
		#region DatePicker Properties

		public static readonly BindableProperty PlaceHolderProperty =
			BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(CMDatePicker), "Select date");

		public string PlaceHolder
		{
			get { return (string)GetValue(PlaceHolderProperty); }
			set
			{
				SetValue(PlaceHolderProperty, value);
			}
		}

		public static readonly BindableProperty PlaceHolderColorProperty =
			BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(CMDatePicker), Colors.Gray);

		public Color PlaceHolderColor
		{
			get { return (Color)GetValue(PlaceHolderColorProperty); }
			set
			{
				SetValue(PlaceHolderColorProperty, value);
			}
		}

		public static readonly BindableProperty SelectCommandProperty =
			BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(CMDatePicker), null);

		public ICommand SelectCommand
		{
			get { return (ICommand)GetValue(SelectCommandProperty); }
			set { SetValue(SelectCommandProperty, value); }
		}

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CMDatePicker), null);

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly BindableProperty NullableDateProperty =
		BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(CMDatePicker), null, defaultBindingMode: BindingMode.TwoWay);

		public DateTime? NullableDate
		{
			get { return (DateTime?)GetValue(NullableDateProperty); }
			set { SetValue(NullableDateProperty, value); }
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

		public CMDatePicker()
		{
			Format = "d";
		}

		~CMDatePicker()
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
