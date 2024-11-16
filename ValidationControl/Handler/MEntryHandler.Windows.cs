using ValidationControl.Controls;

namespace ValidationControl.Handler
{
    public partial class MEntryHandler
    {

        public static void MapNoUnderline(IViewHandler viewHandler, IView entry)
        {
            if (viewHandler != null && viewHandler is MEntryHandler handler && handler.VirtualView is MEntry custom )
            {
                handler.PlatformView.BorderThickness = custom.NoUnderline
                    ? new Microsoft.UI.Xaml.Thickness(0) // Removes border
                    : new Microsoft.UI.Xaml.Thickness(1); // Restores default thickness
            }
        }
    }
}
