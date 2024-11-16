namespace ValidationControl.Hosting
{
	public static class ValidationMauiProgramExtensions
    {
        public static MauiAppBuilder UseValidationControl(this MauiAppBuilder builder)
        {
			builder.ConfigureMauiHandlers(handlers =>
			{
			});

			return builder;
        }
    }
}
