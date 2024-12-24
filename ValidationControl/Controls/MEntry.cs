using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using ValidationControl.Behaviors;
using ValidationControl.Images;

namespace ValidationControl.Controls
{
	[ContentProperty(nameof(Validations))]
    public partial class MEntry : Grid
    {
        protected readonly Entry _entry;
        protected readonly Border _border;
        protected readonly HorizontalStackLayout errorIconsContainer = new();
        protected Grid _content;
		private readonly Image _passwordToggleIcon;

		#region Error Alert Properties

		public static readonly BindableProperty ErrorIconHeightProperty = BindableProperty.Create(
        nameof(ErrorIconHeight), typeof(double), typeof(MEntry), 24.0);

        public double ErrorIconHeight
        {
            get => (double)GetValue(ErrorIconHeightProperty);
            set => SetValue(ErrorIconHeightProperty, value);
        }

        public static readonly BindableProperty ErrorIconWidthProperty = BindableProperty.Create(
            nameof(ErrorIconWidth), typeof(double), typeof(MEntry), 24.0);

        public double ErrorIconWidth
        {
            get => (double)GetValue(ErrorIconWidthProperty);
            set => SetValue(ErrorIconWidthProperty, value);
        }

        public static readonly BindableProperty ErrorIconSourceProperty = BindableProperty.Create(
            nameof(ErrorIconSource), typeof(ImageSource), typeof(MEntry), ImageCollection.ErrorIcon);

        public ImageSource ErrorIconSource
        {
            get => (ImageSource)GetValue(ErrorIconSourceProperty);
            set => SetValue(ErrorIconSourceProperty, value);
        }

        public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(
            nameof(ErrorTextColor), typeof(Color), typeof(MEntry), Colors.Red);

        public Color ErrorTextColor
        {
            get => (Color)GetValue(ErrorTextColorProperty);
            set => SetValue(ErrorTextColorProperty, value);
        }

        public static readonly BindableProperty ErrorFontSizeProperty = BindableProperty.Create(
            nameof(ErrorFontSize), typeof(double), typeof(MEntry), 14.0);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double ErrorFontSize
        {
            get => (double)GetValue(ErrorFontSizeProperty);
            set => SetValue(ErrorFontSizeProperty, value);
        }

        #endregion

