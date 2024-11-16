using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using ValidationControl.Images;

namespace ValidationControl.Controls
{
	[ContentProperty(nameof(Validations))]
	public partial class MDatePicker : Grid
	{
		protected readonly DatePicker _datePicker;
		protected readonly Border _border;
		protected readonly HorizontalStackLayout errorIconsContainer = new();
		protected Grid _content;

		#region Error Alert Properties

		public static readonly BindableProperty ErrorIconHeightProperty = BindableProperty.Create(
			nameof(ErrorIconHeight), typeof(double), typeof(MDatePicker), 24.0);

		public double ErrorIconHeight
		{
			get => (double)GetValue(ErrorIconHeightProperty);
			set => SetValue(ErrorIconHeightProperty, value);
		}

		public static readonly BindableProperty ErrorIconWidthProperty = BindableProperty.Create(
			nameof(ErrorIconWidth), typeof(double), typeof(MDatePicker), 24.0);

		public double ErrorIconWidth
		{
			get => (double)GetValue(ErrorIconWidthProperty);
			set => SetValue(ErrorIconWidthProperty, value);
		}

		public static readonly BindableProperty ErrorIconSourceProperty = BindableProperty.Create(
			nameof(ErrorIconSource), typeof(ImageSource), typeof(MDatePicker), ImageCollection.ErrorIcon);

		public ImageSource ErrorIconSource
		{
			get => (ImageSource)GetValue(ErrorIconSourceProperty);
			set => SetValue(ErrorIconSourceProperty, value);
		}

		public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(
			nameof(ErrorTextColor), typeof(Color), typeof(MDatePicker), Colors.Red);

		public Color ErrorTextColor
		{
			get => (Color)GetValue(ErrorTextColorProperty);
			set => SetValue(ErrorTextColorProperty, value);
		}

		public static readonly BindableProperty ErrorFontSizeProperty = BindableProperty.Create(
			nameof(ErrorFontSize), typeof(double), typeof(MDatePicker), 14.0);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double ErrorFontSize
		{
			get => (double)GetValue(ErrorFontSizeProperty);
			set => SetValue(ErrorFontSizeProperty, value);
		}

		#endregion

		#region DatePicker Properties

		public static readonly BindableProperty DateProperty = BindableProperty.Create(
			nameof(Date), typeof(DateTime), typeof(MDatePicker), DateTime.Now, BindingMode.TwoWay);

		public DateTime Date
		{
			get => (DateTime)GetValue(DateProperty);
			set => SetValue(DateProperty, value);
		}

		public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
			nameof(MinimumDate), typeof(DateTime), typeof(MDatePicker), DateTime.MinValue);

		public DateTime MinimumDate
		{
			get => (DateTime)GetValue(MinimumDateProperty);
			set => SetValue(MinimumDateProperty, value);
		}

