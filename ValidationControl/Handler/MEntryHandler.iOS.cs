using ValidationControl.Controls;

namespace ValidationControl.Handler
{
    public partial class MEntryHandler
    {
        public static void MapNoUnderline(IViewHandler viewHandler, IView entry)
        {
            if (viewHandler != null && viewHandler is MEntryHandler handler && handler.VirtualView is MEntry custom)
            {
                handler.PlatformView.BorderStyle = custom.NoUnderline
                    ? UIKit.UITextBorderStyle.None // Removes border
                    : UIKit.UITextBorderStyle.RoundedRect; // Restores default rounded border
            }
        }
    }
}
