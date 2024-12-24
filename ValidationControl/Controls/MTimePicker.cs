using System.Windows.Input;
using Microsoft.Maui.Controls.Shapes;
using ValidationControl.CustomControl;
using ValidationControl.Images;

namespace ValidationControl.Controls
{

	[ContentProperty(nameof(Validations))]
	public partial class MTimePicker : Grid
	{
		protected readonly CMTimePicker _timePicker;
		protected readonly Border _border;
		protected readonly HorizontalStackLayout errorIconsContainer = new();
		protected Grid _content;

		#region Error Alert Properties

		public static readonly BindableProperty ErrorIconHeightProperty = BindableProperty.Create(
			nameof(ErrorIconHeight), typeof(double), typeof(MTimePicker), 24.0);

		public double ErrorIconHeight
		{
			get => (double)GetValue(ErrorIconHeightProperty);
			set => SetValue(ErrorIconHeightProperty, value);
		}

		public static readonly BindableProperty ErrorIconWidthProperty = BindableProperty.Create(
			nameof(ErrorIconWidth), typeof(double), typeof(MTimePicker), 24.0);

		public double ErrorIconWidth
		{
			get => (double)GetValue(ErrorIconWidthProperty);
			set => SetValue(ErrorIconWidthProperty, value);
		}

		public static readonly BindableProperty ErrorIconSourceProperty = BindableProperty.Create(
			nameof(ErrorIconSource), typeof(ImageSource), typeof(MTimePicker), ImageCollection.ErrorIcon);

		public ImageSource ErrorIconSource
		{
			get => (ImageSource)GetValue(ErrorIconSourceProperty);
			set => SetValue(ErrorIconSourceProperty, value);
		}

		public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(
			nameof(ErrorTextColor), typeof(Color), typeof(MTimePicker), Colors.Red);

		public Color ErrorTextColor
		{
			get => (Color)GetValue(ErrorTextColorProperty);
			set => SetValue(ErrorTextColorProperty, value);
		}

		public static readonly BindableProperty ErrorFontSizeProperty = BindableProperty.Create(
			nameof(ErrorFontSize), typeof(double), typeof(MTimePicker), 14.0);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double ErrorFontSize
		{
			get => (double)GetValue(ErrorFontSizeProperty);
			set => SetValue(ErrorFontSizeProperty, value);
		}

		#endregion

		#region TimePicker Properties

		public static readonly BindableProperty TimeProperty = BindableProperty.Create(
			nameof(Time), typeof(TimeSpan), typeof(MTimePicker), TimeSpan.Zero, BindingMode.TwoWay);

		public TimeSpan Time
		{
			get => (TimeSpan)GetValue(TimeProperty);
			set => SetValue(TimeProperty, value);
		}

		public static readonly BindableProperty TimeTextColorProperty = BindableProperty.Create(
			nameof(TimeTextColor), typeof(Color), typeof(MTimePicker), Colors.Gray);

		public Color TimeTextColor
		{
			get => (Color)GetValue(TimeTextColorProperty);
			set => SetValue(TimeTextColorProperty, value);
		}

		public static readonly BindableProperty TimeFontSizeProperty = BindableProperty.Create(
			nameof(TimeFontSize), typeof(double), typeof(MTimePicker), 14.0);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double TimeFontSize
		{
			get => (double)GetValue(TimeFontSizeProperty);
			set => SetValue(TimeFontSizeProperty, value);
		}

		public static readonly BindableProperty NoUnderlineProperty =
		BindableProperty.Create(
			nameof(NoUnderline),
			typeof(bool),
			typeof(MTimePicker),
			true);

		public bool NoUnderline
		{
			get => (bool)GetValue(NoUnderlineProperty);
			set => SetValue(NoUnderlineProperty, value);
		}

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

		#endregion

		#region Border Properties

		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MTimePicker), new CornerRadius());

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		public static readonly BindableProperty StrokeProperty =
			BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(Border), null);

		public Brush? Stroke
		{
			get => (Brush?)GetValue(StrokeProperty);
			set => SetValue(StrokeProperty, value);
		}

		public static readonly BindableProperty StrokeThicknessProperty =
			BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(Border), 1.0);

		public double StrokeThickness
		{
			get => (double)GetValue(StrokeThicknessProperty);
			set => SetValue(StrokeThicknessProperty, value);
		}

		#endregion

		public MTimePicker()
		{
			this.RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition { Height = GridLength.Auto },
				new RowDefinition { Height = GridLength.Auto }
			};

			_timePicker = new CMTimePicker
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(5,0,0,0)
				
			};

			_timePicker.SetBinding(CMTimePicker.TimeProperty, new Binding(nameof(Time), source: this));
			_timePicker.SetBinding(CMTimePicker.TextColorProperty, new Binding(nameof(TimeTextColor), source: this));
			_timePicker.SetBinding(CMTimePicker.FontSizeProperty, new Binding(nameof(TimeFontSize), source: this));
			_timePicker.SetBinding(CMTimePicker.PlaceHolderProperty, new Binding(nameof(PlaceHolder), source: this));
			_timePicker.SetBinding(CMTimePicker.PlaceHolderColorProperty, new Binding(nameof(PlaceHolderColor), source: this));
			_timePicker.SetBinding(CMTimePicker.SelectCommandProperty, new Binding(nameof(SelectCommand), source: this));
			_timePicker.SetBinding(CMTimePicker.CommandParameterProperty, new Binding(nameof(CommandParameter), source: this));
			_timePicker.SetBinding(CMTimePicker.NullableTimeProperty, new Binding(nameof(NullableTime), source: this));
			_timePicker.SetBinding(CMTimePicker.Is24HourViewProperty, new Binding(nameof(Is24HourView), source: this));
			_timePicker.SetBinding(CMTimePicker.NoUnderlineProperty, new Binding(nameof(NoUnderline), source: this));

			_content = new Grid
			{
				ColumnDefinitions = new ColumnDefinitionCollection
				{
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Auto }
				}
			};
			_content.Add(_timePicker, 0, 0);

			var roundRectangle = new RoundRectangle();
			roundRectangle.SetBinding(RoundRectangle.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

			_border = new Border
			{
				StrokeShape = roundRectangle,
				Content = _content,
			};
			_border.SetBinding(Border.StrokeProperty, new Binding(nameof(Stroke), source: this));
			_border.SetBinding(Border.StrokeThicknessProperty, new Binding(nameof(StrokeThickness), source: this));

			this.Add(_border, 0, 0);


			iconValidation.Value.SetBinding(Image.HeightRequestProperty, new Binding(nameof(ErrorIconHeight), source: this));
			iconValidation.Value.SetBinding(Image.WidthRequestProperty, new Binding(nameof(ErrorIconWidth), source: this));
			iconValidation.Value.SetBinding(Image.SourceProperty, new Binding(nameof(ErrorIconSource), source: this));

			// Update error label properties
			labelValidation.Value.SetBinding(Label.TextColorProperty, new Binding(nameof(ErrorTextColor), source: this));
			labelValidation.Value.SetBinding(Label.FontSizeProperty, new Binding(nameof(ErrorFontSize), source: this));
		}

		#region Events

		#endregion
	}
}
