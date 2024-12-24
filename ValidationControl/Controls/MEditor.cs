using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using ValidationControl.Images;

namespace ValidationControl.Controls
{
	[ContentProperty(nameof(Validations))]
	public partial class MEditor : Grid
	{
		protected readonly Editor _editor;
		protected readonly Border _border;
		protected readonly HorizontalStackLayout errorIconsContainer = new();
		protected Grid _content;

		#region Error Alert Properties

		public static readonly BindableProperty ErrorIconHeightProperty = BindableProperty.Create(
		nameof(ErrorIconHeight), typeof(double), typeof(MEditor), 24.0);

		public double ErrorIconHeight
		{
			get => (double)GetValue(ErrorIconHeightProperty);
			set => SetValue(ErrorIconHeightProperty, value);
		}

		public static readonly BindableProperty ErrorIconWidthProperty = BindableProperty.Create(
			nameof(ErrorIconWidth), typeof(double), typeof(MEditor), 24.0);

		public double ErrorIconWidth
		{
			get => (double)GetValue(ErrorIconWidthProperty);
			set => SetValue(ErrorIconWidthProperty, value);
		}

		public static readonly BindableProperty ErrorIconSourceProperty = BindableProperty.Create(
			nameof(ErrorIconSource), typeof(ImageSource), typeof(MEditor), ImageCollection.ErrorIcon);

		public ImageSource ErrorIconSource
		{
			get => (ImageSource)GetValue(ErrorIconSourceProperty);
			set => SetValue(ErrorIconSourceProperty, value);
		}

		public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(
			nameof(ErrorTextColor), typeof(Color), typeof(MEditor), Colors.Red);

		public Color ErrorTextColor
		{
			get => (Color)GetValue(ErrorTextColorProperty);
			set => SetValue(ErrorTextColorProperty, value);
		}

		public static readonly BindableProperty ErrorFontSizeProperty = BindableProperty.Create(
			nameof(ErrorFontSize), typeof(double), typeof(MEditor), 14.0);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double ErrorFontSize
		{
			get => (double)GetValue(ErrorFontSizeProperty);
			set => SetValue(ErrorFontSizeProperty, value);
		}

		#endregion

		#region Editor Properties
		public static readonly BindableProperty TextProperty = BindableProperty.Create(
			nameof(Text),
			typeof(string),
			typeof(MEditor),
			default(string),
			BindingMode.TwoWay,
			propertyChanged: OnTextChanged
		);

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
			nameof(Placeholder),
			typeof(string),
			typeof(MEditor),
			default(string),
			BindingMode.TwoWay
		);

		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		public static readonly BindableProperty MEditorHeightProperty = BindableProperty.Create(
			nameof(MEditorHeight), typeof(double), typeof(MEditor), 40.0);

		public double MEditorHeight
		{
			get => (double)GetValue(MEditorHeightProperty);
			set => SetValue(MEditorHeightProperty, value);
		}

		public static readonly BindableProperty MEditorWidthProperty = BindableProperty.Create(
			nameof(MEditorWidth), typeof(double), typeof(MEditor), 200.0);

		public double MEditorWidth
		{
			get => (double)GetValue(MEditorWidthProperty);
			set => SetValue(MEditorWidthProperty, value);
		}

		public static readonly BindableProperty AutoSizeProperty = BindableProperty.Create(
			nameof(AutoSize), typeof(EditorAutoSizeOption), typeof(Editor), defaultValue: EditorAutoSizeOption.Disabled);

		public EditorAutoSizeOption AutoSize
		{
			get => (EditorAutoSizeOption)GetValue(AutoSizeProperty);
			set => SetValue(AutoSizeProperty, value);
		}

