using Microsoft.Maui.Handlers;
using ValidationControl.CustomControl;

namespace ValidationControl.Extensions
{
    public static partial class CMTimePickerExtensions
    {
        public static void UpdateUnderline(this ITimePickerHandler handler, CMTimePicker cMTimePicker)
        {
            if (handler == null || handler.PlatformView == null)
                return;

            bool noUnderline = cMTimePicker.NoUnderline;
            
            if (handler.PlatformView is Microsoft.UI.Xaml.Controls.TimePicker platformView)
            {
                platformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0); // Removes the underline
            }
        }
    }
}
