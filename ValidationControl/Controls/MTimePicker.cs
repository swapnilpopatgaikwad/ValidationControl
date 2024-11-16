using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using ValidationControl.Images;

namespace ValidationControl.Controls
{

	[ContentProperty(nameof(Validations))]
	public partial class MTimePicker : Grid
	{
		protected readonly TimePicker _timePicker;
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
			false);

		public bool NoUnderline
		{
			get => (bool)GetValue(NoUnderlineProperty);
			set => SetValue(NoUnderlineProperty, value);
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

			_timePicker = new TimePicker
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center
			};

			_timePicker.SetBinding(TimePicker.TimeProperty, new Binding(nameof(Time), source: this));
			_timePicker.SetBinding(TimePicker.TextColorProperty, new Binding(nameof(TimeTextColor), source: this));
			_timePicker.SetBinding(TimePicker.FontSizeProperty, new Binding(nameof(TimeFontSize), source: this));

			// Subscribe to HandlerChanged to update underline
			_timePicker.HandlerChanged += OnTimePickerHandlerChanged;

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

		protected override void OnHandlerChanging(HandlerChangingEventArgs args)
		{
			base.OnHandlerChanging(args);

			if (args.OldHandler != null)
			{
				// Unsubscribe from events
				_timePicker.HandlerChanged -= OnTimePickerHandlerChanged;
			}
		}

		private void OnTimePickerHandlerChanged(object? sender, EventArgs e)
		{
			if (_timePicker.Handler is TimePickerHandler handler)
			{
				UpdateUnderline(handler);
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(NoUnderline))
			{
				if (_timePicker.Handler is TimePickerHandler handler)
				{
					UpdateUnderline(handler);
				}
			}
		}

		private void UpdateUnderline(TimePickerHandler handler)
		{
			if (handler == null || handler.PlatformView == null)
				return;

			bool noUnderline = NoUnderline;

#if ANDROID
			if (noUnderline)
			{
				handler.PlatformView.SetBackground(null); // Removes underline
			}
			else
			{
				handler.PlatformView.SetBackgroundResource(Resource.Drawable.abc_edit_text_material); // Ensure you have a valid resource for underline
			}
#elif IOS || MACCATALYST
			if (handler.PlatformView is UIKit.UIView platformView)
			{
				if (platformView.Superview != null)
				{
					platformView.Superview.Layer.BorderWidth = 0; // Remove underline/border
					platformView.Superview.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
				}
				else
				{
					// If needed, you can also target platformView.Layer directly if Superview does not work.
					platformView.Layer.BorderWidth = 0;
					platformView.Layer.BorderColor = UIKit.UIColor.Clear.CGColor;
				}
			}
#elif WINDOWS
			if (handler.PlatformView is Microsoft.UI.Xaml.Controls.TimePicker platformView)
			{
				platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0); // Removes the underline
			}
#endif
		}

		#endregion
	}
}
