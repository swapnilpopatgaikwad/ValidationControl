using Microsoft.Maui.Platform;

namespace ValidationControl.Behaviors
{
	public class IconTintColorBehavior : Behavior<Image>
    {
        public static readonly BindableProperty TintColorProperty = BindableProperty.Create(
            nameof(TintColor),
            typeof(Color),
            typeof(IconTintColorBehavior),
            Colors.Transparent,
            propertyChanged: OnTintColorChanged);

        public Color TintColor
        {
            get => (Color)GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        private static void OnTintColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is IconTintColorBehavior behavior && behavior.AssociatedObject != null)
            {
                behavior.ApplyTintColor();
            }
        }

        private Image? AssociatedObject { get; set; }

        protected override void OnAttachedTo(Image bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            ApplyTintColor();
        }

        protected override void OnDetachingFrom(Image bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject = null;
        }

        private void ApplyTintColor()
        {
            if (AssociatedObject?.Handler?.PlatformView is not null)
            {
#if ANDROID
                var imageView = AssociatedObject.Handler.PlatformView as Android.Widget.ImageView;
                imageView?.SetColorFilter(TintColor.ToPlatform());
#elif IOS || MACCATALYST
								var uiImageView = AssociatedObject.Handler.PlatformView as UIKit.UIImageView;
								if (uiImageView != null)
								{
									uiImageView.TintColor = TintColor.ToPlatform();
									uiImageView.Image = uiImageView.Image?.ImageWithRenderingMode(UIKit.UIImageRenderingMode.AlwaysTemplate);
								}
#endif

            }
        }
    }
}