		public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
			nameof(PlaceholderColor),
			typeof(Color),
			typeof(MEditor),
			Colors.Gray,
			BindingMode.TwoWay
		);

		public Color PlaceholderColor
		{
			get => (Color)GetValue(PlaceholderColorProperty);
			set => SetValue(PlaceholderColorProperty, value);
		}

		public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create(
			nameof(EntryTextColor),
			typeof(Color),
			typeof(MEditor),
			Colors.Gray,
			BindingMode.TwoWay
		);

		public Color EntryTextColor
		{
			get => (Color)GetValue(EntryTextColorProperty);
			set => SetValue(EntryTextColorProperty, value);
		}

		public static readonly BindableProperty EntryFontSizeProperty = BindableProperty.Create(
			nameof(EntryFontSize),
			typeof(double),
			typeof(MEditor),
			default(double),
			BindingMode.TwoWay
		);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double EntryFontSize
		{
			get => (double)GetValue(EntryFontSizeProperty);
			set => SetValue(EntryFontSizeProperty, value);
		}

		public static readonly BindableProperty NoUnderlineProperty =
		BindableProperty.Create(
			nameof(NoUnderline),
			typeof(bool),
			typeof(MEditor),
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

		public MEditor()
		{
			this.RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition { Height = GridLength.Auto },
				new RowDefinition { Height = GridLength.Auto }
			};

			// Initialize entry
			_editor = new Editor
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(5, 0, 0, 0)
			};
			_editor.SetBinding(Editor.TextProperty, new Binding(nameof(Text), source: this));
			_editor.SetBinding(Editor.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
			_editor.SetBinding(Editor.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));
			_editor.SetBinding(Editor.TextColorProperty, new Binding(nameof(EntryTextColor), source: this));
			_editor.SetBinding(Editor.FontSizeProperty, new Binding(nameof(EntryFontSize), source: this));
			_editor.SetBinding(Editor.HeightRequestProperty, new Binding(nameof(Height), source: this));
			_editor.SetBinding(Editor.WidthRequestProperty, new Binding(nameof(Width), source: this));
			_editor.SetBinding(Editor.AutoSizeProperty, new Binding(nameof(AutoSize), source: this));

			// Subscribe to HandlerChanged to update underline
			_editor.HandlerChanged += OnEntryHandlerChanged;

			_content = new Grid
			{
				ColumnDefinitions = new ColumnDefinitionCollection
				{
					new ColumnDefinition { Width = GridLength.Star },
					new ColumnDefinition { Width = GridLength.Auto }
				},
			};
			_content.Add(_editor, 0, 0);

			var roundRectangle = new RoundRectangle();
			roundRectangle.SetBinding(RoundRectangle.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

			_border = new Border
			{
				StrokeShape = roundRectangle,
				Content = _content,
			};

			_border.SetBinding(Border.StrokeProperty, new Binding(nameof(Stroke), source: this));
			_border.SetBinding(Border.StrokeThicknessProperty, new Binding(nameof(StrokeThickness), source: this));


			// Add elements to the CustomEntry Grid
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
				_editor.HandlerChanged -= OnEntryHandlerChanged;
			}
		}

		private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (MEditor)bindable;
			control.CheckAndShowValidations();
		}

		private void OnEntryHandlerChanged(object? sender, EventArgs e)
		{
			if (_editor.Handler is EditorHandler handler)
			{
				UpdateUnderline(handler);
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(NoUnderline))
			{
				if (_editor.Handler is EditorHandler handler)
				{
					UpdateUnderline(handler);
				}
			}
		}

		private void UpdateUnderline(EditorHandler handler)
		{
			if (Handler == null)
				return;

			bool noUnderline = NoUnderline;

#if ANDROID
			if (noUnderline)
			{
				handler.PlatformView.SetBackground(null); // Removes underline
			}
			else
			{
				handler.PlatformView.SetBackgroundResource(Resource.Drawable.abc_edit_text_material); // Default underline
			}
#elif IOS || MACCATALYST
			if (handler.PlatformView is UIKit.UITextView textView)
			{
				textView.Layer.BorderWidth = noUnderline ? 0 : 1; // Control border width
				textView.Layer.BorderColor = noUnderline ? UIKit.UIColor.Clear.CGColor : UIKit.UIColor.Gray.CGColor; // Control border color
			}
#elif WINDOWS
			handler.PlatformView.BorderThickness = noUnderline
				? new Microsoft.UI.Xaml.Thickness(0)
				: new Microsoft.UI.Xaml.Thickness(1); // Default thickness
#endif
		}

		#endregion
	}
}
