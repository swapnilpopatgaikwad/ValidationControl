using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using ValidationControl.Images;
using Microsoft.Maui.Controls.Platform;

#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#elif IOS || MACCATALYST
using UIKit;
#endif


namespace ValidationControl.Controls
{
	[ContentProperty(nameof(Validations))]
	public partial class MPicker : Grid
	{
		protected readonly Picker _picker;
		protected readonly Border _border;
		protected readonly HorizontalStackLayout errorIconsContainer = new();
		protected Grid _content;

		#region Error Alert Properties

		public static readonly BindableProperty ErrorIconHeightProperty = BindableProperty.Create(
			nameof(ErrorIconHeight), typeof(double), typeof(MPicker), 24.0);

		public double ErrorIconHeight
		{
			get => (double)GetValue(ErrorIconHeightProperty);
			set => SetValue(ErrorIconHeightProperty, value);
		}

		public static readonly BindableProperty ErrorIconWidthProperty = BindableProperty.Create(
			nameof(ErrorIconWidth), typeof(double), typeof(MPicker), 24.0);

		public double ErrorIconWidth
		{
			get => (double)GetValue(ErrorIconWidthProperty);
			set => SetValue(ErrorIconWidthProperty, value);
		}

		public static readonly BindableProperty ErrorIconSourceProperty = BindableProperty.Create(
			nameof(ErrorIconSource), typeof(ImageSource), typeof(MPicker), ImageCollection.ErrorIcon);

		public ImageSource ErrorIconSource
		{
			get => (ImageSource)GetValue(ErrorIconSourceProperty);
			set => SetValue(ErrorIconSourceProperty, value);
		}

		public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(
			nameof(ErrorTextColor), typeof(Color), typeof(MPicker), Colors.Red);

		public Color ErrorTextColor
		{
			get => (Color)GetValue(ErrorTextColorProperty);
			set => SetValue(ErrorTextColorProperty, value);
		}

		public static readonly BindableProperty ErrorFontSizeProperty = BindableProperty.Create(
			nameof(ErrorFontSize), typeof(double), typeof(MPicker), 14.0);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double ErrorFontSize
		{
			get => (double)GetValue(ErrorFontSizeProperty);
			set => SetValue(ErrorFontSizeProperty, value);
		}

		#endregion

		#region Picker Properties

		public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
			nameof(SelectedItem), typeof(object), typeof(MPicker), default(object), BindingMode.TwoWay);

		public object SelectedItem
		{
			get => GetValue(SelectedItemProperty);
			set => SetValue(SelectedItemProperty, value);
		}

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
			nameof(ItemsSource), typeof(IEnumerable), typeof(MPicker), default(IEnumerable));

		public IEnumerable ItemsSource
		{
			get => (IEnumerable)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public static readonly BindableProperty PickerTextColorProperty = BindableProperty.Create(
			nameof(PickerTextColor), typeof(Color), typeof(MPicker), Colors.Gray, BindingMode.TwoWay);

		public Color PickerTextColor
		{
			get => (Color)GetValue(PickerTextColorProperty);
			set => SetValue(PickerTextColorProperty, value);
		}

		public static readonly BindableProperty PickerFontSizeProperty = BindableProperty.Create(
			nameof(PickerFontSize), typeof(double), typeof(MPicker), default(double), BindingMode.TwoWay);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double PickerFontSize
		{
			get => (double)GetValue(PickerFontSizeProperty);
			set => SetValue(PickerFontSizeProperty, value);
		}

		public static readonly BindableProperty NoUnderlineProperty =
		BindableProperty.Create(
			nameof(NoUnderline),
			typeof(bool),
			typeof(MPicker),
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

		public MPicker()
		{
			this.RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition { Height = GridLength.Auto },
				new RowDefinition { Height = GridLength.Auto }
			};

			_picker = new Picker
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center
			};
			_picker.SetBinding(Picker.SelectedItemProperty, new Binding(nameof(SelectedItem), source: this));
			_picker.SetBinding(Picker.ItemsSourceProperty, new Binding(nameof(ItemsSource), source: this));
			_picker.SetBinding(Picker.TextColorProperty, new Binding(nameof(PickerTextColor), source: this));
			_picker.SetBinding(Picker.FontSizeProperty, new Binding(nameof(PickerFontSize), source: this));

			// Subscribe to HandlerChanged to update underline
			_picker.HandlerChanged += OnPickerHandlerChanged;

			_content = new Grid
			{
				ColumnDefinitions = new ColumnDefinitionCollection
				{
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Auto }
				}
			};
			_content.Add(_picker, 0, 0);

			var roundRectangle = new RoundRectangle();
			roundRectangle.SetBinding(RoundRectangle.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

			_border = new Border
			{
				StrokeShape = roundRectangle,
				Content = _content,
				StrokeThickness = 1
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
				_picker.HandlerChanged -= OnPickerHandlerChanged;
			}
		}

		private void OnPickerHandlerChanged(object? sender, EventArgs e)
		{
			if (_picker.Handler is PickerHandler handler)
			{
				UpdateUnderline(handler);
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(NoUnderline))
			{
				if (_picker.Handler is PickerHandler handler)
				{
					UpdateUnderline(handler);
				}
			}
		}

		private void UpdateUnderline(PickerHandler handler)
		{
			if (Handler == null)
				return;

			bool noUnderline = NoUnderline;

#if ANDROID
			if (noUnderline)
			{
				handler.PlatformView.SetBackground(null); // Removes the underline for Android
			}
			else
			{
				// Default underline
			}
#elif IOS || MACCATALYST
			if (noUnderline)
			{
				handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear; // Removes the underline for iOS/Mac Catalyst
				handler.PlatformView.Layer.BorderWidth = 0; // No border
				handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None; // No border style
			}
			else
			{
				handler.PlatformView.BackgroundColor = UIKit.UIColor.White; // Default background color
				handler.PlatformView.Layer.BorderWidth = 1; // Default border width
				handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.Line; // Default border style
			}
#elif WINDOWS
			if (handler.PlatformView is Microsoft.UI.Xaml.Controls.ComboBox platformView)
			{
				if (noUnderline)
				{
					platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0); // Removes the underline for Windows
				}
				else
				{
					platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(1); // Default border thickness
				}
			}
#endif
		}


		#endregion
	}
}