        #region Entry Properties
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(MEntry),
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
            typeof(MEntry),
            default(string),
            BindingMode.TwoWay
        );

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(MEntry),
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
            typeof(MEntry),
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
            typeof(MEntry),
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
            typeof(MEntry),
            false);

        public bool NoUnderline
        {
            get => (bool)GetValue(NoUnderlineProperty);
            set => SetValue(NoUnderlineProperty, value);
        }

		public static readonly BindableProperty KeyboardTypeProperty =
			BindableProperty.Create(nameof(KeyboardType), typeof(Keyboard), typeof(MEntry), Keyboard.Default);

		public Keyboard KeyboardType
		{
			get => (Keyboard)GetValue(KeyboardTypeProperty);
			set => SetValue(KeyboardTypeProperty, value);
		}

		public static readonly BindableProperty IsPasswordProperty =
			BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MEntry), false, propertyChanged: OnIsPasswordChanged);

		public bool IsPassword
		{
			get => (bool)GetValue(IsPasswordProperty);
			set => SetValue(IsPasswordProperty, value);
		}

		public static readonly BindableProperty PasswordToggleIconProperty =
			BindableProperty.Create(nameof(PasswordToggleIcon), typeof(ImageSource), typeof(MEntry), ImageCollection.ShowPasswordIcon);

		public ImageSource PasswordToggleIcon
		{
			get => (ImageSource)GetValue(PasswordToggleIconProperty);
			set => SetValue(PasswordToggleIconProperty, value);
		}

		public static readonly BindableProperty PasswordToggleIconColorProperty =
			BindableProperty.Create(nameof(PasswordToggleIconColor), typeof(Color), typeof(MEntry), Colors.Black);

		public Color PasswordToggleIconColor
		{
			get => (Color)GetValue(PasswordToggleIconColorProperty);
			set => SetValue(PasswordToggleIconColorProperty, value);
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

        public MEntry()
        {
            this.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            };

            // Initialize entry
            _entry = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(5,0,0,0)
            };
            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this));
            _entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
            _entry.SetBinding(Entry.PlaceholderColorProperty, new Binding(nameof(PlaceholderColor), source: this));
            _entry.SetBinding(Entry.TextColorProperty, new Binding(nameof(EntryTextColor), source: this));
            _entry.SetBinding(Entry.FontSizeProperty, new Binding(nameof(EntryFontSize), source: this));
			_entry.SetBinding(Entry.KeyboardProperty, new Binding(nameof(KeyboardType), source: this));
			_entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));

			// Subscribe to HandlerChanged to update underline
			_entry.HandlerChanged += OnEntryHandlerChanged;

			_content = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
            };
            _content.Add(_entry, 0, 0);

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
            this.Add(_border,0,0);


            iconValidation.Value.SetBinding(Image.HeightRequestProperty, new Binding(nameof(ErrorIconHeight), source: this));
            iconValidation.Value.SetBinding(Image.WidthRequestProperty, new Binding(nameof(ErrorIconWidth), source: this));
            iconValidation.Value.SetBinding(Image.SourceProperty, new Binding(nameof(ErrorIconSource), source: this));

            // Update error label properties
            labelValidation.Value.SetBinding(Label.TextColorProperty, new Binding(nameof(ErrorTextColor), source: this));
            labelValidation.Value.SetBinding(Label.FontSizeProperty, new Binding(nameof(ErrorFontSize), source: this));

			// Initialize password toggle icon
			_passwordToggleIcon = new Image
			{
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(0, 0, 5, 0)
			};
			_passwordToggleIcon.SetBinding(Image.SourceProperty, new Binding(nameof(PasswordToggleIcon), source: this));
			var behavior = new IconTintColorBehavior();
			behavior.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(PasswordToggleIconColor), source: this));
			_passwordToggleIcon.Behaviors.Add(behavior);
			_passwordToggleIcon.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(TogglePasswordVisibility)
			});

			_content.Add(_passwordToggleIcon, 1, 0);
			UpdatePasswordToggleIconVisibility();
		}

		#region Events

		protected override void OnHandlerChanging(HandlerChangingEventArgs args)
		{
			base.OnHandlerChanging(args);

			if (args.OldHandler != null)
			{
				// Unsubscribe from events
				_entry.HandlerChanged -= OnEntryHandlerChanged;
			}
		}

		private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MEntry)bindable;
            control.CheckAndShowValidations();
        }

		private void OnEntryHandlerChanged(object? sender, EventArgs e)
		{
			if (_entry.Handler is EntryHandler handler)
			{
				UpdateUnderline(handler);
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(NoUnderline))
			{
				if (_entry.Handler is EntryHandler handler)
				{
					UpdateUnderline(handler);
				}
			}
		}

		private void UpdateUnderline(EntryHandler handler)
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
        handler.PlatformView.BorderStyle = noUnderline
            ? UIKit.UITextBorderStyle.None
            : UIKit.UITextBorderStyle.RoundedRect; // Default border
#elif WINDOWS
        handler.PlatformView.BorderThickness = noUnderline
            ? new Microsoft.UI.Xaml.Thickness(0)
            : new Microsoft.UI.Xaml.Thickness(1); // Default thickness
#endif
		}

		private static void OnIsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (MEntry)bindable;
			control.UpdatePasswordToggleIconVisibility();
		}

		#endregion

		#region Methods

		private void TogglePasswordVisibility()
		{
			_entry.IsPassword = !_entry.IsPassword;
			PasswordToggleIcon = _entry.IsPassword ? ImageCollection.ShowPasswordIcon : ImageCollection.HidePasswordIcon;
		}

		private void UpdatePasswordToggleIconVisibility()
		{
			_passwordToggleIcon.IsVisible = IsPassword;
		}

		#endregion
	}
}
