using ValidationControl.Controls;
using ValidationControl.Handler;

namespace ValidationControl.Hosting
{
    public static class ValidationMauiProgramExtensions
    {
        public static MauiAppBuilder UseValidationControl(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(MEntry), typeof(MEntryHandler));
            });

            return builder;
        }
    }
}