		public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
			nameof(MaximumDate), typeof(DateTime), typeof(MDatePicker), DateTime.MaxValue);

		public DateTime MaximumDate
		{
			get => (DateTime)GetValue(MaximumDateProperty);
			set => SetValue(MaximumDateProperty, value);
		}

		public static readonly BindableProperty DateTextColorProperty = BindableProperty.Create(
			nameof(DateTextColor), typeof(Color), typeof(MDatePicker), Colors.Gray, BindingMode.TwoWay);

		public Color DateTextColor
		{
			get => (Color)GetValue(DateTextColorProperty);
			set => SetValue(DateTextColorProperty, value);
		}

		public static readonly BindableProperty DateFontSizeProperty = BindableProperty.Create(
			nameof(DateFontSize), typeof(double), typeof(MDatePicker), default(double), BindingMode.TwoWay);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double DateFontSize
		{
			get => (double)GetValue(DateFontSizeProperty);
			set => SetValue(DateFontSizeProperty, value);
		}

		public static readonly BindableProperty NoUnderlineProperty =
		BindableProperty.Create(
			nameof(NoUnderline),
			typeof(bool),
			typeof(MDatePicker),
			false);

		public bool NoUnderline
		{
			get => (bool)GetValue(NoUnderlineProperty);
			set => SetValue(NoUnderlineProperty, value);
		}

		#endregion

		#region Border Properties

		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(RoundRectangle), new CornerRadius());

		public CornerRadius CornerRadius
		{
			set { SetValue(CornerRadiusProperty, value); }
			get { return (CornerRadius)GetValue(CornerRadiusProperty); }
		}

		public static readonly BindableProperty StrokeProperty =
			BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(Border), null);

		public Brush? Stroke
		{
			set { SetValue(StrokeProperty, value); }
			get { return (Brush?)GetValue(StrokeProperty); }
		}

		public double StrokeThickness
		{
			set { SetValue(StrokeThicknessProperty, value); }
			get { return (double)GetValue(StrokeThicknessProperty); }
		}

		public static readonly BindableProperty StrokeThicknessProperty =
			BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(Border), 1.0);

		#endregion

		public MDatePicker()
		{
			this.RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition { Height = GridLength.Auto },
				new RowDefinition { Height = GridLength.Auto }
			};

			_datePicker = new DatePicker
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
			};
			_datePicker.SetBinding(DatePicker.DateProperty, new Binding(nameof(Date), source: this));
			_datePicker.SetBinding(DatePicker.TextColorProperty, new Binding(nameof(DateTextColor), source: this));
			_datePicker.SetBinding(DatePicker.FontSizeProperty, new Binding(nameof(DateFontSize), source: this));
			_datePicker.SetBinding(DatePicker.MinimumDateProperty, new Binding(nameof(MinimumDate), source: this));
			_datePicker.SetBinding(DatePicker.MaximumDateProperty, new Binding(nameof(MaximumDate), source: this));

			// Subscribe to HandlerChanged to update underline
			_datePicker.HandlerChanged += OnDatePickerHandlerChanged;

			_content = new Grid
			{
				ColumnDefinitions = new ColumnDefinitionCollection
				{
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Auto }
				}
			};
			_content.Add(_datePicker, 0, 0);

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

		protected override void OnHandlerChanging(HandlerChangingEventArgs args)
		{
			base.OnHandlerChanging(args);

			if (args.OldHandler != null)
			{
				// Unsubscribe from events
				_datePicker.HandlerChanged -= OnDatePickerHandlerChanged;
			}
		}

		private void OnDatePickerHandlerChanged(object? sender, EventArgs e)
		{
			if (_datePicker.Handler is DatePickerHandler handler)
			{
				UpdateUnderline(handler);
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(NoUnderline))
			{
				if (_datePicker.Handler is DatePickerHandler handler)
				{
					UpdateUnderline(handler);
				}
			}
		}

		private void UpdateUnderline(DatePickerHandler handler)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			bool noUnderline = NoUnderline; // Check if underline should be removed

#if ANDROID
			if (handler.PlatformView is Android.Widget.EditText platformView)
			{
				if (noUnderline)
				{
					platformView.Background = null; // Removes the underline
				}
				else
				{
				  // Apply default underline
				}
			}
#elif IOS || MACCATALYST
			if (handler.PlatformView is UIKit.UIView platformView)
			{
				if (noUnderline)
				{
					if (platformView.Superview != null)
					{
						platformView.Superview.Layer.BorderWidth = 0; // Removes the underline/border
						platformView.Superview.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
					}
					else
					{
						platformView.Layer.BorderWidth = 0; // Removes the underline/border
						platformView.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
					}
				}
				else
				{
					if (platformView.Superview != null)
					{
						platformView.Superview.Layer.BorderWidth = 1; // Adds the underline/border
						platformView.Superview.Layer.BorderColor = UIKit.UIColor.Black.CGColor; // Example color
					}
					else
					{
						platformView.Layer.BorderWidth = 1; // Adds the underline/border
						platformView.Layer.BorderColor = UIKit.UIColor.Black.CGColor; // Example color
					}
				}
			}
#elif WINDOWS
			if (handler.PlatformView is Microsoft.UI.Xaml.Controls.CalendarDatePicker platformView)
			{
				if (noUnderline)
				{
					platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0); // Removes the underline
				}
				else
				{
					platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(1); // Adds the underline
				}
			}
#endif
		}

		#endregion
	}
}
