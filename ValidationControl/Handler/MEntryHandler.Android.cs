using Microsoft.Maui.Controls.Platform;
using ValidationControl.Controls;

namespace ValidationControl.Handler
{
    public partial class MEntryHandler
    {
        public static void MapNoUnderline(IViewHandler viewHandler, IView entry)
        {
            if (viewHandler != null && viewHandler is MEntryHandler handler && handler.VirtualView is MEntry custom)
            {
                if (custom.NoUnderline)
                {
                    handler.PlatformView.SetBackground(null); // Removes underline
                }
                else
                {
                    // Restore the default Android underline. Adjust if necessary for your theme.
                    int defaultBackground = Resource.Drawable.abc_edit_text_material;
                    handler.PlatformView.SetBackgroundResource(defaultBackground);
                }
            }
        }
    }
}
